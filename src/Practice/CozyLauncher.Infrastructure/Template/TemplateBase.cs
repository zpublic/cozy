using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CozyLauncher.Infrastructure.Template
{
    public abstract class TemplateBase
    {
        public List<TemplateBase> Children { get; set; } = new List<TemplateBase>();

        public abstract FrameworkElement InitTemplate();
    }
}
