using Starbound.UI.Primitives;
using Starbound.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.Controls
{
    public class DefaultButtonControlTemplate : IControlTemplate
    {
        public virtual IEnumerable<Primitive> Primitives
        {
            get
            {
                if (!(Control is Button)) return Enumerable.Empty<Primitive>();
                Button button = Control as Button;
                return new List<Primitive>()
                {
                    new BorderSprite(Application.ResourceManager.GetResource<IImageResource>("BorderFill1"), new Vector2(button.X, button.Y), new Vector2(button.ActualWidth, button.ActualHeight), new Thickness(3), new SBColor(24, 24, 24)),
                    new BorderSprite(Application.ResourceManager.GetResource<IImageResource>("BorderStroke1"), new Vector2(button.X, button.Y), new Vector2(button.ActualWidth, button.ActualHeight), new Thickness(3), new SBColor(86, 86, 86)),
                    new TextSprite(button.Content.ToString(), button.Font, new Vector2(button.X, button.Y),new Vector2(button.ActualWidth, button.ActualHeight), HorizontalAlignment.Center, VerticalAlignment.Center, new SBColor(0, 170, 222))
                };
            }
        }

        public virtual Control Control { get; set; }
    }
}
