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

        public Graphics ShowGraphics { get; set; }

        public void Save(string filename)
        {
            if(SourceImage != null)
            {
                SourceImage.data.Save(filename);
            }
        }

        public void DrawPixel(Point p, Color c)
        {
            if (SourceImage != null)
            {
                var b = new SolidBrush(c);
                int w = SourceImage.PixelWidth;

                if (SourceImage.ShowGrid)
                {
                    w += SourceImage.GridWidth;
                }

                int x = p.X / w;
                int y = p.Y / w;

                if (x < SourceImage.data.Width && y < SourceImage.data.Height)
                {
                    SourceImage.SetPixel(x, y, c);
                    BitmapGenerate.DrawPixel(SourceImage, ShowGraphics, x, y, c);
                }
            }
        }

        public void RefreshGrid()
        {
            if (SourceImage != null && SourceImage.ShowGrid)
            {
                BitmapGenerate.DrawGrid(SourceImage, ShowGraphics);
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
                ShowGraphics = CreateGraphics();
            }
        }
    }
}
