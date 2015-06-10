// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWindowButton.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the TestWindowButton type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tests
{
    using System;

    using GUI4UControls.Buttons;
    using GUI4UControls.Text;

    using GUI4UFramework.EventArgs;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Creates a window to test Button-Controls
    /// </summary>
    public class TestWindowButton : TestWindowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestWindowButton"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TestWindowButton(string name)
            : base(name)
        {
            this.Title = "Test buttons";
            this.Config.Width = Theme.ControlWidth + (2 * Theme.ControlLargeSpacing);
        }

        /// <summary>
        /// Gets or sets the toggle button.
        /// </summary>
        /// <value>
        /// The toggle button.
        /// </value>
        private ToggleButton ToggleButton { get; set; }

        /// <summary>
        /// Gets or sets the button.
        /// </summary>
        /// <value>
        /// The button.
        /// </value>
        private Button Button { get; set; }

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

            var posy = Theme.ControlLargeSpacing + Theme.ControlHeight;

            // ToggleButton
            this.ToggleButton = new ToggleButton("Toggle button test")
            {
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = posy,
                },
                ConfigText = "Toggle Button"
            };
            this.AddControl(this.ToggleButton);
            this.ToggleButton.OnToggle += this.OnTogglePress;

            // button
            posy = posy + Theme.ControlHeight + Theme.ControlLargeSpacing;
            this.Button = new Button("MyButton")
            {
                ConfigText = "Button",
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = posy,
                }
            };
            this.AddControl(this.Button);
            this.Button.Clicked += this.OnButtonPress;

            // label
            posy = posy + Theme.ControlHeight + Theme.ControlLargeSpacing;
            this.InfoLabel = new Label("My label")
            {
                ConfigText = "Press button",
                ConfigHorizontalAlignment = HorizontalAlignment.Left,
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = posy,
                }
            };
            this.AddControl(this.InfoLabel);
        }

        /// <summary>
        /// Called when the toggle-button is pressed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="buttonStateEventArgs">The <see cref="ButtonStateEventArgs"/> instance containing the event data.</param>
        private void OnTogglePress(object sender, ButtonStateEventArgs buttonStateEventArgs)
        {
            var state = buttonStateEventArgs.ButtonState;
            this.InfoLabel.ConfigText = "Toggle:" + state + ":" + GetTime();
        }

        /// <summary>
        /// Called when the standard-button is pressed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="gameTimeEventArgs">The <see cref="GameTimeEventArgs"/> instance containing the event data.</param>
        private void OnButtonPress(object sender, GameTimeEventArgs gameTimeEventArgs)
        {
            if (this.InfoLabel != null)
            {
                this.InfoLabel.ConfigText = "Click:" + GetTime();
            }
        }

        /// <summary>
        /// A utility function to get time as string
        /// </summary>
        /// <returns>The time as string</returns>
        private static string GetTime()
        {
            var now = DateTime.Now;
            var s = string.Format("{0:HH:MM:ss}", now);
            return s;
        }
    }
}
