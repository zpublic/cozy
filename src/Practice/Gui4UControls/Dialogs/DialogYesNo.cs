// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DialogYesNo.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the DialogYesNo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Dialogs
{
    using GUI4UControls.Buttons;
    using GUI4UControls.Text;

    using GUI4UFramework.Structural;

    /// <summary>
    /// Shows the user a dialog-window where they have a question and the choice between yes or no.
    /// </summary>
    public class DialogYesNo : DialogBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogYesNo"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public DialogYesNo(string name) : base(name)
        {
            this.Title = "Confirm";
            this.DialogText = "Do you want to do this ?";
        }

        /// <summary>
        /// Gets or sets the yes button control.
        /// </summary>
        /// <value>
        /// The yes button.
        /// </value>
        protected Button YesButton { get; set; }

        /// <summary>
        /// Gets or sets the no button control.
        /// </summary>
        /// <value>
        /// The no button.
        /// </value>
        protected Button NoButton { get; set; }

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
                               ConfigText = "NO",
                               Config =
                                   {
                                       Width = this.Theme.ControlWidth,
                                       Height = this.Theme.ControlHeight
                                   }
                           };
            this.NoButton.Config.PositionX = Config.Width - this.NoButton.Config.Width - Theme.ControlLargeSpacing;
            this.NoButton.Config.PositionY = Config.Height - this.NoButton.Config.Height - Theme.ControlLargeSpacing;
            this.AddControl(this.NoButton);

            // make the button
            this.YesButton = new Button(Name + "-YesButton")
                            {
                                ConfigText = "YES",
                                Config =
                                    {
                                        Width = this.Theme.ControlWidth,
                                        Height = this.Theme.ControlHeight,
                                        PositionX = this.Theme.ControlLargeSpacing,
                                        PositionY = this.Config.Height - this.NoButton.Config.Height - this.Theme.ControlLargeSpacing
                                    }
                            };
            this.AddControl(this.YesButton);

            // make the text
            this.Text = new Label(Name + "-Text")
                       {
                           ConfigText = this.DialogText,
                           Config =
                               {
                                   Width = this.Config.Width - (this.Theme.ControlLargeSpacing * 2),
                                   Height = this.Config.Height - (this.Theme.ControlLargeSpacing * 2),
                                   PositionX = this.Theme.ControlLargeSpacing,
                                   PositionY = this.Theme.ControlLargeSpacing
                               }
                       };
            this.AddControl(this.Text);
        }
    }
}
