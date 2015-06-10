// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWindowImages.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the TestWindowImages type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tests
{
    using GUI4UControls.Images;

    using GUI4UFramework.Structural;

    /// <summary>
    /// A test-window for the Image-control
    /// </summary>
    public class TestWindowImages : TestWindowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestWindowImages"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TestWindowImages(string name)
            : base(name)
        {
            this.Title = "Test images";
        }

        /// <summary>
        /// Gets or sets the image control.
        /// </summary>
        /// <value>
        /// The image control.
        /// </value>
        public ImageControl ImageControl { get; set; }

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

            this.ImageControl = new ImageControl("MyImageTest")
            {
                Config =
                {
                    PositionX = 0, PositionY = 0,
                    Width = 200, Height = 100,
                },
                ImagePath = "Textures\\acid"
            };

            this.AddControl(this.ImageControl);

            this.Config.Width = Theme.ControlLargeSpacing + this.ImageControl.Config.Width + Theme.ControlLargeSpacing;
            this.Config.Height = Theme.ControlLargeSpacing + this.ImageControl.Config.Height + Theme.ControlLargeSpacing;
        }
    }
}
