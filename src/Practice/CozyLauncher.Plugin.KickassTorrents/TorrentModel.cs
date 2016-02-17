using System;

namespace CozyLauncher.Plugin.KickassTorrents {

    public class TorrentModel {

        public string FileName { get; set; }

        public long FileSize { get; set; }

        public string TorrentUrl { get; set; }

        public string MagnetUrl { get; set; }

        public string Seeds { get; set; }

        public string PubDate { get; set; }

        public string SubTitle {
            get {
                return $"文件大小:{Helper.CountSize(FileSize)} | 资源数:{Seeds} | 上传时间:{DateTime.Parse(PubDate).ToString()}";
            }
        }
    }
}
