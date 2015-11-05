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
    public class PixelEraser : PixelToolBase
    {
        public override bool WillModify { get { return true; } }

        public override Keys KeyCode { get { return Keys.D3; } }

        public int Width { get; set; } = 2;

        private Point LastPoint { get; set; }

        private Dictionary<Point, Color> FakeDrawPoints { get; set; } = new Dictionary<Point, Color>();

        private List<Point> DrawPoints { get; set; } = new List<Point>();

        public PixelEraser()
            : base(null)
        {

        }

        protected override void OnBegin(Point p)
        {
            base.OnBegin(p);
            FakeDrawPoints.Clear();

            if (Target != null && Target.IsReady)
            {
                LastPoint = p.ToMap(Target.GridWidth);
                DrawPoints.Add(LastPoint);
            }
        }

        protected override void OnMove(Point p)
        {
            base.OnMove(p);

            if (Target != null && Target.IsReady)
            {
                var old_last = LastPoint;
                LastPoint = p.ToMap(Target.GridWidth);
                DrawPoints.Add(LastPoint);

                GenericDraw.Line(old_last, LastPoint, Target.DefaultDrawColor, FakeDrawPoints);
                Target.FakeDrawPixel(FakeDrawPoints.GetDistributionColor(Target, Width));
            }
        }

        protected override bool OnEnd(Point p)
        {
            base.OnEnd(p);

            if (Target != null && Target.IsReady)
            {
                DrawPoints.Add(p.ToMap(Target.GridWidth));

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

        protected override void OnExit()
        {
            base.OnExit();
            DrawPoints.Clear();
        }
    }
}
