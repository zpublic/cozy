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

            CozyTileJsonResult node = new CozyTileJsonResult();

            JObject jo = JObject.Parse(json);
            var iobj = jo["tiles"];
            if( iobj != null && iobj.HasValues)
            {
                var tiles = JObject.Parse(iobj.ToString());
                if(tiles != null && tiles.HasValues)
                {
                    var i = tiles["i"].ToString();
                    node.tiles = JsonConvert.DeserializeObject<CozyJsonTilesData>(i);
                }
            }

            var bobj = jo["blocks"];
            if(bobj != null && bobj.HasValues)
            {
                var blocks = JObject.Parse(bobj.ToString());
                if (blocks != null && blocks.HasValues)
                {
                    var i = blocks["i"].ToString();
                    node.blocks = JsonConvert.DeserializeObject<CozyJsonBlockData>(i);
                }
            }
            return node;
        }
    }
}
