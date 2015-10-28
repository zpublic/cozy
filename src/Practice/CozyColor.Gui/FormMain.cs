using CozyColor.Core.Color;
using CozyColor.Core.ColorScheme;
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

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 12; ++i)
            {
                Color f = RandomColor.Generate();
                Color t = RandomColor.Generate();
                Color[] arr = DradientColor.Generate(f, t, 7);
                for (int j = 0; j < 7; ++j)
                {
                    Bitmap b = new Bitmap(64, 64);
                    Graphics g = Graphics.FromImage(b);
                    g.FillRectangle(
                        new SolidBrush(arr[j]),
                        0,
                        0,
                        64,
                        64);
                    pbs[i * 7 + j].Image = b;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Color c1 = RandomColor.Generate();
            Color c2 = RandomColor.Generate();
            Color c3 = Color.White;
            Color c4 = Color.Black;
            Color[] arr1 = DradientColor.Generate(c1, c4, 12);
            Color[] arr2 = DradientColor.Generate(c3, c2, 12);
            for (int i = 0; i < 12; ++i)
            {
                Color[] arr = DradientColor.Generate(arr1[i], arr2[i], 7);
                for (int j = 0; j < 7; ++j)
                {
                    Bitmap b = new Bitmap(64, 64);
                    Graphics g = Graphics.FromImage(b);
                    g.FillRectangle(
                        new SolidBrush(arr[j]),
                        0,
                        0,
                        64,
                        64);
                    pbs[i * 7 + j].Image = b;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Color c1 = RandomColor.Generate();
            Color c2 = RandomColor.Generate();
            Color c3 = RandomColor.Generate();
            Color c4 = RandomColor.Generate();
            Color[] arr1 = DradientColor.Generate(c1, c4, 12);
            Color[] arr2 = DradientColor.Generate(c3, c2, 12);
            for (int i = 0; i < 12; ++i)
            {
                Color[] arr = DradientColor.Generate(arr1[i], arr2[i], 7);
                for (int j = 0; j < 7; ++j)
                {
                    Bitmap b = new Bitmap(64, 64);
                    Graphics g = Graphics.FromImage(b);
                    g.FillRectangle(
                        new SolidBrush(arr[j]),
                        0,
                        0,
                        64,
                        64);
                    pbs[i * 7 + j].Image = b;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 12; ++i)
            {
                pbs[i * 7].Image = null;

                Color[] arr = ColorPair.RandomComplementaryColor();
                Bitmap b = new Bitmap(64, 64);
                Graphics g = Graphics.FromImage(b);
                g.FillRectangle(
                    new SolidBrush(arr[0]),
                    0,
                    0,
                    64,
                    64);
                pbs[i * 7 + 1].Image = b;
                b = new Bitmap(64, 64);
                g = Graphics.FromImage(b);
                g.FillRectangle(
                    new SolidBrush(arr[1]),
                    0,
                    0,
                    64,
                    64);
                pbs[i * 7 + 2].Image = b;

                pbs[i * 7 + 3].Image = null;

                arr = ColorPair.RandomComplementaryColor();
                b = new Bitmap(64, 64);
                g = Graphics.FromImage(b);
                g.FillRectangle(
                    new SolidBrush(arr[0]),
                    0,
                    0,
                    64,
                    64);
                pbs[i * 7 + 4].Image = b;
                b = new Bitmap(64, 64);
                g = Graphics.FromImage(b);
                g.FillRectangle(
                    new SolidBrush(arr[1]),
                    0,
                    0,
                    64,
                    64);
                pbs[i * 7 + 5].Image = b;

                pbs[i * 7 + 6].Image = null;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 12; ++i)
            {
                Color[] arr = ColorList.RandomColorLine();
                for (int j = 0; j < 7; ++j)
                {
                    Bitmap b = new Bitmap(64, 64);
                    Graphics g = Graphics.FromImage(b);
                    g.FillRectangle(
                        new SolidBrush(arr[j]),
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
