// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWindowComboBox.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the TestWindowComboBox type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tests
{
    using GUI4UControls.Buttons;
    using GUI4UControls.Text;
    using GUI4UFramework.Structural;
    using ComboBox = GUI4UControls.Buttons.ComboBox;

    /// <summary>
    /// Creates a test-window for ComboBox-Controls
    /// </summary>
    public class TestWindowComboBox : TestWindowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestWindowComboBox"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TestWindowComboBox(string name) : base(name)
        {
            this.Title = "Test combo-box";
            this.Config.Height = (Theme.ControlHeight * 5) + (Theme.ControlLargeSpacing * 4);
            this.Config.Width = (Theme.ControlWidth * 1) + (Theme.ControlLargeSpacing * 2);
        }

        /// <summary>
        /// Gets or sets the ComboBox.
        /// </summary>
        /// <value>
        /// The ComboBox.
        /// </value>
        private ComboBox ComboBox { get; set; }

        /// <summary>
        /// Gets or sets the button add (to add item to ComboBox).
        /// </summary>
        /// <value>
        /// The button add.
        /// </value>
        private Button ButtonAdd { get; set; }

        /// <summary>
        /// Gets or sets the button remove (to remove item from ComboBox).
        /// </summary>
        /// <value>
        /// The button remove.
        /// </value>
        private Button ButtonRemove { get; set; }

        /// <summary>
        /// Gets or sets the label control that shows the user what is happening during buttons presses and scroll etc.
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

            // create the combo-box
            var y = Theme.ControlHeight + Theme.ControlLargeSpacing;
            this.ComboBox = new ComboBox("myFirstComboBox")
            {
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = y
                }
            };
            this.AddControl(this.ComboBox);

            // create the ADD-button
            y = (int)(y + this.ComboBox.Config.Height + Theme.ControlLargeSpacing);
            this.ButtonAdd = new Button("AddButton")
            {
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = y
                }
            };
            this.AddControl(this.ButtonAdd);

            // create the REMOVE-button
            y = (int)(y + this.ButtonAdd.Config.Height + Theme.ControlLargeSpacing);
            this.ButtonRemove = new Button("RemoveButton")
            {
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = y
                }
            };
            this.AddControl(this.ButtonRemove);

            // create the info-label
            y = (int)(y + this.ButtonRemove.Config.Height + Theme.ControlLargeSpacing);
            this.InfoLabel = new Label("Info label")
            {
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = y
                },
                ConfigText = "Oscariotes"
            };
            this.AddControl(this.InfoLabel);

            // now add dynamic items
            this.ComboBox.AddItem("Test1", null);
            this.ComboBox.AddItem("Test2", null);
        }
    }
}
