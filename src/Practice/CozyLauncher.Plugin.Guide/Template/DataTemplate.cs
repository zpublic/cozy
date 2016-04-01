using CozyLauncher.Plugin.Guide.Template.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CozyLauncher.Plugin.Guide.Template
{
    public class DataTemplate : TemplateBase
    {
        public ITemplateInfo Info { get; set; }

        public override FrameworkElement InitTemplate()
        {
            return Info.GetInfoObject();
        }
    }
}
