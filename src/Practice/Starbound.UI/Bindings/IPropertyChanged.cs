using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.Bindings
{
    public interface IPropertyChanged
    {
        event Action<string> PropertyChanged;
    }
}
