using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyPixel.Controls;
using System.Drawing;
using CozyPixel.Draw;
using CozyPixel.Command;

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
            Target      = paint;
            var mapp    = Target.ConvertSceneToMap(p);
            LastPoint   = mapp;
            DrawPoints.Add(mapp);
        }

        public void Move(Point p)
        {
            if(Target != null && ColorHolder != null)
            {
                var mapp = Target.ConvertSceneToMap(p);
                DrawPoints.Add(mapp);
                var nps = GenericDraw.Line(LastPoint, mapp);
                foreach (var np in nps)
                {
                    Target.FakeDrawPixel(np, ColorHolder.CurrColor);
                }
                LastPoint = mapp;
            }
        }

        public bool End(Point p)
        {
            var mapp = Target.ConvertSceneToMap(p);
            if(Target != null && ColorHolder != null)
            {
                DrawPoints.Add(mapp);
                LastPoint   = mapp;

                var points = CozyPixelHelper.GetAllPoint(DrawPoints);
                var command = new DrawPixelCommand()
                {
                    Color   = ColorHolder.CurrColor,
                    Points  = points,
                    Target  = Target,
                };
                CommandManager.Instance.Do(command);

                DrawPoints.Clear();
                Target.UpdateDrawable();
                Target      = null;
                return true;
            }
            return false;
        }
    }
}
