using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine
{
    public class CozyFiniteTimeAction : CozyAction
    {
        public float Duration { get; set; }

        public override object Clone()
        {
            return new CozyFiniteTimeAction();
        }
    }
}
