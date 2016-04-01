using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Plugin.Guide.Template.Info.Model
{
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
}
