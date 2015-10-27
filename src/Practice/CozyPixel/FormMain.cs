using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CozyPixel.Forms;

namespace CozyPixel
{
    public partial class FormMain : Form
    {
        public Bitmap SourceImage { get; set; }

        public Graphics ShowGraphics { get; set; }

        public Model.PixelMap CurrPixelMap { get; set; }

        public FormMain()
        {
            InitializeComponent();
        }

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDlg = new OpenFileDialog();
            OpenDlg.Filter = @"(*.jpg,*.png,*.jpeg,*.bmp,*.gif)| *.jgp; *.png; *.jpeg; *.bmp; *.gif | All files(*.*) | *.* ";

            if (OpenDlg.ShowDialog() == DialogResult.OK)
            {
                SourceImage                     = new Bitmap(OpenDlg.FileName);
                CurrPixelMap                    = new Model.PixelMap();
                CurrPixelMap.ShowGrid           = false;
                CurrPixelMap.data               = SourceImage;
                CurrPixelMap.PixelWidth         = 10;
                CurrPixelMap.GridWidth          = 1;
                CurrPixelMap.GridColor          = Color.WhiteSmoke;
                PictureBox.Image                = Draw.BitmapGenerate.Draw(CurrPixelMap);
                ShowGraphics                    = PictureBox.CreateGraphics();
            }
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            var about = new AboutForm();
            about.ShowDialog();
        }

        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            if(PictureBox.Image != null)
            {
                SaveFileDialog SaveDlg = new SaveFileDialog();
                SaveDlg.Filter = @"位图(*.bmp)|*.bmp|All Files|*.*";

                if (SaveDlg.ShowDialog() == DialogResult.OK)
                {
                    SourceImage.Save(SaveDlg.FileName);
                }
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            TestColor();
            
        }

        private void TestColor()
        {
            ColorList.AddColor(Color.Red);
            ColorList.AddColor(Color.Orange);
            ColorList.AddColor(Color.Yellow);
            ColorList.AddColor(Color.Green);
            ColorList.AddColor(Color.Blue);
            ColorList.AddColor(Color.White);
            ColorList.AddColor(Color.Black);
            ColorList.AddColor(Color.Pink);
            ColorList.AddColor(Color.Purple);
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if(CurrPixelMap != null)
            {
                var p = e.Location;
                var b = new SolidBrush(ColorList.SelectedColor);
                int w = 0;

                if (CurrPixelMap.ShowGrid)
                {
                    w = (CurrPixelMap.PixelWidth + CurrPixelMap.GridWidth);
                }
                else
                {
                    w = CurrPixelMap.PixelWidth;
                }

                int x   = p.X / w;
                int y   = p.Y / w;

                if(x < SourceImage.Width && y < SourceImage.Height)
                {
                    SourceImage.SetPixel(x, y, ColorList.SelectedColor);

                    int fx  = x * w;
                    int fy  = y * w;
                    fx      = Math.Min(fx, PictureBox.Image.Width);
                    fy      = Math.Min(fy, PictureBox.Image.Height);
                    ShowGraphics.FillRectangle(b, fx, fy, w, w);
                }
            }
        }
    }
}
