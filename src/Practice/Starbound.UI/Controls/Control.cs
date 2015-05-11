using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.Controls
{
    public class Control : UIElement
    {
        public IControlTemplate Template { get; set; }
    }
}
