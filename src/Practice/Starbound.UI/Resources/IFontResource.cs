using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbound.UI.Resources
{
    public interface IFontResource : IResource
    {
        Vector2 Measure(string text);
    }
}
