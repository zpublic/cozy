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
        public bool IsEnableDistributed { get; set; }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(RemotePath) && !string.IsNullOrEmpty(LocalPath);
            }
        }
    }
}
