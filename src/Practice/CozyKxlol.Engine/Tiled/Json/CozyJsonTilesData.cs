using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine.Tiled.Json
{
    public class CozyJsonTilesData
    {
        public string type { get; set; }
        public string path { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public uint id { get; set; }
        public string annotate { get; set; }
    }
}
