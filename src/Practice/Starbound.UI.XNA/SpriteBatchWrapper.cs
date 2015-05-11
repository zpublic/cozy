using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Starbound.UI.Resources;
using Starbound.UI.XNA.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Starbound.UI.XNA
{
    using Vector2 = Microsoft.Xna.Framework.Vector2;

    /// <summary>
    /// Wraps up a normal SpriteBatch object and passes along calls to it.
    /// </summary>
    public class SpriteBatchWrapper : ISpriteBatch
    {
        /// <summary>
        /// The wrapped sprite batch object, which calls will be deferred to.
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Creates a new instance of SpriteBatchWrapper with a specific SpriteBatch object.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public SpriteBatchWrapper(SpriteBatch spriteBatch)
        {
            if (spriteBatch == null) { throw new ArgumentNullException("spriteBatch"); }
            this.spriteBatch = spriteBatch;
        }

        public void Begin()
        {
            spriteBatch.Begin();
        }

        public void End()
        {
            spriteBatch.End();
        }


        public void Draw(IImageResource texture, Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(((XNAImageResource)texture).Texture, rectangle, color);
        }

        public void Draw(IImageResource texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)
        {
            spriteBatch.Draw(((XNAImageResource)texture).Texture, destinationRectangle, sourceRectangle, color);
        }

        public void DrawString(IFontResource font, string text, Microsoft.Xna.Framework.Vector2 position, Color color)
        {
            spriteBatch.DrawString(((XNAFontResource)font).Font, text, position, color);
        }
    }
}
