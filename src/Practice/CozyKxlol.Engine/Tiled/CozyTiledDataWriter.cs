using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CozyKxlol.Engine.Tiled.Impl;

namespace CozyKxlol.Engine.Tiled
{
    public class CozyTiledDataWriter
    {
        public string Path { get; set; }

        public CozyTiledDataWriter(string path)
        {
            Path = path;
        }

        public void Write(ICozyWriter writer)
        {
            if(Path != null)
            {
                using (Stream stream = new FileStream(Path, FileMode.Create, FileAccess.Write))
                {
                    writer.Write(stream);
                }
            }
        }
    }
}
