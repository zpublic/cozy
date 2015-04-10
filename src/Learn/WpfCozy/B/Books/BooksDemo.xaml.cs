using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfCozy.B.Books.Data;

namespace WpfCozy.B.Books
{
    /// <summary>
    /// Interaction logic for BookDemo.xaml
    /// </summary>
    public partial class BooksDemo : RibbonWindow
    {
        public BooksDemo()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private ObservableCollection<UIControlInfo> userControls = new ObservableCollection<UIControlInfo>();
        public IEnumerable<UIControlInfo> Controls
        {
            get
            {
                return userControls;
            }
        }

        private void OnClose(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //private void OnShowBook(object sender, ExecutedRoutedEventArgs e)
        //{
        //    var bookUI = new BookUC();
        //    // bookUI.DataContext = new Book { Title = "Professional C# 2018", Publisher = "Wrox Press", Isbn = "978-0-470-19137-8" };
        //    this.tabControl1.SelectedIndex = this.tabControl1.Items.Add(new TabItem { Header = "Book", Content = bookUI });
        //}

        private void OnShowBook(object sender, RoutedEventArgs e)
        {
            var bookUI = new BookUC();
            bookUI.DataContext = new Book { Title = "Professional C# 2008", Publisher = "Wrox Press", Isbn = "978-0-470-19137-8" };
            this.tabControl1.SelectedIndex = this.tabControl1.Items.Add(new TabItem { Header = "Book", Content = bookUI });
        }

        private void OnShowBooks(object sender, RoutedEventArgs e)
        {
            var booksUI = new BooksUC();
            this.tabControl1.SelectedIndex = this.tabControl1.Items.Add(new TabItem { Header = "Books", Content = booksUI });
        }

        private void OnShowGrid(object sender, RoutedEventArgs e)
        {
            var gridUI = new DataGridUC();
            this.tabControl1.SelectedIndex = this.tabControl1.Items.Add(new TabItem { Header = "DataGrid", Content = gridUI });
        }

        private void OnShowBook(object sender, ExecutedRoutedEventArgs e)
        {
            var bookUI = new BookUC();
            bookUI.DataContext = new Book { Title = "Professional C# 4 and .NET 4", Publisher = "Wrox Press", Isbn = "978-0-470-50225-9" };
            userControls.Add(new UIControlInfo { Title = "Book", Content = bookUI });
        }

        private void OnShowBooksList(object sender, ExecutedRoutedEventArgs e)
        {
            var booksUI = new BooksUC();
            userControls.Add(new UIControlInfo { Title = "Books List", Content = booksUI });
        }

        private void OnShowBooksGrid(object sender, ExecutedRoutedEventArgs e)
        {
            var booksGrid = new DataGridUC();
            userControls.Add(new UIControlInfo { Title = "Books Grid", Content = booksGrid });
        }
    }
}
