using Starbound.UI.Primitives;
using Starbound.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.Controls
{
    public class Rectangle : Control
    {
        public SBColor Color { get; set; }

        public Rectangle()
        {
            Template = new DefaultRectangleTemplate();
            Template.Control = this;
        }
    }

    public class DefaultRectangleTemplate : IControlTemplate
    {
        public virtual IEnumerable<Primitive> Primitives
        {
            get
            {
                if (!(Control is Rectangle)) return Enumerable.Empty<Primitive>();
                Rectangle rectangle = Control as Rectangle;
                return new List<Primitive>()
                {
                    new Sprite(
                        Application.ResourceManager.GetResource<IImageResource>("Solid"), 
                        new Vector2(rectangle.X, rectangle.Y), 
                        new Vector2(rectangle.ActualWidth, rectangle.ActualHeight), 
                        rectangle.Color),
                };
            }
        }

        public virtual Control Control { get; set; }
    }
}
