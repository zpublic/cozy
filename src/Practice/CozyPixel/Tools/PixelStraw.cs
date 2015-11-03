using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyPixel.Draw;
using System.Windows.Forms;

namespace CozyPixel.Tools
{
    public class PixelStraw : PixelToolBase
    {
        public override bool WillModify { get { return false; } }

        public override Keys KeyCode { get { return Keys.D4; } }

        public PixelStraw(IPixelColor holder)
            :base(holder)
        {

        }

        protected override void OnBegin(Point p)
        {
            base.OnBegin(p);
            ReadColor(p);
        }

        protected override bool OnEnd(Point p)
        {
            base.OnEnd(p);
            ReadColor(p);
            return true;
        }

        protected override void OnMove(Point p)
        {
            base.OnMove(p);
            ReadColor(p);
        }

        private void ReadColor(Point p)
        {
            if (Target != null && ColorHolder != null)
            {
                ColorHolder.CurrColor = Target.ReadPixel(p.ToMap(Target.GridWidth));
            }
        }
    }
}
