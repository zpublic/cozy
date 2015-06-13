using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine.Tiled.Json
{
    public class CozyTiledJsonParser
    {
        public object parser(string filename)
        {
            string json = null;
            using (var fs = new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read)))
            {
                json = fs.ReadToEnd();
            }

            CozyTileJsonResult node     = new CozyTileJsonResult();
            JObject jo                  = JObject.Parse(json);
            var tiles = jo["tiles"];

            if(tiles != null && tiles.HasValues)
            {
                node.tiles = JsonConvert.DeserializeObject<List<CozyJsonTilesData>>(tiles.ToString());
            }

            var blocks = jo["blocks"];
            if (blocks != null && blocks.HasValues)
            {
                var square = blocks["square"];
                if(square != null && square.HasValues)
                {
                    node.square = JsonConvert.DeserializeObject<List<CozyJsonBlockData>>(square.ToString());
                }
                var rect = blocks["rect"];
                if(rect != null && rect.HasValues)
                {
                    node.rect = JsonConvert.DeserializeObject<List<CozyJsonBlockData>>(rect.ToString());
                }
            }
            return node;
        }
    }
}
