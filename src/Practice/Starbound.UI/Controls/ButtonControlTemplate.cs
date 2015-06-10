using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starbound.UI.Primitives;
using Starbound.UI.Resources;

namespace Starbound.UI.Controls
{
    public class ButtonControlTemplate : IControlTemplate
    {
        public virtual IEnumerable<Primitive> Primitives
        {
            get
            {
                if (!(Control is Button)) return Enumerable.Empty<Primitive>();
                Button button = Control as Button;
                return new List<Primitive>()
                {
                    new BorderSprite(Application.ResourceManager.GetResource<IImageResource>("BorderFill1"), new Vector2(button.X, button.Y), new Vector2(button.ActualWidth, button.ActualHeight), new Thickness(3), button.Background),
                    new BorderSprite(Application.ResourceManager.GetResource<IImageResource>("BorderStroke1"), new Vector2(button.X, button.Y), new Vector2(button.ActualWidth, button.ActualHeight), new Thickness(3), button.Foreground),
                    new TextSprite(button.Content.ToString(), button.Font, new Vector2(button.X, button.Y),new Vector2(button.ActualWidth, button.ActualHeight), HorizontalAlignment.Center, VerticalAlignment.Center, new SBColor(255, 255, 255))
                };
            }
        }

        public virtual Control Control { get; set; }
    }
}
