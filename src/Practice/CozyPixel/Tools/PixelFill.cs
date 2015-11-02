using CozyPixel.Draw;
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
            BeginPoint  = Target.ConvertSceneToMap(p); 
        }

        public bool End(Point p)
        {
            var mapp = Target.ConvertSceneToMap(p);

            if (Target != null && ColorHolder != null && mapp == BeginPoint)
            {
                var nps = GenericDraw.GetPointsWithSameColor(Target, mapp, Target.ReadPixel(mapp));
                foreach(var np in nps)
                {
                    Target.DrawPixel(np, ColorHolder.CurrColor);
                }
                Target.UpdateDrawable();
            }
            return false;
        }

        public void Move(Point p)
        {

        }
    }
}
