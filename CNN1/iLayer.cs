using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNN1
{
    interface iLayer
    {
        double[,] Weights { get; set; }
        double[] Values { get; set; }
        double[] ZVals { get; set; }
        double[] Errors { get; set; }
        double AvgUpdate { get; set; }
        int Length { get; set; }
        int InputLength { get; set; }

        iLayer Init(Random r, bool isoutput);
        void Descend(int batchsize);
        /// <summary>
        /// Descent for other layers
        /// </summary>
        /// <param name="input">Previous layer's values</param>
        /// <param name="output">Whether the layer is the output layer</param>
        void Backprop(double[] input, bool output);
        void CalcError(iLayer output);
        void CalcError(double correct);
        void Calculate(double[] input, bool output);
        void Calculate(double[,] input, bool output);
    }
}