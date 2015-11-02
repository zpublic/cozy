using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyPixel.Model;

namespace CozyPixel.Draw
{
    public interface IPixelDrawable
    {
        Color DefaultDrawColor { get; set; }

        Size PixelSize { get; }

        void DrawPixel(Point p, Color c);

        void FakeDrawPixel(Point p, Color c);

        void UpdateDrawable();

        Color ReadPixel(Point p);
    }
}
