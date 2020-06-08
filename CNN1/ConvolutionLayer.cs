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
        double[,] WMomentum { get; set; }
        public int Length { get; set; }
        public int KernelSize { get; set; }
        public int InputLength { get; set; }
        double[,] RMSGrad { get; set; }
        public double[] Errors { get; set; }
        public double[] ZVals { get; set; }
        public double[] Values { get; set; }
        public double AvgUpdate { get; set; }
        public static int StepSize = 1;

        public ConvolutionLayer(int kernelsize, int inputsize)
        {
            Length = kernelsize * kernelsize; InputLength = inputsize;
            KernelSize = kernelsize;
            Weights = new double[KernelSize, KernelSize];
            Gradients = new double[KernelSize, KernelSize];
            WMomentum = new double[KernelSize, KernelSize];
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
        public void Backprop(double[] input, iLayer outputlayer, bool uselessbool, int uselessint)
        {
            //Calc errors
            double[,] Input = Maths.Convert(input);
            if (outputlayer is FullyConnectedLayer)
            {
                //Errors with respect to the output of the convolution
                //dl/do
                Errors = new double[outputlayer.InputLength];
                for (int k = 0; k < outputlayer.Length; k++)
                {
                    for (int j = 0; j < outputlayer.InputLength; j++)
                    {
                        Errors[j] += outputlayer.Weights[k, j] * Maths.TanhDerriv(outputlayer.ZVals[k]) * outputlayer.Errors[k];
                    }
                }
            }
            if (outputlayer is ConvolutionLayer)
            {
                var CLOutput = outputlayer as ConvolutionLayer;
                //Flipped?
                Errors = Maths.Convert(CLOutput.FullConvolve(CLOutput.Weights, Maths.Convert(CLOutput.Errors)));
            }
            if (outputlayer is PoolingLayer)
            {
                var PLOutput = outputlayer as PoolingLayer;
                int iterator = 0;
                Errors = new double[ZVals.Length];
                for (int i = 0; i < ZVals.Length; i++)
                {
                    if (PLOutput.Mask[i] == 0) { continue; }
                    Errors[i] = PLOutput.Errors[iterator];
                    iterator++;
                }
            }
            //Calc gradients (errors with respect to the filter)
            Gradients = Convolve(Maths.Convert(Maths.Scale(-1, Errors)), Input);
            if (NN.UseMomentum)
            {
                for (int i = 0; i < KernelSize; i++)
                {
                    for (int ii = 0; ii < KernelSize; ii++)
                    {
                        if (NN.UseNesterov)
                        {
                            //Nesterov momentum formula
                            Gradients[i, ii] = ((1 + NN.Momentum) * (NN.LearningRate * Gradients[i, ii]))
                                + (NN.Momentum * NN.Momentum * WMomentum[i, ii]);
                        }
                        else
                        {
                            //Standard momentum formula
                            Gradients[i, ii] = (WMomentum[i, ii] * NN.Momentum) + (NN.LearningRate * Gradients[i, ii]);
                        }
                        //Momentum is the previous iteration's gradient
                        WMomentum[i, ii] = Gradients[i, ii];
                    }
                }
            }
        }
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
            if (NN.UseNesterov && NN.UseMomentum)
            {
                for (int i = 0; i < KernelSize; i++)
                {
                    for (int ii = 0; ii < KernelSize; ii++)
                    {
                        Weights[i, ii] = Weights[i, ii] + (NN.Momentum * WMomentum[i, ii]);
                    }
                }
            }

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