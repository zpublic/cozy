using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine
{
    public class CozyAnimate : CozyActionInterval
    {
        public int NextFrame { get; set; }

        private CozyAnimation _Animaction;
        public CozyAnimation Animaction 
        { 
            get
            {
                return _Animaction;
            }
            set
            {
                if(value != _Animaction)
                {
                    _Animaction = value;
                }
            }
        }

        public CozyTexture OrigFrame { get; set; }

        public uint ExecutedLoops { get; set; }

        private List<float> _SplitTimes = new List<float>();
        public List<float> SplitTimes 
        { 
            get
            {
                return _SplitTimes;
            }
            set
            {
                _SplitTimes = value;
            }
        } 

        public static CozyAnimate Create(CozyAnimation animation)
        {
            var a = new CozyAnimate();
            a.InitWithAnimation(animation);
            return a;
        }

        public bool InitWithAnimation(CozyAnimation animation)
        {
            float signleDuration = animation.Duration;
            base.InitWithDuraction(signleDuration * animation.Loops);
            NextFrame = 0;
            Animaction = animation;
            OrigFrame = null;
            ExecutedLoops = 0;
            float accumUnitOfTime = 0;
            float newUnitOfTimeValue = signleDuration / animation.TotalDelayUnits;

            var frames = animation.Frames;
            foreach(var obj in frames)
            {
                float value = (accumUnitOfTime * newUnitOfTimeValue) / signleDuration;
                accumUnitOfTime += obj.DelayUnits;
                SplitTimes.Add(value);
            }

            return true;
        }

        public override void StartWithTarget(CozyNode target)
        {
            base.StartWithTarget(target);

            NextFrame = 0;
            ExecutedLoops = 0;
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void Update(float dt)
        {
            if(dt < 1.0f)
            {
                dt *= Animaction.Loops;

                uint LoopNum = (uint)dt;
                if(LoopNum > ExecutedLoops)
                {
                    NextFrame = 0;
                    ExecutedLoops++;
                }

                dt = dt % 1.0f;
            }

            var frames = Animaction.Frames;
            int numberOfFrames = frames.Count;
            CozyTexture frameToDisplay = null;
            for(int i = NextFrame; i < numberOfFrames; ++i)
            {
                float splitTime = SplitTimes[i];

                if(splitTime <= dt)
                {
                    CozyAnimationFrame frame = frames[i];
                    frameToDisplay = frame.Texture;
                    (Target as CozySprite).Texture = frameToDisplay;
                    NextFrame = i + 1;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
