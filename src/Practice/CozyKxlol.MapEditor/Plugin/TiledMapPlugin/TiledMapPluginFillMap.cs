using CozyKxlol.MapEditor.TilesPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.MapEditor.Plugin.TiledMapPlugin
{
    class TiledMapPluginFillMap : ITiledMapPlugin
    {
        public string Name
        {
            get { return "FillMap"; }
        }

        public void Execute(TiledMapDataContainer container)
        {
            for (int x = 0; x < container.MapSize.X; ++x)
            {
                for (int y = 0; y < container.MapSize.Y; ++y)
                {
                    container.Write(x, y, CozyTileId.Red);
                }
            }
        }
    }
}
