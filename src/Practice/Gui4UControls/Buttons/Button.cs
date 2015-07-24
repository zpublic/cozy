// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Button.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Just a standard button
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Buttons
{
    using System;

    using GUI4UFramework.EventArgs;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;

    /// <summary>
    /// Just a standard button.
    /// </summary>
    public class Button : ButtonBase
    {
        /// <summary>
        /// Button is pressed in.
        /// </summary>
        public event EventHandler<GameTimeEventArgs> LeftMousePressed;

        /// <summary>
        /// Left mouse is released after button pressed in.
        /// </summary>
        public event EventHandler<GameTimeEventArgs> LeftMouseReleased;

        /// <summary>
        /// Click-release event.
        /// </summary>
        public event EventHandler<GameTimeEventArgs> Clicked;

        /// <summary>
        /// The texture name for in the event of a "left mouse down".
        /// </summary>
        private string textureNameLeftMouseDown;

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        public Button(string name) : base(name)
        {
            this.Config.Width = this.Theme.ControlWidth;
            this.Config.Height = this.Theme.ControlHeight;
        }

        /// <summary>Called when graphics resources need to be loaded. 
        /// Use this for the usage of :
        /// - creation of the internal embedded controls.
        /// - setting of the variables and resources in this control
        /// - to load any game-specific graphics resources
        /// - take over the config width and height and use it into State
        /// - overriding how this item looks like , by settings its texture or theme
        /// 
        /// Call base.LoadContent before you do your override code, this will cause :
        /// - State.SourceRectangle to be reset to the Config.Size. 
        /// </summary>
        public override void LoadContent()
        {
            // this needs to happen first
            base.LoadContent();

            // set my appearance values
            this.textureNameLeftMouseDown = Manager.ImageCompositor.CreateRectangleTexture(
                this.Name,
                (int)this.Config.Width,
                (int)this.Config.Height,
                this.Theme.BorderWidth,
                this.Theme.ClickedFillColor,
                this.Theme.ClickedBorderColor);
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            Manager.ImageCompositor.Delete(this.textureNameLeftMouseDown);
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // retrieve the mouse pressed
            var leftMousePressed = Manager.InputManager.ReadLeftMousePressed();
            var leftMouseReleased = Manager.InputManager.ReadLeftMouseReleased();

            // update the label
            if (this.ConfigText != Label.ConfigText)
            {
                Label.ConfigText = this.ConfigText;
            }

            // update the visibility
            if (this.Config.Changed)
            {
                this.Config.ResetChanged();
                this.Label.Config.Visible = this.Config.Visible;
            }

            // is the control visible ?
            if (Config.Visible)
            {
                // Is mouse hovering over, and does the control have focus ?
                if (State.MouseHoveringOver && this.CheckIsFocused())
                {
                    this.CurrentTextureName = this.TextureNameHover;

                    // Is there a mouse press and the button is OFF ? 
                    if (leftMousePressed && this.ButtonState == ButtonState.Off)
                    {
                        this.RaiseMouseDownEvent(gameTime);
                        this.CurrentTextureName = this.textureNameLeftMouseDown;
                        this.ButtonState = ButtonState.On;
                    }
                    else if (leftMouseReleased && this.ButtonState == ButtonState.On)
                    {
                        this.ButtonState = ButtonState.Off;

                        Config.HoverColorsEnabled = true;

                        this.RaiseMouseUpEvent(gameTime);
                        this.RaiseMouseClickEvent(gameTime);
                    }
                }
                else
                {
                    this.CurrentTextureName = this.TextureNameDefault;

                    // turn it off if the mouse hovers off it
                    if (this.ButtonState == ButtonState.On)
                    {
                        this.ButtonState = ButtonState.Off;
                        this.RaiseMouseUpEvent(gameTime);
                    }
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Gets a value indicating whether this button is pressed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [state is pressed]; otherwise, <c>false</c>.
        /// </value>
        public bool StateIsPressed
        {
            get
            {
                return this.ButtonState == ButtonState.On;
            }
        }

        /// <summary>
        /// Raises the mouse up event.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        private void RaiseMouseUpEvent(GameTime gameTime)
        {
            if (this.LeftMouseReleased != null)
            {
                this.LeftMouseReleased(this, new GameTimeEventArgs(gameTime));
            }
        }

        /// <summary>
        /// Raises the mouse down event.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        private void RaiseMouseDownEvent(GameTime gameTime)
        {
            if (this.LeftMousePressed != null)
            {
                this.LeftMousePressed(this, new GameTimeEventArgs(gameTime));
            }
        }

        /// <summary>
        /// Raises the mouse click event.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        private void RaiseMouseClickEvent(GameTime gameTime)
        {
            if (this.Clicked != null)
            {
                this.Clicked(this, new GameTimeEventArgs(gameTime));
            }
        }
    }
}
