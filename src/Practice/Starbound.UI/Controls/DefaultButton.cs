using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starbound.UI.Resources;

namespace Starbound.UI.Controls
{
    public class DefaultButton : ContentControl
    {
        public SBColor Background { get; set; }
        public SBColor Foreground { get; set; }
        public IFontResource Font { get; set; }

        public DefaultButton()
        {
            Template = new ButtonControlTemplate();
            Template.Control = this;
        }
    }
}
