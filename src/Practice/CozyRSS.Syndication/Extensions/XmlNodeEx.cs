using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CozyRSS.Syndication.Extensions {

    public static class XmlNodeEx {

        public static XmlNode SelectSingleNodeEx(this XmlNode node, string name) {
            return node?.Cast<XmlNode>().FirstOrDefault(x => x.Name == name);
        }

        public static List<XmlNode> SelectNodesEx(this XmlNode node, string name) {
            return node?.Cast<XmlNode>().Where(x => x.Name == name).ToList();
        }
    }
}
