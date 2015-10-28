using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyPixel.Model;

namespace CozyPixel.Draw
{
    public class BitmapGenerate
    {
        public static Bitmap Draw(PixelMap pm)
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

                for (int i = 0; i < pm.data.Width; ++i)
                {
                    for (int j = 0; j < pm.data.Height; ++j)
                    {
                        DrawPixel(pm, g, i, j, pm.GetPixel(i, j));
                    }
                }
                if (pm.ShowGrid)
                {
                    DrawGrid(pm, g);
                }
            }

            return b;
        }

        public static void DrawGrid(PixelMap pm, Graphics g)
        {
            int x = pm.PixelWidth + pm.GridWidth;
            var w = pm.data.Width * x;
            var h = pm.data.Height * x;

            var GridPen     = new Pen(pm.GridColor, pm.GridWidth);
            int BlockWidth  = pm.PixelWidth + pm.GridWidth;

            for (int i = 0; i <= pm.data.Width; ++i)
            {
                g.DrawLine(GridPen, 0, i * BlockWidth, w, i * BlockWidth);
                g.DrawLine(GridPen, i * BlockWidth, 0, i * BlockWidth, h);
            }
        }

        public static void DrawPixel(PixelMap pm, Graphics g, int x, int y, Color c)
        {
            var brush = new SolidBrush(c);
            if (pm.ShowGrid)
            {
                g.FillRectangle(
                    brush,
                    pm.GridWidth / 2.0f + x * (pm.PixelWidth + pm.GridWidth),
                    pm.GridWidth / 2.0f + y * (pm.PixelWidth + pm.GridWidth),
                    pm.PixelWidth,
                    pm.PixelWidth);
            }
            else
            {
                g.FillRectangle(
                    brush,
                    x * pm.PixelWidth,
                    y * pm.PixelWidth,
                    pm.PixelWidth,
                    pm.PixelWidth);
            }
        }
    }
}
