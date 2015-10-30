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

        public IPixelColor ColorHolder { get; set; }

        private IPixelDrawable Target { get; set; }

        private Point LastPoint { get; set; }

        private List<Point> DrawPoints { get; set; } = new List<Point>();

        private Color EraseColor { get; set; }

        public void Begin(IPixelDrawable paint, Point p)
        {
            DrawPoints.Add(p);
            Target      = paint;
            EraseColor  = Target.DefaultDrawColor;
            LastPoint   = p;
        }

        public bool End(Point p)
        {
            if (Target != null)
            {
                DrawPoints.Add(p);
                Target.FakeDrawLine(LastPoint, p, EraseColor);

                LastPoint   = p;
                var ret     = CozyPixelHelper.SavePointsToMap(DrawPoints, Target, EraseColor);
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
                Target.FakeDrawLine(LastPoint, p, EraseColor);
                LastPoint = p;
            }
        }
    }
}
