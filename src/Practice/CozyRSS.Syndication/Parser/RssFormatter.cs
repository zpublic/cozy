using System;
using System.Linq;
using System.Xml;

namespace CozyRSS.Syndication.Parser {

    public class RssFormatter {

        private XmlNode rootNode;

        public T Formatter<T>(string root, Uri url) {
            var reader = XmlReader.Create(url.AbsoluteUri);
            var doc = new XmlDocument();
            doc.Load(reader);
            rootNode = doc.SelectSingleNode(root);
            return Parese(rootNode, Activator.CreateInstance<T>());
        }

        public T Formatter<T>(string root, string xml) {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            rootNode = doc.SelectSingleNode(root);
            return Parese(rootNode, Activator.CreateInstance<T>());
        }

        private T Parese<T>(XmlNode node, T obj) {
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
                            list.Add((dynamic)Parese(n, Activator.CreateInstance(assemblyName, typeName).Unwrap()));
                        }
                        x.SetValue(item, list);
                    }
                    else {
                        var splite = x.PropertyType.AssemblyQualifiedName.Split(',');
                        var typeName = splite[0];
                        var assemblyName = splite[1];
                        x.SetValue(item, Parese(node.SelectSingleNode(x.Name), Activator.CreateInstance(assemblyName, typeName).Unwrap()));
                    }
                });
            return item;
        }
    }
}
