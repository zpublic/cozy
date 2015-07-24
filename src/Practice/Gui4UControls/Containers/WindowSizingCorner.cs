// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowSizingCorner.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the WindowSizingCorner type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Containers
{
    using System;

    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Shows a sizing button on the bottom right , to reshape a window in size.
    /// </summary>
    public class WindowSizingCorner : Control
    {
        /// <summary>
        /// The texture size of the control in the corner.
        /// </summary>
        private DVector2 textureSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowSizingCorner"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public WindowSizingCorner(string name) : base(name)
        {
        }

        /// <summary>
        /// Called when graphics resources need to be loaded.
        /// Use this for the usage of :
        /// - creation of the internal embedded controls.
        /// - setting of the variables and resources in this control
        /// - to load any game-specific graphics resources
        /// - take over the config width and height and use it into State
        /// - overriding how this item looks like , by settings its texture or theme
        /// Call base.LoadContent before you do your override code, this will cause :
        /// - State.SourceRectangle to be reset to the Config.Size
        /// </summary>
        public override void LoadContent()
        {
            this.CurrentTextureName = this.Manager.ImageCompositor.CreateImageTexture(this.Name + "-Sizer", "Textures\\resizeicon");
            this.textureSize = this.Manager.ImageCompositor.ReadSizeTexture(this.CurrentTextureName);
            this.Config.Width = this.textureSize.X;
            this.Config.Height = this.textureSize.Y;

            // do the basic stuff
            base.LoadContent();
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();
            this.UpdateDrawSourceRectangleByConfig();

            if (this.CheckIsFocused())
            {
            }
        }
    }
}
