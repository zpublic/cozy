using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;

namespace CozyKxlol.Kxlol.Scene
{
    public class HappinessGameLayer : CozyLayer
    {

        public static HappinessGameLayer Create()
        {
            var layer = new HappinessGameLayer();
            layer.Init();
            return layer;
        }

        public virtual bool Init()
        {
            return true;
        }
    }
}
