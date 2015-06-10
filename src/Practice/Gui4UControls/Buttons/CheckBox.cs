// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckBox.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Toggle check-box with label
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Buttons
{
    using System.Diagnostics;

    using GUI4UControls.Text;

    using GUI4UFramework.Colors;
    using GUI4UFramework.EventArgs;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Toggle check-box with label
    /// </summary>
    public class CheckBox : Control
    {
        /// <summary>
        /// To allow only one change at a time
        /// </summary>
        private bool mouseReleased = true;

        /// <summary>
        /// If the check-box is set  on this control
        /// </summary>
        private bool isChecked;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBox"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public CheckBox(string name)
            : base(name)
        {
            this.FontColor = GUIColor.MidnightBlue();

            this.Theme.FillColor = this.Theme.InputFillColor;
            this.Theme.BorderColor = this.Theme.BorderColor;

            this.Config.Width = this.Theme.ControlWidth;
            this.Config.Height = this.Theme.ControlHeight;

            this.ConfigText = string.Empty;
        }

        /// <summary> 
        /// Gets or sets the label that contains the text that defines what is been checked.
        /// </summary>
        /// <value>
        /// The text label.
        /// </value>
        protected Label Label { get; set; }

        /// <summary>Gets or sets the button that gives you the option to 'tick' the check-box</summary>
        /// <value>The tick box control.</value>
        protected Button TickBox { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CheckBox"/> is checked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if checked; otherwise, <c>false</c>.
        /// </value>
        public bool Checked
        {
            get
            {
                return this.isChecked;
            }

            set
            {
                this.isChecked = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the font that is used to draw the text
        /// </summary>
        /// <value>
        /// The name of the font.
        /// </value>
        public string FontName { get; set; }

        /// <summary>
        /// Gets or sets the text that is shown on the text-box
        /// </summary>
        /// <value>
        /// The configuration text.
        /// </value>
        public string ConfigText { get; set; }

        /// <summary>
        /// Gets or sets the color of the font of the text that is shown on the check-box
        /// </summary>
        /// <value>
        /// The color of the font.
        /// </value>
        public GUIColor FontColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we are in debug mode to check if layout is correct].
        /// </summary>
        /// <value>
        /// <c>true</c> if [configuration debug layout]; otherwise, <c>false</c>.
        /// </value>
        public bool ConfigDebugLayout { get; set; }

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
            // **** do the basic stuff
            base.LoadContent();
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();

            // the padding is like this
            // outer border
            // pad
            // xText-width
            // pad
            // label-width
            // pad
            // outer border

            // **** add the tick-box
            const int Padding = 2;
            this.TickBox = new Button(this.Name + "-Check")
            {
                Manager = this.Manager,
                ConfigText = string.Empty,
                ConfigDebugLayout = this.ConfigDebugLayout,
                Config = { PositionX = Padding, PositionY = Padding, Height = this.Config.Height - Padding - Padding }
            };

            // make some room between top and bottom
            this.TickBox.Config.Width = this.TickBox.Config.Height; // make me square
            this.TickBox.Clicked += this.HandleToggle;

            // TickBox.LoadContent();
            this.AddControl(this.TickBox);

            // **** add the label
            this.Label = new Label(this.Name + "-ConfigText")
            {
                Manager = this.Manager,
                ConfigText = this.ConfigText,
                ConfigDebugLayout = this.ConfigDebugLayout,
                ConfigHorizontalAlignment = HorizontalAlignment.Left,
                ConfigVerticalAlignment = VerticalAlignment.Center,
                Config =
                {
                    PositionX = Padding + this.TickBox.Config.Width + Padding - this.Theme.BorderWidth,
                    PositionY = Padding,
                    Height = this.TickBox.Config.Height,
                    Width = this.Config.Width - (Padding * 3) - this.TickBox.Config.Width + this.Theme.BorderWidth
                }
            };

            // start with some space next to the check-square
            // place me at the same height as the check-square
            // make me the same height as the check-square
            // Label.LoadContent();
            this.AddControl(this.Label);

            // **** add the background
            this.CurrentTextureName = this.Manager.ImageCompositor.CreateRectangleTexture(this.Name + "-Back", (int)this.State.Width, (int)this.State.Height, this.Theme.BorderWidth, this.Theme.FillColor, this.Theme.BorderColor);
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            this.Label.UnloadContent();
            this.TickBox.UnloadContent();
            this.Manager.ImageCompositor.Delete(this.CurrentTextureName);
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            var leftPressed = this.Manager.InputManager.ReadLeftMousePressed();
            var leftReleased = this.Manager.InputManager.ReadLeftMouseReleased();
            base.Update(gameTime);

            // Is mouse hovering over?
            if (this.CheckIsFocused() && this.State.MouseHoveringOver)
            {
                // Mouse click?
                if (leftPressed && this.mouseReleased)
                {
                    this.HandleToggle(this, null);

                    this.mouseReleased = false;
                }
            }

            if (leftReleased)
            {
                this.mouseReleased = true;
            }
        }

        /// <summary>
        /// When the check event is been toggled
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="gameTimeEventArgs">The <see cref="GameTimeEventArgs"/> instance containing the event data.</param>
        private void HandleToggle(object sender, GameTimeEventArgs gameTimeEventArgs)
        {
            if (this.isChecked)
            {
                this.isChecked = false;
                this.TickBox.ConfigText = string.Empty;
            }
            else
            {
                this.isChecked = true;
                this.TickBox.ConfigText = "X";
            }

            Debug.WriteLine(this.Name + " checked = " + this.isChecked);
        }
    }
}
