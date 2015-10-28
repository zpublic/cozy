using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyColor.Core.Color
{
    public class RandomColor
    {
        public static System.Drawing.Color Generate()
        {
            return System.Drawing.Color.FromArgb(
                Definition.R.Next(256),
                Definition.R.Next(256),
                Definition.R.Next(256));
        }
    }
}
