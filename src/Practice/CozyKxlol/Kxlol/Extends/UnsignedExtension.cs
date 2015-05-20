using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Kxlol.Extends
{
    public static class UnsignedExtension
    {
        public static Color ToColor(this uint value)
        {
            var c           = new Color();
            c.PackedValue   = value;
            return c;
        }
    }
}
