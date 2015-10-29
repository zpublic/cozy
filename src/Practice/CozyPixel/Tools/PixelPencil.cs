using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyPixel.Controls;
using System.Drawing;

namespace CozyPixel.Tools
{
    public class PixelPencil : IPixelTool
    {
        public bool WillModify { get { return true; } }

        public Color DrawColor { get; set; }

        private IPixelDrawAble Target { get; set; }

        private Point LastPoint { get; set; }

        public void Begin(IPixelDrawAble paint, Point p)
        {
            Target      = paint;
            LastPoint   = p;
        }

        public bool Move(Point p)
        {
            if(Target != null)
            {
                bool r = Target.DrawLine(LastPoint, p, DrawColor);
                LastPoint = p;
                return r;
            }
            return false;
        }

        public bool End(Point p)
        {
            if(Target != null)
            {
                bool r      = Target.DrawLine(LastPoint, p, DrawColor);
                LastPoint   = p;
                Target      = null;
                return r;
            }
            return false;
        }
    }
}
