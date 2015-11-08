using System.Drawing;
using CozyPixel.Model;

namespace CozyPixel.Draw
{
    public class BitmapGenerate
    {
        public static Bitmap Draw(PixelArtObject pm)
        {
            int w = pm.PixelWidth * pm.Width;
            int h = pm.PixelWidth * pm.Height;
            if (pm.ShowGrid)
            {
                w += pm.GridWidth * (pm.Width + 1);
                h += pm.GridWidth * (pm.Height + 1);
            }

            Bitmap b = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.Clear(pm.BackColor);

                for (int i = 0; i < pm.Width; ++i)
                {
                    for (int j = 0; j < pm.Height; ++j)
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

        public static void DrawGrid(PixelArtObject pm, Graphics g)
        {
            int x = pm.PixelWidth + pm.GridWidth;
            var w = pm.Width * x;
            var h = pm.Height * x;

            var ePen        = new Pen(pm.BackColor, pm.GridWidth);
            var GridPen     = new Pen(pm.GridColor, pm.GridWidth);
            int BlockWidth  = pm.PixelWidth + pm.GridWidth;

            for (int i = 0; i <= pm.Width; ++i)
            {
                g.DrawLine(ePen, 0, i * BlockWidth, w, i * BlockWidth);
                g.DrawLine(ePen, i * BlockWidth, 0, i * BlockWidth, h);
                g.DrawLine(GridPen, 0, i * BlockWidth, w, i * BlockWidth);
                g.DrawLine(GridPen, i * BlockWidth, 0, i * BlockWidth, h);
            }
        }

        public static void DrawPixel(PixelArtObject pm, Graphics g, int x, int y, Color c)
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
