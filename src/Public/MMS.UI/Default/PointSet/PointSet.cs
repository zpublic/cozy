using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MMS.UI.Default
{
    public class PointSet:System.Windows.Controls.Control
    {
        private ItemsControl mItem;

        public List<Point> Source
        {
            get { return GetValue(SourceProperty) as List<Point>; }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(List<Point>), typeof(PointSet), null);

        public PointSet()
        {
            this.Style = (Style)Application.Current.Resources["PointSetStyle"];
            this.Loaded += PointSet_Loaded;
        }

        void PointSet_Loaded(object sender, RoutedEventArgs e)
        {
            this.mItem.DataContext = this;
            this.mItem.SetBinding(ItemsControl.ItemsSourceProperty, "Source");
        }

        public override void OnApplyTemplate()
        {
            this.mItem = (ItemsControl)this.GetTemplateChild("items");
            base.OnApplyTemplate();
        }
    }
}
