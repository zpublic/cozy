using System.Windows;
using System.Windows.Controls;
using WpfCozy.B.Books.Data;

namespace WpfCozy.B.Books.Utilities
{
    public class BookTemplateSelector : DataTemplateSelector
    {
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            if (item != null && item is Book)
            {
                var book = item as Book;
                switch (book.Publisher)
                {
                    case "Wrox Press":
                        return (container as FrameworkElement).FindResource("wroxTemplate") as DataTemplate;
                    case "For Dummies":
                        return (container as FrameworkElement).FindResource("dummiesTemplate") as DataTemplate;
                    default:
                        return (container as FrameworkElement).FindResource("bookTemplate") as DataTemplate;
                }
            }
            return null;          
        }
    }
}
