// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextBoxCursor.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Is the indicator where text should be typed.
//   TextBoxCursor should be the same size as Text.
//   To set where TextBoxCursor should be show , update CursorPosition.
//   This should move TextBoxCursor to the place needed.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Text
{
    using System.Diagnostics;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Is the indicator where text should be typed.
    /// TextBoxCursor should be the same size as Text.
    /// To set where TextBoxCursor should be show , update CursorPosition.
    /// This should move TextBoxCursor to the place needed.
    /// </summary>
    public class TextBoxCursor : Control
    {
        /// <summary>
        /// A counter used to flash the cursor visible and invisible.
        /// </summary>
        private int cursorFlashCounter;

        /// <summary>
        /// The flash time for this cursor.
        /// </summary>
        private const int CursorFlashTime = 20;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBoxCursor"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TextBoxCursor(string name) : base(name)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether we are [debug mode].
        /// </summary>
        /// <value>
        /// <c>true</c> if [debug mode]; otherwise, <c>false</c>.
        /// </value>
        public bool ConfigDebugMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we [blink] or not.
        /// </summary>
        /// <value>
        /// <c>true</c> if [blink]; otherwise, <c>false</c>.
        /// </value>
        public bool ConfigBlinking { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we [show the cursor].
        /// </summary>
        /// <value>
        /// <c>true</c> if [show cursor]; otherwise, <c>false</c>.
        /// </value>
        public bool ConfigShowCursor { get; set; }

        /// <summary>
        /// Gets or sets the character index for the cursor position.
        /// </summary>
        /// <value>
        /// The index of the configuration.
        /// </value>
        public int ConfigIndex { get; set; }

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
            base.LoadContent();
            this.UpdateDrawSizeByConfig();

            Manager.ImageCompositor.CreateSpriteFont(Theme.FontName);

            // Calculate the width and height
            Config.Width = 1; // width should be really thin
            Config.Height = (int)Manager.ImageCompositor.ReadSizeString(Theme.FontName, "M").Y - 1; // height should be compared to the used font
            this.UpdateDrawSizeByConfig();

            // create the texture
            this.CurrentTextureName = Manager.ImageCompositor.CreateRectangleTexture(this.Name, (int)Config.Width, (int)Config.Height, 0, GUIColor.MidnightBlue(), GUIColor.MidnightBlue());

            // create a new color for the texture..
            var count = (int)(Config.Width * Config.Height);
            var colorArray = new GUIColor[count];
            for (var i = 0; i < count; i++)
            {
                colorArray[i] = GUIColor.MidnightBlue();
            }

            var map = new ColorMap(colorArray, (int)this.Config.Width, (int)this.Config.Height);

            Manager.ImageCompositor.UpdateTexture(this.CurrentTextureName, map);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // calculate the draw position , by adding my parents pos + my pos + where the cursor should be
            State.DrawPosition = Parent.State.DrawPosition + new DVector2(Config.PositionX, Config.PositionY);

            // set cursor-blink on or off per time chunk
            if (this.cursorFlashCounter >= CursorFlashTime)
            {
                this.ConfigBlinking = !this.ConfigBlinking;
                this.cursorFlashCounter = 0;
                if (this.ConfigDebugMode)
                {
                    Debug.WriteLine("Blink : " + this.ConfigBlinking);
                }
            }

            this.cursorFlashCounter++;
        }

        /// <summary>
        /// Draw the texture at DrawPosition combined with its offset
        /// </summary>
        public override void DrawMyData()
        {
            // we are killing the default behavior of drawing data , cause sometimes we DON'T want to be shown.
            // base.DrawMyData();

            // draw the cursor if _blink is not on, and if the cursor should be shown.
            if (this.ConfigBlinking && this.ConfigShowCursor)
            {
                Manager.ImageCompositor.Draw(this.CurrentTextureName, this.State, GUIColor.White());
            }
        }

        /// <summary>
        /// Resets the blink of the cursor.
        /// </summary>
        protected internal void ResetCursorBlink()
        {
            this.ConfigBlinking = false;
            this.cursorFlashCounter = -(CursorFlashTime / 4);
        }
    }
}
