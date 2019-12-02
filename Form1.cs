using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace CNN1
{
    public partial class Form1 : Form
    {
        bool Run = false;
        int BatchSize = 5;
        NN nn = new NN();
        void Learn()
        {
            new Thread(() =>
            {
                while (Run)
                {
                    for (int i = 0; i < BatchSize; i++) { nn.Run(); }
                    nn.Run(BatchSize);

                    Invoke((Action)delegate {
                        AvgGradTxt.Text = Math.Round(nn.AvgGradient, 15).ToString();
                        AvgCorrectTxt.Text = Math.Round(nn.PercCorrect, 15).ToString();
                        ErrorTxt.Text = Math.Round(nn.Error, 15).ToString();
                    });
                }
                Data.Write(nn);
            }).Start();
        }
        
        public Form1()
        {
            InitializeComponent();
            try { Data.Read(nn); }
            catch { MessageBox.Show("Failed to load data; reset to default"); Data.Running = false; nn.Init(); }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (Run == true) { MessageBox.Show("Already running"); return; }
            Run = true;
            Learn();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Run = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (Run) { MessageBox.Show("Cannot reset while running"); return; }
            nn.Init();
            Data.Write(nn);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            nn.TrialNum = 0;
        }
    }
}
