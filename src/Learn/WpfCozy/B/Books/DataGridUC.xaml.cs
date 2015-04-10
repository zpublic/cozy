using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfCozy.B.Books.Data;

namespace WpfCozy.B.Books
{
    /// <summary>
    /// Interaction logic for DataGridUC.xaml
    /// </summary>
    public partial class DataGridUC : UserControl
    {
        private ListCollectionView view;
        public DataGridUC()
        {
            view = new ListCollectionView(new BookFactory().GetBooks() as System.Collections.IList);
            InitializeComponent();
            // this.grid1.DataContext = view;            
        }
        public System.Collections.IEnumerable BooksView
        {
            get
            {
                return view;
            }
        }

        private void OnGroupChecked(object sender, RoutedEventArgs e)
        {
            if (view.CanGroup)
            {
                if (view.GroupDescriptions == null || view.GroupDescriptions.Count == 0)
                {
                    // view.GroupDescriptions = new System.Collections.ObjectModel.ObservableCollection<System.ComponentModel.GroupDescription>();
                    view.GroupDescriptions.Add(new PropertyGroupDescription("Publisher"));
                }
                else
                {
                    view.GroupDescriptions.Clear();
                }
            }

        }
    }
}
