using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.Bindings
{
    public interface IValueConverter
    {
        object Convert(object value, Type type);
        object ConvertBack(object value, Type type);
    }
}
