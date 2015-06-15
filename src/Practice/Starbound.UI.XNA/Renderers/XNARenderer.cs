using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Starbound.UI.Primitives;
using Starbound.UI.XNA.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.UI.XNA.Renderers
{
    public class XNARenderer : RendererBase
    {
        private ISpriteBatch spriteBatch;

        public XNARenderer(SpriteBatch spriteBatch)
            : this(new SpriteBatchWrapper(spriteBatch))
        {
        }

        public XNARenderer(ISpriteBatch spriteBatch = null)
        {
            //if (spriteBatch == null) { throw new ArgumentNullException("spriteBatch"); }

            this.spriteBatch = spriteBatch;
        }

        public void Render(Starbound.UI.Controls.Control control, SpriteBatch spriteBatch)
        {
            if (this.spriteBatch == null)
            {
                this.spriteBatch = new SpriteBatchWrapper(spriteBatch);
            }
            foreach (Primitive primitive in control.Template.Primitives)
            {
                Render(primitive);
            }
        }

        public void Render(Primitive primitive, SpriteBatch spriteBatch)
        {
            if (this.spriteBatch == null)
            {
                this.spriteBatch = new SpriteBatchWrapper(spriteBatch);
            }
            Render(primitive);
        }

        public override void Render(Primitive primitive)
        {
            if (primitive is BorderSprite) { DrawSpriteBorder((BorderSprite)primitive); }
            else if (primitive is TextureSprite) { DrawBlockSprite((TextureSprite)primitive); }
            else if (primitive is Sprite) { DrawSprite((Sprite)primitive); }
            else if (primitive is TextSprite) { DrawTextBlock((TextSprite)primitive); }
            else if (primitive is Placeholder) { return; }
            else throw new NotImplementedException("Unknown primitive type: " + primitive.GetType());
        }

        public void DrawTextBlock(TextSprite textBlock)
        {
            spriteBatch.Begin();
            DrawTextBlockThin(textBlock);
            spriteBatch.End();
        }

        private void DrawTextBlockThin(TextSprite textBlock)
        {
            Vector2 size = textBlock.Font.Measure(textBlock.Text);
            float x = (float)PositionForAlignment(textBlock.Position.X, textBlock.Size.X, size.X, textBlock.HorizontalAlignment);
            float y = (float)PositionForAlignment(textBlock.Position.Y, textBlock.Size.Y, size.Y, textBlock.VerticalAlignment);
            spriteBatch.DrawString(textBlock.Font, textBlock.Text,
                new Microsoft.Xna.Framework.Vector2(x, y), textBlock.Color.ToXNAColor());
        }

        private double PositionForAlignment(double start, double parentWidth, double childWidth, HorizontalAlignment horizontalAlignment)
        {
            if (horizontalAlignment == HorizontalAlignment.Left) { return start; }
            else if (horizontalAlignment == HorizontalAlignment.Right) { return start + parentWidth - childWidth; }
            else { return start + (parentWidth - childWidth) / 2; }
        }

        private double PositionForAlignment(double start, double parentHeight, double childHeight, VerticalAlignment alignment)
        {
            if (alignment == VerticalAlignment.Top) { return start; }
            else if (alignment == VerticalAlignment.Bottom) { return start + parentHeight - childHeight; }
            else { return start + (parentHeight - childHeight) / 2; }
        }
        
        /// <summary>
        /// Draws the given sprite within its own SpriteBatch.Begin/End block.
        /// </summary>
        /// <param name="sprite"></param>
        private void DrawSpriteBorder(BorderSprite sprite)
        {
            spriteBatch.Begin();
            DrawSpriteBorderThin(sprite);
            spriteBatch.End();
        }

        /// <summary>
        /// Draws a Sprite primitive without starting a new SpriteBatch draw operation.
        /// </summary>
        /// <param name="sprite"></param>
        private void DrawSpriteBorderThin(BorderSprite sprite)
        {
            DividedRectangleSizes source = new DividedRectangleSizes(sprite.Resource.Width, sprite.Resource.Height, sprite.Border);
            DividedRectangleSizes destination = new DividedRectangleSizes(sprite.Size.X, sprite.Size.Y, sprite.Border);

            Rectangle[] sourceRectangles = source.GenerateRectangles();
            Rectangle[] destinationRectangles = destination.GenerateRectangles();

            for (int index = 0; index < destinationRectangles.Length; index++)
            {
                destinationRectangles[index].Offset((int)sprite.Position.X, (int)sprite.Position.Y);
            }

            for (int index = 0; index < sourceRectangles.Length; index++)
            {
                Color color = sprite.TintColor.ToXNAColor();
                color.A = (byte)(color.A * sprite.Opacity);
                spriteBatch.Draw(
                    sprite.Resource, 
                    destinationRectangles[index], 
                    sourceRectangles[index],
                    color);
            }
        }
        
        /// <summary>
        /// Draws the given sprite within its own SpriteBatch.Begin/End block.
        /// </summary>
        /// <param name="sprite"></param>
        private void DrawSprite(Sprite sprite)
        {
            spriteBatch.Begin();
            DrawSpriteThin(sprite);
            spriteBatch.End();
        }

        /// <summary>
        /// Draws a Sprite primitive without starting a new SpriteBatch draw operation.
        /// </summary>
        /// <param name="sprite"></param>
        private void DrawSpriteThin(Sprite sprite)
        {
            Color color = sprite.TintColor.ToXNAColor();
            color.A = (byte)(color.A * sprite.Opacity);

            XNAImageResource resource = sprite.Resource as XNAImageResource;
            spriteBatch.Draw(
                resource, 
                new Rectangle((int)sprite.Position.X, (int)sprite.Position.Y,(int)sprite.Size.X, (int)sprite.Size.Y),
                color);
        }

        /// <summary>
        /// Draws the given sprite within its own SpriteBatch.Begin/End block.
        /// </summary>
        /// <param name="sprite"></param>
        private void DrawBlockSprite(TextureSprite sprite)
        {
            spriteBatch.Begin();
            DrawBlockSpriteThin(sprite);
            spriteBatch.End();
        }

        /// <summary>
        /// Draws a Sprite primitive without starting a new SpriteBatch draw operation.
        /// </summary>
        /// <param name="sprite"></param>
        private void DrawBlockSpriteThin(TextureSprite sprite)
        {
            Color color = sprite.TintColor.ToXNAColor();
            color.A     = (byte)(color.A * sprite.Opacity);

            XNAImageResource resource = sprite.Resource as XNAImageResource;
            spriteBatch.Draw(resource,
                new Rectangle((int)sprite.Position.X, (int)sprite.Position.Y,(int)sprite.Size.X, (int)sprite.Size.Y),
                new Rectangle((int)sprite.SourcePosition.X, (int)sprite.SourcePosition.Y, (int)sprite.SourceSize.X, (int)sprite.SourceSize.Y),
                color);
        }

        private class DividedRectangleSizes
        {
            public readonly int Left;
            public readonly int CenterX;
            public readonly int Right;
            public readonly int Top;
            public readonly int CenterY;
            public readonly int Bottom;

            public DividedRectangleSizes(double width, double height, Thickness border)
            {
                Left = (int)border.Left;
                CenterX = (int)(width - border.Left - border.Right);
                Right = (int)border.Right;

                Top = (int)border.Top;
                CenterY = (int)(height - border.Top - border.Bottom);
                Bottom = (int)border.Bottom;
            }

            public Rectangle[] GenerateRectangles()
            {
                Rectangle[] rectangles = new Rectangle[9];
                
                rectangles[0] = new Rectangle(0, 0, Left, Top);
                rectangles[1] = new Rectangle(Left, 0, CenterX, Top);
                rectangles[2] = new Rectangle(Left + CenterX, 0, Right, Top);

                rectangles[3] = new Rectangle(0, Top, Left, CenterY);
                rectangles[4] = new Rectangle(Left, Top, CenterX, CenterY);
                rectangles[5] = new Rectangle(Left + CenterX, Top, Right, CenterY);

                rectangles[6] = new Rectangle(0, Top + CenterY, Left, Bottom);
                rectangles[7] = new Rectangle(Left, Top + CenterY, CenterX, Bottom);
                rectangles[8] = new Rectangle(Left + CenterX, Top + CenterY, Right, Bottom);
                
                return rectangles;
            }
        }
    }
}
