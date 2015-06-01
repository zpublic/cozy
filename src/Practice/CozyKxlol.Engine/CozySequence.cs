using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine
{
    public class CozySequence : CozyActionInterval
    {
        private CozyFiniteTimeAction[] Actions = new CozyFiniteTimeAction[2];
        private float Split { get; set; }
        private int Last { get; set; }

        public static CozySequence Create(params CozyFiniteTimeAction[] args)
        {
            CozySequence seq    = null;
            int Count           = args.Length;
            var prev            = args[0];

            if(Count > 1)
            {
                for(int i = 1; i < Count; ++i)
                {
                    prev        = CreateWithTwoActions(prev, args[i]);
                }
            }
            else
            {
                prev            = CreateWithTwoActions(prev, CozyExtraAction.Create());
            }
            seq                 = prev as CozySequence;
            return seq;
        }

        public static CozySequence CreateWithTwoActions(CozyFiniteTimeAction act1, CozyFiniteTimeAction act2)
        {
            var result = new CozySequence();
            result.InitWithTwoAction(act1, act2);
            return result;
        }

        public bool InitWithTwoAction(CozyFiniteTimeAction act1, CozyFiniteTimeAction act2)
        {
            float d     = act1.Duration + act2.Duration;
            Duration    = d;

            Actions[0]  = act1;
            Actions[1]  = act2;

            return true;
        }

        public override object Clone()
        {
            var a       = new CozySequence();
            var act1    = Actions[0].Clone() as CozyFiniteTimeAction;
            var act2    = Actions[1].Clone() as CozyFiniteTimeAction;
            a.InitWithTwoAction(act1, act2);
            return a;
        }

        public override void StartWithTarget(CozyNode target)
        {
            base.StartWithTarget(target);
            Split   = Actions[0].Duration / Duration;
            Last    = -1;
        }

        public override void Stop()
        {
            if(Last != -1)
            {
                Actions[Last].Stop();
            }
            base.Stop();
        }

        public override void Update(float dt)
        {
            int found   = 0;
            float new_t = 0.0f;

            if(dt < Split)
            {
                // actions[0]
                found = 0;
                if(Split != 0)
                {
                    new_t = dt / Split;
                }
                else
                {
                    new_t = 1;
                }
            }
            else
            {
                // actions[1]
                found = 1;
                if(Split == 1)
                {
                    new_t = 1;
                }
                else
                {
                    new_t = (dt - Split) / (1 - Split);
                }
            }

            if(found == 1)
            {
                if(Last == -1)
                {
                    Actions[0].StartWithTarget(Target);
                    Actions[0].Update(1.0f);
                    Actions[0].Stop();
                }
                else if(Last == 0)
                {
                    Actions[0].Update(1.0f);
                    Actions[0].Stop();
                }
            }
            else if(found == 0 && Last == 1)
            {
                Actions[1].Update(0);
                Actions[1].Stop();
            }

            if(found == Last && Actions[found].IsDone)
            {
                return;
            }

            if(found != Last)
            {
                Actions[found].StartWithTarget(Target);
            }

            Actions[found].Update(new_t);
            Last = found;
        }
    }
}
