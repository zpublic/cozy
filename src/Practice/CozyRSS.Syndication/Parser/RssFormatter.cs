using System;
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

        public T Formatter<T>() {
            return Parese<T>(rootNode, Activator.CreateInstance<T>());
        }

        private T Parese<T>(XmlNode node, T obj) {
            var item = obj;
            item.GetType().GetProperties()
                .ToList()
                .ForEach(x => {
                    if (x.PropertyType.Name == "String") {
                        //第一种String类型
                        x.SetValue(item, node.SelectSingleNode(x.Name)?.InnerText);
                    }
                    else if (x.PropertyType.Name.StartsWith("List")) {
                        //第二种List<T>类型
                        var splite = x.PropertyType.AssemblyQualifiedName.Split(',');
                        var typeName = splite[0].Substring(splite[0].IndexOf('[') + 2);
                        var assemblyName = splite[1];
                        var nodeName = x.Name.Substring(0, x.Name.Length - 1);
                        var nodes = node.SelectNodes(nodeName);
                        if (nodes?.Count > 0) {
                            var list = (dynamic)x.GetValue(item);
                            var array = list.ToArray().GetType().GetConstructors()[0].Invoke(new object[] { nodes.Count });
                            var listIttemType = Activator.CreateInstance(assemblyName, typeName).Unwrap();
                            for (int i = 0; i < nodes.Count; i++) {
                                array[i] = (dynamic)(listIttemType.GetType().GetConstructors()[0].Invoke(null));
                                var source = Parese(nodes[i], Activator.CreateInstance(assemblyName, typeName).Unwrap());
                                var sourceType = source.GetType();
                                var properties = sourceType.GetProperties()
                                    .Where(y => array[i].GetType().GetProperty(y.Name) != null)
                                    .ToList();
                                foreach (var propertyInfo in properties) {
                                    array[i].GetType().GetProperty(propertyInfo.Name)
                                        .SetValue(array[i], propertyInfo.GetValue(source));
                                }
                                list.Add(array[i]);
                            }
                            x.SetValue(item, list);
                        }
                    }
                    else {
                        //剩下的就是class类型
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
