using Starbound.UI.Primitives;
using Starbound.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.Controls
{
    public class DefaultImageButtonControlTemplate : IControlTemplate
    {
        public virtual IEnumerable<Primitive> Primitives
        {
            get
            {
                if (!(Control is ImageButton)) return Enumerable.Empty<Primitive>();
                ImageButton button = Control as ImageButton;
                return new List<Primitive>()
                {
                    new BorderSprite(Application.ResourceManager.GetResource<IImageResource>("BorderFill1"), new Vector2(button.X, button.Y), new Vector2(button.ActualWidth, button.ActualHeight), new Thickness(3), new SBColor(24, 24, 24)),
                    new BorderSprite(Application.ResourceManager.GetResource<IImageResource>("BorderStroke1"), new Vector2(button.X, button.Y), new Vector2(button.ActualWidth, button.ActualHeight), new Thickness(3), new SBColor(86, 86, 86)),
                    new Sprite(
                        Application.ResourceManager.GetResource<IImageResource>(button.Content as string), 
                        new Vector2(button.X, button.Y), 
                        new Vector2(button.ActualWidth, button.ActualHeight), 
                        new SBColor(0, 170, 222))};
            }
        }

        public virtual Control Control { get; set; }
    }
}
