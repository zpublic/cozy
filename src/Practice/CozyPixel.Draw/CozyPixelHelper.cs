using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyPixel.Tools;
using CozyPixel.Draw;

namespace CozyPixel.Draw
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

        public static IEnumerable<KeyValuePair<Point, Color>> GetAllPoint(List<Point> DrawPoints, Color c)
        {
            Dictionary<Point, Color> NeetDraw = new Dictionary<Point, Color>();

            if (DrawPoints.Count > 0)
            {
                NeetDraw[DrawPoints[0]] = c;

                for (int i = 1; i < DrawPoints.Count; ++i)
                {
                    GenericDraw.Line(DrawPoints[i - 1], DrawPoints[i], c, NeetDraw);
                }
            }
            return NeetDraw;
        }

        public static Point ToMap(this Point p, int w)
        {
            return new Point(p.X / w, p.Y / w);
        }

        public static Point ToScreen(this Point p, int w)
        {
            return new Point(p.X * w, p.Y * w);
        }

        public static void FakeDrawPixel(this IPixelDrawable target, IEnumerable<KeyValuePair<Point, Color>> points)
        {
            if(target != null && points != null && target.IsReady)
            {
                foreach(var obj in points)
                {
                    target.FakeDrawPixel(obj.Key, obj.Value);
                }
            }
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
