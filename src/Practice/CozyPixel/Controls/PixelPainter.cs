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
            var mapp = ConvertSceneToMap(p);
            return DrawPixel(mapp, c, ShowGraphics, true);
        }

        public bool DrawLine(Point begin, Point end, Color c)
        {
            var mapBegin = ConvertSceneToMap(begin);
            var mapEnd = ConvertSceneToMap(end);
            return DrawLine(mapBegin, mapEnd, c, ShowGraphics, true);
        }

        public bool FakeDrawPixel(Point p, Color c)
        {
            if (InnerPicBox == null) return false;

            var mapp = ConvertSceneToMap(p);
            using (var g = InnerPicBox.CreateGraphics())
            {
                return DrawPixel(p, c, g, false);
            }
        }

        public bool FakeDrawLine(Point begin, Point end, Color c)
        {
            if (InnerPicBox == null) return false;

            var mapBegin = ConvertSceneToMap(begin);
            var mapEnd = ConvertSceneToMap(end);
            using (var g = InnerPicBox.CreateGraphics())
            {
                return DrawLine(mapBegin, mapEnd, c, g, false);
            }
        }

        public void UpdateDrawable()
        {
            if (InnerPicBox != null)
            {
                InnerPicBox.Refresh();
            }
        }

        public bool TryReadPixel(Point p, out Color c)
        {
            var mapp = ConvertSceneToMap(p);
            if (mapp.X >= 0 && mapp.Y >= 0 && mapp.X < SourceImage.Width && mapp.Y < SourceImage.Height)
            {
                c = SourceImage.GetPixel(mapp.X, mapp.Y);
                return true;
            }

            c = Color.Empty;
            return false;
        }

        public bool Fill(Point p, Color c)
        {
            var mapp = ConvertSceneToMap(p);
            var src = SourceImage.GetPixel(mapp.X, mapp.Y);
            if (src == c)
            {
                return false;
            }

            return SearchAndFillPixel(mapp, src, c) != 0;
        }

        private int SearchAndFillPixel(Point p, Color src, Color dest)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= SourceImage.Width || p.Y >= SourceImage.Height)
            {
                return 0;
            }

            if (SourceImage.GetPixel(p.X, p.Y) != src)
            {
                return 0;
            }

            int count = 1;
            DrawPixel(p, dest, ShowGraphics, true);

            count += SearchAndFillPixel(new Point(p.X, p.Y + 1), src, dest);
            count += SearchAndFillPixel(new Point(p.X, p.Y - 1), src, dest);
            count += SearchAndFillPixel(new Point(p.X + 1, p.Y), src, dest);
            count += SearchAndFillPixel(new Point(p.X - 1, p.Y), src, dest);
            return count;
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

            int n = 0;
            int k = 0;
            int dx = end.X - begin.X;
            int dy = end.Y - begin.Y;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                n = Math.Abs(dx);
            }
            else
            {
                n = Math.Abs(dy);
            }

            float xinc = (float)dx / n;
            float yinc = (float)dy / n;
            float x = begin.X;
            float y = begin.Y;

            bool ret = false;
            for (k = 1; k <= n; k++)
            {
                if (DrawPixel(new Point((int)(x + 0.5f), (int)(y + 0.5f)), c, g, SaveToMap))
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
    }
}
