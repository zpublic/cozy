using System.Windows.Input;


namespace WpfCozy.B.Books
{
  public static class BooksCommands
  {
    private static RoutedUICommand showBook;
    public static ICommand ShowBook
    {
      get
      {
        return showBook ?? (showBook = new RoutedUICommand("Show Book", "ShowBook", typeof(BooksCommands)));
      }
    }

    private static RoutedUICommand showBooksList;
    public static ICommand ShowBooksList
    {
      get
      {
        if (showBooksList == null)
        {
          showBooksList = new RoutedUICommand("Show Books", "ShowBooks", typeof(BooksCommands));
          showBook.InputGestures.Add(new KeyGesture(Key.B, ModifierKeys.Alt));
        }
        return showBooksList;
      }
    }

    private static RoutedUICommand showBooksGrid;
    public static ICommand ShowBooksGrid
    {
      get
      {
        if (showBooksGrid == null)
        {
          showBooksGrid = new RoutedUICommand("Show Books Grid", "ShowBooksGrid", typeof(BooksCommands));
        }
        return showBooksGrid;
      }
    }

    private static RoutedUICommand showAuthors;
    public static ICommand ShowAuthors
    {
      get
      {
        return showAuthors ?? (showAuthors = new RoutedUICommand("Show Authors", "ShowAuthors", typeof(BooksCommands)));
      }
    }
  }
}
