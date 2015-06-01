using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Engine
{
    public class CozyAction : ICloneable
    {
        public int Tag { get; set; }
        public CozyNode Target { get; set; }

        public CozyNode OriginalTarget { get; set; }

        public virtual bool IsDone 
        { 
            get
            {
                return true;
            }
        }

        public virtual void StartWithTarget(CozyNode target)
        {
            OriginalTarget = Target = target;
        }

        public virtual void Step(float dt)
        {

        }

        public virtual void Update(float dt)
        {

        }

        public virtual void Stop()
        {
            Target = null;
        }

        public virtual object Clone()
        {
            return null;
        }
    }
}
