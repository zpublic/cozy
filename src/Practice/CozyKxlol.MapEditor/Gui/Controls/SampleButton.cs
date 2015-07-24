using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.MapEditor.Gui.Controls
{
    class SampleButton : Starbound.UI.Controls.DefaultButton
    {
        private Random random = new Random();

        public SampleButton(double x, double y)
        {
            PreferredHeight = 50;
            PreferredWidth = 150;
            Margin = new Starbound.UI.Thickness(3, 3, 0, 0);
            Font = Starbound.UI.Application.ResourceManager.GetResource<Starbound.UI.Resources.IFontResource>("Font");
            Background = new Starbound.UI.SBColor(random.NextDouble(), random.NextDouble(), random.NextDouble());
            Foreground = new Starbound.UI.SBColor(random.NextDouble(), random.NextDouble(), random.NextDouble());
            X = x;
            Y = y;
        }
    }
}
