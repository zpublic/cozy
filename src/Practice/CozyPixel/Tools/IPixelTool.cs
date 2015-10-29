using CozyPixel.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel.Tools
{
    public interface IPixelTool
    {
        Color DrawColor { get; set; }

        bool WillModify { get; }

        void Begin(IPixelDrawAble paint, Point p);

        void Move(Point p);

        bool End(Point p);
    }
}
