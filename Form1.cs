using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;

namespace CNN1
{
    public partial class Form1 : Form
    {
        bool Run = false;
        public static int[,] image = new int[28, 28];
        int iterator = 0;
        int BatchSize = 5;
        NN nn = new NN();
        void Learn()
        {
            new Thread(() =>
            {
                while (Run)
                {
                    double[,] image = Reader.ReadNextImage();
                    int correct = Reader.ReadNextLabel();
                    for (int i = 0; i < BatchSize; i++) {
                        image = Reader.ReadNextImage(); correct = Reader.ReadNextLabel();
                        nn.Run(image, correct);
                    }
                    nn.Run(BatchSize); iterator++;

                    Invoke((Action)delegate {
                        AvgGradTxt.Text = Math.Round(nn.AvgGradient, 15).ToString();
                        AvgCorrectTxt.Text = Math.Round(nn.PercCorrect, 15).ToString();
                        ErrorTxt.Text = Math.Round(nn.Error, 15).ToString();
                        if (iterator > 30) { iterator = 0;
                            pictureBox1.Image = FromTwoDimIntArrayGray(Scaler());
                            GuessTxt.Text = nn.Guess.ToString();
                        }
                       
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
        public int[,] Scaler()
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
                            scaled[(j * scale) + i, (jj * scale) + ii] = image[j, jj];
                        }
                    }
                }
            }
            return scaled;
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
            {
                if (b == 0 || b == 255) { palette[b] = Color.FromArgb(b, b, b); }
                else { palette[b] = Color.FromArgb(255, 255, 255); }
            }
            // Build image
            return BuildImage(dataBytes, width, height, width, PixelFormat.Format8bppIndexed, palette, null);
        }

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
    }
}
