using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyPixel.Draw;

namespace CozyPixel.Tool
{
    public class PixelStraw : PixelToolBase
    {
        public override bool WillModify { get { return false; } }

        public PixelStraw(Color holder)
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
            if (Target != null && Target.IsReady)
            {
                ColorHolder = Target.ReadPixel(p.ToMap(Target.GridWidth));
            }
        }
    }
}
