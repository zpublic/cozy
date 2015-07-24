// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DialogOkCancel.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the DialogOkCancel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Dialogs
{
    using GUI4UControls.Buttons;
    using GUI4UControls.Text;

    using GUI4UFramework.Structural;

    /// <summary>
    /// Shows a dialog window to give the user a decision.
    /// </summary>
    public class DialogOkCancel : DialogBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogOkCancel"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public DialogOkCancel(string name) : base(name)
        {
            this.Title = "Save file example";
            this.DialogText = "Do you really want to overwrite file ?";
        }

        /// <summary>
        /// Gets or sets the OK button control
        /// </summary>
        /// <value>
        /// The OK button.
        /// </value>
        protected Button OkButton { get; set; }

        /// <summary>
        /// Gets or sets the cancel button control
        /// </summary>
        /// <value>
        /// The cancel button.
        /// </value>
        protected Button CancelButton { get; set; }

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
            this.CancelButton = new Button(Name + "-CancelButton")
                               {
                                   ConfigText = "Cancel",
                                   Config =
                                       {
                                           Width = this.Theme.ControlWidth,
                                           Height = this.Theme.ControlHeight
                                       }
                               };
            this.CancelButton.Config.PositionX = Config.Width - this.CancelButton.Config.Width - Theme.ControlLargeSpacing;
            this.CancelButton.Config.PositionY = Config.Height - this.CancelButton.Config.Height - Theme.ControlLargeSpacing;
            this.AddControl(this.CancelButton);

            // make the button
            this.OkButton = new Button(Name + "-YesButton")
                           {
                               ConfigText = "OK",
                               Config =
                                   {
                                       Width = this.Theme.ControlWidth,
                                       Height = this.Theme.ControlHeight,
                                       PositionX = this.Theme.ControlLargeSpacing,
                                       PositionY = this.Config.Height - this.CancelButton.Config.Height - this.Theme.ControlLargeSpacing
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
                                   PositionX = this.Theme.ControlLargeSpacing,
                                   PositionY = this.Theme.ControlLargeSpacing
                               }
                       };
            this.AddControl(this.Text);
        }
    }
}
