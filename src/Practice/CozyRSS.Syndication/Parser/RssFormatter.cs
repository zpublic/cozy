using System;
using System.Linq;
using System.Xml;
using CozyRSS.Syndication.Model;
using CozyRSS.Syndication.Extensions;

namespace CozyRSS.Syndication.Parser {

    public class RssFormatter {

        public SyndicationFeed Formatter(string url) {
            var reader = XmlReader.Create(url);
            var doc = new XmlDocument();
            doc.Load(reader);
            var rootNode = GetRootNode(doc);
            var isAtom = rootNode.Name == "feed";
            var type = isAtom ? typeof(AtomFeed) : typeof(SyndicationFeed);
            var reslut = Parse(GetRootNode(doc), Activator.CreateInstance(type));
            return isAtom ? Convert((AtomFeed)reslut) : (SyndicationFeed)reslut;
        }

        private T Parse<T>(XmlNode node, T obj) {
            var item = obj;
            item.GetType().GetProperties()
                .ToList()
                .ForEach(x => {
                    if (x.PropertyType.Name == "String") {
                        x.SetValue(item, node.SelectSingleNodeEx(x.Name)?.InnerText);
                    }
                    else if (x.PropertyType.Name.StartsWith("List")) {
                        var splite = x.PropertyType.AssemblyQualifiedName.Split(',');
                        var typeName = splite[0].Substring(splite[0].IndexOf('[') + 2);
                        var assemblyName = splite[1];
                        var nodes = node.SelectNodesEx(x.Name.Substring(0, x.Name.Length - 1));
                        var list = (dynamic)x.GetValue(item) ?? x.PropertyType.GetConstructors()[0].Invoke(null);
                        foreach (XmlNode n in nodes) {
                            list.Add((dynamic)Parse(n, Activator.CreateInstance(assemblyName, typeName).Unwrap()));
                        }
                        x.SetValue(item, list);
                    }
                    else {
                        var splite = x.PropertyType.AssemblyQualifiedName.Split(',');
                        var typeName = splite[0];
                        var assemblyName = splite[1];
                        x.SetValue(item, Parse(node.SelectSingleNodeEx(x.Name), Activator.CreateInstance(assemblyName, typeName).Unwrap()));
                    }
                });
            return item;
        }

        private XmlNode GetRootNode(XmlDocument doc) {
            var name = doc.DocumentElement.Name;
            switch (name) {
                case "rss":
                    return doc.SelectSingleNode("rss/channel");
                case "feed":
                    return doc.SelectSingleNodeEx("feed");
                default:
                    throw new Exception("暂时只支持 rss和atom协议");
            }
        }

        private SyndicationFeed Convert(AtomFeed model) {
            var result = new SyndicationFeed {
                title = model.title,
                pubDate = model.updated,
                items = model.entrys.Select(x => new SyndicationItem {
                    link = x.id,
                    title = x.title,
                    pubDate = x.published,
                    description = x.summary
                }).ToList()
            };
            return result;
        }


        /*使用XPath解析Atom协议格式笔记
         * 
         * atom协议的xml有点特殊
         * 用常规的SelectNode()来解析，会一直报空值，原因是XML有设定XMLNamespace造成无法使用
         * 
         * 解决方法：
         *      加载完xml后要对Namespace进行设定
         *
         *  XmlNamespaceManager nsm = new XmlNamespaceManager(doc.NameTable);
         *  nsm.AddNamespace("atom", "http://www.w3.org/2005/Atom");
         *  nsm.AddNamespace("app", "http://purl.org/atom/app#");
         *  nsm.AddNamespace("media", "http://search.yahoo.com/mrss/");
         *  nsm.AddNamespace("openSearch", "http://a9.com/-/spec/opensearchrss/1.0/");
         *  nsm.AddNamespace("gd", "http://schemas.google.com/g/2005");
         *  nsm.AddNamespace("yt", "http://gdata.youtube.com/schemas/2007");
         *  doc.SelectSingleNode("atom:feed",nsm);
         *  
         *  但这种使用方法会影响到Parse()解析的统一性，暂不采用。 在此作记录防后人跳坑
         */
    }
}
