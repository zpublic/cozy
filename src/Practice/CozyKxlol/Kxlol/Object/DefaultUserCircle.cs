using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CozyKxlol.Kxlol.Extends;

namespace CozyKxlol.Kxlol.Object
{
    class DefaultUserCircle : CozyCircle
    {
        public const float DefaultUserCircleBorderSize  = 2.0f;
        public DefaultUserCircle(Vector2 pos, float radius, uint color)
            : base(pos, radius, color.ToColor(), DefaultUserCircleBorderSize)
        {
            
        }
    }
}
