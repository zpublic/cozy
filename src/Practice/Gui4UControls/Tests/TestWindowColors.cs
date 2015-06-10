// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWindowColors.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the TestWindowColors type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tests
{
    using GUI4UControls.Color;
    using GUI4UControls.Text;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Creates a test-window to test the Color-Controls
    /// </summary>
    public class TestWindowColors : TestWindowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestWindowColors"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TestWindowColors(string name) : base(name)
        {
            this.Title = "Photoshop";
        }

        /// <summary>
        /// Gets or sets the label that tells some Information
        /// </summary>
        /// <value>
        /// The information label.
        /// </value>
        private Label InfoLabel { get; set; }

        /// <summary>
        /// Gets or sets the color picker in photoshop-style
        /// </summary>
        /// <value>
        /// The color picker photoshop-style.
        /// </value>
        private ColorPickerPhotoshop ColorPickerPhotoshop { get; set; }

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

            // Photoshop color picker
            var posy = Theme.ControlLargeSpacing + Theme.ControlHeight;
            this.ColorPickerPhotoshop = new ColorPickerPhotoshop("MyColorPicker")
            {
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = posy,
                },
                HSL = new HSL(1, 1, 0.5)
            };
            this.AddControl(this.ColorPickerPhotoshop);
            
            // label
            posy = (int)(this.ColorPickerPhotoshop.Config.PositionY + this.ColorPickerPhotoshop.Config.Height + Theme.ControlSmallSpacing);
            this.InfoLabel = new Label("My label")
            {
                ConfigText = "Color is", 
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing, 
                    PositionY = posy, 
                }
            };
            this.AddControl(this.InfoLabel);

            Config.Width = Theme.ControlWidth + (2 * Theme.ControlLargeSpacing) + Theme.ControlHeight;
            Config.Height = this.InfoLabel.Config.PositionY + this.InfoLabel.Config.Height + Theme.ControlLargeSpacing;
        }
    }
}
