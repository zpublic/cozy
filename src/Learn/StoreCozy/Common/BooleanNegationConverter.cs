using System;
using Windows.UI.Xaml.Data;

namespace StoreCozy.Common
{
    /// <summary>
    /// 从 true 转换为 false 以及进行相反转换的值转换器。
    /// </summary>
    public sealed class BooleanNegationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !(value is bool && (bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return !(value is bool && (bool)value);
        }
    }
}
