using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Infrastructure.Template
{
    [Flags]
    public enum TextAlignType
    {
        Center      = 0,
        Left        = 1,
        Right       = 2,
        Top         = 4,
        Bottom      = 8,
        LeftTop     = Left | Top,
        LeftBottom  = Left | Bottom,
        RightTop    = Right | Top,
        RightBottom = Right | Bottom,
    }

    public struct MarginInfo
    {
        public MarginInfo(int left)
        {
            Bottom = Top = Right = Left = left;
        }

        public MarginInfo(int left, int top)
        {
            Right = Left = left;
            Bottom = Top = top;
        }

        public MarginInfo(int left, int right, int top, int bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }
        public int Left { get; set; }
        public int Right { get; set; }
        public int Top { get; set; }
        public int Bottom { get; set; }
    }

    public class TextInfo
    {
        public string Text { get; set; }
        public string Font { get; set; }
        public int TextSize { get; set; }
        public TextAlignType TextAlign { get; set; }
        public MarginInfo Margin { get; set; }
    }
}
