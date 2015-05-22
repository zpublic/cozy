using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CozyKxlol.Kxlol.Extends;

namespace CozyKxlol.Kxlol.Object
{
    class DefaultFoodCircle : CozyCircle
    {
        public DefaultFoodCircle(Vector2 pos, int radius, uint color)
            : base(pos, radius, color.ToColor())
        {

        }
    }
}
