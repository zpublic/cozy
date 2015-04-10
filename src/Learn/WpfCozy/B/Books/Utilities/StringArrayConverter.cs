using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Windows.Data;

namespace WpfCozy.B.Books.Utilities
{
  [ValueConversion(typeof(string[]), typeof(string))]
  class StringArrayConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value == null) return null;

      string[] stringCollection = (string[])value;
      string separator = parameter == null ? String.Empty : parameter.ToString();

      return String.Join(separator, stringCollection);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }

  }
}
