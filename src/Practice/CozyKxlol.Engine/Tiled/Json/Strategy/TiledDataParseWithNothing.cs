using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine.Tiled.Json.Strategy
{
    public class TiledDataParseWithNothing : ITiledDataParseStrategy
    {
        // TODO Parse "value"
        public void ParseData(string subData, List<uint> result, int length)
        {
            result.Add(uint.Parse(subData));
        }
    }
}
