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
    public partial class PixelPainter : UserControl, IPixelDrawable
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
                RefreshPixel();
            }
        }

        public Color DefaultDrawColor { get; set; } = Color.White;

        private Graphics ShowGraphics { get; set; }

        public void Save(string filename)
        {
            if (SourceImage != null)
            {
                SourceImage.Save(filename);
            }
        }

        public bool DrawPixel(Point p, Color c)
        {
            return DrawPixel(p, c, ShowGraphics, true);
        }

        public bool FakeDrawPixel(Point p, Color c)
        {
            if (InnerPicBox == null) return false;

            using (var g = InnerPicBox.CreateGraphics())
            {
                return DrawPixel(p, c, g, false);
            }
        }

        public int PixelWidth { get { return SourceImage.Width; } }

        public int PixelHeight { get { return SourceImage.Height; } }

        public void UpdateDrawable()
        {
            if (InnerPicBox != null)
            {
                InnerPicBox.Refresh();
            }
        }

        public Color ReadPixel(Point p)
        {
            return SourceImage.GetPixel(p.X, p.Y);
        }

        /// <summary>
        /// 绘制像素块
        /// </summary>
        /// <param name="p">要绘制的像素块的坐标</param>
        /// <param name="c">绘制颜色</param>
        /// <param name="g">目标设备</param>
        /// <param name="SaveToMap">是否将改动保存到内存中</param>
        /// <returns></returns>
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

        public Point ConvertMapToScene(Point p)
        {
            if(SourceImage != null)
            {
                int w = SourceImage.PixelWidth + (SourceImage.ShowGrid ? SourceImage.GridWidth : 0);
                return new Point(p.X * w, p.Y * w);
            }
            return Point.Empty;
        }

        // 转换屏幕坐标到像素块坐标
        public Point ConvertSceneToMap(Point p)
        {
            if (SourceImage != null)
            {
                int w = SourceImage.PixelWidth + (SourceImage.ShowGrid ? SourceImage.GridWidth : 0);
                return new Point(p.X / w, p.Y / w);
            }
            return Point.Empty;
        }

        public void RefreshGrid()
        {
            if (SourceImage != null && InnerPicBox != null && SourceImage.ShowGrid)
            {
                BitmapGenerate.DrawGrid(SourceImage, ShowGraphics);
                InnerPicBox.Invalidate();
            }
        }

        public void RefreshPixel()
        {
            Image = null;

            if (ShowGraphics != null)
            {
                ShowGraphics.Dispose();
                ShowGraphics = null;
            }

            if (SourceImage != null)
            {
                Image = BitmapGenerate.Draw(SourceImage);
                ShowGraphics = Graphics.FromImage(Image);
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
            var new_loc = InnerPicBox.Location;
            if (InnerPicBox.Width < Width)
            {
                new_loc = new Point((Width - InnerPicBox.Width) / 2, new_loc.Y);
            }
            if (InnerPicBox.Height < Height)
            {
                new_loc = new Point(new_loc.X, (Height - InnerPicBox.Height) / 2);
            }
            InnerPicBox.Location = new_loc;
        }
    }
}
