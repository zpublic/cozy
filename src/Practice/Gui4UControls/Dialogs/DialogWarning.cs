// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DialogWarning.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the DialogWarning type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Dialogs
{
    using GUI4UControls.Buttons;
    using GUI4UControls.Images;
    using GUI4UControls.Text;

    using GUI4UFramework.Structural;

    /// <summary>
    /// Shows the user a warning in a dialog-window
    /// </summary>
    public class DialogWarning : DialogBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogWarning"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public DialogWarning(string name)
            : base(name)
        {
            this.Title = "Warning";
            this.DialogText = "Are you sure ?";
        }

        /// <summary>
        /// Gets or sets the no button control
        /// </summary>
        /// <value>
        /// The no button.
        /// </value>
        protected Button NoButton { get; set; }

        /// <summary>
        /// Gets or sets the yes button control
        /// </summary>
        /// <value>
        /// The yes button.
        /// </value>
        protected Button YesButton { get; set; }

        /// <summary>
        /// Gets or sets the warning image.
        /// </summary>
        /// <value>
        /// The warning image.
        /// </value>
        protected ImageControl WarningImageControl { get; set; }

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
            // load the base dialog - making it load the form
            base.LoadContent();

            // make the button
            this.NoButton = new Button(Name + "-YesButton")
            {
                ConfigText = "No",
                Config =
                {
                    PositionX = Config.Width - Theme.ControlWidth - Theme.ControlLargeSpacing,
                    PositionY = Config.Height - Theme.ControlHeight - Theme.ControlLargeSpacing,
                    Width = Theme.ControlWidth,
                    Height = Theme.ControlHeight
                }
            };
            this.AddControl(this.NoButton);

            // make the button
            this.YesButton = new Button(Name + "-YesButton")
            {
                ConfigText = "YES",
                Config =
                {
                    Width = Theme.ControlWidth,
                    Height = Theme.ControlHeight,
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = Config.Height - this.NoButton.Config.Height - Theme.ControlLargeSpacing
                }
            };
            this.AddControl(this.YesButton);

            // Make the error image
            const int Y = 2;
            const int X = 2;
            this.WarningImageControl = new ImageControl(Name + "-image")
            {
                Config =
                {
                    PositionX = X,
                    PositionY = Y,
                    Width = Theme.ControlHeight - 4,
                    Height = Theme.ControlHeight - 4
                },
                ImagePath = "Textures\\warning"
            };
            this.AddControl(this.WarningImageControl);

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
