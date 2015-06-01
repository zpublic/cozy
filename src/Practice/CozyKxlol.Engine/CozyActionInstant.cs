using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine
{
    public class CozyActionInstant : CozyFiniteTimeAction
    {
        public override bool IsDone
        {
            get
            {
                return true;
            }
        }

        public override void Step(float dt)
        {
            Update(1.0f);
        }

        public override void Update(float dt)
        {

        }
    }
}
