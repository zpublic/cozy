using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyPixel.Tools;

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

        public static bool SavePointsToMap(List<Point> DrawPoints, IPixelDrawable Target, Color c)
        {
            if (DrawPoints.Count > 0)
            {
                bool IsModified = false;
                Target.DrawPixel(DrawPoints[0], c);

                for (int i = 1; i < DrawPoints.Count; ++i)
                {
                    if (Target.DrawLine(DrawPoints[i - 1], DrawPoints[i], c))
                    {
                        IsModified = true;
                    }
                }
                DrawPoints.Clear();
                Target.UpdateDrawable();
                return IsModified;
            }
            return false;
        }
    }
}
