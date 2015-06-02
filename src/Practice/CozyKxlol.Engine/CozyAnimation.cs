using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine
{
    public class CozyAnimation
    {
        public float DelayPerUnit { get; set; }
        public uint Loops { get; set; }
        public float TotalDelayUnits { get; set; }

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

        public static CozyAnimation Create(List<CozyAnimationFrame> frameList, float delayPreUnit = 0.0f, uint loops = 1)
        {
            var animation = new CozyAnimation();


            return animation;
        }

        public bool InitWithAnimationFrames(List<CozyAnimationFrame> frameList, float delayPreUnit = 0.0f, uint loops = 1)
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
    }
}
