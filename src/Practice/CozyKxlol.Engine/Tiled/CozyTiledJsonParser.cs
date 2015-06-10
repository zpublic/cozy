using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine.Tiled
{
    class CozyTiledJsonParser
    {
        public object parser(string filename)
        {
            string json = filename;
            JObject jo = JObject.Parse(json);
            return jo["tiles"].ToString();
        }
    }
}
