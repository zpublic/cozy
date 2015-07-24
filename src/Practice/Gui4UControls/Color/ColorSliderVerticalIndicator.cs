// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorSliderVerticalIndicator.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ColorSliderVerticalIndicator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Color
{
    using System;

    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Is the indicator that you move around in the vertical color slider.
    /// </summary>
    public class ColorSliderVerticalIndicator : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorSliderVerticalIndicator"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ColorSliderVerticalIndicator(string name) : base(name)
        {
            Side = Side.Left;
        }

        /// <summary>
        /// Gets or sets the side on which this indicator will be..
        /// A other side means a other texture used.
        /// </summary>
        /// <value>
        /// The side.
        /// </value>
        public Side Side { get; set; }

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

            string imageLocation;
            if (Manager.ImageCompositor.Contains(this.CurrentTextureName) == false)
            {
                switch (Side)
                {
                    case Side.Left:
                        imageLocation = @"Textures\LeftIndicator";
                        this.CurrentTextureName = Manager.ImageCompositor.CreateImageTexture(this.Name + "-LeftIndicator", imageLocation);
                        break;

                    case Side.Right:
                        imageLocation = @"Textures\RightIndicator";
                        this.CurrentTextureName = Manager.ImageCompositor.CreateImageTexture(this.Name + "-RightIndicator", imageLocation);
                        break;
                }
            }

            var size = Manager.ImageCompositor.ReadSizeTexture(CurrentTextureName);

            Config.Width = size.X;
            Config.Height = size.Y;
            State.Offset = new DVector2(0, -size.Y / 2f);
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
