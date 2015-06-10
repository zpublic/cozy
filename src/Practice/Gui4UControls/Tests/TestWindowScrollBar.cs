// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWindowScrollBar.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the TestWindowScrollBar type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tests
{
    using GUI4UControls.ScrollBar;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Creates a test-window that test the Scrollbar-controls.
    /// </summary>
    public class TestWindowScrollBar : TestWindowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestWindowScrollBar"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TestWindowScrollBar(string name) : base(name)
        {
            this.Title = "Test scroll bar";
        }

        /// <summary>
        /// Gets or sets the vertical scroll bar control
        /// </summary>
        /// <value>
        /// The vertical scroll bar.
        /// </value>
        private ScrollBarVertical ScrollBarVertical { get; set; }

        /// <summary>
        /// Gets or sets the horizontal scroll bar control
        /// </summary>
        /// <value>
        /// The horizontal scroll bar.
        /// </value>
        private ScrollBarHorizontal Horizontal { get; set; }

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

            // Horizontal progress-bar
            var x = Theme.ControlLargeSpacing + Theme.ControlHeight + Theme.ControlLargeSpacing;
            var y = Theme.ControlHeight + Theme.ControlLargeSpacing;
            this.Horizontal = new ScrollBarHorizontal(Name + "- Horizontal")
            {
                Config =
                {
                    PositionX = x,
                    PositionY = y
                }
            };
            this.AddControl(this.Horizontal);

            // VerticalScrollbar progress-bar
            x = Theme.ControlLargeSpacing;
            this.ScrollBarVertical = new ScrollBarVertical(Name + "- VerticalScrollbar")
            {
                Config =
                {
                    PositionX = x,
                    PositionY = y
                }
            };
            this.AddControl(this.ScrollBarVertical);

            Config.Width = this.Horizontal.Config.PositionX + this.Horizontal.Config.Width + (Theme.ControlLargeSpacing * 2);
            Config.Height = this.ScrollBarVertical.Config.PositionY + this.ScrollBarVertical.Config.Height + (Theme.ControlLargeSpacing * 2);
        }
    }
}
