using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using WpfCozy.B.Books.Data;

namespace WpfCozy.B.Books
{
    /// <summary>
    /// Interaction logic for BookUC.xaml
    /// </summary>
    public partial class BookUC : UserControl
    {
        public BookUC()
        {
            InitializeComponent();

            var binding = new Binding { Path = new PropertyPath("Value"), Source = slider1 };
            BindingOperations.SetBinding(scale1, ScaleTransform.ScaleXProperty, binding);
            BindingOperations.SetBinding(scale1, ScaleTransform.ScaleYProperty, binding);
        }

        private void OnShowBook(object sender, RoutedEventArgs e)
        {
            Book theBook = this.DataContext as Book;
            if (theBook != null)
                MessageBox.Show(theBook.Title, theBook.Isbn);
        }

        private void OnChangeBook(object sender, RoutedEventArgs e)
        {
            Book theBook = this.DataContext as Book;
            if (theBook != null)
            {
                theBook.Title = "Professional C# 5";
                theBook.Isbn = "978-0-470-31442-5";
            }
         
        }
    }
}
