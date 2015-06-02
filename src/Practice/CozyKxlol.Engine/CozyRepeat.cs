using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Engine
{
    public class CozyRepeat : CozyActionInterval
    {
        private CozyFiniteTimeAction InnerAction { get; set; }
        public uint Times { get; set; }
        public uint Total { get; set; }
        public override bool IsDone
        {
            get
            {
                return Total == Times;
            }
        }
        public float NextDt { get; set; }
        public static CozyRepeat Create(CozyFiniteTimeAction action, uint times)
        {
            var act = new CozyRepeat();
            act.InitWithAction(action, times);
            return act;
        }

        public bool InitWithAction(CozyFiniteTimeAction action, uint times)
        {
            float d = action.Duration * times;
            InitWithDuraction(d);

            Times = times;
            InnerAction = action;

            return true;
        }

        public override object Clone()
        {
            var a = new CozyRepeat();
            var act = InnerAction.Clone() as CozyFiniteTimeAction;
            a.InitWithAction(act, Times);
            return a;
        }

        public override void Stop()
        {
            InnerAction.Stop();
        }

        public override void StartWithTarget(CozyNode target)
        {
            Total = 0;
            NextDt = InnerAction.Duration / Duration;
            base.StartWithTarget(target);
            InnerAction.StartWithTarget(target);
        }

        public override void Update(float dt)
        {
            if(dt >= NextDt)
            {
                while(dt > NextDt && Total < Times)
                {
                    InnerAction.Update(1.0f);
                    ++Total;
                    InnerAction.Stop();
                    InnerAction.StartWithTarget(Target);
                    NextDt = InnerAction.Duration / Duration * (Total + 1);
                }

                if(dt >= 1.0f && Total < Times)
                {
                    ++Total;
                }

                if(Total == Times)
                {
                    InnerAction.Update(1.0f);
                    InnerAction.Stop();
                }
                else
                {
                    InnerAction.Update(dt - (NextDt - InnerAction.Duration / Duration));
                }
            }
            else
            {
                InnerAction.Update((dt * Times) % 1.0f);
            }
        }
    }
}
