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
    }
}
