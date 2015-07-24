// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowsImageCompositor.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Here all the magic drawing happens.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UWindows
{
    using System;
    using System.Diagnostics;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    using GUI4UWindows.Resources;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using Rectangle = Microsoft.Xna.Framework.Rectangle;

    /// <summary>Here all the magic drawing happens.</summary>
    public sealed class WindowsImageCompositor : ImageCompositor, IDisposable
    {
        /// <summary>The resource pool, containing textures.</summary>
        private readonly ResourcesTexture resourcesTexture;

        /// <summary>The resource pool, containing the RenderTarget2D items.</summary>
        private readonly ResourcesRenderTarget2D resourcesRenderTarget;

        /// <summary>The resource pool, containing the SpriteFonts.</summary>
        private readonly ResourcesSpriteFont resourcesSpriteFont;

        /// <summary>The graphics device.</summary>
        private readonly GraphicsDevice device;

        /// <summary>The sprite batch.</summary>
        private readonly SpriteBatch spriteBatch;

        /// <summary>The disposed.Flag: Has Dispose already been called?. </summary>
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsImageCompositor"/> class.
        /// </summary>
        /// <param name="game">The game that must be used to use me.</param>
        public WindowsImageCompositor(Game game)
        {
#if DEBUG
            if (game == null)
            {
                throw new ArgumentNullException("game", "Game cannot be null, its used here.");
            }
#endif

            this.resourcesTexture = new ResourcesTexture(game.GraphicsDevice, game.Content);
            this.resourcesSpriteFont = new ResourcesSpriteFont(game.Content);
            this.resourcesRenderTarget = new ResourcesRenderTarget2D(game.GraphicsDevice);
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.device = game.GraphicsDevice;
        }

        /// <summary>Creates a rectangle texture.</summary>
        /// <param name="preferredName">The preferred name to use.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="borderWidth">Width of the border.</param>
        /// <param name="fillColor">Color of the fill.</param>
        /// <param name="borderColor">Color of the border.</param>
        /// <returns>The URL to the created texture resource that looks like a rectangle.</returns>
        /// <exception cref="ArgumentException">Width can not be zero or less;width
        /// or
        /// height can not be zero or less;height
        /// or
        /// Control name can not be null;preferredName
        /// or
        /// Border width can not be smaller then zero;borderWidth.</exception>
        public override string CreateRectangleTexture(string preferredName, int width, int height, int borderWidth, GUIColor fillColor, GUIColor borderColor)
        {
#if DEBUG
            if (width <= 0)
            {
                throw new ArgumentException("width can not be zero or less", "width");
            }

            if (height <= 0)
            {
                throw new ArgumentException("height can not be zero or less", "height");
            }

            if (string.IsNullOrEmpty(preferredName))
            {
                throw new ArgumentException("Control name can not be null", "preferredName");
            }

            if (borderWidth < 0)
            {
                throw new ArgumentException("Border width can not be smaller then zero", "borderWidth");
            }
#endif

            var textureName = this.resourcesTexture.Create(preferredName, width, height, fillColor);

            // var colordata = RectangleBrush.CreateRectangleColorMap(width, height,borderWidth,1,1, fillColor, borderColor);
            var colordata = ColorMapDrawer.CreateRectangleColorMap(width, height, borderWidth, fillColor, borderColor);
            // var colordata = ColorMapDrawer.CreateRoundedRectangleColorMap(
            //                                                            width,
            //                                                            height,
            //                                                            borderWidth,
            //                                                            20,
            //                                                            fillColor,
            //                                                            borderColor);
            this.resourcesTexture.Update(textureName, colordata.ToArray());

            return textureName;
        }

        /// <summary>
        /// Creates the image texture.
        /// </summary>
        /// <param name="preferredName">Name of the base.</param>
        /// <param name="imageLocation">Where the image resides.</param>
        /// <returns>
        /// A texture with a image inside.
        /// </returns>
        public override string CreateImageTexture(string preferredName, string imageLocation)
        {
            var textureName = this.resourcesTexture.CreateFromUrl(preferredName, imageLocation);
            return textureName;
        }

       /// <summary>
        /// Creates the flat texture.
        /// </summary>
        /// <param name="preferredName">Name of the preferred.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="color">The color.</param>
        /// <returns>A flat texture.</returns>
        public override string CreateFlatTexture(string preferredName, int width, int height, GUIColor color)
        {
            var imageName = this.resourcesTexture.Create(preferredName, width, height, color);

            return imageName;
        }

        /// <summary>Creates a render target to render onto.</summary>
        /// <param name="preferredName">Name of the base.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="mipMap">If it will use MipMapping.</param>
        /// <returns>A render target.</returns>
        public override string CreateRenderTarget(string preferredName, int width, int height, bool mipMap)
        {
            var renderTargetName = this.resourcesRenderTarget.CreateRenderTarget(preferredName, width, height, mipMap);
            return renderTargetName;
        }

        /// <summary>
        /// Creates a sprite font, to be used for text rendering.
        /// </summary>
        /// <param name="spriteFontName">Name of the sprite font in your Content.</param>
        public override void CreateSpriteFont(string spriteFontName)
        {
            if (string.IsNullOrEmpty(spriteFontName))
            {
                System.Diagnostics.Debug.WriteLine("The font name cannot be NULL or empty");
                Debugger.Break();
            }

            this.resourcesSpriteFont.CreateFromFontName(spriteFontName);
        }

        /// <summary>
        /// Reads the size texture.
        /// </summary>
        /// <param name="textureName">The name of the texture.</param>
        /// <returns>The size found.</returns>
        public override DVector2 ReadSizeTexture(string textureName)
        {
            var texture = this.resourcesTexture.Read(textureName);
            return new DVector2(texture.Width, texture.Height);
        }

        /// <summary>
        /// Reads the size string.
        /// </summary>
        /// <param name="spriteFontName">Name of the sprite font.</param>
        /// <param name="text">The text that is being read.</param>
        /// <returns>The size found.</returns>
        public override DVector2 ReadSizeString(string spriteFontName, string text)
        {
            var font = this.resourcesSpriteFont.Read(spriteFontName);
            var size = font.MeasureString(text);
            return new DVector2(size.X, size.Y);
        }

        /// <summary>
        /// Updates the texture with given render-target.
        /// </summary>
        /// <param name="textureName">Name of the texture.</param>
        /// <param name="renderTargetName">Name of the render target.</param>
        public override void UpdateTexture(string textureName, string renderTargetName)
        {
            var texture = this.resourcesTexture.Read(textureName);
            if (texture == null)
            {
                System.Diagnostics.Debug.WriteLine("Could not update texture with render-target");
                Debugger.Break();
            }

            var rendertarget = this.resourcesRenderTarget.Read(renderTargetName);
            // ReSharper disable once RedundantAssignment
            texture = rendertarget;
        }

        /// <summary>
        /// Updates the texture color image with give color image.
        /// </summary>
        /// <param name="textureName">Name of the texture.</param>
        /// <param name="colorMap">The color array.</param>
        public override void UpdateTexture(string textureName, ColorMap colorMap)
        {
#if DEBUG
            if (colorMap == null)
            {
                throw new ArgumentNullException("colorMap", "Given array can not be null , cause we use it to update the texture.");
            }
#endif

            // first reverse the data
            var reverseData = colorMap.Reverse();

            var texture = this.resourcesTexture.Read(textureName);
            if (texture == null)
            {
                System.Diagnostics.Debug.WriteLine("Could not update texture, could not find texture");
                Debugger.Break();
                return;
            }

            texture.SetData(reverseData.ToArray());
        }

        /// <summary>
        /// Deletes the specified resource from the resource pool.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        public override void Delete(string resourceName)
        {
            if (this.resourcesTexture.Contains(resourceName))
            {
                this.resourcesTexture.Delete(resourceName);
                return;
            }

            if (this.resourcesRenderTarget.Contains(resourceName))
            {
                this.resourcesRenderTarget.Delete(resourceName);
                return;
            }

            if (this.resourcesSpriteFont.Contains(resourceName))
            {
                this.resourcesSpriteFont.Delete(resourceName);
                return;
            }

            System.Diagnostics.Debug.WriteLine("Could not delete " + resourceName);
            Debugger.Break();
        }

        /// <summary>
        /// Notes the beginning of drawing the textures. This is for the engine a important signal.
        /// </summary>
        public override void BeginDraw()
        {
            this.spriteBatch.Begin();
        }

        /// <summary>
        /// Draws the specified texture using specified draw-state , with a tint color.
        /// </summary>
        /// <param name="name">The name of the item to draw.</param>
        /// <param name="drawState">The state that contains all the info to draw the item.</param>
        /// <param name="tintColor">The color to use to tint the shown item.</param>
        public override void Draw(string name, DrawState drawState, GUIColor tintColor)
        {
#if DEBUG
            if (drawState == null)
            {
                throw new ArgumentNullException("drawState", "DrawState is used for drawing. This can not be Null.");
            }
#endif
            // get the texture to draw
            var texture = this.resourcesTexture.Read(name);

            // calculate what part to draw where
            var scaleVector = new Vector2(drawState.Width / texture.Width, drawState.Height / texture.Height);
            var position = new Vector2(drawState.DrawPosition.X + drawState.Offset.X, drawState.DrawPosition.Y + drawState.Offset.Y);
            var origin = new Vector2();
            var color = new Color(tintColor.R, tintColor.G, tintColor.B, tintColor.A);
            
            Rectangle srcRect;
            if (drawState.SourceRectangle == null)
            {
                srcRect = new Rectangle(0, 0, texture.Width, texture.Height);
            }
            else
            {
                srcRect = new Rectangle((int)drawState.SourceRectangle.PositionX, (int)drawState.SourceRectangle.PositionY, (int)drawState.SourceRectangle.Width, (int)drawState.SourceRectangle.Height);
            }

            this.spriteBatch.Draw(
                                texture, 
                                position, 
                                srcRect, 
                                color, 
                                0, 
                                origin, 
                                scaleVector, 
                                SpriteEffects.None, 
                                0);
        }

        /// <summary>
        /// Draws the string using specified location font and color.
        /// </summary>
        /// <param name="fontName">Name of the font.</param>
        /// <param name="text">The text to draw.</param>
        /// <param name="position">The position of the text.</param>
        /// <param name="fontColor">Color of the font.</param>
        public override void DrawString(string fontName, string text, DVector2 position, GUIColor fontColor)
        {
            //// spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            
            var castedPosition = new Vector2(position.X, position.Y);
            var origin = new Vector2(0, 0);
            var spriteFont = this.resourcesSpriteFont.Read(fontName);
            var color = new Color(fontColor.R, fontColor.G, fontColor.B, fontColor.A);

            this.spriteBatch.DrawString(
                                    spriteFont,
                                    text,
                                    castedPosition,
                                    color,
                                    0,
                                    origin,
                                    1,
                                    SpriteEffects.None,
                                    0);
            //// spriteBatch.End();
        }

        /// <summary>
        /// Ends the drawing of textures. This is for the engine a important signal.
        /// </summary>
        public override void EndDraw()
        {
            this.spriteBatch.End();

            // Reset various things stuffed up by SpriteBatch
            this.device.DepthStencilState = new DepthStencilState { DepthBufferEnable = true, DepthBufferWriteEnable = true };
            //// device.BlendState = BlendState.Opaque;
            this.device.SamplerStates[0] = new SamplerState { AddressU = TextureAddressMode.Wrap, AddressV = TextureAddressMode.Wrap };
        }

        /// <summary>
        /// Sets the render target to given target-name.
        /// </summary>
        /// <param name="renderTargetName">Name of the render target.</param>
        public override void SetRenderTarget(string renderTargetName)
        {
            var renderTarget = this.resourcesRenderTarget.Read(renderTargetName);
            this.device.SetRenderTarget(renderTarget);
        }

        /// <summary>
        /// Clears the current render target with given color.
        /// </summary>
        /// <param name="clearColor">Color of the clear.</param>
        public override void Clear(GUIColor clearColor)
        {
            var clr = new Color(clearColor.R, clearColor.G, clearColor.B, clearColor.A);
            this.device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, clr, 1, 0);
        }

        /// <summary>
        /// Unsets the render target.
        /// </summary>
        /// <param name="renderTargetName">Name of the render target.</param>
        public override void UnsetRenderTarget(string renderTargetName)
        {
            this.device.SetRenderTarget(null);
        }

        /// <summary>
        /// Debugs this instance. Making it spit out values of current state of this compositor.
        /// </summary>
        public override void Debug()
        {
            System.Diagnostics.Debug.WriteLine("*** ImageCompositor Debug ***");
            this.resourcesTexture.Debug();
            System.Diagnostics.Debug.WriteLine(string.Empty);
            this.resourcesRenderTarget.Debug();
            System.Diagnostics.Debug.WriteLine(string.Empty);
            this.resourcesSpriteFont.Debug();
            System.Diagnostics.Debug.WriteLine("*** ImageCompositor Debug End ***");
        }

        /// <summary>
        /// Determines whether a resource pool [contains] [the specified asset name].
        /// </summary>
        /// <param name="assetName">Name of the asset.</param>
        /// <returns>
        /// Whether we have the asset (True) or not (False).
        /// </returns>
        public override bool Contains(string assetName)
        {
            if (this.resourcesTexture.Contains(assetName))
            {
                return true;
            }

            if (this.resourcesRenderTarget.Contains(assetName))
            {
                return true;
            }

            if (this.resourcesSpriteFont.Contains(assetName))
            {
                return true;
            }

            return false;
        }

        /// <summary>Public implementation of Dispose pattern callable by consumers.</summary>
        public void Dispose()
        {
            this.Dispose(true);

            // ReSharper disable once GCSuppressFinalizeForTypeWithoutDestructor
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// Protected implementation of Dispose pattern. 
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                //// Free any other managed objects here. 

                this.spriteBatch.Dispose();
            }

            //// Free any unmanaged objects here.
            
            this.disposed = true;
        }
    }
}