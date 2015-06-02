using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Engine
{
    public class CozyMoveBy : CozyActionInterval
    {
        protected Vector2 PositionDelta { get; set; }
        protected Vector2 StartPosition { get; set; }

        public override void StartWithTarget(CozyNode target)
        {
            base.StartWithTarget(target);
            StartPosition = target.Position;
        }

        public override void Update(float dt)
        {
            if(Target != null)
            {
                Target.Position = StartPosition + PositionDelta * dt;
            }
        }

        public static CozyMoveBy Create(float duration, Vector2 deltaPosition)
        {
            var act             = new CozyMoveBy();
            act.PositionDelta   = deltaPosition;
            act.InitWithDuraction(duration);
            if(act.Duration == 0.0f)
            {
                act.Duration    = float.Epsilon;
            }
            return act;
        }

        public override object Clone()
        {
            var act             = new CozyMoveBy();
            act.InitWithDuraction(this.Duration);
            act.PositionDelta   = this.PositionDelta;
            return act;
        }
    }
}
