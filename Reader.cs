using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CNN1
{ 
    public static class Reader
    {
        //Stored paths for ease of use
        public static bool Testing = false;
        public static bool LabelReaderRunning = false;
        public static bool ImageReaderRunning = false;

        static readonly string TrainImagePath = @"C:\Users\gwflu\Desktop\Test\train-images-idx3-ubyte\train-images.idx3-ubyte";
        static readonly string TrainLabelPath = @"C:\Users\gwflu\Desktop\Test\train-labels-idx1-ubyte\train-labels.idx1-ubyte";
        static readonly string TestLabelPath = @"C:\Users\gwflu\Desktop\Test\t10k-labels-idx1-ubyte\t10k-labels.idx1-ubyte";
        static readonly string TestImagePath = @"C:\Users\gwflu\Desktop\Test\t10k-images-idx3-ubyte\t10k-images.idx3-ubyte";

        private static string LabelPath = Testing ? TestLabelPath : TrainLabelPath;
        private static string ImagePath = Testing ? TestImagePath : TrainImagePath;
        static int LabelOffset = 8;
        static int ImageOffset = 16;
        static int Resolution = 28;
        //Simple code to read a single number from a file, offset by a byte of metadata
        public static int ReadNextLabel()
        {
            //Singleton process
            if (LabelReaderRunning) { throw new Exception("Already accessing file"); }

            FileStream fs = File.OpenRead(LabelPath);
            //Reset parameters and decrement NN hyperparameters upon new epoch (currently disabled)
            if (!(LabelOffset < fs.Length)) { LabelOffset = 8; ImageOffset = 16; }

            fs.Position = LabelOffset;
            byte[] b = new byte[1];
            try
            {
                fs.Read(b, 0, 1);
            }
            catch (Exception ex) { Console.WriteLine("Reader exception: " + ex.ToString()); Console.ReadLine(); }
            int[] result = Array.ConvertAll(b, Convert.ToInt32);
            LabelOffset++;
            fs.Close();
            foreach (int i in result) { return i; }
            return -1;
        }
        //Read a matrix from a file offset by two bytes of metadata
        public static double[,] ReadNextImage()
        {
            //Singleton
            if (ImageReaderRunning) { throw new Exception("Already accessing file"); }

            //Read image
            FileStream fs = File.OpenRead(ImagePath);
            //Reset parameters and decrement NN hyperparameters upon new epoch (currently disabled)
            if (!(ImageOffset < fs.Length)) { ImageOffset = 16; LabelOffset = 8; }
            fs.Position = ImageOffset;
            byte[] b = new byte[Resolution * Resolution];
            try
            {
                fs.Read(b, 0, Resolution * Resolution);
            }
            catch (Exception ex) { Console.WriteLine("Reader exception: " + ex.ToString()); Console.ReadLine(); }
            int[] array = Array.ConvertAll(b, Convert.ToInt32);
            ImageOffset += Resolution * Resolution;
            //Convert to 2d array
            double[,] result = new double[Resolution, Resolution];
            //Convert array to doubles and store in result
            for (int i = 0; i < Resolution; i++)
            {
                for (int ii = 0; ii < Resolution; ii++)
                {
                    result[i, ii] = (double)array[(Resolution * i) + ii];
                }
            }
            //Normalize the result matrix
            ActivationFunctions.Normalize(result, Resolution, Resolution);

            fs.Close();
            return result;
        }
    }
}
