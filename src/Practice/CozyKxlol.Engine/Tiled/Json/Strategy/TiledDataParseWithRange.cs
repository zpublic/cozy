using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine.Tiled.Json.Strategy
{
    public class TiledDataParseWithRange : ITiledDataParseStrategy
    {
        public void ParseData(string subData, List<uint> result, int length)
        {
            int pos = subData.IndexOf('-');
            uint first = uint.Parse(subData.Substring(0, subData.Length - pos - 1));
            uint last = uint.Parse(subData.Substring(pos + 1, subData.Length - pos - 1));
            for (uint i = first; i <= last; ++i)
            {
                result.Add(i);
            }
        }
    }
}
