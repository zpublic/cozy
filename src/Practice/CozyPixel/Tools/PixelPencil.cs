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

        private List<Point> DrawPoints { get; set; } = new List<Point>();

        public void Begin(IPixelDrawAble paint, Point p)
        {
            DrawPoints.Add(p);
            Target      = paint;
            LastPoint   = p;
        }

        public void Move(Point p)
        {
            if(Target != null)
            {
                DrawPoints.Add(p);
                Target.FakeDrawLine(LastPoint, p, DrawColor);
                LastPoint = p;
            }
        }

        public bool End(Point p)
        {
            if(Target != null)
            {
                DrawPoints.Add(p);
                Target.FakeDrawLine(LastPoint, p, DrawColor);

                LastPoint   = p;
                var ret     = SavePointsToMap();
                Target      = null;
                return ret;
            }
            return false;
        }

        private bool SavePointsToMap()
        {
            if(DrawPoints.Count > 0)
            {
                bool IsModified = false;
                Target.DrawPixel(DrawPoints[0], DrawColor);

                for (int i = 1; i < DrawPoints.Count; ++i)
                {
                    if (Target.DrawLine(DrawPoints[i - 1], DrawPoints[i], DrawColor))
                    {
                        IsModified = true;
                    }
                }
                DrawPoints.Clear();
                Target.PixelRefresh();
                return IsModified;
            }
            return false;
        }
    }
}
