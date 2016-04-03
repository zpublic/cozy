using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CozyLauncher.Plugin.Guide.Template.Info.Model
{
    public static class ExtFunction
    {
        public static Tuple<HorizontalAlignment, VerticalAlignment> ToAlignment(this TextAlignType align)
        {
            var hori    = HorizontalAlignment.Left;
            var vert    = VerticalAlignment.Top;

            if ((align & TextAlignType.Left) != 0)
            {
                hori = HorizontalAlignment.Left;
            }
            else if((align & TextAlignType.Right) != 0)
            {
                hori = HorizontalAlignment.Right;
            }
            else
            {
                hori = HorizontalAlignment.Center;
            }

            if((align & TextAlignType.Top) != 0)
            {
                vert = VerticalAlignment.Top;
            }
            else if((align & TextAlignType.Bottom) != 0)
            {
                vert = VerticalAlignment.Bottom;
            }
            else
            {
                vert = VerticalAlignment.Center;
            }

            return Tuple.Create(hori, vert);
        }
    }
}
