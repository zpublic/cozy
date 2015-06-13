using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine.Tiled.Json.Strategy
{
    public class TiledDataParseWithFill : ITiledDataParseStrategy
    {
        public void ParseData(string subData, List<uint> result, int length)
        {
            if (subData.EndsWith("*"))
            {
                uint value = uint.Parse(subData.Substring(0, subData.Length - 1));
                for (int i = 0; i < length; ++i)
                {
                    result.Add(value);
                }
            }
            else
            {
                // TODO 
                int pos = subData.IndexOf('*');
                uint value = uint.Parse(subData.Substring(0, subData.Length - pos - 1));
                int loop = int.Parse(subData.Substring(pos + 1, subData.Length - pos - 1));
                for (int i = 0; i < loop; ++i)
                {
                    result.Add(value);
                }
            }
        }
    }
}
