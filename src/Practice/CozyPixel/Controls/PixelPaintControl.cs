using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CozyPixel.Model;
using CozyPixel.Draw;

namespace CozyPixel.Controls
{
    public class PixelPaintControl : PictureBox
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

                if (ShowGraphics != null)
                {
                    ShowGraphics.Dispose();
                }

                if (value != null)
                {
                    Image = BitmapGenerate.Draw(value);
                    ShowGraphics = CreateGraphics();
                }
            }
        }

        public Graphics ShowGraphics { get; set; }

        public void Save(string filename)
        {
            SourceImage.data.Save(filename);
        }

        public void DrawPixel(Point p, Color c)
        {
            if (SourceImage != null)
            {
                var b = new SolidBrush(c);
                int w = 0;

                if (SourceImage.ShowGrid)
                {
                    w = (SourceImage.PixelWidth + SourceImage.GridWidth);
                }
                else
                {
                    w = SourceImage.PixelWidth;
                }

                int x = p.X / w;
                int y = p.Y / w;

                if (x < SourceImage.data.Width && y < SourceImage.data.Height)
                {
                    SourceImage.SetPixel(x, y, c);

                    int fx = x * w;
                    int fy = y * w;
                    fx = Math.Min(fx, Image.Width);
                    fy = Math.Min(fy, Image.Height);
                    ShowGraphics.FillRectangle(b, fx, fy, w, w);
                }
            }
        }
    }
}
