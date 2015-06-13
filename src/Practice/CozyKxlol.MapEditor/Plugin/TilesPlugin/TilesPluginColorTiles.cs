using CozyKxlol.MapEditor.TilesPlugin;
using CozyKxlol.MapEditor.TilesPlugin.ColorTiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.MapEditor.Plugin.TilesPlugin
{
    class TilesPluginColorTiles : ITilesPlugin
    {
        public IEnumerable<Tuple<uint, Engine.Tiled.CozyTiledNode>> GetTiles()
        {
            var arr = new List<Tuple<uint, Engine.Tiled.CozyTiledNode>>()
            {
                new Tuple<uint, Engine.Tiled.CozyTiledNode>(CozyTileId.Green, new CozyGreenTile()),
                new Tuple<uint, Engine.Tiled.CozyTiledNode>(CozyTileId.Red, new CozyRedTile())
            };
            return arr;
        }
    }
}
