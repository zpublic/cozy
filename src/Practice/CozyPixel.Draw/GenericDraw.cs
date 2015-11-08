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
        public static void Line(Point begin, Point end, Color c, Dictionary<Point, Color> result)
        {
            result[end] = c;

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
                result[new Point((int)(x + 0.5f), (int)(y + 0.5f))] = c;

                x += xinc;
                y += yinc;
            }
        }

        public static IEnumerable<KeyValuePair<Point, Color>> GetPointsWithSameColor(IPixelDrawable d, Point begin, Color c)
        {
            Dictionary<Point, Color> visited = new Dictionary<Point, Color>();
            SearchPointsWithColor(d, begin, c, visited);
            return visited;
        }

        private static int SearchPointsWithColor(IPixelDrawable d, Point p, Color c, Dictionary<Point, Color> visited)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= d.PixelSize.Width || p.Y >= d.PixelSize.Height || d.ReadPixel(p) != c)
            {
                return 0;
            }

            if (visited.ContainsKey(p))
            {
                return 0;
            }

            int count = 1;
            visited[p] = c;

            count += SearchPointsWithColor(d, new Point(p.X, p.Y + 1), c, visited);
            count += SearchPointsWithColor(d, new Point(p.X, p.Y - 1), c, visited);
            count += SearchPointsWithColor(d, new Point(p.X + 1, p.Y), c, visited);
            count += SearchPointsWithColor(d, new Point(p.X - 1, p.Y), c, visited);
            return count;
        }

        public static IEnumerable<KeyValuePair<Point, Color>> GetDistributionColor(this IEnumerable<KeyValuePair<Point, Color>> points, IPixelDrawable d, int width)
        {
            Dictionary<Point, Color> result = new Dictionary<Point, Color>();
            foreach(var obj in points)
            {
                if(obj.Key.X >= 0 && obj.Key.X <= d.PixelSize.Width && obj.Key.Y >= 0 && obj.Key.Y <= d.PixelSize.Height)
                {
                    int l = (int)Math.Ceiling((width - 1) / 2.0);
                    for(int i = obj.Key.X - l; i <= obj.Key.X + l; ++i)
                    {
                        for (int j = obj.Key.Y - l; j <= obj.Key.Y + l; ++j)
                        {
                            var p       = new Point(i, j);
                            var old_c   = d.ReadPixel(p);
                            if (result.ContainsKey(p))
                            {
                                old_c = result[p];
                            }

                            result[p] = CozyPixelHelper.Blend(obj.Value, old_c, CozyPixelHelper.LinearWeight(obj.Key.Length(p), width)); ;
                        }
                    }
                }
            }
            return result;
        }
    }
}
