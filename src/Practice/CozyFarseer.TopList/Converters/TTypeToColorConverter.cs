using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CozyFarseer.TopList.Converters
{
    public class TTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (int)value;
            if (v == 0 || v == 3)
            {
                return new SolidColorBrush(Color.FromRgb(102, 181, 124));
            }
            else if (v == 1 || v == 2)
            {
                return new SolidColorBrush(Color.FromRgb(248, 126, 137));
            }
            else
            {
                return new InvalidOperationException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
