// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWindowText.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the TestWindowText type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tests
{
    using System.Diagnostics.CodeAnalysis;

    using GUI4UControls.Text;

    using GUI4UFramework.Graphics;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Creates a test window to test Text-controls
    /// </summary>
    public class TestWindowText : TestWindowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestWindowText"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TestWindowText(string name) : base(name)
        {
            this.Title = "Test text";
        }

        /// <summary>
        /// Gets or sets the label control to test
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public Label Label { get; set; }

        /// <summary>
        /// Gets or sets the text box-control to test
        /// </summary>
        /// <value>
        /// The text box.
        /// </value>
        public TextBox TextBox { get; set; }

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
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "Blablablabla\nffdfdoj\nfdfdsfjdsi\nfdfdsfdsf\n")]
        public override void LoadContent()
        {
            base.LoadContent();

            // label
            var y = (float)Theme.ControlLargeSpacing + Theme.ControlHeight;
            this.Label = new Label("MyLabel")
            {
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing, 
                    PositionY = y, 
                }, 
                ConfigText = "Test label ff", 
                ConfigHorizontalAlignment = HorizontalAlignment.Left, 
                ConfigVerticalAlignment = VerticalAlignment.Center
            };
            this.AddControl(this.Label);

            // text-box
            y = y + this.Label.Config.Height + Theme.ControlSmallSpacing;
            this.TextBox = new TextBox("MyTextBox")
            {
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing, 
                    PositionY = y, 
                }, 
                Text = "My text control", 
            };
            this.AddControl(this.TextBox);

            // multi-line text-box
            y = y + this.TextBox.Config.Height + Theme.ControlSmallSpacing;
            var multilineTextBox = new MultilineTextBox("MyMultiLineTextBox")
            {
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = y,
                },
                ConfigText = "This is test text.\nTo see if this is working.\nIf you see this.\nAll is working.\n"
            };
            this.AddControl(multilineTextBox);

            Config.Width = this.Label.Config.Width + (Theme.ControlLargeSpacing * 4);
            Config.Height = multilineTextBox.Config.PositionY + (multilineTextBox.Config.Height * 4) + Theme.ControlLargeSpacing;
        }
    }
}
