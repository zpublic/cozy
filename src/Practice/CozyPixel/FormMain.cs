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
                Bitmap b            = new Bitmap(OpenDlg.FileName);
                Model.PixelMap pm   = new Model.PixelMap();
                pm.ShowGrid         = true;
                pm.data             = b;
                pm.PixelWidth       = 10;
                PictureBox.Image    = Draw.BitmapGenerate.Draw(pm);
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
                    PictureBox.Image.Save(SaveDlg.FileName);
                }
            }
        }
    }
}
