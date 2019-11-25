using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CNN1
{
    class NN
    {
        //Must be 1 for now
        static int Resolution = 28;
        public int NumConvPools = 0;
        public int NumLayers = 3;
        public int INCount = 28;
        public int NCount = 15;
        public int ONCount = 10;
        public int ConvSteps = 3;
        public int PoolSize = 1;
        public int KernelSize = 3;
        public List<Convolution> Convolutions { get; set; }
        public List<Pooling> Poolings { get; set; }
        public List<Layer> Layers { get; set; }
        double Momentum = .9;
        double LearningRate = .000146;

        public int TrialNum = 1;
        public double AvgGradient = 0;
        public double PercCorrect = 0;
        public double Error = 0;
        public double Guess { get; set; }
        public void Init()
        {
            Convolutions = new List<Convolution>();
            Poolings = new List<Pooling>();
            Layers = new List<Layer>();
            int count = NCount;
            int lowercount = Resolution;
            Random r = new Random();
            for (int i = 0; i < NumConvPools; i++)
            { Convolutions.Add(new Convolution(KernelSize)); Poolings.Add(new Pooling()); }
            for (int i = 0; i < NumLayers; i++)
            {
                if (NumConvPools == 0) { lowercount = Resolution * Resolution; }
                for (int ii = 0; ii < NumConvPools; ii++)
                {
                    lowercount = (int)Math.Pow(((lowercount / ConvSteps) - KernelSize), 2) / PoolSize;
                }
                if (i != 0) { lowercount = Layers[i - 1].Length; count = NCount; }
                if (i == 0) { count = INCount; }
                if (i == NumLayers - 1) { count = ONCount; }
                Layers.Add(new Layer(count, lowercount));
                Layers[i].Weights = new double[count, lowercount];
                Layers[i].Biases = new double[count];
                for (int j = 0; j < count; j++)
                {
                    for (int jj = 0; jj < lowercount; jj++)
                    {
                        //Weights initialized to a random number in range (-1/(2 * lowercount^2) - 1/(2 * lowercount^2))
                        //This is Lecun initialization
                        Layers[i].Weights[j, jj] = (r.NextDouble() > .5 ? -1 : 1) * r.NextDouble() * Math.Sqrt(3d / (double)(lowercount * lowercount));
                    }
                }
            }
            for (int i = 0; i < NumConvPools; i++)
            {
                Convolutions[i].Kernel = new double[KernelSize, KernelSize];
                for (int ii = 0; ii < KernelSize; ii++)
                {
                    for (int iii = 0; iii < KernelSize; iii++)
                    {
                        Convolutions[i].Kernel[ii, iii] = (r.NextDouble() > .5 ? -1 : 1) * r.NextDouble() * Math.Sqrt(3d / (double)(lowercount * lowercount));
                    }
                }
            }
        }
        public void Run(int batchsize)
        {
            for (int timer = 0; timer < batchsize; timer++)
            {
                double[,] input = Reader.ReadNextImage();
                int correct = Reader.ReadNextLabel();
                //Forward
                for (int i = 0; i < Convolutions.Count; i++)
                {
                    input = Poolings[i].Pool(Convolutions[i].Convolve(input, ConvSteps), PoolSize);
                }
                for (int i = 0; i < Layers.Count; i++)
                {
                    if (i == 0) { Layers[i].Calculate(input); continue; }
                    Layers[i].Calculate(Layers[i - 1].Values, i == Layers.Count - 1);
                }
                //Backward
                for (int i = Layers.Count - 1; i >= 0; i--)
                {
                    if (i == Layers.Count - 1) { Layers[i].Backprop(correct); continue; }
                    Layers[i].Backprop(Layers[i + 1]);
                }
                for (int i = Convolutions.Count - 1; i >= 0; i--)
                {
                    if (i == Convolutions.Count - 1) { Poolings[i].Backprop(Layers[0]); }
                    else { /*backprop from a convolution (undefined for now)*/ }
                    Convolutions[i].Backprop(Poolings[i], PoolSize);
                }
                //Descend

                //Need a convolution descent loop

                for (int i = 0; i < Layers.Count; i++)
                {
                    if (i == 0) { Layers[i].Descend(input, Momentum, LearningRate); continue; }
                    Layers[i].Descend(Layers[i - 1].ZVals, Momentum, LearningRate, i == Layers.Count - 1);
                }
                UI(correct);
            }
            //Batch descend
            foreach (Layer l in Layers) { l.Descend(batchsize, LearningRate); }
        }
        public void UI(int correct)
        {
            double avggrad = 0;
            foreach (Layer l in Layers) { avggrad += l.AvgGradient; }
            AvgGradient = (AvgGradient * ((TrialNum) / (TrialNum + 1))) + (avggrad * (1 / (TrialNum)));

            int guess = -1; double certainty = -99;
            for (int i = 0; i < ONCount; i++)
            { if (Layers[Layers.Count - 1].Values[i] > certainty)
                { guess = i; certainty = Layers[Layers.Count - 1].Values[i]; }}
            PercCorrect = (PercCorrect * ((TrialNum) / (TrialNum + 1))) + ((guess == correct) ? (1 / (TrialNum)) : 0d);

            double error = 0;
            for (int i = 0; i < ONCount; i++)
            { error += Math.Pow(((i == correct ? 1d : 0d) - Layers[Layers.Count - 1].Values[i]), 2); }
            Error = (Error * ((TrialNum) / (TrialNum + 1))) + (error * (1 / (TrialNum)));

            TrialNum++;
        }
    }
    class ActivationFunctions
    {
        public static double Tanh(double number)
        {
            return (Math.Pow(Math.E, 2 * number) - 1) / (Math.Pow(Math.E, 2 * number) + 1);
        }
        public static double TanhDerriv(double number)
        {
            return (1 - Math.Pow(Tanh(number), 2));
        }
        public static double[] Normalize(double[] array)
        {
            double mean = 0;
            double stddev = 0;
            //Calc mean of data
            foreach (double d in array) { mean += d; }
            mean /= array.Length;
            //Calc std dev of data
            foreach (double d in array) { stddev += (d - mean) * (d - mean); }
            stddev /= array.Length;
            stddev = Math.Sqrt(stddev);
            //Prevent divide by zero b/c of sigma = 0
            if (stddev == 0) { stddev = .000001; }
            //Calc zscore
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (array[i] - mean) / stddev;
            }

            return array;
        }
        public static double[,] Normalize(double[,] array, int depth, int count)
        {
            double[] smallarray = new double[depth * count];
            int iterator = 0;
            for (int i = 0; i < depth; i++)
            {
                for (int ii = 0; ii < count; ii++)
                {
                    smallarray[iterator] = array[i, ii];
                    iterator++;
                }
            }
            smallarray = Normalize(smallarray);
            iterator = 0;
            for (int i = 0; i < depth; i++)
            {
                for (int ii = 0; ii < count; ii++)
                {
                    array[i, ii] = smallarray[iterator];
                    iterator++;
                }
            }
            return array;
        }
    }
    class Layer
    {
        public double[,] Weights { get; set; }
        double[,] WeightMomentum { get; set; }
        double[,] WeightGradient { get; set; }
        public double[] Biases { get; set; }
        double[] BiasMomentum { get; set; }
        double[] BiasGradient { get; set; }
        public double[] Values { get; set; }
        public double[] ZVals { get; set; }
        public double[] Errors { get; set; }
        public double AvgGradient { get; set; }
        public int Length { get; set; }
        public int InputLength { get; set; }
        public Layer(int length, int inputlength)
        {
            Length = length;
            InputLength = inputlength;
            Weights = new double[Length, InputLength];
            WeightMomentum = new double[Length, InputLength];
            Biases = new double[Length];
            BiasMomentum = new double[Length];
        }
        public void Descend(int batchsize, double learningrate)
        {
            for (int i = 0; i < Length; i++)
            {
                for (int ii = 0; ii < InputLength; ii++)
                {
                    double gradient = learningrate * WeightGradient[i, ii] * (-2d / batchsize);
                    Weights[i, ii] -= gradient;
                }
                Biases[i] -= learningrate * BiasGradient[i] * (-2d / batchsize);
            }
        }
        public void Descend(double[] input, double momentum, double learningrate, bool output)
        {
            WeightGradient = new double[Length, InputLength];
            BiasGradient = new double[Length];
            for (int i = 0; i < Length; i++)
            {
                for (int ii = 0; ii < InputLength; ii++)
                {
                    //Weight gradients
                    double wgradient = input[ii] * ActivationFunctions.TanhDerriv(ZVals[i]) * Errors[i];
                    WeightMomentum[i, ii] = (WeightMomentum[i, ii] * momentum) - (learningrate * wgradient);
                    WeightGradient[i, ii] += wgradient + WeightMomentum[i, ii];
                    AvgGradient -= wgradient;
                }
                if (output) { continue; }
                //Bias gradients
                double bgradient = ActivationFunctions.TanhDerriv(ZVals[i]) * Errors[i];
                BiasMomentum[i] = (BiasMomentum[i] * momentum) - (learningrate * bgradient);
                BiasGradient[i] += bgradient + BiasMomentum[i];
            }
            AvgGradient /= Weights.Length;
        }
        public void Descend(double[,] input, double momentum, double learningrate)
        {
            double[] input2 = new double[input.Length];
            int iterator = 0;
            foreach (double d in input) { input2[iterator] = d; iterator++; }
            Descend(input2, momentum, learningrate, false);
        }
        public void Backprop(Layer output)
        {
            Errors = new double[Length];
            for (int k = 0; k < output.Length; k++)
            {
                for (int j = 0; j < Length; j++)
                {
                    Errors[j] += output.Weights[k, j] * ActivationFunctions.TanhDerriv(output.ZVals[k]) * output.Errors[k];
                }
            }
        }
        public void Backprop(int correct)
        {
            Errors = new double[Length];
            for (int i = 0; i < Length; i++)
            {
                Errors[i] = 2d * ((i == correct ? 1d : 0d) - Values[i]);
            }
        }
        public void Calculate(double[] input, bool output)
        {
            ZVals = new double[Length]; Values = new double[Length];
            for (int k = 0; k < Length; k++)
            {
                for (int j = 0; j < InputLength; j++)
                {
                    if (output) { ZVals[k] = ((Weights[k, j] + WeightMomentum[k, j]) * input[j]); }
                    else { ZVals[k] = ((Weights[k, j] + WeightMomentum[k, j]) * input[j]) + Biases[k] + BiasMomentum[k]; }
                }
                if (!output) { Values[k] = ActivationFunctions.Tanh(ZVals[k]); }
                else { Values[k] = ZVals[k]; }
            }
        }
        public void Calculate(double[,] input)
        {
            double[] input2 = new double[input.Length];
            int iterator = 0;
            foreach (double d in input) { input2[iterator] = d; iterator++; }
            Calculate(input2, false);
        }
    }
}
