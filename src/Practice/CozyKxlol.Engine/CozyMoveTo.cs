using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Engine
{
    public class CozyMoveTo : CozyMoveBy
    {
        private Vector2 EndPosition;

        public static CozyMoveTo Create(float duration, Vector2 endPosition)
        {
            var act = new CozyMoveTo();
            act.Duration = duration;
            act.EndPosition = endPosition;
            return act;
        }

        public override void StartWithTarget(CozyNode target)
        {
            base.StartWithTarget(target);
            PositionDelta = EndPosition - StartPosition;
        }

        public override object Clone()
        {
            var temp = new CozyMoveTo();
            temp.Duration = Duration;
            temp.EndPosition = EndPosition;
            return temp;
        }
    }
}
