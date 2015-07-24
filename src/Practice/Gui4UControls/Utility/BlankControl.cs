// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BlankControl.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the BlankControl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Utility
{
    using GUI4UFramework.Colors;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Create a blank user-control with no extra functionality.
    /// It looks like a colored square.
    /// </summary>
    public class BlankControl : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlankControl"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public BlankControl(string name) : base(name)
        {
            Config.Width = 15;
            Config.Height = 15;
            this.Color = new GUIColor(255, 255, 0);
        }

        /// <summary>
        /// Gets or sets the color for this control.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public GUIColor Color { get; set; }

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
            // do the basic stuff
            base.LoadContent();
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();

            // make the background
            this.CurrentTextureName = Manager.ImageCompositor.CreateRectangleTexture(
                                                    this.Name,
                                                    (int)Config.Width,
                                                    (int)Config.Height,
                                                    0,
                                                    this.Color,
                                                    this.Color);
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
