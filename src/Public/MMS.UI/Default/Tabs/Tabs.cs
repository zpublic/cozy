using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MMS.UI.Default
{
    public class Tabs : System.Windows.Controls.Control
    {
        private TabControl mTabController;

        public Tabs()
        {
            this.Style = (Style)Application.Current.Resources["TabsStyle"];
        }

        public override void OnApplyTemplate()
        {
            this.mTabController = (TabControl)this.GetTemplateChild("tabController");
            base.OnApplyTemplate();
        }

        public void Add(string title, System.Windows.Controls.Control control)
        {
            TabItem item = new TabItem();
            item.Header = title;
            item.FontSize = 12;
            item.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF3CD"));
            item.BorderThickness = new Thickness(2);
            StackPanel panel = new StackPanel();
            panel.Children.Add(control);
            item.Content = panel;
            this.mTabController.Items.Add(item);
        }
    }
}
