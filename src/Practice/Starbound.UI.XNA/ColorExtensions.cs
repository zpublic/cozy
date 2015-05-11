using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.XNA
{
    public static class ColorExtensions
    {
        public static Microsoft.Xna.Framework.Color ToXNAColor(this Starbound.UI.SBColor color)
        {
            return new Microsoft.Xna.Framework.Color((float)color.R, (float)color.G, (float)color.B, (float)color.A);
        }
    }
}
