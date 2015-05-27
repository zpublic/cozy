using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CozyKxlol.Kxlol.Extends;
using CozyKxlol.Engine;

namespace CozyKxlol.Kxlol.Object
{
    class DefaultUserCircle : UserCircle
    {
        public const float DefaultUserCircleBorderSize  = 2.0f;

        public DefaultUserCircle(Vector2 pos, int radius, uint color, string name)
            : base(pos, radius, color, DefaultUserCircleBorderSize, name)
        {

        }
    }
}
