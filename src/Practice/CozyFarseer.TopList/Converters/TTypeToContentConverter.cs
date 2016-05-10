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
    public class TTypeToContentConverter : IValueConverter
    {
        private readonly string[] TTypeDesc = { "未知", "具体看涨", "具体看跌", "纯看涨", "纯看跌" };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var t = (int)value;
            if (t >= -1 && t < TTypeDesc.Length - 1)
            {
                return TTypeDesc[t + 1];
            }
            return TTypeDesc[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
