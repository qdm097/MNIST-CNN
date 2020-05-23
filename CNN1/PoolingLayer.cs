using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNN1
{
    class PoolingLayer : iLayer
    {
        //iLayer implementation stuff
        public double[,] Weights { get; set; }
        public double[] ZVals { get; set; }
        public double[] Errors { get; set; }
        public double AvgUpdate { get; set; }
        public int Length { get; set; }
        public int InputLength { get; set; }
        public void Descend(int anint) { }
        public void Backprop(double[] anarray, bool abool) { }
        public iLayer Init(Random arandom, bool abool) { return this; }
        public void CalcError(double adouble) { }
        //Pooling stuff
        public int PoolSize { get; set; }
        public double[] Values { get; set; }
        public double[] Mask { get; set; }
        public PoolingLayer(int poolsize, int priorsize)
        {
            PoolSize = poolsize; InputLength = priorsize;
            Length = (int)Math.Pow(Math.Sqrt(priorsize) / PoolSize, 2);
            Values = new double[Length];
            ZVals = new double[Length];
        }
        public void CalcError(iLayer outputlayer)
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
                Errors = Maths.Convert(CLOutput.FullConvolve(CLOutput.Weights, Maths.Convert(CLOutput.IntermediaryErrors)));
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
        public void Calculate(double[] input, bool useless)
        {
            Calculate(Maths.Convert(input), useless);
        }
        public void Calculate(double[,] input, bool useless)
        {

            if (input.GetLength(0) % PoolSize != 0 || input.GetLength(1) % PoolSize != 0)
            { throw new Exception("Unclean divide in PoolSizeing"); }
            double[,] output = new double[input.GetLength(0) / PoolSize, input.GetLength(1) / PoolSize];
            var mask = new double[input.GetLength(0), input.GetLength(1)];
            int currentx = 0, currenty = 0;
            for (int i = 0; i < input.GetLength(0); i += PoolSize)
            {
                currenty = 0;
                for (int ii = 0; ii < input.GetLength(1); ii += PoolSize)
                {
                    double max = double.MinValue; int maxX = i, maxY = ii;
                    for (int j = 0; j < PoolSize; j++)
                    {
                        for (int jj = 0; jj < PoolSize; jj++)
                        {
                            if (input[i + j, ii + jj] > max)
                            { max = input[i + j, ii + jj]; maxX = i + j; maxY = ii + jj; continue; }
                        }
                    }
                    mask[maxX, maxY] = 1;
                    output[currentx, currenty] = input[maxX, maxY];
                    currenty++;
                }
                currentx++;
            }
            Values = Maths.Convert(output);
            ZVals = Values;
            Mask = Maths.Convert(mask);
        }
    }
}