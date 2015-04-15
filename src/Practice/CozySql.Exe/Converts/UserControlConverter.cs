using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using CozySql.Exe.Models;
using CozySql.Model;

namespace CozySql.Exe.Converts
{
    public class UserControlConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var dict = (Dictionary<string, UserControlEnum>)value;
            if (dict.Keys.Count > 0)
            {
                return dict.Keys.Select(key => new UIControlInfo
                {
                     Title = key, Content = UserControlsAdapter.GetUserControl(dict[key])
                }).ToList();
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
