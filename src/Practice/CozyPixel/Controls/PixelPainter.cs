using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CozyPixel.Draw;
using CozyPixel.Tools;
using CozyPixel.Model;

namespace CozyPixel.Controls
{
    public partial class PixelPainter : UserControl, IPixelGridDrawable
    {
        public PixelPainter()
        {
            InitializeComponent();
        }

        public Image Image
        {
            get
            {
                return InnerPicBox == null ? null : InnerPicBox.Image;
            }
            set
            {
                if (InnerPicBox != null)
                {
                    InnerPicBox.Image = value;
                }
            }
        }

        private PixelMap sourceImage;
        public PixelMap SourceImage
        {
            get
            {
                return sourceImage;
            }
            set
            {
                sourceImage = value;
                if (value != null)
                {
                    DefaultDrawColor = value.BackColor;
                }

                RefreshPixel();
            }
        }

        public Color DefaultDrawColor { get; set; } = Color.White;

        public int GridWidth { get { return SourceImage.PixelWidth + (SourceImage.ShowGrid ? SourceImage.GridWidth : 0); } }

        public Size PixelSize { get { return new Size(SourceImage.Width, SourceImage.Height); } }

        public void Save(string filename)
        {
            if (SourceImage != null)
            {
                SourceImage.Save(filename);
            }
        }

        public void DrawPixel(Point p, Color c)
        {
            using (var g = Graphics.FromImage(Image))
            {
                DrawPixel(p, c, g, true);
            }
        }

        public void FakeDrawPixel(Point p, Color c)
        {
            if (InnerPicBox == null) return;

            using (var g = InnerPicBox.CreateGraphics())
            {
                DrawPixel(p, c, g, false);
            }
        }

        public void UpdateDrawable()
        {
            if (InnerPicBox != null)
            {
                InnerPicBox.Refresh();
            }
        }

        public Color ReadPixel(Point p)
        {
            if(p.X >= 0 && p.Y >= 0 && p.X < SourceImage.Width && p.Y < sourceImage.Height)
            {
                if (SourceImage != null)
                {
                    return SourceImage.GetPixel(p.X, p.Y);
                }
            }
            return Color.Empty;
        }

        private bool DrawPixel(Point p, Color c, Graphics g, bool SaveToMap)
        {
            if (SourceImage != null)
            {
                var b = new SolidBrush(c);

                if (p.X >= 0 && p.Y >= 0 && p.X < SourceImage.Width && p.Y < SourceImage.Height)
                {
                    if (SaveToMap)
                    {
                        SourceImage.SetPixel(p.X, p.Y, c);
                    }
                    BitmapGenerate.DrawPixel(SourceImage, g, p.X, p.Y, c);
                }
                return true;
            }
            return false;
        }

        public void RefreshGrid()
        {
            if (SourceImage != null && InnerPicBox != null && SourceImage.ShowGrid)
            {
                using (var g = Graphics.FromImage(Image))
                {
                    BitmapGenerate.DrawGrid(SourceImage, g);
                }
                InnerPicBox.Invalidate();
            }
        }

        public void RefreshPixel()
        {
            Image = null;

            if (SourceImage != null)
            {
                Image = BitmapGenerate.Draw(SourceImage);
            }

            if (InnerPicBox != null)
            {
                InnerPicBox.Invalidate();
            }
        }

        private void InnerPicBox_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        private void InnerPicBox_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        private void InnerPicBox_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        private void InnerPicBox_SizeChanged(object sender, EventArgs e)
        {
            InnerPicBox.Location = new Point( (Width - InnerPicBox.Width) / 2, (Height - InnerPicBox.Height) / 2);
        }
    }
}
