using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CozyLauncher.Plugin.Guide.Template
{
    public class TabTemplate : TemplateBase
    {
        public override UIElement InitTemplate(double width)
        {
            var ctrl = new TabControl();

            foreach (var info in Children)
            {
                ctrl.Items.Add(info.InitTemplate(width));
            }

            return ctrl;
        }
    }
}
