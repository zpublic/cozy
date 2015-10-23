using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWiki.Cache
{
    public class CacheBlock
    {
        public string Data { get; set; }

        public DateTime LastWriteTime { get; set; }

        public DateTime CreateTime { get; set; }

        public long FileSize { get; set; }

        public bool IsEffective(FileInfo info)
        {
            if (info.Length != FileSize) return false;
            if (info.CreationTime != CreateTime) return false;
            if (info.LastWriteTime > LastWriteTime) return false;
            return true;
        }

        public void Update(string data, FileInfo info)
        {
            Data            = data;
            LastWriteTime   = info.LastWriteTime;
            CreateTime      = info.CreationTime;
            FileSize        = info.Length;
        }
    }
}
