using CozyKxlol.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Kxlol.Scene
{
    class WelcomeScene : CozyScene
    {
        public WelcomeScene()
        {
            Layer = new WelcomeSceneLayer();
        }
    }
}
