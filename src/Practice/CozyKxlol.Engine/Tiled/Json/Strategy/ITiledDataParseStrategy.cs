using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine.Tiled.Json.Strategy
{
    public interface ITiledDataParseStrategy
    {
        void ParseData(string subData, List<uint> result, int length);
    }
}
