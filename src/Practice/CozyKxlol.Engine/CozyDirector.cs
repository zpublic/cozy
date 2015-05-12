using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Engine
{
    public class CozyDirector
    {
        readonly List<CozyScene> scenesStack = new List<CozyScene>();
        public CozyScene RunningScene { get; private set; }
        public Point WindowSize { get; set; }

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

        private static CozyDirector _Instance = new CozyDirector();
        public static CozyDirector Instance
        {
            get
            {
                return _Instance;
            }
        }
    }
}
