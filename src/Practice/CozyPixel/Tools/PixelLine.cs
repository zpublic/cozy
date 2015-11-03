using CozyPixel.Command;
using CozyPixel.Draw;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CozyPixel.Tools
{
    public class PixelLine : PixelToolBase
    {
        public override bool WillModify { get { return true; } }

        public override Keys KeyCode { get { return Keys.L; } }

        private Point BeginPoint { get; set; }

        public PixelLine(IPixelColor holder)
            :base(holder)
        {

        }

        protected override void OnBegin(Point p)
        {
            base.OnBegin(p);
            BeginPoint = p.ToMap(Target.GridWidth);
        }

        protected override void OnMove(Point p)
        {
            base.OnMove(p);

            if(Target != null && ColorHolder != null)
            {
                Target.UpdateDrawable();
                Target.FakeDrawPixel(GenericDraw.Line(BeginPoint, p.ToMap(Target.GridWidth)), ColorHolder.CurrColor);
            }
        }

        protected override bool OnEnd(Point p)
        {
            base.OnEnd(p);

            if (Target != null && ColorHolder != null)
            {
                var command = new DrawPixelCommand()
                {
                    Color   = ColorHolder.CurrColor,
                    Points  = GenericDraw.Line(BeginPoint, p.ToMap(Target.GridWidth)),
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
