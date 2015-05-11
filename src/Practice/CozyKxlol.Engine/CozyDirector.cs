using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine
{
    public class CozyDirector
    {
        readonly List<CozyScene> scenesStack = new List<CozyScene>();
        public CozyScene RunningScene { get; private set; }

        public void RunWithScene(CozyScene scene)
        {
            PushScene(scene);
        }

        public void ReplaceScene(CozyScene scene)
        {
        }

        public void PushScene(CozyScene scene)
        {
            RunningScene = scene;
        }

        public void PopScene()
        {
        }
    }
}
