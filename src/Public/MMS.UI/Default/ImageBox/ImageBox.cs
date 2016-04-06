using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MMS.UI.Default
{
    public class ImageBox : System.Windows.Controls.Control
    {
        private ItemsControl mItems;

        public ImageBox()
        {
            this.Style = (Style)Application.Current.Resources["ImageBoxStyle"];
            this.Loaded += ImageBox_Loaded;
        }

        void ImageBox_Loaded(object sender, RoutedEventArgs e)
        {
            this.mItems.DataContext = this;
            this.mItems.SetBinding(ItemsControl.ItemsSourceProperty, "Source");
        }

        public override void OnApplyTemplate()
        {
            this.mItems = (ItemsControl)this.GetTemplateChild("imageItems");
            base.OnApplyTemplate();
        }

        public List<ImageInfo> Source { get { return GetValue(SourceProperty) as List<ImageInfo>; } set { SetValue(SourceProperty, value); } }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(
            "Source",
            typeof(List<ImageInfo>),
            typeof(ImageBox));
    }
}
