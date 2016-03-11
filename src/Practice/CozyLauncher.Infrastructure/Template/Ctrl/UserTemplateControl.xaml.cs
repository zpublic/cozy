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

namespace CozyLauncher.Infrastructure.Template.Ctrl
{
    /// <summary>
    /// UserTemplateControl.xaml 的交互逻辑
    /// </summary>
    public partial class UserTemplateControl : UserControl
    {
        private TemplateBase CtrlTemplate { get; set; }

        public UserTemplateControl(TemplateBase template = null)
        {
            InitializeComponent();

            if(template != null)
            {
                InitTemplate(template);
            }
        }

        private void InitTemplate(TemplateBase template)
        {
            foreach(var obj in template.GetUseTemplate())
            {
                this.panel.Children.Add(obj);
            }

            //var type = template.GetType();
            //if (type == typeof(PureTextTemplate) || type == typeof(SignalPureTextTemplate))
            //{

            //}
            //else if(type == typeof(TextImageTemplate))
            //{

            //}
            //else if(type == typeof(MultiImageTextTemplate) || type == typeof(SignalTextImageTemplate))
            //{

            //}
        }
    }
}
