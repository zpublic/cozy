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
    public class PixelPaintControl : PictureBox, IPixelDrawable
    {
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
            if(SourceImage != null)
            {
                SourceImage.data.Save(filename);
            }
        }

        public bool DrawPixel(Point p, Color c)
        {
            var mapp = ConvertSceneToMap(p);
            return DrawPixel(mapp, c, ShowGraphics, true);
        }

        public bool DrawLine(Point begin, Point end, Color c)
        {
            var mapBegin    = ConvertSceneToMap(begin);
            var mapEnd      = ConvertSceneToMap(end);
            return DrawLine(mapBegin, mapEnd, c, ShowGraphics, true);
        }

        public bool FakeDrawPixel(Point p, Color c)
        {
            var mapp = ConvertSceneToMap(p);
            using (var g = CreateGraphics())
            {
                return DrawPixel(p, c, g, false);
            }
        }

        public bool FakeDrawLine(Point begin, Point end, Color c)
        {
            var mapBegin    = ConvertSceneToMap(begin);
            var mapEnd      = ConvertSceneToMap(end);
            using (var g = CreateGraphics())
            {
                return DrawLine(mapBegin, mapEnd, c, g, false);
            }
        }

        public void UpdateDrawable()
        {
            Refresh();
        }

        public Color ReadPixel(Point p)
        {
            var mapp = ConvertSceneToMap(p);
            return SourceImage.GetPixel(mapp.X, mapp.Y);
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
                var b       = new SolidBrush(c);
                int x       = p.X;
                int y       = p.Y;

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

        // 转换屏幕坐标到像素块坐标
        private Point ConvertSceneToMap(Point p)
        {
            if (SourceImage != null)
            {
                int w = SourceImage.PixelWidth + (SourceImage.ShowGrid ? SourceImage.GridWidth : 0);
                return new Point(p.X / w, p.Y / w);
            }
            return Point.Empty;
        }

        /// <summary>
        /// DDA算法绘制直线
        /// </summary>
        /// <param name="begin">线的起点</param>
        /// <param name="end">线的重点</param>
        /// <param name="c">颜色</param>
        /// <param name="g">目标设备</param>
        /// <param name="SaveToMap">是否将改动保存到内存中</param>
        /// <returns></returns>
        private bool DrawLine(Point begin, Point end, Color c, Graphics g, bool SaveToMap)
        {
            DrawPixel(end, c, g, SaveToMap);

            int n   = 0;
            int k   = 0;
            int dx  = end.X - begin.X;
            int dy  = end.Y - begin.Y;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                n = Math.Abs(dx);
            }
            else
            {
                n = Math.Abs(dy);
            }

            float xinc  = (float)dx / n;
            float yinc  = (float)dy / n;
            float x     = begin.X;
            float y     = begin.Y;

            bool ret = false;
            for (k = 1; k <= n; k++)
            {
                if(DrawPixel(new Point((int)(x + 0.5f), (int)(y + 0.5f)), c, g, SaveToMap))
                {
                    ret = true;
                }

                x += xinc;
                y += yinc;
            }
            return ret;
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
