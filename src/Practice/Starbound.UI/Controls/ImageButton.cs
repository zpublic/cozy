using Starbound.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.Controls
{
    public class ImageButton : ContentControl
    {
        public ImageButton()
        {
            Template = new DefaultImageButtonControlTemplate();
            Template.Control = this;
        }
    }
}
