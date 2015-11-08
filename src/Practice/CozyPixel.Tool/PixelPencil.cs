using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CozyPixel.Draw;
using CozyPixel.Command;

namespace CozyPixel.Tool
{
    public class PixelPencil : PixelToolDraggable
    {
        public override bool WillModify { get { return true; } }

        public int Width { get; set; } = 2;

        public PixelPencil(Color holder)
            :base(holder)
        {
        }

        protected override void OnMove(Point p)
        {
            base.OnMove(p);

            if (Target != null && ColorHolder != null && Target.IsReady)
            {
                var old_last    = LastPoint;
                LastPoint       = p.ToMap(Target.GridWidth);

                GenericDraw.Line(old_last, LastPoint, ColorHolder, FakeDrawPoints);
                Target.FakeDrawPixel(FakeDrawPoints.GetDistributionColor(Target, Width));
            }
        }

        protected override bool OnEnd(Point p)
        {
            base.OnEnd(p);

            if (Target != null && ColorHolder != null && Target.IsReady)
            {
                var points = CozyPixelHelper.GetAllPoint(DrawPoints, ColorHolder);
                var command = new DrawPixelCommand()
                {
                    Points  = points.GetDistributionColor(Target, Width),
                    Target  = Target,
                };
                CommandManager.Instance.Do(command);

                Target.UpdateDrawable();
                return true;
            }
            return false;
        }
    }
}
