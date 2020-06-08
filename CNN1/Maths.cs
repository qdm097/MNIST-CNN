using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNN1
{
    class Maths
    {
        public static double[] Tanh(double[] input)
        {
            var output = new double[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = Tanh(input[i]);
            }
            return output;
        }
        public static double[] TanhDerriv(double[] input)
        {
            var output = new double[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = TanhDerriv(input[i]);
            }
            return output;
        }
        public static double[,] Tanh(double[,] input)
        {
            var output = new double[input.GetLength(0), input.GetLength(1)];
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int ii = 0; ii < input.GetLength(1); ii++)
                {
                    output[i, ii] = Tanh(input[i, ii]);
                }
            }
            return output;
        }
        public static double Tanh(double number)
        {
            return (Math.Pow(Math.E, 2 * number) - 1) / (Math.Pow(Math.E, 2 * number) + 1); 
        }
        public static double TanhDerriv(double number)
        {
            return (1 - number) * (1 + number);
        }
        public static double[] Rescale(double[] array, double mean, double stddev)
        {
            //zscore
            var arraymean = CalcMean(array);
            var output = Normalize(array, arraymean, CalcStdDev(array, arraymean));
            //Rescale the dataset (opposite of zscore)
            for (int i = 0; i < array.Length; i++)
            {
                // min + ((array[i] - setmin) * (max - min) / (setmax - setmin))
                output[i] = (output[i] * stddev) + mean;
            }
            return output;
        }
        public static T[] Convert<T>(T[,] input)
        {
            T[] output = new T[input.Length];
            int iterator = 0;
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int ii = 0; ii < input.GetLength(1); ii++)
                {
                    output[iterator] = input[i, ii]; iterator++;
                }
            }
            return output;
        }
        public static T[,] Convert<T>(T[] input)
        {
            double sqrt = Math.Sqrt(input.Length);
            //If the input cannot be turned into a square array, throw an error
            if (sqrt != (int)sqrt) { throw new Exception("Invalid input array size"); }
            T[,] output = new T[(int)sqrt, (int)sqrt];
            int iterator = 0;
            for (int i = 0; i < output.GetLength(0); i++)
            {
                for (int ii = 0; ii < output.GetLength(1); ii++)
                {
                    output[i, ii] = input[iterator]; iterator++;
                }
            }
            return output;
        }
        public static double[] Normalize(double[] array, double mean, double stddev)
        {
            var output = new double[array.Length];
            //Prevent errors
            if (stddev == 0) 
            { 
                stddev = .000001; 
            }
            //Calc zscore
            for (int i = 0; i < array.Length; i++)
            {
                output[i] = (array[i] - mean) / stddev;
            }

            return output;
        }
        public static double[] Normalize(double[] input)
        {
            double mean = 0;
            double stddev = 0;
            //Calc mean of data
            foreach (double d in input) { mean += d; }
            mean /= input.Length;
            //Calc std dev of data
            foreach (double d in input) { stddev += (d - mean) * (d - mean); }
            stddev /= input.Length;
            stddev = Math.Sqrt(stddev);
            //Prevent divide by zero b/c of sigma = 0
            if (stddev == 0) { stddev = .000001; }
            double[] output = new double[input.Length];
            //Calc zscores
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = (input[i] - mean) / stddev;
            }

            return output;
        }
        /// <summary>
        /// Calculates the mean of the array
        /// </summary>
        /// <param name="array">The set from which the mean is taken</param>
        /// <returns></returns>
        public static double CalcMean(double[] array)
        {
            double mean = 0;
            foreach (double d in array) { mean += d; }
            mean /= array.Length;
            return mean;
        }
        /// <summary>
        /// Calculates the standard deviation of the array
        /// </summary>
        /// <param name="array">The set from which the stddev is taken</param>
        /// <param name="mean">The mean of the set</param>
        /// <returns></returns>
        public static double CalcStdDev(double[] array, double mean)
        {
            double stddev = 0;
            //Calc std dev of data
            foreach (double d in array) { stddev += (d - mean) * (d - mean); }
            stddev /= array.Length;
            stddev = Math.Sqrt(stddev);
            return stddev;
        }
        public static double[] Scale(double scale, double[] array)
        {
            double[] output = new double[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                output[i] = array[i] * scale;
            }
            return output;
        }
    }
}