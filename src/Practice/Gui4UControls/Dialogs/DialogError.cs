// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DialogError.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the DialogError type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Dialogs
{
    using GUI4UControls.Buttons;
    using GUI4UControls.Images;
    using GUI4UControls.Text;

    using GUI4UFramework.Structural;

    /// <summary>
    /// A dialog-window that tells the user that there is a error.
    /// </summary>
    public class DialogError : DialogBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogError"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public DialogError(string name) : base(name)
        {
            this.Title = "Error";
            this.DialogText = "Something totally unexpected just happened";
        }

        /// <summary>
        /// Gets or sets the OK button control.
        /// </summary>
        /// <value>
        /// The OK button.
        /// </value>
        protected Button OkButton { get; set; }

        /// <summary>
        /// Gets or sets the error image control.
        /// </summary>
        /// <value>
        /// The error image.
        /// </value>
        public ImageControl ErrorImageControl { get; set; }

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
            // load the base dialog - making it load the window
            base.LoadContent();

            // make the button
            this.OkButton = new Button(Name + "-YesButton")
            {
                ConfigText = "OK",
                Config =
                    {
                        Width = Theme.ControlWidth, Height = Theme.ControlHeight
                    }
            };
            this.OkButton.Config.PositionX = Config.Width - this.OkButton.Config.Width - Theme.ControlLargeSpacing;
            this.OkButton.Config.PositionY = Config.Height - this.OkButton.Config.Height - Theme.ControlLargeSpacing;
            this.AddControl(this.OkButton);

            // Make the error image
            const int Y = 2;
            const int X = 2;
            this.ErrorImageControl = new ImageControl(Name + "-image")
            {
                Config =
                {
                    PositionX = X,
                    PositionY = Y,
                    Width = Theme.ControlHeight - 4,
                    Height = Theme.ControlHeight - 4
                },
                ImagePath = "Textures\\error"
            };
            this.AddControl(this.ErrorImageControl);

            // make the text
            this.Text = new Label(Name + "-Text")
            {
                ConfigText = DialogText,
                Config =
                    {
                        Width = Config.Width - (Theme.ControlLargeSpacing * 2),
                        Height = Config.Height - (Theme.ControlLargeSpacing * 2),
                        PositionX = Theme.ControlLargeSpacing,
                    PositionY = Theme.ControlLargeSpacing
                }
            };
            this.AddControl(this.Text);
        }
    }
}
