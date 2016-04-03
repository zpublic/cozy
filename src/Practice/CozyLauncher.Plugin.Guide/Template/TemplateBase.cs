using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CozyLauncher.Plugin.Guide.Template
{
    public abstract class TemplateBase
    {
        public IList<TemplateBase> Children { get; set; } = new List<TemplateBase>();

        public abstract UIElement InitTemplate(double width);
    }
}
