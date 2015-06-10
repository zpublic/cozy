// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowTitleBar.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the WindowTitleBar type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Containers
{
    using GUI4UControls.Buttons;
    using GUI4UControls.Text;

    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// The title-bar is shown on the top of a window.
    /// It has a Title and a close-button and should also have a Icon.
    /// </summary>
    public class WindowTitleBar : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowTitleBar"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public WindowTitleBar(string name) : base(name)
        {
            // create a close button to be placed in the right corner
            this.CloseButton = new Button(this.Name + "-CloseButton")
            {
                ConfigText = "X",
                Config = { PositionX = 0, PositionY = 0 }
            };

            // create a label that should be used to show a title
            this.TextLabel = new Label(this.Name + "-Title")
            {
                ConfigText = "Window name here",
                Config =
                    {
                        PositionX = 0, PositionY = 0
                    }
            };
        }

        /// <summary>
        /// Gets or sets the close button. The close-button should close the window.
        /// </summary>
        /// <value>
        /// The close button.
        /// </value>
        public Button CloseButton { get; set; }

        /// <summary>
        /// Gets or sets the text label in the title bar.
        /// </summary>
        /// <value>
        /// The text label.
        /// </value>
        protected Label TextLabel { get; set; }

        /// <summary>
        /// Gets or sets the text shown in the title bar.
        /// </summary>
        /// <value>
        /// The configuration text.
        /// </value>
        public string ConfigText { get; set; }

        /// <summary>
        /// Gets a value indicating whether the title-bar is pressed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [state is pressed]; otherwise, <c>false</c>.
        /// </value>
        public bool StateIsPressed { get; private set; }

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
            // do the basic stuff
            base.LoadContent();
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();

            // make the close button label
            this.CloseButton.Config.Width = this.Theme.ControlHeight;
            this.CloseButton.Config.Height = this.Theme.ControlHeight;
            this.CloseButton.Config.PositionX = this.Config.Width - this.CloseButton.Config.Width;
            this.CloseButton.Config.PositionY = 0;
            this.AddControl(this.CloseButton);

            // make the window title label
            if (string.IsNullOrEmpty(this.ConfigText))
            {
                this.ConfigText = "Some bla title";
            }

            this.TextLabel.Config.PositionX = 0;
            this.TextLabel.Config.PositionY = 0;
            this.TextLabel.Config.Width = this.Config.Width - this.CloseButton.Config.Width;
            this.TextLabel.Config.Height = this.Theme.ControlHeight;
            this.TextLabel.ConfigText = this.ConfigText;
            this.AddControl(this.TextLabel);

            // make the background
            this.CurrentTextureName = this.Manager.ImageCompositor.CreateRectangleTexture(
                                                    this.Name,
                                                    (int)this.Config.Width,
                                                    (int)this.Config.Height,
                                                    1,
                                                    this.Theme.ContainerFillColor,
                                                    this.Theme.BorderColor);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // do the basic stuff first
            base.Update(gameTime);

            // get all the values from current configuration , 
            // and create a visual state that can be drawn from it
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();
            this.UpdateDrawSourceRectangleByConfig();

            // Update the title
            this.TextLabel.ConfigText = this.ConfigText;

            // take mouse input
            var leftMousePressed = this.Manager.InputManager.ReadLeftMousePressed();
            var leftMouseReleased = this.Manager.InputManager.ReadLeftMouseReleased();

            // is the control visible ?
            if (this.Config.Visible)
            {
                // Is mouse hovering over, and does the control have focus ?
                if (this.State.MouseHoveringOver && this.CheckIsFocused())
                {
                    // Is there a mouse press and the button is OFF ? 
                    if (leftMousePressed && this.StateIsPressed == false)
                    {
                        this.StateIsPressed = true;
                    }
                    else if (leftMouseReleased && this.StateIsPressed == true)
                    {
                        this.StateIsPressed = false;
                    }
                }
                else if (leftMouseReleased)
                {
                    this.StateIsPressed = false;
                }
            }
        }
    }
}
