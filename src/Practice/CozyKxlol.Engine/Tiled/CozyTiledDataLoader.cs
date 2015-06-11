using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CozyKxlol.Engine.Tiled
{
    public class CozyTiledDataLoader 
    {
        public string Path { get; set; }

        public CozyTiledDataLoader(string path)
        {
            Path = path;
        }

        public void Load(ICozyLoader loader)
        {
            if(Path != null)
            {
                if(File.Exists(Path))
                {
                    using (Stream stream = new FileStream(Path, FileMode.Open, FileAccess.Read))
                    {
                        loader.Load(stream);
                    }
                }
            }

        }
    }
}
