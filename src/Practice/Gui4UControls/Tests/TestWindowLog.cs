// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWindowLog.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the TestWindowLog type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tests
{
    using GUI4UControls.Utility;

    using GUI4UFramework.Structural;

    /// <summary>
    /// Create a test-window for the Log-Control
    /// </summary>
    public class TestWindowLog : TestWindowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestWindowLog"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TestWindowLog(string name) : base(name)
        {
            this.Title = "Test log";
            this.Config.Height = (Theme.ControlHeight * 10) + (2 * Theme.ControlLargeSpacing);
            this.Config.Width = (Theme.ControlWidth * 6) + (2 * Theme.ControlLargeSpacing);
        }

        /// <summary>
        /// Gets or sets the log control.
        /// </summary>
        /// <value>
        /// The log control.
        /// </value>
        private Log LogControl { get; set; }

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

            // Log
            this.LogControl = new Log(this.Name + "-LogControl")
            {
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = Theme.ControlLargeSpacing + Theme.ControlHeight,
                    Width = this.Config.Width - (Theme.ControlLargeSpacing * 2)
                },
                DebugTextGeneration = true
            };
            this.LogControl.Config.Height = this.Config.Height - this.LogControl.Config.PositionY - Theme.ControlLargeSpacing;
            this.AddControl(this.LogControl);
        }
    }
}