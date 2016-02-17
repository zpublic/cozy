using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyLauncher.PluginBase;
using System.Net.Http;
using System.IO;
using System.IO.Compression;

namespace CozyLauncher.Plugin.KickassTorrents {

    public class Main : BasePlugin {

        private PluginInitContext context_;

        private const string torrentsUrl = "https://kat.cr/usearch/{0}/?rss=1";

        public override PluginInfo Init(PluginInitContext context) {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "t";
            return info;
        }

        public override List<Result> Query(Query query) {
            if (query.RawQuery.StartsWith("t ")) {
                var result = new List<Result>();
                var querySrting = query.RawQuery.Substring(2);
                var respones = Request(querySrting);
            }
            return null;
        }

        private string Request(string query) {
            using (var client = new HttpClient()) {
                var requestMsg = new HttpRequestMessage(HttpMethod.Get, string.Format(torrentsUrl, query));
                var respones = client.SendAsync(requestMsg).Result;
                var byteArray = respones.Content.ReadAsByteArrayAsync().Result;
                var de = Decompress(byteArray);
                string str = Encoding.UTF8.GetString(de);
                return str;
            }
        }

        public static byte[] Decompress(byte[] zippedData)
        {
            MemoryStream ms = new MemoryStream(zippedData);
            GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Decompress);
            MemoryStream outBuffer = new MemoryStream();
            byte[] block = new byte[1024];
            while (true)
            {
                int bytesRead = compressedzipStream.Read(block, 0, block.Length);
                if (bytesRead <= 0)
                    break;
                else
                    outBuffer.Write(block, 0, bytesRead);
            }
            compressedzipStream.Close();
            return outBuffer.ToArray();
        }
    }
}
