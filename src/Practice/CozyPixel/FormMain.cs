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

            Bitmap b = new Bitmap(@"g:\1.bmp");

            Model.PixelMap pm = new Model.PixelMap();
            pm.data = b;
            pm.PixelWidth = 10;
            pictureBox1.Image = Draw.BitmapGenerate.Draw(pm);
        }
    }
}
