using System.ComponentModel;
using System.Collections.Generic;

namespace WpfCozy.B.Books.Data
{
  public class Book : BindableObject
  {
    public Book(string title, string publisher, string isbn, params string[] authors)
    {
      this.title = title;
      this.publisher = publisher;
      this.isbn = isbn;
      this.authors.AddRange(authors);
    }
    public Book()
      : this("unknown", "unknown", "unknown")
    {
    }


    private string title;
    public string Title
    {
      get
      {
        return title;
      }
      set
      {
        SetProperty(ref title, value);
      }
    }
    private string publisher;
    public string Publisher
    {
      get
      {
        return publisher;
      }
      set
      {
        SetProperty(ref publisher, value);
      }
    }
    private string isbn;
    public string Isbn
    {
      get
      {
        return isbn;
      }
      set
      {
        SetProperty(ref isbn, value);
      }
    }

    private readonly List<string> authors = new List<string>();
    public string[] Authors
    {
      get
      {
        return authors.ToArray();
      }
    }

    public override string ToString()
    {
      return this.Title;
    }
  }
}
