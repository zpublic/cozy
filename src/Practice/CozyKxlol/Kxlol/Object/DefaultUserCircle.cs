using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Kxlol.Object
{
    class DefaultUserCircle : CozyCircle
    {
        public const float DefaultUserCircleRadius      = 15.0f;
        public const float DefaultUserCircleBorderSize  = 2.0f;
        public DefaultUserCircle(Vector2 pos)
            : base(pos, DefaultUserCircleRadius, Color.White, DefaultUserCircleBorderSize)
        {
            
        }
    }
}
