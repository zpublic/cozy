using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Engine
{
    public class CozyActionInterval : CozyFiniteTimeAction
    {
        public float Elapsed { get; set; }

        public override bool IsDone
        {
            get
            {
                return Elapsed >= Duration;
            }
        }

        public bool FirstTick { get; set; }

        public override void StartWithTarget(CozyNode target)
        {
            base.StartWithTarget(target);
            Elapsed = 0.0f;
            FirstTick = true;
        }

        public override void Step(float dt)
        {
            if(FirstTick)
            {
                FirstTick   = false;
                Elapsed     = 0.0f;
            }
            else
            {
                Elapsed     += dt;
            }
            this.Update(Math.Max(0, Math.Min(1, Elapsed / Math.Max(Duration, float.Epsilon))));
        }

        public override object Clone()
        {
            return null;
        }
    }
}
