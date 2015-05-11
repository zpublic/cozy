using Starbound.UI.Controls;
using Starbound.UI.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI
{
    public abstract class RendererBase : IRenderer
    {
        public abstract void Render(Primitive primitive);

        public void Render(Control control)
        {
            foreach(Primitive primitive in control.Template.Primitives)
            {
                Render(primitive);
            }
        }
    }
}
