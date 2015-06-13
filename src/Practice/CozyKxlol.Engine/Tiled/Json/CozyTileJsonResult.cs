using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine.Tiled.Json
{
    public class CozyTileJsonResult
    {
        public List<CozyJsonTilesData> tiles { get; set; }
        public List<CozyJsonBlockData> square { get; set; }
        public List<CozyJsonBlockData> rect { get; set; }
    }
}
