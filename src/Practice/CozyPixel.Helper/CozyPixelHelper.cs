using System;
using System.Collections.Generic;
using System.Drawing;

namespace CozyPixel.Helper
{
    public static class CozyPixelHelper
    {
        public static Bitmap ReadBitmapFromFile(string filename)
        {
            Bitmap res = null;
            using (var bmp = new Bitmap(filename))
            {
                res = new Bitmap(bmp);
            }
            return res;
        }

        public static Point ToMap(this Point p, int w)
        {
            return new Point(p.X / w, p.Y / w);
        }

        public static Point ToScreen(this Point p, int w)
        {
            return new Point(p.X * w, p.Y * w);
        }

        public static Color Blend(Color Dest, Color source, double alpha)
        {
            return Color.FromArgb(
                (int)(Dest.A * alpha + source.A * (1 - alpha)),
                (int)(Dest.R * alpha + source.R * (1 - alpha)),
                (int)(Dest.G * alpha + source.G * (1 - alpha)),
                (int)(Dest.B * alpha + source.B * (1 - alpha)));
        }

        public static double Length(this Point p, Point o)
        {
            return Math.Sqrt((p.X - o.X) * (p.X - o.X) + (p.Y - o.Y) * (p.Y - o.Y));
        }

        public static double GaussianWeight(double length)
        {
            return 42;
        }

        public static double LinearWeight(double length, double width)
        {
            return 1 - (length / width);
        }
    }
}
