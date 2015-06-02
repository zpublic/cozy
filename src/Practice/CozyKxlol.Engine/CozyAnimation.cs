using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine
{
    public class CozyAnimation : ICloneable
    {
        public float DelayPerUnit { get; set; }
        public uint Loops { get; set; }
        public float TotalDelayUnits { get; set; }

        public float Duration
        {
            get
            {
                return TotalDelayUnits * DelayPerUnit;
            }
        }

        private List<CozyAnimationFrame> _Frames = new List<CozyAnimationFrame>();
        public List<CozyAnimationFrame> Frames
        {
            get
            {
                return _Frames;
            }
            set
            {
                _Frames = value;
            }
        }

        public static CozyAnimation Create()
        {
            var animation = new CozyAnimation();
            animation.Init();
            return animation;
        }

        public static CozyAnimation Create(List<CozyAnimationFrame> frameList, float delayPreUnit = 0.0f, uint loops = 1)
        {
            var animation = new CozyAnimation();
            animation.InitWithAnimationFrames(frameList, delayPreUnit, loops);

            return animation;
        }

        public bool Init()
        {
            Loops = 1;
            DelayPerUnit = 0.0f;
            return true;
        }

        public bool InitWithAnimationFrames(List<CozyAnimationFrame> frameList, float delayPreUnit, uint loops)
        {
            DelayPerUnit = delayPreUnit;
            Loops = loops;

            Frames = frameList;

            foreach(var obj in Frames)
            {
                TotalDelayUnits += obj.DelayUnits;
            }

            return true;
        }

        public void AddFrame(CozyTexture texture)
        {
            var frame = CozyAnimationFrame.Create(texture, 1.0f);
            Frames.Add(frame);
            TotalDelayUnits ++;
        }

        public void AddFrameWithFile(string path)
        {
            var texture = CozyDirector.Instance.TextureCacheInstance.AddImage(path);
            AddFrame(texture);
        }

        public object Clone()
        {
            var a = new CozyAnimation();
            a.InitWithAnimationFrames(_Frames, DelayPerUnit, Loops);
            return a;
        }
    }
}
