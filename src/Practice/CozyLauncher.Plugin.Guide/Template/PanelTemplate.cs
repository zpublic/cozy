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
    public class PanelTemplate : TemplateBase
    {
        public bool IsHorizontal { get; set; }

        public override UIElement InitTemplate(double width)
        {
            var panel = new StackPanel();
            panel.HorizontalAlignment   = HorizontalAlignment.Center;
            panel.VerticalAlignment     = VerticalAlignment.Center;
            panel.Orientation = IsHorizontal ? Orientation.Horizontal : Orientation.Vertical;

            foreach(var info in Children)
            {
                panel.Children.Add(info.InitTemplate(width));
            }
            return panel; 
        }
    }
}
