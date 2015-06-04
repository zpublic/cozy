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
            Tileds = new CozyTiledMap(new Point(15, 20));
            CozyTiledFactory.Create += Create;
            this.AddChind(Tileds);

            var loader = new CozyTiledDataLoader();
            Tileds.LoadData(loader);
            return true;
        }

        public CozyTiledNode Create(uint id)
        {
            CozyTiledNode node = null;
            switch(id)
            {
                case CozyTiledId.NoneTiled:
                    node = new CozyNoneTiled();
                    break;
                case CozyTiledId.RedTiled:
                    node = new CozyRedTiled();
                    break;
                case CozyTiledId.GreenTiled:
                    node = new CozyGreenTiled();
                    break;
                default:
                    break;
            }
            return node;
        }
    }
}
