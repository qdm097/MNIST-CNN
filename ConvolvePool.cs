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
        double[,] Momentums { get; set; }
        public Convolution(int kernelsize)
        {
            KernelSize = kernelsize;
            Kernel = new double[KernelSize, KernelSize];
            Momentums = new double[KernelSize, KernelSize];
        }
        public void Descend(int batchsize, double learningrate)
        {
            for (int i = 0; i < KernelSize; i++)
            {
                for (int ii = 0; ii < KernelSize; ii++)
                {
                    Kernel[i, ii] -= learningrate * Gradients[i, ii] *(-2d / batchsize);
                }
            }
        }
        public void Descend(double[,] input, double momentum, double learningrate)
        {
            Gradients = new double[KernelSize, KernelSize];
            for (int i = 0; i < KernelSize; i++)
            {
                for (int ii = 0; ii < KernelSize; ii++)
                {
                    for (int j = 0; j < input.GetLength(0); j++)
                    {
                        for (int jj = 0; jj < input.GetLength(1); jj++)
                        {
                            Gradients[i, ii] +=
                                input[j, jj] * Errors[i, ii]
                                * ActivationFunctions.TanhDerriv(Kernel[i, ii] * input[j, jj]);
                        }
                    }
                    Momentums[i, ii] = (Momentums[i, ii] * momentum) - (learningrate * Gradients[i, ii]);
                    Gradients[i, ii] += Momentums[i, ii];
                }
            }
        }
        public void Backprop(Pooling p, int poolsize, int step)
        {
            Errors = new double[KernelSize, KernelSize];
            for (int i = 0; i < KernelSize; i++)
            {
                for (int ii = 0; ii < KernelSize; ii++)
                {
                    for (int k = 0; k < length; k++)
                    {
                        for (int kk = 0; kk < width; kk++)
                        {
                            for (int j = 0; j < p.Errors.GetLength(0); j++)
                            {
                                for (int jj = 0; jj < p.Errors.GetLength(1); jj++)
                                {
                                    //No idea if this is right
                                    Errors[i, ii] += p.Errors[(k * step) + j, (ii * step) + jj];
                                }
                            }
                        }
                    }
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
                            output[i, ii] += input[(i * step) + j, (ii * step) + jj] * (Kernel[j, jj] + Momentums[j, jj]);
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
        public double[,] Mask { get; set; }
        public void Backprop(Layer l, int pool)
        {
            Errors = new double[Mask.GetLength(0), Mask.GetLength(1)];
            for (int k = 0; k < Errors.GetLength(0); k++)
            {
                for (int j = 0; j < Errors.GetLength(1); j++)
                {
                    Errors[k, j] += Mask[k, j] * l.Weights[k, j] * ActivationFunctions.TanhDerriv(l.ZVals[k]) * l.Errors[k];
                }
            }
        }
        public double[,] Pool(double[,] input, int pool)
        {
            length = (input.GetLength(0) / pool) + 1;
            width = (input.GetLength(1) / pool) + 1;
            double[,] output = new double[length, width];
            Mask = new double[input.GetLength(0), input.GetLength(1)];
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int ii = 0; ii < input.GetLength(1); ii++)
                {
                    if (output[i / pool, ii / pool] < input[i, ii] || output[i / pool, ii / pool] == 0)
                    {
                        Mask[i, ii] = 1;
                        output[i / pool, ii / pool] = input[i, ii];
                    }
                }
            }
            return output;
        }
    }
}
