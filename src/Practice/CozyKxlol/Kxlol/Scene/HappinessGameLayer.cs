using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using CozyKxlol.Engine.Tiled;
using CozyKxlol.Kxlol.Object.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Kxlol.Scene
{
    public class HappinessGameLayer : CozyLayer
    {
        public CozyTiledMap Tileds { get; set; }
        public static HappinessGameLayer Create()
        {
            var layer = new HappinessGameLayer();
            layer.Init();
            return layer;
        }

        public virtual bool Init()
        {
            RegisterTiled();

            Tileds                  = new CozyTiledMap(new Point(15, 20));
            Tileds.NodeContentSize  = Vector2.One * 32;
            this.AddChind(Tileds);

            return true;
        }

        private void RegisterTiled()
        {
            CozyTiledFactory.RegisterTiled(CozyTiledId.RedTiled, new CozyRedTiled());
            CozyTiledFactory.RegisterTiled(CozyTiledId.GreenTiled, new CozyGreenTiled());
        }
    }
}
