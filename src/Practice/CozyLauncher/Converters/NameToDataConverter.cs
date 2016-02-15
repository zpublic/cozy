using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace CozyLauncher.Converters
{
    public class NameToDataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var name = value as string;
            if(name != null)
            {
                ResourceKey resKey = null;
                switch(name)
                {
                    case "app":
                        resKey = IconRes.appDrawingImageKey;
                        break;
                    case "baidu":
                        resKey = IconRes.baiduDrawingImageKey;
                        break;
                    case "exit":
                        resKey = IconRes.exitDrawingImageKey;
                        break;
                    case "folder_open":
                        resKey = IconRes.folder_openDrawingImageKey;
                        break;
                    case "help":
                        resKey = IconRes.helpDrawingImageKey;
                        break;
                    case "setting":
                        resKey = IconRes.settingDrawingImageKey;
                        break;
                    default:
                        resKey = IconRes.defaultDrawingImageKey;
                        break;
                }
                if(resKey != null)
                {
                    return Application.Current.TryFindResource(resKey);
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
