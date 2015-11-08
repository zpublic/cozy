using CozyPixel.Interface;
using System.Collections.Generic;
using System.Drawing;

namespace CozyPixel.Tool
{
    public static class PixelDrawableHelper
    {
        public static void FakeDrawPixel(this IPixelDrawable target, IEnumerable<KeyValuePair<Point, Color>> points)
        {
            if (target != null && points != null && target.IsReady)
            {
                foreach (var obj in points)
                {
                    target.FakeDrawPixel(obj.Key, obj.Value);
                }
            }
        }
    }
}
