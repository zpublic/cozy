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

        public override Keys KeyCode { get { return Keys.E; } }

        private Point LastPoint { get; set; }

        private List<Point> DrawPoints { get; set; } = new List<Point>();

        public PixelEraser()
            : base(null)
        {

        }

        protected override void OnBegin(Point p)
        {
            base.OnBegin(p);

            LastPoint = p.ToMap(Target.GridWidth);
            DrawPoints.Add(LastPoint);
        }

        protected override void OnMove(Point p)
        {
            base.OnMove(p);

            if (Target != null)
            {
                var old_last    = LastPoint;
                LastPoint       = p.ToMap(Target.GridWidth);
                DrawPoints.Add(LastPoint);

                Target.FakeDrawPixel(GenericDraw.Line(old_last, LastPoint), Target.DefaultDrawColor);
            }
        }

        protected override bool OnEnd(Point p)
        {
            base.OnEnd(p);

            if (Target != null)
            {
                DrawPoints.Add(p.ToMap(Target.GridWidth));

                var command = new DrawPixelCommand()
                {
                    Color   = Target.DefaultDrawColor,
                    Points  = CozyPixelHelper.GetAllPoint(DrawPoints),
                    Target  = Target,
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
