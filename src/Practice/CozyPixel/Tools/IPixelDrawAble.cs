using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel.Tools
{
    public interface IPixelDrawAble
    {
        bool DrawPixel(Point p, Color c);

        bool DrawLine(Point start, Point end, Color c);
    }
}
