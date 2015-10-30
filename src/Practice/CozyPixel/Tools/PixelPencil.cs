using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyPixel.Controls;
using System.Drawing;

namespace CozyPixel.Tools
{
    public class PixelPencil : IPixelTool
    {
        public bool WillModify { get { return true; } }

        private IPixelDrawable Target { get; set; }

        private Point LastPoint { get; set; }

        private List<Point> DrawPoints { get; set; } = new List<Point>();

        public IPixelColor ColorHolder { get; set; }

        public PixelPencil(IPixelColor holder)
        {
            ColorHolder = holder;
        }

        public void Begin(IPixelDrawable paint, Point p)
        {
            DrawPoints.Add(p);
            Target      = paint;
            LastPoint   = p;
        }

        public void Move(Point p)
        {
            if(Target != null && ColorHolder != null)
            {
                DrawPoints.Add(p);
                Target.FakeDrawLine(LastPoint, p, ColorHolder.CurrColor);
                LastPoint = p;
            }
        }

        public bool End(Point p)
        {
            if(Target != null && ColorHolder != null)
            {
                DrawPoints.Add(p);
                Target.FakeDrawLine(LastPoint, p, ColorHolder.CurrColor);

                LastPoint   = p;
                var ret     = CozyPixelHelper.SavePointsToMap(DrawPoints, Target, ColorHolder.CurrColor);
                Target      = null;
                return ret;
            }
            return false;
        }
    }
}
