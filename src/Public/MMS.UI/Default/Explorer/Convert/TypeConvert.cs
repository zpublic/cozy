using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MMS.UI.Default
{
    public class TypeConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ExplorerItemType? type = value as ExplorerItemType?;
            if (type != null)
            {
                switch (type)
                {
                    case ExplorerItemType.Server:
                        {
                            return "/MMS.UI;Component/Default/Explorer/Images/Database.ico";
                        }
                    default:
                        {
                            return "/MMS.UI;Component/Default/Explorer/Images/folder.ico";
                        }
                }
            }
            return "/MMS.UI;Component/Default/Explorer/Images/folder.ico";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
