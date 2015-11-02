using CozyPixel.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel.Draw
{
    public static class GenericDraw
    {
        public static IEnumerable<Point> Line(Point begin, Point end)
        {
            var result = new HashSet<Point>();
            result.Add(end);

            int n   = 0;
            int k   = 0;
            int dx  = end.X - begin.X;
            int dy  = end.Y - begin.Y;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                n = Math.Abs(dx);
            }
            else
            {
                n = Math.Abs(dy);
            }

            float xinc  = (float)dx / n;
            float yinc  = (float)dy / n;
            float x     = begin.X;
            float y     = begin.Y;

            for (k = 1; k <= n; k++)
            {
                result.Add(new Point((int)(x + 0.5f), (int)(y + 0.5f)));

                x += xinc;
                y += yinc;
            }
            return result;
        }

        public static IEnumerable<Point> GetPointsWithSameColor(IPixelDrawable d, Point begin, Color c)
        {
            HashSet<Point> visited = new HashSet<Point>();
            SearchPointsWithColor(d, begin, c, visited);
            return visited;
        }

        private static int SearchPointsWithColor(IPixelDrawable d, Point p, Color c, HashSet<Point> visited)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= d.PixelSize.Width || p.Y >= d.PixelSize.Height || d.ReadPixel(p) != c)
            {
                return 0;
            }

            if (visited.Contains(p))
            {
                return 0;
            }

            int count = 1;
            visited.Add(p);
            count += SearchPointsWithColor(d, new Point(p.X, p.Y + 1), c, visited);
            count += SearchPointsWithColor(d, new Point(p.X, p.Y - 1), c, visited);
            count += SearchPointsWithColor(d, new Point(p.X + 1, p.Y), c, visited);
            count += SearchPointsWithColor(d, new Point(p.X - 1, p.Y), c, visited);
            return count;
        }
    }
}
