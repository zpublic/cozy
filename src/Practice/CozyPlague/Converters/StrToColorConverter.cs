using CozyPlague.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CozyPlague.Converters
{
    public class StrToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var obj = value as UserColor;
            if (obj != null && !string.IsNullOrEmpty(obj.RGB) && obj.RGB.Length == 6)
            {
                byte r = System.Convert.ToByte(obj.RGB.Substring(0, 2), 16);
                byte g = System.Convert.ToByte(obj.RGB.Substring(2, 2), 16);
                byte b = System.Convert.ToByte(obj.RGB.Substring(4, 2), 16);

                return new SolidColorBrush(Color.FromRgb(r, g, b));
            }
            return Brushes.WhiteSmoke;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
