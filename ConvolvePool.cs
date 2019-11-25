using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNN1
{
    class Convolution
    {
        public double[,] Kernel { get; set; }
        public int KernelSize { get; set; }
        double[,] Errors { get; set; }
        double[,] Gradients { get; set; }
        public Convolution(int kernelsize)
        {
            KernelSize = kernelsize;
            Kernel = new double[KernelSize, KernelSize];
        }
        public void Backprop(Pooling p, int poolsize)
        {
            Errors = new double[KernelSize, KernelSize];
            for (int i = 0; i < Errors.GetLength(0); i++)
            {
                for (int ii = 0; ii < Errors.GetLength(1); ii++)
                {
                    Errors[i, ii] = p.Errors[i / poolsize, ii / poolsize];
                }
            }
        }
        public double[,] Convolve(double[,] input, int step)
        {
            int length = (input.GetLength(0) / step) - Kernel.GetLength(0);
            int width = (input.GetLength(1) / step) - Kernel.GetLength(1);
            double[,] output = new double[length, width];
            for (int i = 0; i < length; i++)
            {
                for (int ii = 0; ii < width; ii++)
                {
                    for (int j = 0; j < Kernel.GetLength(0); j++)
                    {
                        for (int jj = 0; jj < Kernel.GetLength(1); jj++)
                        {
                            //Check this
                            output[i, ii] += input[(i * step) + j, (ii * step) + jj] * (Kernel[j, jj]);
                        }
                    }
                    output[i, ii] = ActivationFunctions.Tanh(output[i, ii]);
                }
            }
            return output;
        }
    }
    class Pooling
    {
        int length { get; set; }
        int width { get; set; }
        public double[,] Errors { get; set; }
        public void Backprop(Layer l)
        {
            Errors = new double[length, width];
            for (int k = 0; k < l.Values.GetLength(0); k++)
            {
                for (int j = 0; j < width; j++)
                {
                    Errors[j / l.Weights.GetLength(0), j % l.Weights.GetLength(0)] += l.Weights[k, j] * ActivationFunctions.TanhDerriv(l.ZVals[k]) * l.Errors[k];
                }
            }
        }
        public double[,] Pool(double[,] input, int pool)
        {
            length = input.GetLength(0) / pool;
            width = input.GetLength(1) / pool;
            double[,] output = new double[length, width];
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int ii = 0; ii < input.GetLength(1); ii++)
                {
                    if (output[i / pool, ii / pool] < input[i, ii])
                    {
                        output[i / pool, ii / pool] = input[i, ii];
                    }
                }
            }
            return output;
        }
    }
}
