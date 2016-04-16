using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.DistributedDownload.MasterGui.Models
{
    public class DownloadTaskInfo
    {
        public string RemotePath { get; set; }
        public string LocalPath { get; set; }
        public int MinThread { get; set; }
        public int MaxThread { get; set; }
        public bool EnableDistributed { get; set; }
    }
}
