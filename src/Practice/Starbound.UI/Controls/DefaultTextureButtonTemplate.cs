using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starbound.UI.Primitives;
using Starbound.UI.Resources;

namespace Starbound.UI.Controls
{
    public class DefaultTextureButtonTemplate : IControlTemplate
    {
        public virtual IEnumerable<Primitive> Primitives
        {
            get
            {
                if (!(Control is TextureButton)) return Enumerable.Empty<Primitive>();
                TextureButton button = Control as TextureButton;
                return new List<Primitive>()
                {
                    new BorderSprite(Application.ResourceManager.GetResource<IImageResource>("BorderFill1"), new Vector2(button.X, button.Y), new Vector2(button.ActualWidth, button.ActualHeight), new Thickness(3), new SBColor(24, 24, 24)),
                    new BorderSprite(Application.ResourceManager.GetResource<IImageResource>("BorderStroke1"), new Vector2(button.X, button.Y), new Vector2(button.ActualWidth, button.ActualHeight), new Thickness(3), new SBColor(86, 86, 86)),
                    new TextureSprite(
                        button.Content as IImageResource, 
                        new Vector2(button.X, button.Y),
                        new Vector2(button.ActualWidth, button.ActualHeight), 
                        new Vector2(button.SourcePosition.X, button.SourcePosition.Y),
                        new Vector2(button.SourceSize.X, button.SourceSize.Y),
                        new SBColor(255, 255, 255))};
            }
        }

        public virtual Control Control { get; set; }
    }
}
