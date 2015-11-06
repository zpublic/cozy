using CozyPixel.Draw;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyPixel.Command;
using System.Windows.Forms;

namespace CozyPixel.Tools
{
    public class PixelEraser : DragPixelTool
    {
        public override bool WillModify { get { return true; } }

        public override Keys KeyCode { get { return Keys.D3; } }

        public int Width { get; set; } = 2;

        public PixelEraser()
            : base(null)
        {

        }

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
                var points = CozyPixelHelper.GetAllPoint(DrawPoints, Target.DefaultDrawColor);
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
