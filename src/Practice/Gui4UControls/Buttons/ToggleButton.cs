// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ToggleButton.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Toggle button
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Buttons
{
    using System;

    using GUI4UFramework.EventArgs;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;

    /// <summary>
    /// Toggle button
    /// </summary>
 public class ToggleButton : ButtonBase
    {
        /// <summary>
        /// Occurs when this buttons is toggled.
        /// </summary>
        public event EventHandler<ButtonStateEventArgs> OnToggle;

        /// <summary>
        /// The texture name for when there is a hover
        /// </summary>
        private string textureHovered;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleButton"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ToggleButton(string name) : base(name)
        {
            this.ToggleIsPressed = false;

            Config.Width = Theme.ControlWidth;
            Config.Height = Theme.ControlHeight;

            this.OnToggle += this.Toggle;
        }

        /// <summary>
        /// Gets a value indicating whether [toggle is pressed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [toggle is pressed]; otherwise, <c>false</c>.
        /// </value>
        public bool ToggleIsPressed { get; private set; }

        /// <summary>
        /// Called when graphics resources need to be loaded. 
        /// 
        /// Use this for the usage of :
        /// - creation of the internal embedded controls.
        /// - setting of the variables and resources in this control
        /// - to load any game-specific graphics resources
        /// - take over the config width and height and use it into State
        /// - overriding how this item looks like , by settings its texture or theme
        /// 
        /// Call base.LoadContent before you do your override code, this will cause :
        /// - State.SourceRectangle to be reset to the Config.Size 
        /// </summary>
        public override void LoadContent()
        {
            // this needs to happen first
            base.LoadContent();

            // take over the config width and height and use it into State
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();

            // set my appearance values
            this.textureHovered = Manager.ImageCompositor.CreateRectangleTexture(
                                                                                this.CurrentTextureName,
                                                                                (int)State.Width,
                                                                                (int)State.Height,
                                                                                Theme.BorderWidth,
                                                                                Theme.ClickedFillColor,
                                                                                Theme.ClickedBorderColor);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // if we are not enabled then do not do anything
            if (!this.Config.Enabled)
            {
                return;
            }

            if (this.ConfigText != this.Label.ConfigText)
            {
                this.Label.ConfigText = this.ConfigText;
            }

            var pressed = this.Manager.InputManager.ReadLeftMousePressed();
            var released = this.Manager.InputManager.ReadLeftMouseReleased();

            // Is mouse hovering over?
            if (this.State.MouseHoveringOver && this.CheckIsFocused())
            {
                // Mouse click?
                if (pressed && !this.ToggleIsPressed)
                {
                    this.ToggleIsPressed = true;
                    var state = this.ButtonState == ButtonState.Off
                                    ? ButtonState.On
                                    : ButtonState.Off;
                    this.OnToggle(this, new ButtonStateEventArgs(state));
                }
            }

            if (released)
            {
                this.ToggleIsPressed = false;
            }
        }

        /// <summary>Reacts to when there is a toggle event</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="stateEventArgs">The <see cref="ButtonStateEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.ArgumentNullException">StateEventArgs can not be null.</exception>
        public void Toggle(object sender, ButtonStateEventArgs stateEventArgs)
        {
#if DEBUG
            if (stateEventArgs == null)
            {
                throw new ArgumentNullException("stateEventArgs");
            }
#endif
            var state = stateEventArgs.ButtonState;
            if (state == ButtonState.On)
            {
                this.Config.HoverColorsEnabled = false;
                this.Theme.BorderWidth = 2;
                this.ButtonState = ButtonState.On;
                this.CurrentTextureName = this.textureHovered;
            }
            else
            {
                this.Config.HoverColorsEnabled = true;
                this.Theme.BorderWidth = 1;
                this.ButtonState = ButtonState.Off;
                this.CurrentTextureName = this.TextureNameDefault;
            }
        }
    }
}
