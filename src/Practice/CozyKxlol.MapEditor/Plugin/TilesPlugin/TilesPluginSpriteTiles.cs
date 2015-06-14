using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using CozyKxlol.Engine.Tiled;
using CozyKxlol.Engine.Tiled.Json.Strategy;
using Microsoft.Xna.Framework;

namespace CozyKxlol.MapEditor.Plugin.TilesPlugin
{
    class TilesPluginSpriteTiles : ITilesPlugin
    {
        public IEnumerable<Tuple<uint, CozyTiledNode>> GetTiles()
        {
            var arr = new List<Tuple<uint, CozyTiledNode>>();

            var data = CozyDirector.Instance.JsonManagerInstance.Parse(@".\Content\tiles.json");

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
                                arr.Add(CreateTiledNodeTuple(CurrId++, texture, new Rectangle(32 * i, 32 * j, 32, 32)));
                            }
                        }
                    }
                    else if (tile.type.Equals("tile"))
                    {
                        // TODO 取图片里的一块
                        arr.Add(CreateTiledNodeTuple(tile.id, texture, new Rectangle(32 * tile.x, 32 * tile.y, 32, 32)));
                    }
                }
            }
            return arr;
        }

        private Tuple<uint, CozyTiledNode> CreateTiledNodeTuple(uint id, CozyTexture texture, Rectangle rect)
        {
            return Tuple.Create<uint, CozyTiledNode>(id, CozySpriteTiled.Create(texture, rect));
        }
    }
}
