// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWindowListBox.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the TestWindowListBox type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tests
{
    using GUI4UControls.Buttons;

    using GUI4UFramework.EventArgs;
    using GUI4UFramework.Structural;

    using ListBox = GUI4UControls.Buttons.ListBox;

    /// <summary>
    /// A test-window to test the List-Box
    /// </summary>
    public class TestWindowListBox : TestWindowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestWindowListBox"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TestWindowListBox(string name) : base(name)
        {
            this.Title = "Test list-box";
        }

        /// <summary>
        /// Gets or sets the ListBox control.
        /// </summary>
        /// <value>
        /// The ListBox.
        /// </value>
        public ListBox ListBox { get; set; }

        /// <summary>
        /// Gets or sets the add button. This will add a ListBox-item.
        /// </summary>
        /// <value>
        /// The button add.
        /// </value>
        public Button ButtonAdd { get; set; }

        /// <summary>
        /// Gets or sets the remove button. This will remove a ListBox-item.
        /// </summary>
        /// <value>
        /// The button remove.
        /// </value>
        public Button ButtonRemove { get; set; }

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

            // the list-box
            this.ListBox = new ListBox("MyListBox")
            {
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = Theme.ControlLargeSpacing + Theme.ControlHeight,

                    Visible = true
                },
            };

            this.AddControl(this.ListBox);

            // the add-button
            this.ButtonAdd = new Button("AddButton");
            this.ButtonAdd.Clicked += this.AddClicked;
            this.AddControl(this.ButtonAdd);

            // the remove-button
            this.ButtonRemove = new Button("RemoveButton");
            this.ButtonRemove.Clicked += this.RemoveClicked;
            this.AddControl(this.ButtonAdd);

            // now add dynamic items
            this.ListBox.AddListItem(new ListBoxItem("Test 1", "MyListItem"));
            this.ListBox.AddListItem(new ListBoxItem("Test 2", "MyListItem2"));
            this.ListBox.AddListItem(new ListBoxItem("Test 1", "MyListItem3"));
            this.ListBox.AddListItem(new ListBoxItem("Test 2", "MyListItem4"));
            this.ListBox.AddListItem(new ListBoxItem("Test 1", "MyListItem5"));
            this.ListBox.AddListItem(new ListBoxItem("Test 2", "MyListItem6"));

            Config.Width = this.ListBox.Config.Width + (Theme.ControlLargeSpacing * 2);
            Config.Height = this.ListBox.Config.Height + (Theme.ControlHeight * 2);
        }

        /// <summary>
        /// Called when the add-button is pressed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="gameTimeEventArgs">The <see cref="GameTimeEventArgs"/> instance containing the event data.</param>
        private void AddClicked(object sender, GameTimeEventArgs gameTimeEventArgs)
        {
            this.ListBox.AddListItem(new ListBoxItem("Test 2", "MyListItem6"));
        }

        /// <summary>
        /// Called when the remove-button is pressed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="gameTimeEventArgs">The <see cref="GameTimeEventArgs"/> instance containing the event data.</param>
        private void RemoveClicked(object sender, GameTimeEventArgs gameTimeEventArgs)
        {
            if (this.ListBox.ListBoxItems.Count > 0)
            {
                this.ListBox.RemoveListItem(this.ListBox.ListBoxItems[this.ListBox.ListBoxItems.Count - 1]);
            }
        }
    }
}
