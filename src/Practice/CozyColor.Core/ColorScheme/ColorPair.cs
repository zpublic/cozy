using CozyColor.Core.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyColor.Core.ColorScheme
{
    public class ColorPair
    {
        public static System.Drawing.Color ComplementaryColor(System.Drawing.Color c)
        {
            var color = System.Drawing.Color.FromArgb(
                ~c.R,
                ~c.G,
                ~c.B);
            return color;
        }

        public static System.Drawing.Color[] RandomComplementaryColor()
        {
            var arr = new System.Drawing.Color[2];
            arr[0] = RandomColor.Generate();
            arr[1] = System.Drawing.Color.FromArgb(
                (byte)~arr[0].R,
                (byte)~arr[0].G,
                (byte)~arr[0].B);
            return arr;
        }
    }
}
