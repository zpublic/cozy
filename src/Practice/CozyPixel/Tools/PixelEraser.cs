using CozyPixel.Draw;
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
            Target      = paint;
            var mapp    = Target.ConvertSceneToMap(p);
            EraseColor  = Target.DefaultDrawColor;
            LastPoint   = mapp;
            DrawPoints.Add(mapp);
        }

        public bool End(Point p)
        {
            if (Target != null)
            {
                var mapp = Target.ConvertSceneToMap(p);
                DrawPoints.Add(mapp);
                LastPoint   = mapp;
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
                var mapp = Target.ConvertSceneToMap(p);
                DrawPoints.Add(mapp);
                var nps = GenericDraw.Line(LastPoint, mapp);
                foreach (var np in nps)
                {
                    Target.FakeDrawPixel(np, EraseColor);
                }
                LastPoint = mapp;
            }
        }
    }
}
