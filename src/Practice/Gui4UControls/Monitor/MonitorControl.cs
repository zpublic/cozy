// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonitorControl.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the MonitorControl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Monitor
{
    using GUI4UControls.Text;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Shows a representation of a monitor
    /// </summary>
    public class MonitorControl : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorControl"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public MonitorControl(string name) : base(name)
        {
        }

        /// <summary>
        /// Gets or sets a Label that shows the adapter device name.
        /// </summary>
        /// <value>
        /// The adapter device name label.
        /// </value>
        public Label AdapterDeviceNameLabel { get; set; }

        /// <summary>
        /// Gets or sets a Label that show the width and height.
        /// </summary>
        /// <value>
        /// The width height label.
        /// </value>
        public Label WidthHeightLabel { get; set; }

        /// <summary>
        /// Gets or sets a Label that shows the left and top.
        /// </summary>
        /// <value>
        /// The left top label.
        /// </value>
        public Label LeftTopLabel { get; set; }

        /// <summary>
        /// Gets or sets a Label that shows the pixel-format and refresh rate
        /// </summary>
        /// <value>
        /// The pixel refresh label.
        /// </value>
        public Label PixelRefreshLabel { get; set; }

        /// <summary>
        /// Gets or sets the monitor setting that will be shown on the control.
        /// </summary>
        /// <value>
        /// The monitor setting.
        /// </value>
        public MonitorSetting MonitorSetting { get; set; }

        /// <summary>
        /// Called when graphics resources need to be loaded. 
        /// 
        /// Use this for the usage of :
        /// - creation of the internal embedded controls.
        /// - setting of the variables and resources in this control
        /// - to load any game-specific graphics resources
        /// - take over the config width and height and use it into State
        /// - overriding how this item looks like , by settings its texture or theme
        /// 
        /// Call base.LoadContent before you do your override code, this will cause :
        /// - State.SourceRectangle to be reset to the Config.Size 
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            // create my background
            this.CurrentTextureName = Manager.ImageCompositor.CreateRectangleTexture(this.Name + "-Background", (int)Config.Width, (int)Config.Height, 1, GUIColor.MidnightBlue(), Theme.BorderColor);
        }
    }
}