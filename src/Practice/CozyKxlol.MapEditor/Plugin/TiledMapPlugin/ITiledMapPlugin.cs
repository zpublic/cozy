using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.MapEditor.Plugin.TiledMapPlugin
{
    interface ITiledMapPlugin
    {
        string Name { get; }
        void Execute(TiledMapDataContainer container);
    }
}
