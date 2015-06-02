using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine
{
    public class CozyExtraAction : CozyFiniteTimeAction
    {
        public override object Clone()
        {
            return new CozyExtraAction();
        }

        public static CozyExtraAction Create()
        {
            return new CozyExtraAction();
        }
    }
}
