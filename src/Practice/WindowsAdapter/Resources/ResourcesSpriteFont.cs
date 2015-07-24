// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourcesSpriteFont.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ResourcesSpriteFont type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UWindows.Resources
{
    using GUI4UFramework.Management;

    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Of pool of resources containing Sprite-Fonts.
    /// </summary>
    public class ResourcesSpriteFont : ResourcePool<SpriteFont>
    {
        /// <summary>
        /// Contains the content manager.
        /// </summary>
        private readonly ContentManager contentManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcesSpriteFont"/> class.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        public ResourcesSpriteFont(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }

        /// <summary>
        /// Creates font using the given name.
        /// </summary>
        /// <param name="fontName">Name of the font.</param>
        /// <returns>The name of the location in the resource pool, where the created font resides.</returns>
        public string CreateFromFontName(string fontName)
        {
            if (Resources.ContainsKey(fontName))
            {
                return fontName;
            }

            var spriteFont = this.contentManager.Load<SpriteFont>(fontName);

            Resources.Add(fontName, spriteFont);

            return fontName;
        }

        /// <summary>
        /// Debugs this instance. Let this class spit out debug info.
        /// </summary>
        public override void Debug()
        {
            System.Diagnostics.Debug.WriteLine("Library for SpriteFonts contains : ");
            foreach (var resource in this.Resources)
            {
                var value = resource.Value;
                System.Diagnostics.Debug.WriteLine("[" + resource.Key + "] : " + value);
            }
        }
    }
}