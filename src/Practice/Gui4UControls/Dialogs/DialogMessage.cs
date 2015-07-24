// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DialogMessage.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the DialogMessage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Dialogs
{
    using GUI4UControls.Buttons;
    using GUI4UControls.Text;

    using GUI4UFramework.Structural;

    /// <summary>
    /// Shows a dialog-window with a message that needs to be told to the user.
    /// </summary>
    public class DialogMessage : DialogBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogMessage"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public DialogMessage(string name) : base(name)
        {
            this.Title = "Message";
            this.DialogText = "Press OK to continue";
        }

        /// <summary>
        /// Gets or sets the OK button control
        /// </summary>
        /// <value>
        /// The OK button.
        /// </value>
        public Button OkButton { get; set; }

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
            this.OkButton = new Button(Name + "-YesButton")
                                {
                                    ConfigText = "OK",
                                    Config =
                                        {
                                            Width = this.Theme.ControlWidth,
                                            Height = this.Theme.ControlHeight,
                                            PositionX = this.Config.Width - this.Theme.ControlWidth - this.Theme.ControlLargeSpacing,
                                            PositionY = this.Config.Height - this.Theme.ControlHeight - this.Theme.ControlLargeSpacing
                                        }
                                };
            this.AddControl(this.OkButton);

            // make the text
            this.Text = new Label(Name + "-Text")
                            {
                                ConfigText = this.DialogText,
                                Config =
                                    {
                                        Width = this.Config.Width - (this.Theme.ControlLargeSpacing * 2),
                                        Height = this.Config.Height - (this.Theme.ControlLargeSpacing * 2),
                                        PositionX = this.Theme.ControlLargeSpacing
                                    }
                            };

            Text.Config.PositionY = Theme.ControlLargeSpacing;
            this.AddControl(this.Text);
        }
    }
}
