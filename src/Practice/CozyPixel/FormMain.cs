using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            OpenFileDialog OpenDlg  = new OpenFileDialog();
            OpenDlg.Filter          = @"(*.jpg,*.png,*.jpeg,*.bmp,*.gif)| *.jgp; *.png; *.jpeg; *.bmp; *.gif | All files(*.*) | *.* ";

            if (OpenDlg.ShowDialog() == DialogResult.OK)
            {
                Bitmap b            = new Bitmap(OpenDlg.FileName);
                Model.PixelMap pm   = new Model.PixelMap();
                pm.ShowGrid         = false;
                pm.data             = b;
                pm.PixelWidth       = 10;
                PictureBox.Image    = Draw.BitmapGenerate.Draw(pm);
            }
        }
    }
}
