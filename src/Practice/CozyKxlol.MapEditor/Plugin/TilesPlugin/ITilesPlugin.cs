using CozyKxlol.Engine.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.MapEditor.Plugin.TilesPlugin
{
    interface ITilesPlugin
    {
        IEnumerable<Tuple<uint, CozyTiledNode>> GetTiles();
    }
}
