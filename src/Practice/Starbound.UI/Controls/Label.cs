using Starbound.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.Controls
{
    public class Label : Control
    {
        public string Text { get; set; }
        public SBColor Color { get; set; }
        public IFontResource Font { get; set; }

        public Label()
        {
            Template = new DefaultLabelControlTemplate();
            Template.Control = this;
        }
    }
}
