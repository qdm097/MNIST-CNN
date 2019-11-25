using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CNN1
{
    static class Data
    {
        static readonly string Path = @"C:\Users\gwflu\Desktop\Test\DataBackup.txt";
        public static bool Running = false;
        public static void Read(NN nn)
        {
            if (Running) { throw new Exception("Already accessing file"); }
            Running = true;
            FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs);
            string text = sr.ReadToEnd();
            sr.Close(); fs.Close();
            string[] split = text.Split(' ');

            int numlayers = int.Parse(split[0]);
            nn.Layers = new List<Layer>();
            //Seems like these nums are not used fully,
            //I want to use for persistance later on
            int numconv = int.Parse(split[1]);
            nn.Convolutions = new List<Convolution>();
            nn.Poolings = new List<Pooling>();

            int iterator = 2;
            for (int i = 0; i < numconv; i++)
            {
                int kernelsize = int.Parse(split[iterator]); iterator++;
                nn.Convolutions.Add(new Convolution(kernelsize));
                nn.Poolings.Add(new Pooling());
                for (int ii = 0; ii < kernelsize; ii++)
                {
                    for (int iii = 0; iii < kernelsize; iii++)
                    {
                        nn.Convolutions[i].Kernel[ii, iii] = double.Parse(split[iterator]);
                        iterator++;
                    }
                }
            }
            for (int j = 0; j < numlayers; j++)
            {
                int length = int.Parse(split[iterator]); iterator++;
                int inputlength = int.Parse(split[iterator]); iterator++;
                nn.Layers.Add(new Layer(length, inputlength));
                for (int i = 0; i < length; i++)
                {
                    for (int ii = 0; ii < inputlength; ii++)
                    {
                        nn.Layers[j].Weights[i, ii] = double.Parse(split[iterator]);
                        iterator++;
                    }
                    nn.Layers[j].Biases[i] = double.Parse(split[iterator]);
                    iterator++;
                }
            }           
            Running = false;
        }
        public static void Write(NN nn)
        {
            if (Running) { throw new Exception("Already accessing file"); }
            Running = true;
            FileStream fs = new FileStream(Path, FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(nn.NumLayers + " " + nn.NumConvPools + " ");
            foreach (Convolution c in nn.Convolutions)
            {
                sw.Write(c.KernelSize + " ");
                foreach (double d in c.Kernel)
                {
                    sw.Write(d + " ");
                }
            }
            foreach(Layer l in nn.Layers)
            {
                sw.Write(l.Length + " " + l.InputLength + " ");
                for (int i = 0; i < l.Length; i++)
                {
                    for (int ii = 0; ii < l.InputLength; ii++)
                    {
                        sw.Write(l.Weights[i, ii] + " ");
                    }
                    sw.Write(l.Biases[i] + " ");
                }
            }
            sw.Close(); fs.Close();
            Running = false;
        }
    }
}
