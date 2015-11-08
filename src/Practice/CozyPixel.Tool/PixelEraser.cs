using CozyPixel.Helper;
using System.Drawing;
using CozyPixel.Command;
using CozyPixel.Draw;
using CozyPixel.Interface;

namespace CozyPixel.Tool
{
    public class PixelEraser : PixelToolDraggable
    {
        public override bool WillModify { get { return true; } }

        public int Width { get; set; } = 2;

        protected override void OnMove(Point p)
        {
            base.OnMove(p);

            if (Target != null && Target.IsReady)
            {
                var old_last = LastPoint;
                LastPoint = p.ToMap(Target.GridWidth);

                GenericDraw.Line(old_last, LastPoint, Target.DefaultDrawColor, FakeDrawPoints);
                Target.FakeDrawPixel(FakeDrawPoints.GetDistributionColor(Target, Width));
            }
        }

        protected override bool OnEnd(Point p)
        {
            base.OnEnd(p);

            if (Target != null && Target.IsReady)
            {
                var points = GenericDraw.GetAllPoint(DrawPoints, Target.DefaultDrawColor);
                var command = new DrawPixelCommand()
                {
                    Points = points.GetDistributionColor(Target, Width),
                    Target = Target,
                };
                CommandManager.Instance.Do(command);

                Target.UpdateDrawable();
                return true;
            }
            return false;
        }
    }
}
