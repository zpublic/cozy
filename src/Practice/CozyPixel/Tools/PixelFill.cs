using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel.Tools
{
    public class PixelFill : IPixelTool
    {
        public IPixelColor ColorHolder { get; set; }

        public bool WillModify { get { return true; } }

        private Point BeginPoint { get; set; }

        private IPixelDrawable Target { get; set; }

        public PixelFill(IPixelColor holder)
        {
            ColorHolder = holder;
        }

        public void Begin(IPixelDrawable paint, Point p)
        {
            Target      = paint;
            BeginPoint  = p;
        }

        public bool End(Point p)
        {
            if(Target != null && ColorHolder != null && p == BeginPoint)
            {
                Target.Fill(p, ColorHolder.CurrColor);
                Target.UpdateDrawable();
            }
            return false;
        }

        public void Move(Point p)
        {

        }
    }
}
