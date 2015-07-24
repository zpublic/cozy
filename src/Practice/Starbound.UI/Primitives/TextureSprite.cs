using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starbound.UI.Resources;

namespace Starbound.UI.Primitives
{
    public class TextureSprite : Sprite
    {
        public Vector2 SourcePosition { get; set; }
        public Vector2 SourceSize { get; set; }

        public TextureSprite(IImageResource resource, Vector2 position, Vector2 size, 
            Vector2 sourcePos, Vector2 sourceSize,SBColor tintColor, double opacity = 1.0)
            :base(resource, position, size, tintColor, opacity)
        {
            SourcePosition  = sourcePos;
            SourceSize      = sourceSize;
        }
    }
}
