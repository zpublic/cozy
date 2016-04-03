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
using System.IO;
using CozyLauncher.Plugin.Guide.Template;
using CozyLauncher.Plugin.Guide.Template.Info;
using CozyLauncher.Plugin.Guide.Template.Info.Model;

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
        }

        public static TemplateBase TestData
        {
            get
            {
                var mainPanel = new PanelTemplate();

                var data1 = new Template.DataTemplate();
                data1.Info = new ImageInfo() { Path = "Resources/5.png" };
                mainPanel.Children.Add(data1);

                var data2 = new Template.DataTemplate();
                data2.Info = new TextInfo()
                {
                    Text = "testTitle",
                    Font = "微软雅黑",
                    TextAlign = TextAlignType.Center,
                    TextSize = 24,
                    Margin = new MarginInfo(10),
                };
                mainPanel.Children.Add(data2);

                var data3 = new Template.DataTemplate();
                data3.Info = new TextInfo()
                {
                    Text = "testText",
                    Font = "微软雅黑",
                    TextAlign = TextAlignType.Center,
                    TextSize = 16,
                    Margin = new MarginInfo(20),
                };
                mainPanel.Children.Add(data3);

                var tab = new TabTemplate();
                var data4 = new Template.DataTemplate();
                data4.Info = new TabInfo()
                {
                    Header = "test1",
                    Content = new TextInfo()
                    {
                        Text = "test1 content",
                        Font = "微软雅黑",
                        TextAlign = TextAlignType.Center,
                        TextSize = 16,
                        Margin = new MarginInfo(20),
                    },
                };
                tab.Children.Add(data4);

                var data5 = new Template.DataTemplate();
                data5.Info = new TabInfo()
                {
                    Header = "test2",
                    Content = new TextInfo()
                    {
                        Text = "test2 content",
                        Font = "微软雅黑",
                        TextAlign = TextAlignType.Center,
                        TextSize = 16,
                        Margin = new MarginInfo(20),
                    },
                };
                tab.Children.Add(data5);

                mainPanel.Children.Add(tab);

                return mainPanel;
            }
        }

        public static string TestTextData
        {
            get
            {
                if(File.Exists("test.json"))
                {
                    using (var fs = new FileStream("test.json", FileMode.Open))
                    {
                        using (var reader = new StreamReader(fs))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
                return string.Empty;
            }
        }
    }
}
