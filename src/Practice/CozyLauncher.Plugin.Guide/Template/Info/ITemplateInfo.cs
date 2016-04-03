using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CozyLauncher.Plugin.Guide.Template.Info
{
    public interface ITemplateInfo
    {
        UIElement GetInfoObject(double width);
    }
}
