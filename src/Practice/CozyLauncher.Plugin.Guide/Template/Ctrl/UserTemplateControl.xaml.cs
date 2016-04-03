using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CozyLauncher.Plugin.Guide.Template.Ctrl
{
    /// <summary>
    /// UserTemplateControl.xaml 的交互逻辑
    /// </summary>
    public partial class UserTemplateControl : StackPanel
    {
        public static readonly DependencyProperty RootTemplateProperty
            = DependencyProperty.RegisterAttached("RootTemplate", typeof(TemplateBase), typeof(UserTemplateControl), 
                new PropertyMetadata(null));

        public UserTemplateControl()
        {
            this.Loaded += (s, e) =>
            {
                AdjustTemplate();
            };
        }

        public TemplateBase RootTemplate
        {
            get
            {
                return (TemplateBase)this.GetValue(RootTemplateProperty);
            }
            set
            {
                this.SetValue(RootTemplateProperty, value);
            }
        }

        private void AdjustTemplate()
        {
            if (RootTemplate != null)
            {
                this.Children.Add(RootTemplate.InitTemplate((int)this.Width));
            }
        }
    }
}
