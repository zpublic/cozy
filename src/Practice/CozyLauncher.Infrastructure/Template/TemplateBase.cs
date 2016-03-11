using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CozyLauncher.Infrastructure.Template
{
    public abstract class TemplateBase
    {
        public abstract IList<FrameworkElement> GetUseTemplate();
    }
}
