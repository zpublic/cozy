using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MMS.UI.Default
{
    public class Navigation : System.Windows.Controls.Control
    {
        private ItemsControl mItem;

        public List<NavigationItem> Source { get { return GetValue(SourceProperty) as List<NavigationItem>; } set { SetValue(SourceProperty, value); } }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(
            "Source",
            typeof(List<NavigationItem>),
            typeof(Navigation));

        public Navigation()
        {
            this.Style = (Style)Application.Current.Resources["NavigationStyle"];
            this.Loaded += Navigation_Loaded;
        }

        void Navigation_Loaded(object sender, RoutedEventArgs e)
        {
            this.mItem.DataContext = this;
            this.mItem.SetBinding(ItemsControl.ItemsSourceProperty, "Source");
        }

        public override void OnApplyTemplate()
        {
            this.mItem = (ItemsControl)this.GetTemplateChild("menus");
            base.OnApplyTemplate();
        }
    }
}
