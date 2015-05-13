using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Kxlol.Object
{
    class DefaultFoodCircle : CozyCircle
    {
        public const float DefaultFoodRadius = 5.0f;
        public DefaultFoodCircle(Vector2 pos)
            :base(pos, DefaultFoodRadius,RandomColor())
        { 

        }
    }
}
