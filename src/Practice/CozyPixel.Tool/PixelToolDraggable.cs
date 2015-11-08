using CozyPixel.Draw;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel.Tool
{
    public abstract class PixelToolDraggable : PixelToolBase
    {
        protected Point LastPoint { get; set; }

        protected Dictionary<Point, Color> FakeDrawPoints { get; set; } = new Dictionary<Point, Color>();

        protected List<Point> DrawPoints { get; set; } = new List<Point>();

        public PixelToolDraggable()
        {

        }

        public PixelToolDraggable(Color holder)
            :base(holder)
        {

        }

        protected override void OnBegin(Point p)
        {
            base.OnBegin(p);

            if (Target.IsReady)
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
                DrawPoints.Add(p.ToMap(Target.GridWidth));
            }
        }

        protected override bool OnEnd(Point p)
        {
            base.OnEnd(p);
            if (Target != null && Target.IsReady)
            {
                DrawPoints.Add(p.ToMap(Target.GridWidth));
            }
            return false;
        }

        protected override void OnExit()
        {
            base.OnExit();
            DrawPoints.Clear();
            FakeDrawPoints.Clear();
        }
    }
}
