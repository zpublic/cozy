using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Plugin.Guide.Template
{
    public class TemplateJsonObject
    {
        public string ObjectType { get; set; }

        public string Content { get; set; }

        public IEnumerable<string> Children { get; set; }
    }
}
