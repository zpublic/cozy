// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWindowCheckBoxes.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the TestWindowCheckBoxes type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tests
{
    using GUI4UControls.Text;

    using GUI4UFramework.Structural;

    using CheckBox = GUI4UControls.Buttons.CheckBox;

    /// <summary>
    /// Creates a  test-window for CheckBox-Controls
    /// </summary>
    public class TestWindowCheckBoxes : TestWindowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestWindowCheckBoxes"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TestWindowCheckBoxes(string name) : base(name)
        {
            this.Title = "Check-boxes";
            Config.Width = Theme.ControlWidth + (2 * Theme.ControlLargeSpacing);
        }

        /// <summary>
        /// Gets or sets the check box1.
        /// </summary>
        /// <value>
        /// The check box1.
        /// </value>
        private CheckBox CheckBox1 { get; set; }

        /// <summary>
        /// Gets or sets the check box2.
        /// </summary>
        /// <value>
        /// The check box2.
        /// </value>
        private CheckBox CheckBox2 { get; set; }

        /// <summary>
        /// Gets or sets the information label.
        /// </summary>
        /// <value>
        /// The information label.
        /// </value>
        private Label InfoLabel { get; set; }

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

            // ToggleButton
            var posy = Theme.ControlLargeSpacing + Theme.ControlHeight;
            this.CheckBox1 = new CheckBox("CheckBox1")
            {
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = posy,
                },
                ConfigText = "Check-box 1"
            };
            this.AddControl(this.CheckBox1);

            // button
            posy = posy + Theme.ControlHeight + Theme.ControlLargeSpacing;
            this.CheckBox2 = new CheckBox("CheckBox2")
            {
                ConfigText = "Check-box 2",
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = posy,
                }
            };
            this.AddControl(this.CheckBox2);

            // label
            posy = posy + Theme.ControlHeight + Theme.ControlLargeSpacing;
            this.InfoLabel = new Label("My label")
            {
                ConfigText = "Change in check",
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = posy,
                }
            };
            this.AddControl(this.InfoLabel);
        }
    }
}
