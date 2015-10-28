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
using CozyPixel.Model;

namespace CozyPixel
{
    public partial class CozyPixelForm : Form
    {
        private PixelMap CurrPixelMap { get; set; }

        public bool IsModified { get; set; }

        public CozyPixelForm()
        {
            InitializeComponent();
        }

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            if(IsModified)
            {
                var r = MessageBox.Show("是否保存", "", MessageBoxButtons.YesNoCancel);
                if (r == DialogResult.OK)
                {
                    if (!SaveFile())
                    {
                        return;
                    }
                }
                else if (r == DialogResult.Cancel)
                {
                    return;
                }
            }
            OpenFile();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            if(IsModified)
            {
                var r = MessageBox.Show("是否保存", "", MessageBoxButtons.YesNoCancel);
                if (r == DialogResult.OK)
                {
                    if(!SaveFile())
                    {
                        return;
                    }
                }
                else if(r == DialogResult.Cancel)
                {
                    return;
                }
            }

            Close();
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            var about = new AboutForm();
            about.ShowDialog();
        }

        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
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
            if(!IsModified)
            {
                IsModified = true;
            }
        }

        private void GridColorButton_Click(object sender, EventArgs e)
        {
            GridColorButton.BackColor   = ColorList.SelectedColor;
            if(CurrPixelMap != null)
            {
                CurrPixelMap.GridColor = ColorList.SelectedColor;
                PixelPainter.RefreshGrid();
            }
        }

        private void ShowGridCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrPixelMap != null)
            {
                CurrPixelMap.ShowGrid = ShowGridCheckBox.Checked;
                if (CurrPixelMap.ShowGrid)
                {
                    CurrPixelMap.PixelWidth = 16;
                }
                else
                {
                    CurrPixelMap.PixelWidth = 18;
                }
                PixelPainter.RefreshPixel();
            }
        }

        private void GridWidthBox_TextChanged(object sender, EventArgs e)
        {
            int w = 0;
            if(int.TryParse(GridWidthBox.Text, out w))
            {
                if (CurrPixelMap != null)
                {
                    CurrPixelMap.GridWidth = w;
                    PixelPainter.RefreshPixel();
                }
            }
        }
    }
}
