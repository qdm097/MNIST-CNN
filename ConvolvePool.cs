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
        public double[,] RMSGrad { get; set; }
        public int KernelSize { get; set; }
        double[,] Errors { get; set; }
        double[,] Gradients { get; set; }
        double[,] Momentums { get; set; }
        double[,] Zvals { get; set; }
        public Convolution(int kernelsize)
        {
            KernelSize = kernelsize;
            Kernel = new double[KernelSize, KernelSize];
            Momentums = new double[KernelSize, KernelSize];
            RMSGrad = new double[KernelSize, KernelSize];
        }
        public void Descend(int batchsize, double learningrate, bool useRMS, double RMSdecay)
        {
            double avg = 0;
            if (!useRMS)
            {
                for (int i = 0; i < KernelSize; i++)
                {
                    for (int ii = 0; ii < KernelSize; ii++)
                    {
                        double gradient = learningrate * Gradients[i, ii] * (-2d / batchsize);
                        Kernel[i, ii] -= gradient;
                        avg -= gradient;
                    }
                }
            }
            else
            {
                for (int i = 0; i < KernelSize; i++)
                {
                    for (int ii = 0; ii < KernelSize; ii++)
                    {
                        double gradient = Gradients[i, ii] * (-2d / batchsize);
                        RMSGrad[i, ii] = (RMSGrad[i, ii] * RMSdecay) + ((1 - RMSdecay) * (gradient * gradient));
                        double update = (learningrate / Math.Sqrt(RMSGrad[i, ii])) * gradient;
                        Kernel[i, ii] -= update;
                        avg -= update;
                    }
                }
            }
            Gradients = new double[KernelSize, KernelSize];
        }
        public void Descend(double[,] input, double momentum, double learningrate, int step, bool usemomentum)
        {
            Gradients = new double[KernelSize, KernelSize];
            int length = (input.GetLength(0) / step) - Kernel.GetLength(0);
            int width = (input.GetLength(1) / step) - Kernel.GetLength(1);
            for (int i = 0; i < length; i++)
            {
                for (int ii = 0; ii < width; ii++)
                {
                    for (int j = 0; j < KernelSize; j++)
                    {
                        for (int jj = 0; jj < KernelSize; jj++)
                        {
                            //Check this
                            Gradients[j, jj] += input[(i * step) + j, (ii * step) + jj] 
                                * Errors[i, ii]
                                * ActivationFunctions.TanhDerriv(Zvals[i, ii]);
                        }
                    }
                }
            }
            if (usemomentum)
            {
                for (int i = 0; i < KernelSize; i++)
                {
                    for (int ii = 0; ii < KernelSize; ii++)
                    {
                        Momentums[i, ii] = (Momentums[i, ii] * momentum) - (learningrate * Gradients[i, ii]);
                        Gradients[i, ii] += Momentums[i, ii];
                    }
                }
            }
        }
        public void Backprop(Pooling p)
        {
            Errors = p.Errors;
        }
        //Crosscorrelate not convolve
        public double[,] Convolve(double[,] input, int step)
        {
            int length = (input.GetLength(0) / step) - Kernel.GetLength(0);
            int width = (input.GetLength(1) / step) - Kernel.GetLength(1);
            Zvals = new double[length, width];
            double[,] output = new double[length, width];
            for (int i = 0; i < length; i++)
            {
                for (int ii = 0; ii < width; ii++)
                {
                    for (int j = 0; j < KernelSize; j++)
                    {
                        for (int jj = 0; jj < KernelSize; jj++)
                        {
                            //Check this
                            output[i, ii] += input[(i * step) + j, (ii * step) + jj] * (Kernel[j, jj] + Momentums[j, jj]);
                        }
                    }
                    Zvals[i, ii] = output[i, ii];
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
            //Calc 1d errors
            double[] smallerrors = new double[l.InputLength];
            Errors = new double[Mask.GetLength(0), Mask.GetLength(1)];
            for (int k = 0; k < l.Length; k++)
            {
                for (int j = 0; j < l.InputLength; j++)
                {
                    smallerrors[j] += l.Weights[k, j] * ActivationFunctions.TanhDerriv(l.ZVals[k]) * l.Errors[k];
                }
            }
            //Convert to 2d array
            double[,] convertederrors = new double[(Mask.GetLength(0) / pool), (Mask.GetLength(1) / pool)];
            for (int i = 0; i < (int)Math.Sqrt(smallerrors.Length); i++)
            {
                for (int ii = 0; ii < (int)Math.Sqrt(smallerrors.Length); ii++)
                {
                    convertederrors[i, ii] = smallerrors[(i * (int)Math.Sqrt(smallerrors.Length)) + ii];
                }
            }
            //Compute errors for pool
            int iterator = 0;
            for (int i = 0; i < Mask.GetLength(0); i++)
            {
                for (int ii = 0; ii < Mask.GetLength(1); ii++)
                {
                    if (Mask[i, ii] == 0) { continue; }
                    Errors[i, ii] = convertederrors[iterator / convertederrors.GetLength(0), iterator % convertederrors.GetLength(1)];
                    iterator++;
                }
            }
        }
        public double[,] Pool(double[,] input, int pool)
        {
            if (input.GetLength(0) % pool != 0 || input.GetLength(1) % pool != 0)
            { throw new Exception("Unclean divide in pooling"); }
            double[,] output = new double[input.GetLength(0) / pool, input.GetLength(1) / pool];
            Mask = new double[input.GetLength(0), input.GetLength(1)];
            int currentx = 0, currenty = 0;
            for (int i = 0; i < input.GetLength(0); i += pool)
            {
                currenty = 0;
                for (int ii = 0; ii < input.GetLength(1); ii += pool)
                {
                    double max = double.MinValue; int maxX = i, maxY = ii;
                    for (int j = 0; j < pool; j++)
                    {
                        for (int jj = 0; jj < pool; jj++)
                        {
                            if (input[i + j, ii + jj] > max)
                            { max = input[i + j, ii + jj]; maxX = i + j; maxY = ii + jj; continue; }
                        }
                    }
                    Mask[maxX, maxY] = 1;
                    output[currentx, currenty] = input[maxX, maxY];
                    currenty++;
                }
                currentx++;
            }
            return output;
        }
    }
}
