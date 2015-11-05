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

        public float ScaleStep { get; set; } = 0.1f;

        public int MaxScale { get; set; } = 8;

        public int MinScale { get; set; } = 8;

        public bool IsReady { get { return SourceImage != null; } }

        public int DefaultScaleWidth { get; set; }

        public Color DefaultDrawColor { get; set; } = Color.White;

        private float CurrScale { get; set; } = 1.0f;

        public Image Image
        {
            get
            {
                return InnerPicBox.Image;
            }
            set
            {
                InnerPicBox.Image = value;
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
                    DefaultDrawColor    = value.BackColor;
                    DefaultScaleWidth   = value.PixelWidth;
                }

                RefreshPixel();
            }
        }
        public int CurrPixelWidth
        {
            get
            {
                if(IsReady)
                {
                    return (int)(CurrScale * DefaultScaleWidth + float.Epsilon);
                }
                return PixelMap.DefaultPixelWidth;
            }
        }

        public int GridWidth
        {
            get
            {
                if(IsReady)
                {
                    return SourceImage.PixelWidth + (SourceImage.ShowGrid ? SourceImage.GridWidth : 0);
                }
                return 0;
            }
        }

        public Size PixelSize
        {
            get
            {
                if(IsReady)
                {
                    return new Size(SourceImage.Width, SourceImage.Height);
                }
                return Size.Empty;
            }
        }

        public void Save(string filename)
        {
            if (IsReady)
            {
                SourceImage.Save(filename);
            }
        }

        public void DrawPixel(Point p, Color c)
        {
            if(IsReady)
            {
                using (var g = Graphics.FromImage(Image))
                {
                    DrawPixel(p, c, g, true);
                }
            }
        }

        public void FakeDrawPixel(Point p, Color c)
        {
            if (IsReady)
            {
                using (var g = InnerPicBox.CreateGraphics())
                {
                    DrawPixel(p, c, g, false);
                }
            }
        }

        public void UpdateDrawable()
        {
            if (IsReady)
            {
                InnerPicBox.Refresh();
            }
        }

        public Color ReadPixel(Point p)
        {
            if(IsReady)
            {
                if (p.X >= 0 && p.Y >= 0 && p.X < SourceImage.Width && p.Y < sourceImage.Height)
                {
                    return SourceImage.GetPixel(p.X, p.Y);
                }
            }
            return Color.Empty;
        }

        private bool DrawPixel(Point p, Color c, Graphics g, bool SaveToMap)
        {
            if(IsReady)
            {
                var b = new SolidBrush(c);

                if (p.X >= 0 && p.Y >= 0 && p.X < SourceImage.Width && p.Y < SourceImage.Height)
                {
                    if (SaveToMap)
                    {
                        SourceImage.SetPixel(p.X, p.Y, c);
                    }
                    BitmapGenerate.DrawPixel(SourceImage, g, p.X, p.Y, c);
                    return true;
                }
            }
            return false;
        }

        public void RefreshGrid()
        {
            if (IsReady && SourceImage.ShowGrid)
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
            if (IsReady)
            {
                Image = BitmapGenerate.Draw(SourceImage);
                InnerPicBox.Invalidate();
            }
        }

        public void Zoom(int n)
        {
            var newScale = Clamp(CurrScale + n * ScaleStep, 1.0f - MinScale * ScaleStep, 1.0f + MaxScale * ScaleStep);
            if (CurrScale != newScale)
            {
                CurrScale = newScale;
                RefreshPixelMapSize();
            }
        }

        public void ZoomReset()
        {
            if (Math.Abs(CurrScale - 1.0f) < float.Epsilon)
            {
                CurrScale = 1.0f;
                RefreshPixelMapSize();
            }
        }

        private void RefreshPixelMapSize()
        {
            if (IsReady)
            {
                SourceImage.PixelWidth = (int)(DefaultScaleWidth * CurrScale);
                RefreshPixel();
            }
        }

        private float Clamp(float value, float min, float max)
        {
            if (value > max) return max;
            if (value < min) return min;
            return value;
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

        const int WHEEL_DELTA = 120;
        private void InnerPicBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None && ModifierKeys == Keys.Control)
            {
                Zoom(e.Delta / WHEEL_DELTA);
            }
        }
    }
}
