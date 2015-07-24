using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.Controls
{
    public interface IEnumDrawableUIElemt
    {
        IEnumerable<UIElement> GetDrawableElemt();
    }
}
