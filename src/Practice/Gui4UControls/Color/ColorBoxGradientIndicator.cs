// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorBoxGradientIndicator.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ColorBoxGradientIndicator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Color
{
    using System;

    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Is the indicator that sits on top of the ColorBoxGradient , you can move me around with the mouse.
    /// </summary>
    public class ColorBoxGradientIndicator : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorBoxGradientIndicator"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ColorBoxGradientIndicator(string name) : base(name)
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
            base.LoadContent();

            if (Manager.ImageCompositor.Contains(this.CurrentTextureName) == false)
            {
                var imageLocation = "Textures\\white_circle_10x10";
                this.CurrentTextureName = Manager.ImageCompositor.CreateImageTexture(this.Name + "-LeftIndicator", imageLocation);
            }

            var size = Manager.ImageCompositor.ReadSizeTexture(CurrentTextureName);

            Config.Width = size.X;
            Config.Height = size.Y;
            State.Offset = new DVector2(-size.X / 2f, -size.Y / 2f);
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
        }
    }
}
