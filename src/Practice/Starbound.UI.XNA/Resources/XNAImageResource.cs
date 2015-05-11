using Microsoft.Xna.Framework.Graphics;
using Starbound.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.XNA.Resources
{
    public class XNAImageResource : IImageResource
    {
        public readonly Texture2D Texture;

        public XNAImageResource(Texture2D texture)
        {
            Texture = texture;
        }

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }
    }
}
