using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel.Tools
{
    public class PixelStraw : IPixelTool
    {
        public IPixelColor ColorHolder { get; set; }

        public bool WillModify { get { return false; } }

        private IPixelDrawable Target { get; set; }

        public PixelStraw(IPixelColor holder)
        {
            ColorHolder = holder;
        }

        public void Begin(IPixelDrawable paint, Point p)
        {
            Target                  = paint;
            ColorHolder.CurrColor   = Target.ReadPixel(p);
        }

        public bool End(Point p)
        {
            if (Target != null && ColorHolder != null)
            {
                ColorHolder.CurrColor = Target.ReadPixel(p);
                Target = null;
            }
            return true;
        }

        public void Move(Point p)
        {
            if (Target != null && ColorHolder != null)
            {
                ColorHolder.CurrColor = Target.ReadPixel(p);
            }
        }
    }
}
