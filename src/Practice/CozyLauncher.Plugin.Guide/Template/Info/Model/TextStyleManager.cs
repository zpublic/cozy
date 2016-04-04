using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CozyLauncher.Plugin.Guide.Template.Info.Model
{
    public class TextStyleManager
    {
        public static TextStyleManager Instance { get; set; } = new TextStyleManager();

        private static TextStyle defaultStyle;
        public static TextStyle DefaultStyle
        {
            get
            {
                return defaultStyle = defaultStyle ?? new TextStyle()
                {
                    Name = "DefaultStyle",
                    Font = "微软雅黑",
                    Margin = new MarginInfo(10),
                    TextAlign = TextAlignType.Center,
                    TextSize = 16,
                };
            }
        }

        private TextStyleCollection StyleCollection { get; set; }

        public TextStyleManager()
        {
            using (var fs = new FileStream("Resources/TextStyle.json", FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    StyleCollection = JsonConvert.DeserializeObject<TextStyleCollection>(reader.ReadToEnd());
                }
            }
        }

        public TextStyle GetStyle(string name)
        {
            var res = StyleCollection?.StyleList?.Where(x => x.Name == name).First();
            return res ?? DefaultStyle;
        }
    }
}
