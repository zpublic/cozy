using Starbound.UI.Controls;
using Starbound.UI.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI
{
    /// <summary>
    /// Defines the capabilities of a renderer.
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Draws a single primitive of any of the core types.
        /// </summary>
        /// <param name="primitive"></param>
        void Render(Primitive primitive);

        void Render(Control control);
    }
}
