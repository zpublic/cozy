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
            var c                   = Color.Empty;

            if(Target.TryReadPixel(p, out c))
            {
                ColorHolder.CurrColor = c;
            }
        }

        public bool End(Point p)
        {
            var c = Color.Empty;

            if (Target != null && ColorHolder != null && Target.TryReadPixel(p, out c))
            {
                ColorHolder.CurrColor   = c;
                Target                  = null;
            }
            return true;
        }

        public void Move(Point p)
        {
            var c = Color.Empty;

            if (Target != null && ColorHolder != null && Target.TryReadPixel(p, out c))
            {
                ColorHolder.CurrColor   = c;
            }
        }
    }
}
