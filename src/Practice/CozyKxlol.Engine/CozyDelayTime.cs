using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine
{
    public class CozyDelayTime : CozyActionInterval
    {
        public static CozyDelayTime Create(float time)
        {
            var act = new CozyDelayTime();
            act.InitWithDuraction(time);
            return act;
        }

        public override object Clone()
        {
            var act = new CozyDelayTime();
            act.InitWithDuraction(Duration);
            return act;
        }

        public override void Update(float dt)
        {
            // Nothing
        }
    }
}
