using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;

namespace CozyKxlol.Kxlol.Scene
{
    class BallGameScene : CozyScene
    {
        public BallGameScene()
        {
            this.AddChind(new BallGameSceneLayer());
        }
    }
}
