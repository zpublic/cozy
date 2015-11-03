using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel.Controls.Other
{
    /// <summary>
    /// // 24色
    /// </summary>
    public static class OstwaldColor
    {
        public static List<Color> GetColor(int alpha = 255)
        {
            return new List<Color>()
            {
                Color.FromArgb(alpha, 230, 0, 18),
                Color.FromArgb(alpha, 235, 97, 0),
                Color.FromArgb(alpha, 243, 152, 0),
                Color.FromArgb(alpha, 252, 200, 0),
                Color.FromArgb(alpha, 255, 251, 0),
                Color.FromArgb(alpha, 207, 0, 219),
                Color.FromArgb(alpha, 143, 195, 31),
                Color.FromArgb(alpha, 34, 172, 56),
                Color.FromArgb(alpha, 0, 153, 68),
                Color.FromArgb(alpha, 0, 155, 107),
                Color.FromArgb(alpha, 0, 158, 150),
                Color.FromArgb(alpha, 0, 160, 193),
                Color.FromArgb(alpha, 0, 160, 233),
                Color.FromArgb(alpha, 0, 134, 209),
                Color.FromArgb(alpha, 0, 104, 183),
                Color.FromArgb(alpha, 0, 71, 157),
                Color.FromArgb(alpha, 29, 32, 136),
                Color.FromArgb(alpha, 96, 25, 136),
                Color.FromArgb(alpha, 146, 7, 131),
                Color.FromArgb(alpha, 190, 0, 129),
                Color.FromArgb(alpha, 228, 0, 127),
                Color.FromArgb(alpha, 229, 0, 106),
                Color.FromArgb(alpha, 229, 0, 79),
                Color.FromArgb(alpha, 230, 0, 51),
            };
        }
    }
}
