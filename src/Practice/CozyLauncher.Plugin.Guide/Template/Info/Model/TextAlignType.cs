using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Plugin.Guide.Template.Info.Model
{
    [Flags]
    public enum TextAlignType
    {
        Center = 0,
        Left = 1,
        Right = 2,
        Top = 4,
        Bottom = 8,
        LeftTop = Left | Top,
        LeftBottom = Left | Bottom,
        RightTop = Right | Top,
        RightBottom = Right | Bottom,
    }
}
