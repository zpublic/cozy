// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorBoxSolid.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ColorBoxSolid type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Color
{
    using System;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Shows a solid color inside this control. To be used for example, to show what color you  are picking.
    /// </summary>
    public class ColorBoxSolid : Control
    {
        /// <summary>
        /// The RGBA representation of the color shown.
        /// </summary>
        private GUIColor rgba;

        /// <summary>
        /// The HSL representation of the color shown.
        /// </summary>
        private HSL hsl;

        /// <summary>
        /// If we must redraw me
        /// </summary>
        private bool mustRedrawBox;

        /// <summary>
        /// The current color shown
        /// </summary>
        private GUIColor currentrgba;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorBoxSolid"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ColorBoxSolid(string name) : base(name)
        {
            Config.Width = Theme.ControlWidth;
            Config.Height = Theme.ControlHeight;

            this.RGBA = new GUIColor(128, 120, 10, 50);
        }

        /// <summary>
        /// Gets or sets a value indicating whether i am in debug mode.
        /// </summary>
        /// <value>
        ///   <c>true</c> if you want to debug me otherwise, <c>false</c>.
        /// </value>
        public bool ConfigDebug { get; set; }

        /// <summary>
        /// Gets or sets the RGBA presentation of the color shown.
        /// </summary>
        /// <value>
        /// The RGBA version of the color shown.
        /// </value>
        public GUIColor RGBA
        {
            get
            {
                return this.rgba;
            }

            set
            {
                this.rgba = value;
                this.hsl = AdobeColors.RGBToHSL(this.rgba);
                this.mustRedrawBox = true;
            }
        }

        /// <summary>
        /// Gets or sets the HSL presentation of the color shown
        /// </summary>
        /// <value>
        /// The HSL version of the color shown.
        /// </value>
        public HSL HSL
        {
            get
            {
                return this.hsl;
            }

            set
            {
                this.hsl = value;
                this.rgba = AdobeColors.HSLToRGB(this.hsl);
                this.mustRedrawBox = true;
            }
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

            // create the new
            this.CurrentTextureName = Manager.ImageCompositor.CreateRectangleTexture(this.Name + "-FillColor", (int)Config.Width, (int)Config.Height, 1, this.rgba, Theme.BorderColor);
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

            if (this.mustRedrawBox)
            {
                this.RedrawBox();
            }

            if (this.ConfigDebug)
            {
                var rnd = new Random();
                var r = rnd.Next(255);
                var g = rnd.Next(255);
                var b = rnd.Next(255);
                var a = rnd.Next(255);
                this.RGBA = new GUIColor((byte)r, (byte)g, (byte)b, (byte)a);
            }
        }

        /// <summary>
        /// Redraws the box.
        /// </summary>
        private void RedrawBox()
        {
            // when there are no changes , do nothing
            if (this.currentrgba.Equals(this.rgba))
            {
                return;
            }

            // we have a new color , destroy the old , plant the new
            if (Manager.ImageCompositor.Contains(this.CurrentTextureName))
            {
                // destroy the old !
                Manager.ImageCompositor.Delete(this.CurrentTextureName);
            }

            // create the new
            this.CurrentTextureName = Manager.ImageCompositor.CreateRectangleTexture(this.Name + "-FillColor", (int)Config.Width, (int)Config.Height, 1, this.rgba, Theme.BorderColor);
            this.currentrgba = this.rgba;
        }
    }
}
