using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyColor.Core.Color
{
    public class RandomColor
    {
        private static Random r = new Random();

        public static System.Drawing.Color Generate()
        {
            return System.Drawing.Color.FromArgb(
                r.Next(256),
                r.Next(256),
                r.Next(256));
        }
    }
}
