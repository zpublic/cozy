using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyPixel.Tools;
using CozyPixel.Draw;

namespace CozyPixel
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

        public static IEnumerable<Point> GetAllPoint(List<Point> DrawPoints)
        {
            HashSet<Point> NeetDraw = new HashSet<Point>();

            if (DrawPoints.Count > 0)
            {
                NeetDraw.Add(DrawPoints[0]);

                for (int i = 1; i < DrawPoints.Count; ++i)
                {
                    var ps = GenericDraw.Line(DrawPoints[i - 1], DrawPoints[i]);
                    foreach(var p in ps)
                    {
                        NeetDraw.Add(p);
                    }
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

        public static void FakeDrawPixel(this IPixelGridDrawable target, IEnumerable<Point> points, Color c)
        {
            if(target != null && points != null)
            {
                foreach(var obj in points)
                {
                    target.FakeDrawPixel(obj, c);
                }
            }
        }
    }
}
