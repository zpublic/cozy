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
    public class PixelFill : PixelToolBase
    {
        public override bool WillModify { get { return true; } }

        public override Keys KeyCode { get { return Keys.F; } }

        public PixelFill(IPixelColor holder)
            :base(holder)
        {

        }

        protected override bool OnEnd(Point p)
        {
            base.OnEnd(p);

            if (Target != null && ColorHolder != null)
            {
                var mapp = p.ToMap(Target.GridWidth);

                var command = new DrawPixelCommand()
                {
                    Color   = ColorHolder.CurrColor,
                    Points  = GenericDraw.GetPointsWithSameColor(Target, mapp, Target.ReadPixel(mapp)),
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
