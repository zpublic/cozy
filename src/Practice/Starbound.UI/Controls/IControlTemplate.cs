using Starbound.UI.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.Controls
{
    public interface IControlTemplate
    {
        IEnumerable<Primitive> Primitives { get; }
        Control Control { get; set; }
    }
}
