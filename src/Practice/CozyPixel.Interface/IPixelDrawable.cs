using System.Drawing;

namespace CozyPixel.Interface
{
    public interface IPixelDrawable
    {
        Color DefaultDrawColor { get; set; }

        Size PixelSize { get; }

        bool IsReady { get; }

        void DrawPixel(Point p, Color c);

        void FakeDrawPixel(Point p, Color c);

        void UpdateDrawable();

        Color ReadPixel(Point p);
    }
}
