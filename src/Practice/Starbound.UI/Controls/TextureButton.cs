using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.Controls
{
    public class TextureButton : ContentControl
    {
        public Vector2 SourcePosition { get; set; }
        public Vector2 SourceSize { get; set; }
        public TextureButton()
        {
            Template            = new DefaultTextureButtonTemplate();
            Template.Control    = this;
        }
    }
}
