// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorPickerPhotoshop.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ColorPickerPhotoshop type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Color
{
    using GUI4UFramework.Colors;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Shows a color picker in that looks like the one in Photoshop//
    /// </summary>
    public class ColorPickerPhotoshop : Control
    {
        /// <summary>
        /// A control that shows a gradient in a box where you control X and Y with a cursor
        /// </summary>
        private ColorBoxGradient colorBoxGradient;

        /// <summary>
        /// A control that shows the final result color
        /// </summary>
        private ColorBoxSolid colorBoxSolid;

        /// <summary>
        /// A slider with a gradient in the back.
        /// </summary>
        private ColorSliderVertical slider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPickerPhotoshop"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ColorPickerPhotoshop(string name) : base(name)
        {
            HSL = new HSL(.3, .4, .5);

            Config.Width = Theme.ControlWidth + Theme.ControlHeight;
            Config.Height = Theme.ControlWidth + Theme.ControlHeight;
        }

        /// <summary>
        /// Gets or sets the color that is shown on this control
        /// </summary>
        /// <value>
        /// The HSL.
        /// </value>
        public HSL HSL { get; set; }

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

            // the gradient
            this.colorBoxGradient = new ColorBoxGradient(Name + "-Gradient")
            {
                Config =
                {
                    PositionX = 0,
                    PositionY = 0
                }
            };
            this.AddControl(this.colorBoxGradient);

            // slider vertical
            this.slider = new ColorSliderVertical(Name + "-Slider")
            {
                Config =
                {
                    PositionX = this.colorBoxGradient.Config.Width,
                    PositionY = 0
                }
            };
            this.AddControl(this.slider);

            // the box with the final color
            this.colorBoxSolid = new ColorBoxSolid(Name + "-ColorBox")
            {
                Config =
                {
                    PositionX = 0,
                    PositionY = this.colorBoxGradient.Config.Height,
                    Width = this.colorBoxGradient.Config.Width + this.slider.Config.Width
                }
            };
            this.AddControl(this.colorBoxSolid);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.colorBoxGradient.HSL = this.slider.HSL;
            this.colorBoxSolid.HSL = this.colorBoxGradient.HSL;
        }

        /// <summary>
        /// Draw the texture from CurrentTextureName at DrawPosition combined with its offset
        /// </summary>
        public override void DrawMyData()
        {
            // we don't have anything to draw , only my child controls
        }
    }
}
