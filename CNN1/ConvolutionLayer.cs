using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNN1
{
    class ConvolutionLayer : iLayer
    {
        //Kernel
        public double[,] Weights { get; set; }
        double[,] Gradients { get; set; }
        double[,] Momentums { get; set; }
        public int Length { get; set; }
        public int KernelSize { get; set; }
        public int InputLength { get; set; }
        double[,] RMSGrad { get; set; }
        public double[] Errors { get; set; }
        public double[] IntermediaryErrors { get; set; }
        public double[] ZVals { get; set; }
        double[,] Input { get; set; }
        public double[] Values { get; set; }
        public double AvgUpdate { get; set; }
        public static int StepSize = 1;

        public ConvolutionLayer(int kernelsize, int inputsize)
        {
            Length = kernelsize * kernelsize; InputLength = inputsize;
            KernelSize = kernelsize;
            Weights = new double[KernelSize, KernelSize];
            Gradients = new double[KernelSize, KernelSize];
            Momentums = new double[KernelSize, KernelSize];
            RMSGrad = new double[KernelSize, KernelSize];
            AvgUpdate = 0;
        }
        public iLayer Init(Random r, bool useless)
        {
            Weights = new double[KernelSize, KernelSize];
            for (int i = 0; i < KernelSize; i++)
            {
                for (int ii = 0; ii < KernelSize; ii++)
                {
                    Weights[i, ii] = (r.NextDouble() > .5 ? -1 : 1) * r.NextDouble() * Math.Sqrt(3d / (InputLength * InputLength));
                }
            }
            return this;
        }
        public void Descend(int batchsize)
        {
            AvgUpdate = 0;
            for (int i = 0; i < KernelSize; i++)
            {
                for (int ii = 0; ii < KernelSize; ii++)
                {
                    double gradient = Gradients[i, ii] * (-2d / batchsize);
                    double update = NN.LearningRate * gradient;
                    //Root mean square propegation
                    if (NN.UseRMSProp)
                    {
                        RMSGrad[i, ii] = (RMSGrad[i, ii] * NN.RMSDecay) + ((1 - NN.RMSDecay) * (gradient * gradient));
                        update = (NN.LearningRate / Math.Sqrt(RMSGrad[i, ii])) * gradient;
                    }
                    //Gradient clipping
                    if (NN.UseClipping)
                    {
                        if (update > NN.ClipParameter) { update = NN.ClipParameter; }
                        if (update < -NN.ClipParameter) { update = -NN.ClipParameter; }
                    }
                    Weights[i, ii] -= update;
                    AvgUpdate -= update;
                }
            }
            Gradients = new double[KernelSize, KernelSize];
        }
        public void Backprop(double[] input, bool useless)
        {
            Gradients = new double[KernelSize, KernelSize];
            for (int k = 0; k < KernelSize; k++)
            {
                for (int j = 0; j < KernelSize; j++)
                {
                    Gradients[k, j] += input[j] * Maths.TanhDerriv(ZVals[k]) * Errors[k];
                    if (NN.UseMomentum)
                    {
                        Momentums[k, j] = (Momentums[k, j] * NN.Momentum) - (NN.LearningRate * Gradients[k, j]);
                        Gradients[k, j] += Momentums[k, j];
                    }
                }
            }
        }
        /// <summary>
        /// Calculates the errors of the convolution
        /// </summary>
        /// <param name="outputlayer">The layer which comes after the convolutional layer</param>
        public void CalcError(iLayer outputlayer)
        {
            if (outputlayer is FullyConnectedLayer)
            {
                //Errors with respect to the output of the convolution
                //dl/do
                IntermediaryErrors = new double[outputlayer.InputLength];
                for (int k = 0; k < outputlayer.Length; k++)
                {
                    for (int j = 0; j < outputlayer.InputLength; j++)
                    {
                        IntermediaryErrors[j] += outputlayer.Weights[k, j] * Maths.TanhDerriv(outputlayer.ZVals[k]) * outputlayer.Errors[k];
                    }
                }
                //Errors with respect to the filter
                Errors = Maths.Convert(Convolve(Maths.Convert(IntermediaryErrors), Input));
            }
            if (outputlayer is ConvolutionLayer)
            {
                var CLOutput = outputlayer as ConvolutionLayer;
                //Flipped?
                IntermediaryErrors = Maths.Convert(CLOutput.FullConvolve(CLOutput.Weights, Maths.Convert(CLOutput.IntermediaryErrors)));
                Errors = Maths.Convert(Convolve(Maths.Convert(IntermediaryErrors), Input));
            }
            if (outputlayer is PoolingLayer)
            {
                var PLOutput = outputlayer as PoolingLayer;
                int iterator = 0;
                IntermediaryErrors = new double[ZVals.Length];
                for (int i = 0; i < ZVals.Length; i++)
                {
                    if (PLOutput.Mask[i] == 0) { continue; }
                    IntermediaryErrors[i] = PLOutput.Errors[iterator];
                    iterator++;
                }
                //Errors with respect to the filter
                Errors = Maths.Convert(Convolve(Maths.Convert(IntermediaryErrors), Input));
            }
        }
        public void CalcError(double useless) { throw new Exception("The convolution layer is never an output layer"); }
        /// <summary>
        /// Calculates the dot product of the kernel and input matrix.
        /// Matrices should be size [x, y] and [y], respectively, where x is the output size and y is the latent space's size
        /// </summary>
        /// <param name="input">The input matrix</param>
        /// <param name="isoutput">Whether to use hyperbolic tangent on the output</param>
        /// <returns></returns>
        public void Calculate(double[] input, bool isoutput)
        {
            Calculate(Maths.Convert(input), isoutput);
        }
        /// <summary>
        /// Partial convolution layer
        /// </summary>
        /// <param name="input"></param>
        /// <param name="isoutput"></param>
        public void Calculate(double[,] input, bool isoutput)
        {
            Input = input;
            var output = Convolve(Weights, input);
            ZVals = Maths.Convert(output);
            if (!isoutput) { output = Maths.Tanh(output); }
            Values = Maths.Convert(output);
        }
        public double[,] Convolve(double[,] filter, double[,] input)
        {
            int kernelsize = filter.GetLength(0);
            int length = (input.GetLength(0) / StepSize) - kernelsize + 1;
            int width = (input.GetLength(1) / StepSize) - kernelsize + 1;

            double[,] output = new double[length, width];
            for (int i = 0; i < length; i++)
            {
                for (int ii = 0; ii < width; ii++)
                {
                    for (int j = 0; j < kernelsize; j += StepSize)
                    {
                        for (int jj = 0; jj < kernelsize; jj += StepSize)
                        {
                            output[i, ii] += input[(i * StepSize) + j, (ii * StepSize) + jj] * filter[j, jj];
                        }
                    }
                }
            }
            return output;
        }
        public double[,] FullConvolve(double[,] filter, double[,] input)
        {
            var kernelsize = input.GetLength(0) + filter.GetLength(0) - 1;
            double[,] output = new double[kernelsize, kernelsize];
            for (int i = 0; i < input.GetLength(0); i += StepSize)
            {
                for (int ii = 0; ii < input.GetLength(1); ii += StepSize)
                {
                    for (int j = 0; j < filter.GetLength(0); j += StepSize)
                    {
                        for (int jj = 0; jj < filter.GetLength(1); jj += StepSize)
                        {
                            if ((i * StepSize) + j >= kernelsize || (ii * StepSize) + jj >= kernelsize) { continue; }
                            output[(i * StepSize) + j, (ii * StepSize) + jj] += input[i, ii] * filter[j, jj];
                        }
                    }
                }
            }
            return output;
        }
        public double[,] Flip(double[,] input)
        {
            int length = input.GetLength(0);
            int width = input.GetLength(1);
            var output = new double[length, width];
            for (int i = 0; i < length; i++)
            {
                for (int ii = 0; ii < width; ii++)
                {
                    output[ii, i] = input[i, ii];
                }
            }
            return output;
        }
    }
}