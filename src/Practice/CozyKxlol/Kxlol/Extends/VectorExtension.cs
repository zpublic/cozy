using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Kxlol.Extends
{
    public static class VectorExtension
    {
        public static Vector2 ClampWithLength(this Vector2 vec, float max)
        {
            float len = vec.Length();
            float len2 = len * len;
            float max2 = max * max;
            if(len2 > max2)
            {
                return vec.Scala((float)Math.Sqrt(max2 / len2));
            }
            return vec;
        }

        public static Vector2 Scala(this Vector2 vec, float scalar)
        {
            vec.X *= scalar;
            vec.Y *= scalar;
            return vec;
        }
    }
}
