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
using CozyLauncher.Infrastructure.Template;
using CozyLauncher.Infrastructure.Template.Ctrl;
using System.IO;

namespace CozyLauncher.Plugin.Guide.GuidePage.PageInfo
{
    /// <summary>
    /// TestPage.xaml 的交互逻辑
    /// </summary>
    public partial class TestPage : UserControl
    {
        public TestPage()
        {
            InitializeComponent();

            var uri = new Uri("/CozyLauncher.Plugin.Guide;component/Resources/5.png", UriKind.RelativeOrAbsolute);
            panel.Children.Add(new UserTemplateControl(new TextImageTemplate(true, new BitmapImage(uri),
            new List<TextInfo>()
                    {
                        new TextInfo()
                        {
                            Text = "testTitle",
                            Font = "微软雅黑",
                            TextAlign = TextAlignType.Center,
                            TextSize = 24,
                            Margin = new MarginInfo(10),
                        },
                        new TextInfo()
                        {
                            Text = "testText",
                            Font = "微软雅黑",
                            TextAlign = TextAlignType.Center,
                            TextSize = 16,
                            Margin = new MarginInfo(20),
                        },
                    }
                )));
        }
    }
}
