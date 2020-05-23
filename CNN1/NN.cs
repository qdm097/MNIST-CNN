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
        public static int Resolution = 28;
        public int NumLayers { get; set; }
        public int PoolSize = 2;
        public int KernelSize = 2;
        public List<iLayer> Layers { get; set; }
        public static double Momentum = .9;
        public static double LearningRate = .000146;
        public static double RMSDecay = .9;

        //TODO: add to GUI
        public static double ClipParameter = 1;

        public static bool UseMomentum { get; set; }
        public static bool UseRMSProp { get; set; }
        public static bool UseNesterov { get; set; }
        public static bool UseClipping { get; set; }
        //public static bool UseADAM = false;

        public double TrialNum = 0;
        public double AvgGradient = 0;
        public double PercCorrect = 0;
        public double Error = 0;
        public int Guess { get; set; }

        /// <summary>
        /// Create a NN of the specified statuses
        /// </summary>
        /// <param name="learningrate">Speed of learning</param>
        /// <param name="momentum">Whether to use [Nesterov] weight momentum</param>
        /// <param name="userms">Whether to use Root Mean Square propegation</param>
        /// <param name="numlayers">How many layers are in the model</param>
        /// <param name="incount">Number of neurons in the input layer</param>
        /// <param name="hidcount">Number of neurons in the hidden layer</param>
        /// <param name="outcount">Number of neurons in the output layer</param>
        public NN(double learningrate, int numlayers)
        {
            NumLayers = numlayers; LearningRate = learningrate;
        }
        public NN() { }
        public NN Init(List<iLayer> layers)
        {
            Layers = layers;
            NumLayers = Layers.Count;
            Random r = new Random();
            for (int i = 0; i < NumLayers; i++)
            {
                Layers[i].Init(r, i == NumLayers - 1);
            }
            return this;
        }
        public void Run(double[] input, int correct, bool testing)
        {
            //Forward
            Layers[0].Calculate(input, NumLayers == 1);
            for (int i = 1; i < Layers.Count; i++)
            {
                Layers[i].Calculate(Layers[i - 1].Values, i == Layers.Count - 1);
            }
            if (!testing)
            {
                //Errors
                Layers[NumLayers - 1].CalcError(correct);
                for (int i = NumLayers - 2; i >= 0; i--)
                {
                    Layers[i].CalcError(Layers[i + 1]); 
                }
                //Backprop
                Layers[0].Backprop(input, NumLayers == 1);
                for (int i = 1; i < NumLayers; i++)
                {
                    Layers[i].Backprop(Layers[i - 1].Values, i == Layers.Count - 1); continue; 
                }
            }
            //Report values
            Guess = -1; double certainty = -5; double error = 0;
            for (int i = 0; i < 10; i++)
            {
                if (Layers[Layers.Count - 1].Values[i] > certainty) { Guess = i; certainty = Layers[Layers.Count - 1].Values[i]; }
                error += ((i == correct ? 1d : 0d) - Layers[Layers.Count - 1].Values[i]) * ((i == correct ? 1d : 0d) - Layers[Layers.Count - 1].Values[i]);
            }
            error = Math.Sqrt(error);
            TrialNum++;

            PercCorrect = (PercCorrect * ((TrialNum) / (TrialNum + 1))) + ((Guess == correct) ? (1 / (TrialNum)) : 0d);
            Error = (Error * ((TrialNum) / (TrialNum + 1))) + (error * (1 / (TrialNum)));
        }
        //Descend
        public void Run(int batchsize)
        {
            double tempavg = 0; int numneurons = 0;
            foreach (iLayer l in Layers)
            {
                l.Descend(batchsize);
                tempavg += l.AvgUpdate;
                l.AvgUpdate = 0;
                numneurons += l.Values.Length;
            }
            tempavg /= numneurons * batchsize;
            if (TrialNum == 0) { TrialNum++; }
            AvgGradient = (AvgGradient * ((TrialNum) / (TrialNum + 1))) + (tempavg * (1 / (TrialNum)));
        }
    }
}