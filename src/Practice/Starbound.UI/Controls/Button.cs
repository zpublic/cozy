using Starbound.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.Controls
{
    public class Button : ContentControl
    {
        public SBColor Background { get; set; }
        public SBColor Foreground { get; set; }
        public IFontResource Font { get; set; }

        public Button()
        {
            Template = new ButtonControlTemplate();
            Template.Control = this;
        }
    }
}
