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
                var CurrPixelMap                = new Model.PixelMap();
                CurrPixelMap.ShowGrid           = false;
                CurrPixelMap.data               = new Bitmap(OpenDlg.FileName);
                CurrPixelMap.PixelWidth         = 10;
                CurrPixelMap.GridWidth          = 1;
                CurrPixelMap.GridColor          = Color.WhiteSmoke;

                PixelPainter.SourceImage          = CurrPixelMap;
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
            if(PixelPainter.Image != null)
            {
                SaveFileDialog SaveDlg = new SaveFileDialog();
                SaveDlg.Filter = @"位图(*.bmp)|*.bmp|All Files|*.*";

                if (SaveDlg.ShowDialog() == DialogResult.OK)
                {
                    PixelPainter.Save(SaveDlg.FileName);
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

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PixelPainter.DrawPixel(e.Location, ColorList.SelectedColor);
        }
    }
}
