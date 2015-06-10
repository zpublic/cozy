// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWindowContainers.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the TestWindowContainers type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tests
{
    using GUI4UControls.Containers;
    using GUI4UControls.Text;

    using GUI4UFramework.Graphics;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Create a test-window 
    /// </summary>
    public class TestWindowContainers : TestWindowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestWindowContainers"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TestWindowContainers(string name) : base(name)
        {
            this.Title = "Test containers";
        }

        /// <summary>
        /// Gets or sets the tab control.
        /// </summary>
        /// <value>
        /// The tab control.
        /// </value>
        private TabControl TabControl { get; set; }

        /// <summary>
        /// Gets or sets the grid control
        /// </summary>
        /// <value>
        /// The grid.
        /// </value>
        private Grid Grid { get; set; }

        /// <summary>
        /// Gets or sets the information label.
        /// </summary>
        /// <value>
        /// The information label.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
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

            // tab control
            var y = Theme.ControlHeight + Theme.ControlLargeSpacing;
            this.TabControl = new TabControl("MyTreeView")
            {
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = y,
                }
            };
            this.AddControl(this.TabControl);

            // grid control
            y = (int)(y + this.TabControl.Config.Height + Theme.ControlLargeSpacing);
            this.Grid = new Grid("MyGrid")
            {
                ConfigFillType = GridFillType.Button,
                ConfigLineWidth = 1,
                ConfigColumnCount = 3,
                ConfigRowCount = 3,
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = y,
                }
            };
            this.AddControl(this.Grid);

            // set my size
            Config.Height = this.Grid.Config.PositionY + this.Grid.Config.Height;
            Config.Width = this.TabControl.Config.Width + (Theme.ControlLargeSpacing * 2);
        }
    }
}
