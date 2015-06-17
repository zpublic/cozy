using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using CozyKxlol.Engine.Tiled;
using CozyKxlol.Engine.Tiled.Json;
using Microsoft.Xna.Framework;

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
            RegisterTiles();
            var layer = HappinessGameLayer.Create();
            this.AddChind(layer);
            return true;
        }

        private void RegisterTiles()
        {
            var arr = new List<Tuple<uint, CozyTiledNode>>();
            var data = CozyDirector.Instance.JsonManagerInstance.ParseWithFile(@".\Content\tiles.json");

            if (data.tiles != null)
            {
                var tiles = data.tiles;
                foreach (var tile in tiles)
                {
                    var texture = CozyDirector.Instance.TextureCacheInstance.AddImage(tile.path);
                    if (tile.type.Equals("tiles"))
                    {
                        // TODO 分割图片
                        var CurrId = tile.id;
                        for (int i = 0; i < tile.w; ++i)
                        {
                            for (int j = 0; j < tile.h; ++j)
                            {
                                var subTile = CozySpriteTiled.Create(texture, new Rectangle(32 * i, 32 * j, 32, 32));
                                CozyTiledFactory.RegisterTiled(CurrId++, subTile);
                            }
                        }
                    }
                    else if (tile.type.Equals("tile"))
                    {
                        // TODO 取图片里的一块
                        var subTile = CozySpriteTiled.Create(texture, new Rectangle(32 * tile.x, 32 * tile.y, 32, 32));
                        CozyTiledFactory.RegisterTiled(tile.id, subTile);
                    }
                }
            }
        }
    }
}
