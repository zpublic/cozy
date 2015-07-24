using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine.Tiled;

namespace CozyKxlol.MapEditor.Plugin.TilesPlugin
{
    public class TilesPluginMgr
    {
        static List<ITilesPlugin> mPluginList = new List<ITilesPlugin>();

        public static void RegistAllTiles()
        {
            mPluginList.Add(new TilesPluginColorTiles());
            mPluginList.Add(new TilesPluginSpriteTiles());

            foreach (var i in mPluginList)
            {
                var tiles = i.GetTiles();
                foreach (var t in tiles)
                {
                    CozyTiledFactory.RegisterTiled(t.Item1, t.Item2);
                }
            }
        }
    }
}
