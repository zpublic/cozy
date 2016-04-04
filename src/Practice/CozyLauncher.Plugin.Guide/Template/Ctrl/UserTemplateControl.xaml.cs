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
using Newtonsoft.Json;

namespace CozyLauncher.Plugin.Guide.Template.Ctrl
{
    /// <summary>
    /// UserTemplateControl.xaml 的交互逻辑
    /// </summary>
    public partial class UserTemplateControl : StackPanel
    {
        public static readonly DependencyProperty RootTemplateProperty = 
            DependencyProperty.RegisterAttached("RootTemplate", typeof(TemplateBase), typeof(UserTemplateControl), new PropertyMetadata(null));

        public static readonly DependencyProperty TextTemplateProperty =
           DependencyProperty.RegisterAttached("TextTemplate", typeof(string), typeof(UserTemplateControl), new PropertyMetadata(null));


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

        public string TextTemplate
        {
            get
            {
                return (string)this.GetValue(TextTemplateProperty);
            }
            set
            {
                this.SetValue(TextTemplateProperty, value);
            }
        }

        private UIElement TemplateStorage  { get; set; }
        private void AdjustTemplate()
        {
            try
            {
                if (RootTemplate != null && TextTemplate != null)
                {
                    throw new Exception("RootTemplate and TextTemplate cannot  be used together");
                }
                else if (RootTemplate != null)
                {
                    this.Children.Add(RootTemplate.InitTemplate((int)this.Width));
                }
                else if (!string.IsNullOrEmpty(TextTemplate))
                {
                    if(TemplateStorage == null)
                    {
                        var t = TemplateJsonConverter.Deserialization(TextTemplate);
                        TemplateStorage = t.InitTemplate((int)this.Width);
                    }

                    this.Children.Add(TemplateStorage);
                }
            }
            catch(Exception)
            {
                // TODO Deserialization Failed
            }
        }
    }
}
