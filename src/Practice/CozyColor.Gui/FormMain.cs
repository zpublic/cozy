using CozyColor.Core.Color;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CozyColor.Gui
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            initPicBox();
            randomColor();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            randomColor();
        }

        private PictureBox[] pbs = new PictureBox[12 * 7];

        private void initPicBox()
        {
            for (int i = 0; i < 12; ++i)
            {
                for (int j = 0; j < 7; ++j)
                {
                    var pb = new System.Windows.Forms.PictureBox();
                    pb.Location = new System.Drawing.Point(10 + 64 * i, 80 + 64 * j);
                    pb.Size = new System.Drawing.Size(64, 64);
                    Controls.Add(pb);
                    pbs[i * 7 + j] = pb;
                }
            }
        }

        private void randomColor()
        {
            for (int i = 0; i < 12; ++i)
            {
                for (int j = 0; j < 7; ++j)
                {
                    Color c = RandomColor.Generate();
                    Bitmap b = new Bitmap(64, 64);
                    Graphics g = Graphics.FromImage(b);
                    g.FillRectangle(
                        new SolidBrush(c),
                        0,
                        0,
                        64,
                        64);
                    pbs[i * 7 + j].Image = b;
                }
            }
        }
    }
}
