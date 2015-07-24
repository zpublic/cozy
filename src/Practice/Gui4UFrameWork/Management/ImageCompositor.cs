// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageCompositor.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Contains a mountain of functions for drawing images
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Management
{
    using System.Diagnostics.CodeAnalysis;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Contains a mountain of functions for drawing images.
    /// </summary>
    public abstract class ImageCompositor
    {
        /// <summary>Creates a rectangle texture. </summary>
        /// <param name="preferredName">The preferred name to use.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="borderWidth">Width of the border.</param>
        /// <param name="fillColor">Color of the fill.</param>
        /// <param name="borderColor">Color of the border.</param>
        /// <returns>The URL to the created texture resource that looks like a rectangle.</returns>
        public abstract string CreateRectangleTexture(string preferredName, int width, int height, int borderWidth, GUIColor fillColor, GUIColor borderColor);

        /// <summary>Creates the image texture.</summary>
        /// <param name="preferredName">Name of the base.</param>
        /// <param name="imageLocation">The file location.</param>
        /// <returns>a texture with a image inside.</returns>
        public abstract string CreateImageTexture(string preferredName, string imageLocation);

        /// <summary>
        /// Creates a flat texture with one color.
        /// </summary>
        /// <param name="preferredName">The preferred name to use.</param>
        /// <param name="width">The width of the line texture.</param>
        /// <param name="height">The height of the line texture.</param>
        /// <param name="color">The fill color of the line texture.</param>
        /// <returns>the name to use to find the texture back.</returns>
        public abstract string CreateFlatTexture(string preferredName, int width, int height, GUIColor color);

        /// <summary>
        /// Creates a render target to render onto.
        /// </summary>
        /// <param name="preferredName">Name of the base.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="mipMap">if set to <c>true</c> [mip map].</param>
        /// <returns>A render target.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "mip")]
        public abstract string CreateRenderTarget(string preferredName, int width, int height, bool mipMap);

        /// <summary>
        /// Creates a sprite font, to be used for text rendering.
        /// </summary>
        /// <param name="spriteFontName">Name of the sprite font in your Content.</param>
        public abstract void CreateSpriteFont(string spriteFontName);

        /// <summary>
        /// Reads the width and height of a texture in your resource-pool.
        /// </summary>
        /// <param name="textureName">Name of the texture.</param>
        /// <returns>The width and height contained in a DVector2.</returns>
        public abstract DVector2 ReadSizeTexture(string textureName);

        /// <summary>Reads the resulting width and height ,of a given string with given font in your resource-pool.</summary>
        /// <param name="spriteFontName">Name of the sprite font.</param>
        /// <param name="text">The text to use for the size check..</param>
        /// <returns>The size of a string with given font , contained in a DVector2.</returns>
        public abstract DVector2 ReadSizeString(string spriteFontName, string text);

        /// <summary>
        /// Updates the texture with given render-target.
        /// </summary>
        /// <param name="textureName">Name of the texture.</param>
        /// <param name="renderTargetName">Name of the render target.</param>
        public abstract void UpdateTexture(string textureName, string renderTargetName);

        /// <summary>
        /// Updates the texture.
        /// </summary>
        /// <param name="textureName">Name of the texture.</param>
        /// <param name="colorMap">The color map.</param>
        public abstract void UpdateTexture(string textureName, ColorMap colorMap);

        /// <summary>
        /// Deletes the specified resource from the resource pool.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        public abstract void Delete(string resourceName);

        /// <summary>
        /// Notes the beginning of drawing the textures. This is for the engine a important signal.
        /// </summary>
        public abstract void BeginDraw();

        /// <summary>
        /// Draws the specified texture using specified draw-state , with a tint color.
        /// </summary>
        /// <param name="name">The name of the item to draw.</param>
        /// <param name="drawState">The state that contains all the info to draw the item.</param>
        /// <param name="tintColor">The color to use to tint the shown item.</param>
        public abstract void Draw(string name, DrawState drawState, GUIColor tintColor);

        /// <summary>Draws the string using specified location font and color.</summary>
        /// <param name="fontName">Name of the font.</param>
        /// <param name="text">The text to draw.</param>
        /// <param name="position">The position of the text.</param>
        /// <param name="fontColor">Color of the font.</param>
        public abstract void DrawString(string fontName, string text, DVector2 position, GUIColor fontColor);

        /// <summary>
        /// Ends the drawing of textures. This is for the engine a important signal.
        /// </summary>
        public abstract void EndDraw();

        /// <summary>Sets the render target to given target-name.</summary>
        /// <param name="renderTargetName">Name of the render target.</param>
        public abstract void SetRenderTarget(string renderTargetName);

        /// <summary>
        /// Clears the current render target with given color.
        /// </summary>
        /// <param name="clearColor">Color of the clear.</param>
        public abstract void Clear(GUIColor clearColor);

        /// <summary>
        /// unsets the render target.
        /// </summary>
        /// <param name="renderTargetName">Name of the render target.</param>
        public abstract void UnsetRenderTarget(string renderTargetName);

        /// <summary>
        /// Debugs this instance. Making it spit out values of current state of this compositor.
        /// </summary>
        public abstract void Debug();

        /// <summary>Determines whether a resource pool [contains] [the specified asset name].</summary>
        /// <param name="assetName">Name of the asset.</param>
        /// <returns>Whether we have the asset (True) or not (False).</returns>
        public abstract bool Contains(string assetName);
    }
}