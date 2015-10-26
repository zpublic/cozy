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
            for (int i = 0; i < pm.data.Width; ++i)
            {
                DrawVerticalLine(b, i * (pm.PixelWidth + pm.GridWidth), pm.GridColor);
                for (int j = 0; j < pm.data.Height; ++j)
                {
                    DrawRectangle(
                        b,
                        new Point(
                            pm.GridWidth + i * (pm.PixelWidth + pm.GridWidth),
                            pm.GridWidth + j * (pm.PixelWidth + pm.GridWidth)),
                        new Point(
                            pm.GridWidth + pm.PixelWidth + i * (pm.PixelWidth + pm.GridWidth),
                            pm.GridWidth + pm.PixelWidth + j * (pm.PixelWidth + pm.GridWidth)),
                        pm.GetPixel(i, j));
                }
            }
            DrawVerticalLine(b, pm.data.Width * (pm.PixelWidth + pm.GridWidth), pm.GridColor);
            for (int i = 0; i <= pm.data.Height; ++i)
            {
                DrawHorizonLine(b, i * (pm.PixelWidth + pm.GridWidth), pm.GridColor);
            }
            return b;
        }

        public static void DrawHorizonLine(Bitmap b, int y, Color c)
        {
            for (int i = 0; i < b.Width - 1; ++i)
            {
                b.SetPixel(i, y, c);
            }
        }

        public static void DrawVerticalLine(Bitmap b, int x, Color c)
        {
            for (int i = 0; i < b.Height - 1; ++i)
            {
                b.SetPixel(x, i, c);
            }
        }

        public static void DrawRectangle(Bitmap b, Point leftUp, Point rightDown, Color c)
        {
            for (int x = leftUp.X; x < rightDown.X; ++x)
            {
                for (int y = leftUp.Y; y < rightDown.Y; ++y)
                {
                    b.SetPixel(x, y, c);
                }
            }
        }
    }
}
