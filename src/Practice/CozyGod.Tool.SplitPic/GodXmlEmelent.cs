using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CozyGod.Tool.SplitPic
{
    [XmlType("Background")]
    public class GodXmlEmelent
    {
        [XmlElement]
        public Size Size { get; set; }

        [XmlElement(ElementName = "Layer")]
        public List<Layer> Layers { get; set; }
    }

    [XmlType(TypeName = "Size")]
    public class Size
    {
        [XmlAttribute("width")]
        public int Width { get; set; }

        [XmlAttribute("height")]
        public int Height { get; set; }
    }

    [XmlType(TypeName = "Layer")]
    public class Layer
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("path")]
        public string Path { get; set; }

        [XmlAttribute("x")]
        public int X { get; set; }

        [XmlAttribute("y")]
        public int Y { get; set; }
    }

}
