using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Histogram_Eşitleme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
       
        int[] HistogramRed=new int[256];
        int[] HistogramGreen = new int[256];
        int[] HistogramBlue = new int[256];
        int[] HistogramRedK = new int[256];
        int[] HistogramGreenK = new int[256];
        int[] HistogramBlueK = new int[256];
        int[] YüzdelikRed=new int[256];
        int[] YüzdelikGreen = new int[256];
        int[] YüzdelikBlue = new int[256];
        int Çözünürlük;

        String a = "";

        private void button1_Click(object sender, EventArgs e)
        {
          
            //   string[] karakter = new string[8];
            OpenFileDialog resimsec = new OpenFileDialog();

            resimsec.Title = "Lütfen Bir Gri Resim Seçiniz";
            resimsec.Filter = " (*.jpg)|*.jpg|(*.png)|*.png";
            if (resimsec.ShowDialog() == DialogResult.OK)
            {

                var orjinalGoruntu = new Bitmap(resimsec.OpenFile());
                this.pictureBox1.Image = orjinalGoruntu;
                var goruntuGenislik = orjinalGoruntu.Width;
                var goruntuYukseklik = orjinalGoruntu.Height;
                Çözünürlük = goruntuGenislik * goruntuYukseklik;

                var outputmap = new Bitmap(goruntuGenislik, goruntuYukseklik);
              
                for (var i = 0; i < goruntuGenislik; i++)
                {
                    for (var j = 0; j < goruntuYukseklik; j++)
                    {
                        var piksel = orjinalGoruntu.GetPixel(i, j);

                        HistogramRed[(int)piksel.R]++;
                        HistogramGreen[(int)piksel.G]++;
                        HistogramBlue[(int)piksel.B]++;
                       
                    }
                }




                HistogramRedK[0] = HistogramRed[0];
                HistogramGreenK[0] = HistogramGreen[0];
                HistogramBlueK[0] = HistogramBlue[0];

                for (int j = 0; j < 255; j++)
                {
                    HistogramRedK[j + 1] += HistogramRed[j];
                    HistogramGreenK[j + 1] += HistogramGreen[j];
                    HistogramBlueK[j + 1] += HistogramBlue[j];
                }

                for (int j = 0; j < 256; j++)
                {
                    HistogramRedK[j] = ((int)HistogramRedK[j] * 255) / (Çözünürlük);
                    HistogramGreenK[j] = ((int)HistogramGreenK[j] * 255) / (Çözünürlük);
                    HistogramBlueK[j] = ((int)HistogramBlueK[j] * 255) / (Çözünürlük);
                }


                for (int i = 0; i < goruntuGenislik; i++)
                {
                    for (int j = 0; j < goruntuYukseklik; j++)
                    {
                        
                        var piksel2 = orjinalGoruntu.GetPixel(i, j);
                

var renkliPiksel1 = Color.FromArgb((int)(HistogramRedK[(int)(piksel2.R)]), (int)(HistogramGreenK[piksel2.G]), (int)(HistogramRedK[piksel2.B]));
                        /*
                        outputmap.pixels[i][j].red = histogramredk[(int)kaynak.pixels[i][j].red];
                        outputmap.pixels[i][j].green = histogramgreenk[(int)kaynak.pixels[i][j].green];
                        outputmap.pixels[i][j].blue = histogrambluek[(int)kaynak.pixels[i][j].blue];
                        */
                        outputmap.SetPixel(i,j,(renkliPiksel1));
                    }
                }

/*

                for (int f = 0; f < 256; f++)
                {
                    Yüzdelik[f] =( S[f]*10000) / Çözünürlük;
                    a += Yüzdelik[f].ToString() + "-";
                    //  a = Çözünürlük.ToString();
                }

                for (var i = 0; i < goruntuGenislik; i++)
                {
                    for (var j = 0; j < goruntuYukseklik; j++)
                    {
                        var piksel2 = orjinalGoruntu.GetPixel(i, j);
                            int pixel;
                        for (int f = 0; f < 256; f++)
                        {
                            
                            if (S[f] == 0)
                            {
                                pixel= Yüzdelik[f]/10000;
                            }
                            else if (S[f] >= 255)
                            {
                                pixel = 255;
                            }
                            else if(f > 0)
                            {
                                pixel = S[f - 1] + (Yüzdelik[f]/10000); 
                            }
                            
                        }
                            
                        var renkliPiksel1 = Color.FromArgb((int)(piksel2.R), (int)(piksel2.R), (int)(piksel2.R));

                                           

                        piksellestirilmisGoruntu1.SetPixel(i, j, renkliPiksel1);

                    }
                }
*/
                pictureBox2.Image = outputmap;
               
              //  MessageBox.Show(a);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
                var orjinalGoruntu = new Bitmap(pictureBox1.Image);



                pictureBox2.Image = histogramEşitleme(orjinalGoruntu);



            
        }

        public Bitmap histogramEşitleme(Bitmap KaynakResim)
        {
            Bitmap renderedImage = KaynakResim;

            uint pixels = (uint)renderedImage.Height * (uint)renderedImage.Width;
            decimal Const = 255 / (decimal)pixels;

            int x, y, R, G, B;

          
            int[] HistogramRed2 = new int[256];
            int[] HistogramGreen2 = new int[256];
            int[] HistogramBlue2 = new int[256];


            for (var i = 0; i < renderedImage.Width; i++)
            {
                for (var j = 0; j < renderedImage.Height; j++)
                {
                    var piksel = renderedImage.GetPixel(i, j);

                    HistogramRed2[(int)piksel.R]++;
                    HistogramGreen2[(int)piksel.G]++;
                    HistogramBlue2[(int)piksel.B]++;

                }
            }

            int[] cdfR = HistogramRed2;
            int[] cdfG = HistogramGreen2;
            int[] cdfB = HistogramBlue2;
           
            for (int r = 1; r <= 255; r++)
            {
                cdfR[r] = cdfR[r] + cdfR[r - 1];
                cdfG[r] = cdfG[r] + cdfG[r - 1];
                cdfB[r] = cdfB[r] + cdfB[r - 1];
            }

            for (y = 0; y < renderedImage.Height; y++)
            {
                for (x = 0; x < renderedImage.Width; x++)
                {
                    Color pixelColor = renderedImage.GetPixel(x, y);

                    R = (int)((decimal)cdfR[pixelColor.R] * Const);
                    G = (int)((decimal)cdfG[pixelColor.G] * Const);
                    B = (int)((decimal)cdfB[pixelColor.B] * Const);

                    Color newColor = Color.FromArgb(R, G, B);
                    renderedImage.SetPixel(x, y, newColor);
                }
            }
            return renderedImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog resimsec = new OpenFileDialog();

            resimsec.Title = "Lütfen Bir Gri Resim Seçiniz";
            resimsec.Filter = " (*.jpg)|*.jpg|(*.png)|*.png";
            if (resimsec.ShowDialog() == DialogResult.OK)
            {

                var orjinalGoruntu = new Bitmap(resimsec.OpenFile());
                var orj = orjinalGoruntu;
                this.pictureBox1.Image = orj;
                var goruntuGenislik = orjinalGoruntu.Width;
                var goruntuYukseklik = orjinalGoruntu.Height;
                Çözünürlük = goruntuGenislik * goruntuYukseklik;


            //    pictureBox1.Image = histogramEqualization(orjinalGoruntu);


            }
        }
        }
   
}
