using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyColor.Core.Color
{
    public class DradientColor
    {
        public static System.Drawing.Color[] Generate(
            System.Drawing.Color from,
            System.Drawing.Color to,
            int step = 3)
        {
            System.Drawing.Color[] arr = new System.Drawing.Color[step];
            arr[0] = from;
            arr[step - 1] = to;
            int stepR = (to.R - from.R) / (step - 1);
            int stepG = (to.G - from.G) / (step - 1);
            int stepB = (to.B - from.B) / (step - 1);
            for (int i = 1; i < step - 1; ++i)
            {
                System.Drawing.Color c = System.Drawing.Color.FromArgb(
                    from.R + (stepR * i),
                    from.G + (stepG * i),
                    from.B + (stepB * i));
                arr[i] = c;
            }
            return arr;
        }
    }
}
