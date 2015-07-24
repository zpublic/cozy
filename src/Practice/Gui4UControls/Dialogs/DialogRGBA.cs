// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DialogRGBA.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the DialogRGBA type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Dialogs
{
    using GUI4UControls.Buttons;
    using GUI4UControls.Color;
    using GUI4UControls.Tests;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Shows a dialog-window where the user could pick a color
    /// </summary>
    public class DialogRGBA : TestWindowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogRGBA"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public DialogRGBA(string name) : base(name)
        {
            this.Title = "RGBA";
        }

        /// <summary>
        /// Gets or sets the color picker control in RGBA-style.
        /// </summary>
        /// <value>
        /// The color picker in RGBA-style.
        /// </value>
        private ColorPickerRGBA ColorPickerRGBA { get; set; }

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

            // RGBA color picker
            var posy = Theme.ControlLargeSpacing + Theme.ControlHeight;
            this.ColorPickerRGBA = new ColorPickerRGBA("MyColorPickerRGBA")
            {
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = posy,
                },
                Color = new GUIColor(118, 45, 23)
            };
            this.AddControl(this.ColorPickerRGBA);

            Config.Height = 
                            this.ColorPickerRGBA.Config.PositionY + 
                            this.ColorPickerRGBA.Config.Height + 
                            Theme.ControlLargeSpacing;

            Config.Width =
                this.ColorPickerRGBA.Config.PositionX +
                this.ColorPickerRGBA.Config.Width +
                Theme.ControlLargeSpacing;
        }
    }
}
