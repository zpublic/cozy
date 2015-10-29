using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CozyPixel.Model;
using CozyPixel.Draw;
using CozyPixel.Tools;

namespace CozyPixel.Controls
{
    public class PixelPaintControl : PictureBox, IPixelDrawAble
    {
        public PixelPaintControl()
        {
            Cursor = Cursors.Cross;
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

        private Graphics ShowGraphics { get; set; }

        public void Save(string filename)
        {
            if(SourceImage != null)
            {
                SourceImage.data.Save(filename);
            }
        }

        public bool DrawPixel(Point p, Color c)
        {
            return DrawPixel(p, c, ShowGraphics, true);
        }

        public bool DrawLine(Point begin, Point end, Color c)
        {
            return DrawLine(begin, end, c, ShowGraphics, true);
        }

        public bool FakeDrawPixel(Point p, Color c)
        {
            using (var g = CreateGraphics())
            {
                return DrawPixel(p, c, g, false);
            }
        }

        public bool FakeDrawLine(Point begin, Point end, Color c)
        {
            using (var g = CreateGraphics())
            {
                return DrawLine(begin, end, c, g, false);
            }
        }

        public void PixelRefresh()
        {
            Invalidate();
        }

        private bool DrawPixel(Point p, Color c, Graphics g, bool SaveToMap)
        {
            if (SourceImage != null)
            {
                var b = new SolidBrush(c);
                int w = SourceImage.PixelWidth + (SourceImage.ShowGrid ? SourceImage.GridWidth : 0);

                int x = p.X / w;
                int y = p.Y / w;

                if (x >= 0 && y >= 0 && x < SourceImage.data.Width && y < SourceImage.data.Height)
                {
                    if(SaveToMap)
                    {
                        SourceImage.SetPixel(x, y, c);
                    }
                    BitmapGenerate.DrawPixel(SourceImage, g, x, y, c);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Bresenham算法
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool DrawLine(Point begin, Point end, Color c, Graphics g, bool SaveToMap)
        {
            if(begin == end)
            {
                return DrawPixel(begin, c, g, SaveToMap);
            }

            Point left  = begin.X < end.X ? begin : end;
            Point right = begin == left ? end : begin;

            int dx  = right.X - left.X;
            int dy  = right.Y - left.Y;
            int e   = -dx;
            int x   = left.X;
            int y   = left.Y;

            for(int i = 0; i <= dx; ++i)
            {
                DrawPixel(new Point(x, y), c, g, SaveToMap);
                x++;
                e = e + 2 * dy;
                if(e >= 0)
                {
                    y++;
                    e = e - 2 * dx;
                }
            }
            return true;
        }

        public void RefreshGrid()
        {
            if (SourceImage != null && SourceImage.ShowGrid)
            {
                BitmapGenerate.DrawGrid(SourceImage, ShowGraphics);
                Invalidate();
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
            Invalidate();
        }
    }
}
