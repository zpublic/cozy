using CozyPixel.Draw;
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
            var mapp    = Target.ConvertSceneToMap(p);
            BeginPoint  = mapp;
        }

        public void Move(Point p)
        {
            if(Target != null && ColorHolder != null)
            {
                var mapp = Target.ConvertSceneToMap(p);

                Target.UpdateDrawable();
                var nps = GenericDraw.Line(BeginPoint, mapp);
                foreach(var np in nps)
                {
                    Target.FakeDrawPixel(np, ColorHolder.CurrColor);
                }
            }
        }

        public bool End(Point p)
        {
            if (Target != null && ColorHolder != null)
            {
                var mapp = Target.ConvertSceneToMap(p);

                var nps = GenericDraw.Line(BeginPoint, mapp);
                foreach (var np in nps)
                {
                    Target.DrawPixel(np, ColorHolder.CurrColor);
                }
                Target.UpdateDrawable();
                Target = null;
                return true;
            }
            return false;
        }
    }
}
