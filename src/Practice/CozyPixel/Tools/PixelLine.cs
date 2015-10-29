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
        public Color DrawColor { get; set; }

        public bool WillModify { get { return true; } }

        private IPixelDrawable Target { get; set; }

        private Point BeginPoint { get; set; }

        public void Begin(IPixelDrawable paint, Point p)
        {
            Target = paint;
            BeginPoint = p;
        }

        public void Move(Point p)
        {
            if(Target != null)
            {
                Target.UpdateDrawable();
                Target.FakeDrawLine(BeginPoint, p, DrawColor);
            }
        }

        public bool End(Point p)
        {
            Target.FakeDrawLine(BeginPoint, p, DrawColor);

            bool ret = Target.DrawLine(BeginPoint, p, DrawColor);
            Target.UpdateDrawable();
            Target = null;
            return ret;
        }
    }
}
