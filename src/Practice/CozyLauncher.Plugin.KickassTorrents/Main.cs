using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyLauncher.PluginBase;
using System.Net.Http;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Reflection;
using System.Diagnostics;

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

                result.Add(new Result {
                    Title = $"回车搜索 {querySrting}",
                    SubTitle = "KickassTorrents暂不支持中文搜索",
                    Action = e => {
                        var stream = Request(querySrting);
                        var torrentList = LoadData(stream);
                        if (torrentList.Count > 0) {
                            context_.Api.PushResults(torrentList.Select(x =>
                                new Result {
                                    Title = x.FileName,
                                    SubTitle = x.SubTitle,
                                    IcoPath = "sys",
                                    Action = ex => {
                                        try
                                        {
                                            Process.Start(x.TorrentUrl);
                                        }
                                        catch (Exception) { }
                                        context_.Api.HideAndClear();
                                        return true;
                                    }
                                }).ToList());
                        }
                        else {
                            //context_.Api.Clear();
                            context_.Api.PushResults(new[] { new Result {
                                Title = "找不到内容",
                                IcoPath = "sys",
                                Action = ex => {
                                    context_.Api.HideAndClear();
                                    return true;
                                }
                            } }.ToList());
                        }
                        return true;
                    }
                });
                return result;
            }
            return null;
        }

        private Stream Request(string query) {
            using (var client = new HttpClient()) {
                var requestMsg = new HttpRequestMessage(HttpMethod.Get, string.Format(torrentsUrl, query));
                var respones = client.SendAsync(requestMsg).Result;
                var byteArray = respones.Content.ReadAsByteArrayAsync().Result;
                var de = Helper.Decompress(byteArray);
                var stream = new MemoryStream(de);
                return stream;
            }
        }

        public List<TorrentModel> Proc(string querySrting) {
            var stream = Request(querySrting);
            var torrentList = LoadData(stream);
            return torrentList;
        }

        public List<TorrentModel> LoadData(Stream stream) {
            try {
                var xdoc = XDocument.Load(stream);
                return xdoc.Root.Element("channel").Elements()
                    .Where(x => x.Name == "item").Select(x =>
                     new TorrentModel {
                         FileName = x.Element("title").Value,
                         TorrentUrl = x.Element("enclosure").FirstAttribute.Value,
                         PubDate = x.Element("pubDate").Value,
                         FileSize = long.Parse(x.Elements().First(y => y.Name.ToString().EndsWith("contentLength")).Value),
                         Seeds = x.Elements().First(y => y.Name.ToString().EndsWith("seeds")).Value,
                         MagnetUrl = $"magnet:?xt=urn:btih:{x.Elements().First(y => y.Name.ToString().EndsWith("infoHash")).Value}"
                     }
                ).ToList();
            }
            catch {
                return new List<TorrentModel>();
            }

        }

    }
}
