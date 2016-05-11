using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace CozyRSS.Syndication.Parser {

    public class RssFormatter {

        private readonly XmlNode rootNode;

        public RssFormatter(string root, string url) {
            var reader = XmlReader.Create(url);
            var doc = new XmlDocument();
            doc.Load(reader);
            rootNode = doc.SelectSingleNode(root);
        }

        public T Parese<T>() {
            return PareseTest<T>(rootNode, Activator.CreateInstance<T>());
        }

        private T PareseTest<T>(XmlNode node, T obj) {
            var item = obj;
            item.GetType().GetProperties()
                .ToList()
                .ForEach(x => {
                    if (x.PropertyType.Name == "String") {
                        //第一种String类型的属性
                        x.SetValue(item, node.SelectSingleNode(x.Name)?.InnerText);
                    }
                    else if (x.PropertyType.Name.StartsWith("List")) {
                        //第二张List<T>类型属性
                        var splite = x.PropertyType.AssemblyQualifiedName?.Split(',');
                        var typeName = splite[0].Substring(splite[0].IndexOf('[') + 2);
                        var namespaceName = splite[1];
                        var nodeName = x.Name.Substring(0, x.Name.Length - 1);
                        var nodes = node.SelectNodes(nodeName);
                        if (nodes?.Count > 0) {
                            var list = (dynamic)x.GetValue(item);
                            //x.SetValue(item, nodes.Cast<XmlNode>()
                            //    .Select(y => PareseTest(y, Activator.CreateInstance(namespaceName, typeName).Unwrap()))
                            //    .ToList());
                            foreach (XmlNode n in nodes) {
                                var t = Type.GetType(typeName);
                                var tt = t.GetType();
                                list.Add(PareseTest(n, Activator.CreateInstance(namespaceName, typeName).Unwrap()));
                            }

                            //new List<Syndication.Model.SyndicationFeed>().Add(new object());
                        }
                    }
                    else {
                        //剩下的就是class类
                        var splite = x.PropertyType.AssemblyQualifiedName.Split(',');
                        var typeName = splite[0];
                        var namespaceName = splite[1];
                        x.SetValue(item, PareseTest(node.SelectSingleNode(x.Name), Activator.CreateInstance(namespaceName, typeName).Unwrap()));
                    }
                });
            return item;
        }

        public static TItem Parese<TItem>(XmlNode node) {
            var item = Activator.CreateInstance<TItem>();
            item.GetType().GetProperties()
                .Where(x => x.PropertyType.Name == "String")
                .ToList()
                .ForEach(x => x.SetValue(item, node.SelectSingleNode(x.Name)?.InnerText));
            return item;
        }
    }
}
