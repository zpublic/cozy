using System;
using System.Linq;
using System.Xml;

namespace CozyRSS.Syndication.Parser {

    public class RssFormatter {

        public T Formatter<T>(string root, Uri url) {
            var reader = XmlReader.Create(url.AbsoluteUri);
            var doc = new XmlDocument();
            doc.Load(reader);
            return Parse(doc.SelectSingleNode(root), Activator.CreateInstance<T>());
        }

        public T Formatter<T>(string root, string xml) {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            return Parse(doc.SelectSingleNode(root), Activator.CreateInstance<T>());
        }

        private T Parse<T>(XmlNode node, T obj) {
            var item = obj;
            item.GetType().GetProperties()
                .ToList()
                .ForEach(x => {
                    if (x.PropertyType.Name == "String") {
                        x.SetValue(item, node.SelectSingleNode(x.Name)?.InnerText);
                    }
                    else if (x.PropertyType.Name.StartsWith("List")) {
                        var splite = x.PropertyType.AssemblyQualifiedName.Split(',');
                        var typeName = splite[0].Substring(splite[0].IndexOf('[') + 2);
                        var assemblyName = splite[1];
                        var nodes = node.SelectNodes(x.Name.Substring(0, x.Name.Length - 1));
                        nodes = nodes?.Count > 0 ? nodes : node.SelectSingleNode(x.Name).ChildNodes;
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
                        x.SetValue(item, Parse(node.SelectSingleNode(x.Name), Activator.CreateInstance(assemblyName, typeName).Unwrap()));
                    }
                });
            return item;
        }
    }
}
