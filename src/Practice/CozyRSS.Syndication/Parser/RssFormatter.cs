using System;
using System.Linq;
using System.Xml;

namespace CozyRSS.Syndication.Parser {

    public class RssFormatter {

        public static TItem Parese<TItem>(XmlNode node) {
            var item = Activator.CreateInstance<TItem>();
            item.GetType().GetProperties()
                .Where(x => !x.PropertyType.Name.StartsWith("ICollection"))
                .ToList()
                .ForEach(x => x.SetValue(item, node.SelectSingleNode(nameof(x.Name))?.InnerText));
            return item;
        }
    }
}
