using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MMS.UI.Default
{
    public class StatusConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            NavigationType? status = value as NavigationType?;
            if (status != null)
            {
                switch (status)
                {
                    case NavigationType.Wait:
                        {
                            return "";
                        }
                    case NavigationType.Process:
                        {
                            return "/MMS.UI;Component/Default/Navigation/Images/wi_Configuring_12x12.png";
                        }
                    case NavigationType.Complete:
                        {
                            return "/MMS.UI;Component/Default/Navigation/Images/wi_Configured_12x12.png";
                        }
                    default:
                        {
                            return "";
                        }
                }
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
