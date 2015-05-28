using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine
{
    public class CozyAction : ICloneable
    {
        public int Tag { get; set; }
        public CozyNode Target { get; set; }

        public virtual void StartWithTarget(CozyNode target)
        {

        }

        protected virtual void Step(float dt)
        {

        }

        public virtual void Update(float dt)
        {

        }

        public virtual void Stop()
        {
            Target = null;
        }

        public object Clone()
        {
            return new CozyAction();
        }
    }
}
