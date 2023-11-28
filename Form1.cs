using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taboada_Image_Processing
{
    public partial class Form1 : Form
    {
        Bitmap OrigImg, ResImg;

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            OrigImg = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = OrigImg;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg, *.jpeg)|*.jpg;*.jpeg|Bitmap Image (*.bmp)|*.bmp|All Files (*.*)|*.*";
            saveFileDialog1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResImg = new Bitmap(OrigImg.Width, OrigImg.Height);
            for (int x = 0; x < ResImg.Width; x++)
                for (int y = 0; y < ResImg.Height; y++)
                {
                    Color pixel = OrigImg.GetPixel(x, y);
                    ResImg.SetPixel(x, y, pixel);
                }
            pictureBox2.Image = ResImg;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResImg = new Bitmap(OrigImg.Width, OrigImg.Height);
            for (int x = 0; x < ResImg.Width; x++)
                for (int y = 0; y < ResImg.Height; y++)
                {
                    Color pixel = OrigImg.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    ResImg.SetPixel(x, y, Color.FromArgb(grey, grey, grey));
                }
            pictureBox2.Image = ResImg;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ResImg = new Bitmap(OrigImg.Width, OrigImg.Height);
            for (int x = 0; x < ResImg.Width; x++)
                for (int y = 0; y < ResImg.Height; y++)
                {
                    Color pixel = OrigImg.GetPixel(x, y);
                    ResImg.SetPixel(x, y, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                }
            pictureBox2.Image = ResImg;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ResImg = new Bitmap(OrigImg.Width, OrigImg.Height);
            for (int y = 0; y < ResImg.Height; y++)
                for (int x = 0; x < ResImg.Width; x++)
                {
                    Color pixelColor = OrigImg.GetPixel(x, y);

                    int outputRed = (int)(pixelColor.R * 0.393 + pixelColor.G * 0.769 + pixelColor.B * 0.189);
                    int outputGreen = (int)(pixelColor.R * 0.349 + pixelColor.G * 0.686 + pixelColor.B * 0.168);
                    int outputBlue = (int)(pixelColor.R * 0.272 + pixelColor.G * 0.534 + pixelColor.B * 0.131);


                    outputRed = Math.Min(255, Math.Max(0, outputRed));
                    outputGreen = Math.Min(255, Math.Max(0, outputGreen));
                    outputBlue = Math.Min(255, Math.Max(0, outputBlue));

                    Color outputColor = Color.FromArgb(outputRed, outputGreen, outputBlue);
                    ResImg.SetPixel(x, y, outputColor);
                }

            pictureBox2.Image = ResImg;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ResImg = new Bitmap(OrigImg.Width, OrigImg.Height);
            for (int x = 0; x < ResImg.Width; x++)
                for (int y = 0; y < ResImg.Height; y++)
                {
                    Color pixel =   OrigImg.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    ResImg.SetPixel(x, y, Color.FromArgb(grey, grey, grey));
                }
            Color sample;
            int[] histdata = new int[256];
            for (int x = 0; x < ResImg.Width; x++)
                for (int y = 0; y < ResImg.Height; y++)
                {
                    sample = ResImg.GetPixel(x, y);
                    histdata[sample.R]++;
                }

            Bitmap mydata = new Bitmap(256, 800);
            for (int x = 0; x < 256; x++)
                for (int y = 0; y < 800; y++)
                {
                    mydata.SetPixel(x, y, Color.White);
                }

            for (int x = 0; x < 256; x++)
                for (int y = 0; y < Math.Min(histdata[x] / 5, 800); y++) //we get the data from each intensity we have
                {
                    mydata.SetPixel(x, 799 - y, Color.Black);
                }
            pictureBox2.Image = mydata;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

            if (ResImg != null)
            {

                if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
                {

                    string fileExtension = System.IO.Path.GetExtension(saveFileDialog1.FileName);
                    ResImg.Save(saveFileDialog1.FileName, GetImageFormatFromExtension(fileExtension));
                }
            }
        }

        private ImageFormat GetImageFormatFromExtension(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".png":
                    return ImageFormat.Png;
                case ".jpg":
                case ".jpeg":
                    return ImageFormat.Jpeg;
                case ".bmp":
                    return ImageFormat.Bmp;
                default:
                    return ImageFormat.Png;
            }
        }

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.FileOk += new CancelEventHandler(openFileDialog1_FileOk);
        }
    }
}
