using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using CozyLauncher.ResourcesMgr;

namespace CozyLauncher.Converters
{
    public class NameToDataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var name = value as string;
            if (name != null)
            {
                if (targetType == typeof(ImageSource))
                {
                    ImageSource img = ImageResourceMgr.Instance.Load(name);
                    return img;
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
