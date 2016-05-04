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
    public class TStatusToContentConverter : IValueConverter
    {
        private readonly string[] TStatusDesc = { "未知", "进行中", "成功", "失败" };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (int)value;
            if (v >= -1 && v < TStatusDesc.Length - 1)
            {
                return TStatusDesc[v + 1];
            }
            return TStatusDesc[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
