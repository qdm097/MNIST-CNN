using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNN1
{
    class FullyConnectedLayer : iLayer
    {
        public int Length { get; set; }
        public int InputLength { get; set; }
        public double[] Values { get; set; }
        public double[] ZVals { get; set; }
        public double[] Errors { get; set; }
        //Weights
        public double[,] Weights { get; set; }
        double[,] WeightGradient { get; set; }
        double[,] WRMSGrad { get; set; }
        double[,] WMomentum { get; set; }
        //Biases
        public double[] Biases { get; set; }
        double[] BRMSGrad { get; set; }
        double[] BMomentum { get; set; }
        double[] BiasGradient { get; set; }
        //Average gradient of the layer
        public double AvgUpdate { get; set; }
        public FullyConnectedLayer(int l, int il)
        {
            Length = l; InputLength = il;
            //Weights
            Weights = new double[l, il];
            WeightGradient = new double[l, il];
            WRMSGrad = new double[l, il];
            WMomentum = new double[l, il];
            //Biases
            Biases = new double[l];
            BiasGradient = new double[l];
            BRMSGrad = new double[l];
            BMomentum = new double[l];
            
            AvgUpdate = 0;
        }
        public iLayer Init(Random r, bool isoutput)
        {
            //All layers have weights
            Weights = new double[Length, InputLength];
            //Output layer has no biases
            if (!isoutput) { Biases = new double[Length]; }
            //Initialize weights (and biases to zero)
            for (int j = 0; j < Length; j++)
            {
                for (int jj = 0; jj < InputLength; jj++)
                {
                    Weights[j, jj] = (r.NextDouble() > .5 ? -1 : 1) * r.NextDouble() * Math.Sqrt(3d / (InputLength * InputLength));
                }
            }
            return this;
        }
        /// <summary>
        /// Applies the gradients to the weights as a batch
        /// </summary>
        /// <param name="batchsize">The number of trials run per cycle</param>
        /// <param name="clipparameter">What the max/min </param>
        /// <param name="RMSDecay">How quickly the RMS gradients decay</param>
        public void Descend(int batchsize)
        {
            AvgUpdate = 0;
            for (int i = 0; i < Length; i++)
            {
                for (int ii = 0; ii < InputLength; ii++)
                {
                    //Normal gradient descent update
                    double gradient = WeightGradient[i, ii] * (-2d / batchsize);
                    double update = NN.LearningRate * gradient;
                    //Root mean square propegation
                    if (NN.UseRMSProp)
                    {
                        WRMSGrad[i, ii] = (WRMSGrad[i, ii] * NN.RMSDecay) + ((1 - NN.RMSDecay) * (gradient * gradient));
                        update = (NN.LearningRate / Math.Sqrt(WRMSGrad[i, ii])) * gradient;
                    }
                    //Gradient clipping
                    if (NN.UseClipping)
                    {
                        if (update > NN.ClipParameter) { update = NN.ClipParameter; }
                        if (update < -NN.ClipParameter) { update = -NN.ClipParameter; }
                    }
                    //Update weight and average
                    Weights[i, ii] -= update;
                    AvgUpdate -= update;
                }
                //Normal gradient descent update
                double bgradient = BiasGradient[i] * (-2d / batchsize);
                double bupdate = NN.LearningRate * bgradient;
                //Root mean square propegation
                if (NN.UseRMSProp)
                {
                    BRMSGrad[i] = (BRMSGrad[i] * NN.RMSDecay) + ((1 - NN.RMSDecay) * (bgradient * bgradient));
                    bupdate = (NN.LearningRate / Math.Sqrt(BRMSGrad[i])) * bgradient;
                }
                //Gradient clipping
                if (NN.UseClipping)
                {
                    if (bupdate > NN.ClipParameter) { bupdate = NN.ClipParameter; }
                    if (bupdate < -NN.ClipParameter) { bupdate = -NN.ClipParameter; }
                }
                //Update bias
                Biases[i] -= bupdate;
            }
            //Reset gradients (but not RMS gradients)
            WeightGradient = new double[Length, InputLength];
            BiasGradient = new double[Length];
        }
        /// <summary>
        /// Backpropegation of error and calcluation of gradients
        /// </summary>
        /// <param name="input">Previous layer's values</param>
        /// <param name="isoutput">Whether the layer is the output layer</param>
        public void Backprop(double[] input, iLayer outputlayer, bool isoutput, int correct)
        {
            //Calculate error
            if (isoutput)
            {
                Errors = new double[Length];
                for (int i = 0; i < Length; i++)
                {
                    Errors[i] = 2d * ((i == correct ? 1d : 0d) - Values[i]);
                }
            }
            else
            {
                if (outputlayer is FullyConnectedLayer)
                {
                    var FCLOutput = outputlayer as FullyConnectedLayer;
                    Errors = new double[Length];
                    for (int k = 0; k < FCLOutput.Length; k++)
                    {
                        for (int j = 0; j < Length; j++)
                        {
                            Errors[j] += FCLOutput.Weights[k, j] * Maths.TanhDerriv(outputlayer.ZVals[k]) * FCLOutput.Errors[k];
                        }
                    }
                }
                if (outputlayer is ConvolutionLayer)
                {
                    var CLOutput = outputlayer as ConvolutionLayer;
                    Errors = Maths.Convert(CLOutput.FullConvolve(CLOutput.Weights, Maths.Convert(CLOutput.Errors)));
                }
                if (outputlayer is PoolingLayer)
                {
                    var PLOutput = outputlayer as PoolingLayer;
                    int iterator = 0;
                    Errors = new double[Length];
                    for (int i = 0; i < Length; i++)
                    {
                        if (PLOutput.Mask[i] == 0) { continue; }
                        Errors[i] = PLOutput.Errors[iterator];
                        iterator++;
                    }
                }
            }
            //Calculate gradients
            for (int i = 0; i < Length; i++)
            {
                for (int ii = 0; ii < InputLength; ii++)
                {
                    //Weight gradients
                    WeightGradient[i, ii] = input[ii] * Maths.TanhDerriv(ZVals[i]) * Errors[i];
                    if (NN.UseMomentum)
                    {
                        if (NN.UseNesterov)
                        {
                            //Nesterov momentum formula
                            WeightGradient[i, ii] = ((1 + NN.Momentum) * (NN.LearningRate * WeightGradient[i, ii])) 
                                + (NN.Momentum * NN.Momentum * WMomentum[i, ii]);
                        }
                        else
                        {
                            //Standard momentum formula
                            WeightGradient[i, ii] = (WMomentum[i, ii] * NN.Momentum) + (NN.LearningRate * WeightGradient[i, ii]);
                        }
                        //Momentum is the previous iteration's gradient
                        WMomentum[i, ii] = WeightGradient[i, ii];
                    }
                }
                if (isoutput) { continue; }
                //Bias gradients
                BiasGradient[i] = Maths.TanhDerriv(ZVals[i]) * Errors[i];
                if (NN.UseMomentum)
                {
                    if (NN.UseNesterov)
                    {
                        BiasGradient[i] = ((1 + NN.Momentum) * (NN.LearningRate * BiasGradient[i]))
                            + (NN.Momentum * NN.Momentum * BMomentum[i]);
                    }
                    else
                    {
                        BiasGradient[i] = (BMomentum[i] * NN.Momentum) + (NN.LearningRate * BiasGradient[i]);
                    }
                    //Momentum is the previous iteration's gradient
                    BMomentum[i] = BiasGradient[i];
                }
            }
        }
        public void Calculate(double[] input, bool output)
        {
            var vals = new double[Length];
            if (NN.UseNesterov && NN.UseMomentum)
            {
                for (int i = 0; i < Length; i++)
                {
                    for (int ii = 0; ii < InputLength; ii++)
                    {
                        Weights[i, ii] = Weights[i, ii] + (NN.Momentum * WMomentum[i, ii]);
                    }
                    Biases[i] = Biases[i] + (NN.Momentum * BMomentum[i]);
                }
            }
            for (int k = 0; k < Length; k++)
            {
                for (int j = 0; j < InputLength; j++)
                {
                    vals[k] += Weights[k, j] * input[j];
                }
                if (!output)
                {
                    vals[k] += Biases[k];
                }
            }
            ZVals = vals;
            if (!output) { Values = Maths.Tanh(vals); }
            else { Values = vals; }
        }
        public void Calculate(double[,] input, bool output)
        {
            Calculate(Maths.Convert(input), output);
        }
    }
}