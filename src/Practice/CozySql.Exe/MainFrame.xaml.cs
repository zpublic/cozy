using System.Windows;
using System.Windows.Controls;
using CozySql.Exe.Models;
using MahApps.Metro.Controls;

namespace CozySql.Exe
{
    /// <summary>
    /// Interaction logic for MainFrame.xaml
    /// </summary>
    public partial class MainFrame
    {
        public MainFrame()
        {
            InitializeComponent();
            MainTab.AddHandler(Button.ClickEvent, new RoutedEventHandler((sender, args) =>
            {
                var button = (Button)args.OriginalSource;
                //因为VS Style的TabItem结构是Grid下面套一个Button，所以拿它父节点的父节点
                //如果后面的勇士有更好的方法请继续修改
                var tabItem = button.GetParentObject().GetParentObject() as TabItem;
                if (tabItem != null)
                {
                    var info = tabItem.Content as UIControlInfo;
                    ViewModel.CloseTab(info);
                }
            }));
        }
    }
}
