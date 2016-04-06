using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MMS.UI.Default
{
    public class BorderConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            NavigationType? status = value as NavigationType?;
            if (status != null)
            {
                switch (status)
                {
                    case NavigationType.Wait:
                    case NavigationType.Complete:
                        {
                            Thickness border = new Thickness(0, 0, 0, 0);
                            return border;
                        }
                    case NavigationType.Process:
                        {
                            Thickness border = new Thickness(0, 1, 0, 1);
                            return border;
                        }
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
