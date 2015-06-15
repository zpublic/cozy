using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine.Tiled.Json.Strategy
{
    public class TiledDataParseContext
    {
        public ITiledDataParseStrategy Strategy { get; set; }

        public void Parse(string subData, List<uint> result, int length)
        {
            if(Strategy != null)
            {
                Strategy.ParseData(subData, result, length);
            }
        }
    }
}
