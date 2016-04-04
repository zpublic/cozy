using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Plugin.Guide.Template.Info.Model
{
    public class TextStyle
    {
        public string Name { get; set; }
        public string Font { get; set; }
        public int TextSize { get; set; }
        public TextAlignType TextAlign { get; set; }
        public MarginInfo Margin { get; set; }
    }
}
