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

        public new static CozyMoveTo Create(float duration, Vector2 endPosition)
        {
            var act = new CozyMoveTo();
            act.InitWithDuraction(duration);
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
            var temp            = new CozyMoveTo();
            temp.InitWithDuraction(Duration);
            temp.EndPosition    = EndPosition;
            return temp;
        }
    }
}
