using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Plugin.Guide.Template.Info.Model
{
    public class TextStyle
    {
        public static TextStyle DefaultTitleInfo
        {
            get
            {
                return new TextStyle()
                {
                    Font = "微软雅黑",
                    Margin = new MarginInfo(3),
                    TextAlign = TextAlignType.Center,
                    TextSize = 24,
                };
            }
        }

        public static TextStyle DefaultTextInfo
        {
            get
            {
                return new TextStyle()
                {
                    Font = "微软雅黑",
                    Margin = new MarginInfo(3),
                    TextAlign = TextAlignType.Center,
                    TextSize = 16,
                };
            }
        }

        public string Font { get; set; }
        public int TextSize { get; set; }
        public TextAlignType TextAlign { get; set; }
        public MarginInfo Margin { get; set; }
    }
}
