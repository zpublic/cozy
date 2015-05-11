using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Starbound.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.XNA.Resources
{
    public class XNAResourceManager : IResourceManager
    {
        private ContentManager contentManager;

        public XNAResourceManager(ContentManager contentManager)
        {
            Application.ResourceManager = this;
            this.contentManager = contentManager;
        }

        public T GetResource<T>(string id) where T : IResource
        {
            if (typeof(T) == typeof(IImageResource)) { return (T)GetImageResource(id); }
            if (typeof(T) == typeof(IFontResource)) { return (T)GetFontResource(id); }
            throw new InvalidOperationException("Unknown type parameter: " + typeof(T));
        }

        private IImageResource GetImageResource(string id)
        {
            Texture2D texture = contentManager.Load<Texture2D>(id);
            return new XNAImageResource(texture);
        }

        private IFontResource GetFontResource(string id)
        {
            SpriteFont font = contentManager.Load<SpriteFont>(id);
            return new XNAFontResource(font);
        }
    }
}
