using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CNN1
{
    public partial class Form1 : Form
    {
        bool Run = false;
        public static double[] image = new double[28 * 28];
        int imagespeed = 20;
        int testiterator = 0;
        int BatchSize = 5;
        bool Testing = false;
        public List<string> LayerTypes { get; set; }
        public List<int> LayerCounts { get; set; }
        NN nn = new NN();
        void Learn()
        {
            Thread thread = new Thread(() =>
            {
                int imageiterator = 0;
                while (Run)
                {
                    List<double[]> Images = new List<double[]>();
                    //[0] = mean of image; [1] = stddev of image
                    List<double[]> Stats = new List<double[]>();
                    List<int> Labels = new List<int>();
                    if (!Testing)
                    {
                        double mean = 0;
                        double stddev = 0;
                        //Batch generation
                        for (int i = 0; i < BatchSize; i++)
                        {
                            //Find image and label
                            Images.Add(IO.ReadNextImage()); Labels.Add(IO.ReadNextLabel());
                            //Generate stats for image
                            double samplemean = Maths.CalcMean(Images[i]);
                            Stats.Add(new double[] { samplemean, Maths.CalcStdDev(Images[i], samplemean) });
                            mean += Stats[i][0]; stddev += Stats[i][1];
                        }
                        //Adjust stats for batchsize
                        mean /= BatchSize;
                        stddev /= BatchSize;
                        for (int i = 0; i < BatchSize; i++)
                        {
                            //Batch normalization
                            if (BatchNrmlCB.Checked) { nn.Run(Maths.Normalize(Images[i], mean, stddev), Labels[i], false); }
                            else { nn.Run(Maths.Normalize(Images[i]), Labels[i], false); }
                        }
                        nn.Run(BatchSize);
                    }
                    else
                    {
                        if (testiterator >= 10000) { Run = false; MessageBox.Show("Full epoch completed"); }
                        //Find image and label
                        Images.Add(IO.ReadNextImage()); Labels.Add(IO.ReadNextLabel());
                        double mean = Maths.CalcMean(Images[testiterator]);
                        nn.Run(Maths.Normalize(Images[testiterator], mean, Maths.CalcStdDev(Images[testiterator], mean)), Labels[testiterator], true); testiterator++;
                    }
                    image = Images[Images.Count - 1];
                    Invoke((Action)delegate {
                        AvgGradTxt.Text = Math.Round(nn.AvgGradient, 15).ToString();
                        AvgCorrectTxt.Text = Math.Round(nn.PercCorrect, 15).ToString();
                        ErrorTxt.Text = Math.Round(nn.Error, 15).ToString();
                        if (imageiterator >= imagespeed)
                        {
                            imageiterator = 0;
                            pictureBox1.Image = FromTwoDimIntArrayGray(ResizeImg(Maths.Convert(image)));
                            GuessTxt.Text = nn.Guess.ToString();
                        }
                        imageiterator++;
                    });
                }
                IO.Write(nn);
            });
            thread.IsBackground = true;
            thread.Start();
        }

        public Form1()
        {
            InitializeComponent();

            //Various textboxes
            Batchtxt.Text = BatchSize.ToString();
            AlphaTxt.Text = NN.LearningRate.ToString();
            SpecialDecay.Text = NN.RMSDecay.ToString();
            Nesterov.Checked = NN.UseNesterov;
            ConvStepsTxt.Text = ConvolutionLayer.StepSize.ToString();

            //Special combobox
            Special.Items.Add("None");
            Special.Items.Add("RMSProp");
            Special.Items.Add("Momentum");
            //Special.Items.Add("ADAM");
            Special.SelectedIndex = 1;

            //Layer types combobox
            LayerTypeCB.Items.Add("Fully Connected");
            LayerTypeCB.Items.Add("Convolution");
            LayerTypeCB.Items.Add("Pooling");
            LayerTypeCB.SelectedIndex = 0;

            //NN loading or reset if invalid
            try
            {
                nn = IO.Read();
            }
            catch 
            { 
                MessageBox.Show("Failed to load data; reset to default");
                nn = ResetNN();
            }
            RefreshListBoxes(nn);
            if (LayerTypes.Count == 0)
            {
                nn = ResetNN();
            }
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
        private NN ResetNN()
        {
            NN nn = new NN();
            if (LayerTypes is null || LayerTypes.Count == 0) 
            {
                LayerTypes = DefaultTypes();
                LayerCounts = DefaultCounts();
            }
            nn.Init(GenerateLayers());
            IO.Write(nn);
            return nn;
        }
        private List<string> DefaultTypes()
        {
            var list = new List<string>();
            list.Add("c");
            list.Add("c");
            list.Add("f");
            list.Add("f");
            list.Add("f");
            return list;
        }
        private List<int> DefaultCounts()
        {
            var list = new List<int>();
            list.Add(3);
            list.Add(2);
            list.Add(36);
            list.Add(17);
            list.Add(10);
            return list;
        }
        private void RefreshListBoxes(NN desired)
        {
            LayerTypes = new List<string>();
            LayerCounts = new List<int>();
            LayerLB.Items.Clear();

            foreach (iLayer l in desired.Layers)
            {
                string name = null;
                int len = l.Length;
                if (l is PoolingLayer) { len = (l as PoolingLayer).PoolSize; }
                if (l is FullyConnectedLayer) { LayerTypes.Add("f"); name = "Fully Connected"; }
                if (l is ConvolutionLayer) { LayerTypes.Add("c"); name = "Convolution"; len = (l as ConvolutionLayer).KernelSize; }
                if (l is PoolingLayer) { LayerTypes.Add("p"); name = "Pooling"; }
                LayerCounts.Add(len);
                LayerLB.Items.Add("[" + (LayerCounts.Count - 1).ToString() + "] " + name + ", " + len.ToString());
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (Run) { MessageBox.Show("Cannot reset while running"); return; }
            nn = ResetNN();
        }
        List<iLayer> GenerateLayers()
        {
            int priorsize = NN.Resolution * NN.Resolution;
            List<iLayer> layers = new List<iLayer>();
            for (int i = 0; i < LayerCounts.Count; i++)
            {
                int ncount = LayerCounts[i];
                //Output layer has 10 neurons
                if (i == LayerCounts.Count - 1) { ncount = 10; }
                //If a convolution
                if (LayerTypes[i] == "c")
                {
                    layers.Add(new ConvolutionLayer(ncount, priorsize));
                    //Calculate the padded matrix size (if applicable)
                    int temp = (int)Math.Sqrt(priorsize);
                    priorsize = (int)((temp / ConvolutionLayer.StepSize) - ncount + 1);
                    priorsize *= priorsize;
                    continue;
                }
                if (LayerTypes[i] == "f")
                {
                    layers.Add(new FullyConnectedLayer(ncount, priorsize));
                    priorsize = ncount;
                    continue;
                }
                if (LayerTypes[i] == "p")
                {
                    layers.Add(new PoolingLayer(ncount, priorsize));
                    priorsize = (int)Math.Pow(Math.Sqrt(priorsize) / ncount, 2);
                    continue;
                }
                throw new Exception("Invalid layer type");
            }
            return layers;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            nn.TrialNum = 0;
        }
        int[,] ResizeImg(double[,] input)
        {
            int scale = 10;
            int[,] scaled = new int[28 * scale, 28 * scale];
            //Foreach int in Obstacles
            for (int j = 0; j < 28; j++)
            {
                for (int jj = 0; jj < 28; jj++)
                {
                    //Scale by scale
                    for (int i = 0; i < scale; i++)
                    {
                        for (int ii = 0; ii < scale; ii++)
                        {
                            scaled[(j * scale) + i, (jj * scale) + ii] = (int)input[jj, j];
                        }
                    }
                }
            }
            return scaled;
        }
        public static double[,] Rescale(double[,] array)
        {
            double setmin = 0, setmax = 0;
            //Find the minimum and maximum values of the dataset
            foreach (double d in array)
            {
                if (d > setmax) { setmax = d; }
                if (d < setmin) { setmin = d; }
            }
            //Rescale the dataset
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int ii = 0; ii < array.GetLength(1); ii++)
                {
                    array[i, ii] = 255 * ((array[i, ii] - setmin) / (setmax - setmin));
                }
            }
            return array;
        }
        public static Bitmap FromTwoDimIntArrayGray(Int32[,] data)
        {
            // Transform 2-dimensional Int32 array to 1-byte-per-pixel byte array
            Int32 width = data.GetLength(0);
            Int32 height = data.GetLength(1);
            Int32 byteIndex = 0;
            Byte[] dataBytes = new Byte[height * width];
            for (Int32 y = 0; y < height; y++)
            {
                for (Int32 x = 0; x < width; x++)
                {
                    // logical AND to be 100% sure the int32 value fits inside
                    // the byte even if it contains more data (like, full ARGB).
                    dataBytes[byteIndex] = (Byte)(((UInt32)data[x, y]) & 0xFF);
                    // More efficient than multiplying
                    byteIndex++;
                }
            }
            // generate palette
            Color[] palette = new Color[256];
            for (Int32 b = 0; b < 256; b++)
                palette[b] = Color.FromArgb(b, b, b);
            // Build image
            return BuildImage(dataBytes, width, height, width, PixelFormat.Format8bppIndexed, palette, null);
        }
        /// <summary>
        /// Creates a bitmap based on data, width, height, stride and pixel format.
        /// </summary>
        /// <param name="sourceData">Byte array of raw source data</param>
        /// <param name="width">Width of the image</param>
        /// <param name="height">Height of the image</param>
        /// <param name="stride">Scanline length inside the data</param>
        /// <param name="pixelFormat">Pixel format</param>
        /// <param name="palette">Color palette</param>
        /// <param name="defaultColor">Default color to fill in on the palette if the given colors don't fully fill it.</param>
        /// <returns>The new image</returns>
        public static Bitmap BuildImage(Byte[] sourceData, Int32 width, Int32 height, Int32 stride, PixelFormat pixelFormat, Color[] palette, Color? defaultColor)
        {
            Bitmap newImage = new Bitmap(width, height, pixelFormat);
            BitmapData targetData = newImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, newImage.PixelFormat);
            Int32 newDataWidth = ((Image.GetPixelFormatSize(pixelFormat) * width) + 7) / 8;
            // Compensate for possible negative stride on BMP format.
            Boolean isFlipped = stride < 0;
            stride = Math.Abs(stride);
            // Cache these to avoid unnecessary getter calls.
            Int32 targetStride = targetData.Stride;
            Int64 scan0 = targetData.Scan0.ToInt64();
            for (Int32 y = 0; y < height; y++)
                Marshal.Copy(sourceData, y * stride, new IntPtr(scan0 + y * targetStride), newDataWidth);
            newImage.UnlockBits(targetData);
            // Fix negative stride on BMP format.
            if (isFlipped)
                newImage.RotateFlip(RotateFlipType.Rotate180FlipX);
            // For indexed images, set the palette.
            if ((pixelFormat & PixelFormat.Indexed) != 0 && palette != null)
            {
                ColorPalette pal = newImage.Palette;
                for (Int32 i = 0; i < pal.Entries.Length; i++)
                {
                    if (i < palette.Length)
                        pal.Entries[i] = palette[i];
                    else if (defaultColor.HasValue)
                        pal.Entries[i] = defaultColor.Value;
                    else
                        break;
                }
                newImage.Palette = pal;
            }
            return newImage;
        }
        private void TestCheck_CheckedChanged(object sender, EventArgs e)
        {
            IO.Testing = TestCheck.Checked;
            Testing = TestCheck.Checked;
        }

        private void AlphaTxt_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(AlphaTxt.Text, out double lr)) { MessageBox.Show("NAN"); return; }
            if (lr < 0 || lr > 1) { MessageBox.Show("Learning rate must be between 0 and 1"); return; }
            NN.LearningRate = lr;
        }

        private void Batchtxt_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(Batchtxt.Text, out int bs)) { MessageBox.Show("NAN"); return; }
            if (bs < 0 || bs > 1000) { MessageBox.Show("Batch size must be between 0 and 1000"); return; }
            BatchSize = bs;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            NN.UseNesterov = Nesterov.Checked;
        }

        private void Special_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Special.SelectedItem)
            {
                case "None": NN.UseRMSProp = false; NN.UseMomentum = false; /*NN.UseADAM = false;*/ break;
                case "RMSProp": NN.UseRMSProp = true; NN.RMSDecay = double.Parse(SpecialDecay.Text); NN.UseMomentum = false; /*NN.UseADAM = false;*/ break;
                case "Momentum": NN.UseRMSProp = false; NN.Momentum = double.Parse(SpecialDecay.Text); NN.UseMomentum = true; /*NN.UseADAM = false;*/ break;
                //case "ADAM": NN.UseRMSProp = false; NN.UseMomentum = false; NN.UseADAM = true; break;
            }
        }
        private void SpecialDecay_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(SpecialDecay.Text, out double rate)) { MessageBox.Show("NAN"); return; }
            if (rate < 0 || rate > 1) { MessageBox.Show("Rate must be between 0 and 1"); return; }
            switch (Special.SelectedItem)
            {
                case "None": break;
                case "RMSProp": NN.RMSDecay = rate; break;
                case "Momentum": NN.Momentum = rate; break;
                //case "ADAM": break;
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            int result = int.Parse(LayerCountTxt.Text);
            string type = null;
            if (LayerTypeCB.Text == "Fully Connected") { type = "f"; }
            if (LayerTypeCB.Text == "Convolution") 
            { 
                type = "c";
                if (result > 10)
                { 
                    MessageBox.Show("Convolution's layer count is squared, must still be between 0 and 100");
                    return;
                } 
            }
            if (LayerTypeCB.Text == "Pooling") { type = "p"; }
            LayerTypes.Add(type);
            LayerCounts.Add(result);
            LayerLB.Items.Add("[" + (LayerCounts.Count - 1).ToString() + "] " + LayerTypeCB.Text + ", " + result.ToString());
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (LayerLB.Items.Count == 0) { return; }
            if (LayerLB.SelectedIndex == LayerTypes.Count - 1) { MessageBox.Show("Can't remove the output layer"); return; }
            LayerTypes.RemoveAt(LayerLB.SelectedIndex);
            LayerCounts.RemoveAt(LayerLB.SelectedIndex);
            LayerLB.Items.RemoveAt(LayerLB.SelectedIndex);
        }
        private void LayerCountTxt_TextChanged(object sender, EventArgs e)
        {
            if (!(int.TryParse(LayerCountTxt.Text, out int result)) || result > 100 || result < 1)
            { LayerCountTxt.Text = 30.ToString(); MessageBox.Show("Layer count must be an int between 0 and 100\nReset to default"); return; }
        }

        private void ConvStepsTxt_TextChanged(object sender, EventArgs e)
        {
            if (!(int.TryParse(ConvStepsTxt.Text, out int result)) || result > 5 || result < 1)
            { LayerCountTxt.Text = 1.ToString(); MessageBox.Show("Step size must be an int between 1 and 5\nReset to default"); return; }
            ConvolutionLayer.StepSize = result;
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (LayerLB.Items.Count == 0) { return; }
            if (LayerLB.SelectedIndex == LayerTypes.Count - 1) { MessageBox.Show("Can't change the output layer"); return; }
            int result = int.Parse(LayerCountTxt.Text);
            string type = null;
            if (LayerTypeCB.Text == "Fully Connected") { type = "f"; }
            if (LayerTypeCB.Text == "Convolution")
            {
                type = "c";
                if (result > 10)
                {
                    MessageBox.Show("Convolution's layer count is squared, must be between 0 and 10");
                    return;
                }
            }
            if (LayerTypeCB.Text == "Pooling")
            { 
                type = "p"; 
                if (result > 5 || result < 2)
                {
                    MessageBox.Show("Layer count must be between 2 and 5 for pooling to function");
                    return;
                }

            }
            LayerTypes[LayerLB.SelectedIndex] = type;
            LayerCounts[LayerLB.SelectedIndex] = result;
            LayerLB.Items[LayerLB.SelectedIndex] = "[" + LayerLB.SelectedIndex.ToString() + "] " + LayerTypeCB.Text + ", " + result.ToString();
        }
        private void DefaultBtn_Click(object sender, EventArgs e)
        {
            LayerTypes = DefaultTypes();
            LayerCounts = DefaultCounts();
            NN newnn = ResetNN();
            RefreshListBoxes(newnn);
        }

        private void UpBtn_Click(object sender, EventArgs e)
        {
            if (LayerLB.Items.Count < 2) { return; }
            if(LayerLB.SelectedIndex == 0) { return; }

            //Layercounts
            int i = LayerCounts[LayerLB.SelectedIndex];
            LayerCounts[LayerLB.SelectedIndex] = LayerCounts[LayerLB.SelectedIndex - 1];
            LayerCounts[LayerLB.SelectedIndex - 1] = i;

            //Layer types
            string s = LayerTypes[LayerLB.SelectedIndex];
            LayerTypes[LayerLB.SelectedIndex] = LayerTypes[LayerLB.SelectedIndex - 1];
            LayerTypes[LayerLB.SelectedIndex - 1] = s;

            //Layer list box
            string selected = LayerLB.Items[LayerLB.SelectedIndex].ToString();
            string replaced = LayerLB.Items[LayerLB.SelectedIndex - 1].ToString();
            selected = selected.Remove(1, 1).Insert(1, (LayerLB.SelectedIndex - 1).ToString());
            replaced = replaced.Remove(1, 1).Insert(1, (LayerLB.SelectedIndex).ToString());
            LayerLB.Items[LayerLB.SelectedIndex] = replaced;
            LayerLB.Items[LayerLB.SelectedIndex - 1] = selected;
            LayerLB.SelectedIndex--;
        }
        private void DownBtn_Click(object sender, EventArgs e)
        {
            if (LayerLB.Items.Count < 2) { return; }
            if (LayerLB.SelectedIndex == LayerLB.Items.Count - 1) { return; }

            //Layercounts
            int i = LayerCounts[LayerLB.SelectedIndex];
            LayerCounts[LayerLB.SelectedIndex] = LayerCounts[LayerLB.SelectedIndex + 1];
            LayerCounts[LayerLB.SelectedIndex + 1] = i;

            //Layer types
            string s = LayerTypes[LayerLB.SelectedIndex];
            LayerTypes[LayerLB.SelectedIndex] = LayerTypes[LayerLB.SelectedIndex + 1];
            LayerTypes[LayerLB.SelectedIndex + 1] = s;

            //Layer list box
            string selected = LayerLB.Items[LayerLB.SelectedIndex].ToString();
            string replaced = LayerLB.Items[LayerLB.SelectedIndex + 1].ToString();
            selected = selected.Remove(1, 1).Insert(1, (LayerLB.SelectedIndex + 1).ToString());
            replaced = replaced.Remove(1, 1).Insert(1, (LayerLB.SelectedIndex).ToString());
            LayerLB.Items[LayerLB.SelectedIndex] = replaced;
            LayerLB.Items[LayerLB.SelectedIndex + 1] = selected;
            LayerLB.SelectedIndex++;
        }
        private void LayerLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LayerLB.SelectedIndex < 0) { return; }
            LayerCountTxt.Text = LayerCounts[LayerLB.SelectedIndex].ToString();
        }
    }
} 