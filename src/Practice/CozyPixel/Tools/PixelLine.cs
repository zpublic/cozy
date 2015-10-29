using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel.Tools
{
    public class PixelLine : IPixelTool
    {
        public bool WillModify { get { return true; } }

        private IPixelDrawable Target { get; set; }

        private Point BeginPoint { get; set; }

        public IPixelColor ColorHolder { get; set; }

        public PixelLine(IPixelColor holder)
        {
            ColorHolder = holder;
        }

        public void Begin(IPixelDrawable paint, Point p)
        {
            Target      = paint;
            BeginPoint  = p;
        }

        public void Move(Point p)
        {
            if(Target != null && ColorHolder != null)
            {
                Target.UpdateDrawable();
                Target.FakeDrawLine(BeginPoint, p, ColorHolder.CurrColor);
            }
        }

        public bool End(Point p)
        {
            if (Target != null && ColorHolder != null)
            {
                Target.FakeDrawLine(BeginPoint, p, ColorHolder.CurrColor);

                bool ret = Target.DrawLine(BeginPoint, p, ColorHolder.CurrColor);
                Target.UpdateDrawable();
                Target = null;
                return ret;
            }
            return false;
        }
    }
}
