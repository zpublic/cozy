using Starbound.UI.Primitives;
using Starbound.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.Controls
{
    public class DefaultLabelControlTemplate : IControlTemplate
    {
        public virtual IEnumerable<Primitive> Primitives
        {
            get
            {
                if (!(Control is Label)) return Enumerable.Empty<Primitive>();
                Label label = Control as Label;
                return new List<Primitive>()
                {
                    new BorderSprite(Application.ResourceManager.GetResource<IImageResource>("BorderWithCornerRadius3"), new Vector2(label.X, label.Y), new Vector2(label.ActualWidth, label.ActualHeight), new Thickness(3), SBColor.White),
                    new TextSprite(label.Text, label.Font, new Vector2(label.X, label.Y),new Vector2(label.ActualWidth, label.ActualHeight), HorizontalAlignment.Left, VerticalAlignment.Top, label.Color)
                };
            }
        }

        public virtual Control Control { get; set; }
    }
}
