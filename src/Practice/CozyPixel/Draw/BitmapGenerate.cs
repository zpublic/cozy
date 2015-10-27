using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel.Draw
{
    public class BitmapGenerate
    {
        public static Bitmap Draw(Model.PixelMap pm)
        {
            int w = pm.PixelWidth * pm.data.Width;
            int h = pm.PixelWidth * pm.data.Height;
            if (pm.ShowGrid)
            {
                w += pm.GridWidth * (pm.data.Width + 1);
                h += pm.GridWidth * (pm.data.Height + 1);
            }
            Bitmap b = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(b))
            {
                var GridPen = new Pen(pm.GridColor);

                for (int i = 0; i < pm.data.Width; ++i)
                {
                    int GridX = i * (pm.PixelWidth + pm.GridWidth);

                    for (int j = 0; j < pm.data.Height; ++j)
                    {
                        g.FillRectangle(
                                new SolidBrush(pm.GetPixel(i, j)),
                                pm.GridWidth + GridX,
                                pm.GridWidth + j * (pm.PixelWidth + pm.GridWidth),
                                pm.PixelWidth,
                                pm.PixelWidth);
                    }
                    if(pm.ShowGrid)
                    {
                        g.DrawLine(GridPen, GridX, 0, GridX, h);
                    }
                }
                if(pm.ShowGrid)
                {
                    g.DrawLine(GridPen, pm.data.Width * (pm.PixelWidth + pm.GridWidth), 0, pm.data.Width * (pm.PixelWidth + pm.GridWidth), h);
                    for (int i = 0; i <= pm.data.Height; ++i)
                    {
                        g.DrawLine(GridPen, 0, i * (pm.PixelWidth + pm.GridWidth), w, i * (pm.PixelWidth + pm.GridWidth));
                    }
                }
            }

            return b;
        }
    }
}
