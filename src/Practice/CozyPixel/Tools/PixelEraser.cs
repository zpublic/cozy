using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel.Tools
{
    public class PixelEraser : IPixelTool
    {
        public bool WillModify { get { return true; } }

        public Color DrawColor { get; set; }

        private IPixelDrawable Target { get; set; }

        private Point LastPoint { get; set; }

        private List<Point> DrawPoints { get; set; } = new List<Point>();

        public void Begin(IPixelDrawable paint, Point p)
        {
            DrawPoints.Add(p);
            Target      = paint;
            DrawColor   = Target.DefaultDrawColor;
            LastPoint   = p;
        }

        public bool End(Point p)
        {
            if (Target != null)
            {
                DrawPoints.Add(p);
                Target.FakeDrawLine(LastPoint, p, DrawColor);

                LastPoint   = p;
                var ret     = SavePointsToMap();
                Target      = null;
                return ret;
            }
            return false;
        }

        public void Move(Point p)
        {
            if (Target != null)
            {
                DrawPoints.Add(p);
                Target.FakeDrawLine(LastPoint, p, DrawColor);
                LastPoint = p;
            }
        }

        private bool SavePointsToMap()
        {
            if (DrawPoints.Count > 0)
            {
                bool IsModified = false;
                Target.DrawPixel(DrawPoints[0], DrawColor);

                for (int i = 1; i < DrawPoints.Count; ++i)
                {
                    if (Target.DrawLine(DrawPoints[i - 1], DrawPoints[i], DrawColor))
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
