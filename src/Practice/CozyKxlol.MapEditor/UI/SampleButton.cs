using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.MapEditor.OperateLayer
{
    class SampleButton : Starbound.UI.Controls.Button
    {
        private Random random = new Random();

        public SampleButton(double x, double y)
        {
            PreferredHeight = 80;
            PreferredWidth = 150;
            Margin = new Starbound.UI.Thickness(3, 3, 0, 0);
            Font = Starbound.UI.Application.ResourceManager.GetResource<Starbound.UI.Resources.IFontResource>("Font");
            Content = "SampleButton";
            Background = new Starbound.UI.SBColor(random.NextDouble(), random.NextDouble(), random.NextDouble());
            Foreground = new Starbound.UI.SBColor(random.NextDouble(), random.NextDouble(), random.NextDouble());
            X = x;
            Y = y;
        }
    }
}
