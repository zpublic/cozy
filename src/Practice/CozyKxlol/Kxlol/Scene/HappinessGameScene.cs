using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;

namespace CozyKxlol.Kxlol.Scene
{
    public class HappinessGameScene : CozyScene
    {
        public static HappinessGameScene Create()
        {
            var scene = new HappinessGameScene();
            scene.Init();
            return scene;
        }

        public virtual bool Init()
        {
            var layer = HappinessGameLayer.Create();
            this.AddChind(layer);
            return true;
        }

    }

}
