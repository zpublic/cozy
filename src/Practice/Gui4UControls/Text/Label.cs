// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Label.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Shows only ConfigText , just like a winform-label
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Text
{
    using System;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Shows only text
    /// </summary>
    public class Label : Control
    {
        /// <summary>
        /// The current text that is shown on screen.
        /// </summary>
        private string currentText;

        /// <summary>
        /// The horizontal text stripper utility.
        /// </summary>
        private HorizontalTextStripper horizontalTextStripper;

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Label(string name) : base(name)
        {
            this.ConfigVerticalAlignment = VerticalAlignment.Center;
            this.ConfigHorizontalAlignment = HorizontalAlignment.Center;

            Config.Width = Theme.ControlWidth;
            Config.Height = Theme.ControlHeight;

            this.ConfigFont = Theme.FontName;

            this.ConfigText = string.Empty;  // hack to avoid a render-time null check
            Config.AcceptsFocus = false;
        }

        /// <summary>
        /// Gets or sets the ConfigText should be shown , but will be clipped into StateTextShown , to fit inside the control.
        /// </summary>
        /// <value>
        /// The configuration text.
        /// </value>
        public string ConfigText { get; set; }

        /// <summary>
        /// Gets or sets the font for the text shown. Use this sparingly ! cause it needs to unload this control totally first when changed.
        /// </summary>
        /// <value>
        /// The configuration font.
        /// </value>
        public string ConfigFont { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we are debugging the layout of the control.
        /// </summary>
        /// <value>
        /// <c>true</c> if [configuration debug layout]; otherwise, <c>false</c>.
        /// </value>
        public bool ConfigDebugLayout { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment of the text shown.
        /// </summary>
        /// <value>
        /// The configuration horizontal alignment.
        /// </value>
        public HorizontalAlignment ConfigHorizontalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the vertical alignment of the text shown.
        /// </summary>
        /// <value>
        /// The configuration vertical alignment.
        /// </value>
        public VerticalAlignment ConfigVerticalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the ConfigText that will fit and will be shown in the control
        /// </summary>
        /// <value>
        /// The text shown.
        /// </value>
        public string StateTextShown { get; set; }

        /// <summary>
        /// Gets or sets the maximum size where the text must fit inside.
        /// </summary>
        /// <value>
        /// The maximum size of the state.
        /// </value>
        public DVector2 StateMaximumSize { get; set; }

        /// <summary>
        /// Gets the calculated width for the text.
        /// </summary>
        /// <value>
        /// The calculated width of the state.
        /// </value>
        public float StateCalculatedWidth { get; private set; }

        /// <summary>
        /// Gets the calculated height for the text.
        /// </summary>
        /// <value>
        /// The calculated height of the state.
        /// </value>
        public float StateCalculatedHeight { get; private set; }

        /// <summary>
        /// Gets the calculated x-offset, to position the text in the control.
        /// </summary>
        /// <value>
        /// The calculated offset x.
        /// </value>
        public float StateCalculatedOffsetX { get; private set; }

        /// <summary>
        /// Gets the calculated y-offset, to position the text in the control.
        /// </summary>
        /// <value>
        /// The calculated offset y.
        /// </value>
        public float StateCalculatedOffsetY { get; private set; }

        /// <summary>
        /// Gets or sets the first character index shown.
        /// </summary>
        /// <value>
        /// The state first character index shown.
        /// </value>
        public int StateFirstCharacterIndexShown { get; set; }

        /// <summary>
        /// Gets or sets the last character index shown.
        /// </summary>
        /// <value>
        /// The state last character index shown.
        /// </value>
        public int StateLastCharacterIndexShown { get; set; }

        /// <summary>
        /// Gets the count of the shown characters.
        /// </summary>
        /// <value>
        /// The length of the state shown character.
        /// </value>
        public int StateShownCharacterCount
        {
            get { return this.StateLastCharacterIndexShown - this.StateFirstCharacterIndexShown; }
        }

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
            base.LoadContent();
            this.UpdateDrawSizeByConfig();

            // make the sprite font
            if (string.IsNullOrEmpty(this.ConfigFont))
            {
                this.ConfigFont = Theme.FontName;
            }

            Manager.ImageCompositor.CreateSpriteFont(this.ConfigFont);

            // always fill the ConfigText-measurement , so we will always have a nonzero size..
            this.currentText = !string.IsNullOrEmpty(this.currentText)
                                                        ? this.currentText
                                                        : "T";

            // create the stripper ,this will create the StateTextShown from ConfigText
            this.horizontalTextStripper = new HorizontalTextStripper(Manager.ImageCompositor);

            // use the sprite-font to check how big the size would be
            this.UpdateTextWhenNeeded();

            this.CurrentTextureName = Manager.ImageCompositor.CreateRectangleTexture(
                                                                                this.Name + "-ConfigText",
                                                                                (int)State.Width,
                                                                                (int)State.Height,
                                                                                0,
                                                                                new GUIColor(128, 0, 0, 128),
                                                                                Theme.BorderColor);
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            Manager.ImageCompositor.Delete(this.CurrentTextureName);
            this.horizontalTextStripper = null;

            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateTextWhenNeeded();
        }

        /// <summary>
        /// 1 Finds out draw-position
        /// 2 If partly obscured , draw my string partly obscured
        /// 3 If not partly obscured , draw my string at draw-position 
        /// </summary>
        public override void DrawMyData()
        {
            if (Config.Visible == false)
            {
                return;
            }

            ////if (State.SourceRectangle != null && State.SourceRectangle.HasValue)
            ////// if (State.SourceRectangle != null && State.SourceRectangle.HasValue && State.PartiallyObscured)
            ////{
            ////    // say that we are drawing to a surface in memory
            ////    Manager.ImageCompositor.SetRenderTarget(_renderTargetName);

            ////    // clear that surface
            ////    Manager.ImageCompositor.Clear(GUIColor.Gainsboro());

            ////    // draw the ConfigText to the surface
            ////    var txtPos = new DVector2(State.SourceRectangle.X, -State.SourceRectangle.Y);
            ////    Manager.ImageCompositor.DrawString(Theme.FontName, ConfigText, txtPos, Theme.FontColor,1 );

            ////    // switch back to drawing on display1
            ////    Manager.ImageCompositor.UnsetRenderTarget(_renderTargetName);

            ////    // let the current texture use the rendered surface
            ////    Manager.ImageCompositor.UpdateTexture(CurrentTextureName, _renderTargetName);

            ////    // draw the ConfigText on screen
            ////    State.DrawPosition = drawPos + new DVector2(State.SourceRectangle.X, State.SourceRectangle.Y);
            ////    State.SourceRectangle = new Rectangle(0, 0, State.SourceRectangle.Width, State.SourceRectangle.Height);
            ////    Manager.ImageCompositor.Draw(CurrentTextureName, State, Theme.FontColor, 1);
            ////}
            ////else if (Parent.State.TotallyObscured == false)
            ////{
            ////    //draw the ConfigText!
            ////    string s = string.IsNullOrEmpty(ConfigText) ? "empty" : ConfigText;
            ////    Manager.ImageCompositor.DrawString(Theme.FontName, s, drawPos, Theme.FontColor, 1);
            ////}

            // draw the ConfigText!
            var position = this.State.DrawPosition + new DVector2(this.StateCalculatedOffsetX, this.StateCalculatedOffsetY);
            if (this.ConfigDebugLayout)
            {
                Manager.ImageCompositor.Draw(this.CurrentTextureName, this.State, new GUIColor(255, 255, 255));
            }

            if (string.IsNullOrEmpty(this.StateTextShown) == false)
            {
                Manager.ImageCompositor.DrawString(Theme.FontName, this.StateTextShown, position, Theme.FontColor);
            }
        }

        /// <summary>
        /// If public ConfigText changed, set internal current ConfigText to the same and change the rectangle by the new size. 
        /// </summary> 
        private void UpdateTextWhenNeeded()
        {
            // check for new ConfigText
            if (this.currentText == this.ConfigText)
            {
                return;
            }

            this.currentText = this.ConfigText;

            // start to strip the ConfigText !
            this.horizontalTextStripper.FontName = Theme.FontName;
            this.horizontalTextStripper.Alignment = this.ConfigHorizontalAlignment;
            this.StateTextShown = this.horizontalTextStripper.CalculateVisibleString(this.ConfigText, State.Width);
            this.StateFirstCharacterIndexShown = this.horizontalTextStripper.FirstCharacterIndexShown;
            this.StateLastCharacterIndexShown = this.horizontalTextStripper.LastCharacterIndexShown;

            // Calculate the width and height of the ConfigText
            if (!string.IsNullOrEmpty(this.StateTextShown))
            {
                var measure = Manager.ImageCompositor.ReadSizeString(Theme.FontName, this.ConfigText);
                this.StateCalculatedWidth = measure.X;
                this.StateCalculatedHeight = measure.Y;
            }
            else
            {
                this.StateCalculatedWidth = 1;
                this.StateCalculatedHeight = State.Height;
            }

            // change offset by alignment
            switch (this.ConfigHorizontalAlignment)
            {
                case HorizontalAlignment.Center:
                    this.StateCalculatedOffsetX = (State.Width / 2) - (this.StateCalculatedWidth / 2);
                    break;
                case HorizontalAlignment.Left:
                    this.StateCalculatedOffsetX = Theme.BorderWidth;
                    break;
                case HorizontalAlignment.Right:
                    this.StateCalculatedOffsetX = State.Width - this.StateCalculatedWidth - Theme.BorderWidth;
                    break;
            }

            // ConfigVerticalAlignment = VerticalAlignment.Bottom;
            switch (this.ConfigVerticalAlignment)
            {
                case VerticalAlignment.Center:
                    this.StateCalculatedOffsetY = (State.Height / 2) - (this.StateCalculatedHeight / 3);
                    break;
                case VerticalAlignment.Top:
                    this.StateCalculatedOffsetY = Theme.BorderWidth;
                    break;
                case VerticalAlignment.Bottom:
                    this.StateCalculatedOffsetY = State.Height - this.StateCalculatedHeight;
                    break;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            var s = string.Format(
                                    "{0} {1} {2}",
                                    base.ToString(),
                                    this.ConfigText,
                                    this.StateTextShown);

            return base.ToString() + " " + s;
        }

        /// <summary>
        /// Shifts the shown text to the left.
        /// </summary>
        /// <param name="visualTextData">The visual text data.</param>
        /// <returns>True when successful , otherworldly false.</returns>
        /// <exception cref="System.ArgumentNullException">VisualTextData was null.</exception>
        public static bool ShiftShownTextToTheLeft(Label visualTextData)
        {
#if DEBUG
            if (visualTextData == null)
            {
                throw new ArgumentNullException("visualTextData");
            }
#endif

            if (ShiftFirstCharShownToLeft(visualTextData) == false)
            {
                return false;
            }

            if (ShiftLastCharShownToLeft(visualTextData) == false)
            {
                return false;
            }

            return true;
        }

        /// <summary>Shifts the shown text to the right.</summary>
        /// <param name="internalTextData">The internal text data.</param>
        /// <param name="visualTextData">The visual text data.</param>
        /// <returns>True when successful , otherworldly false.</returns>
        /// <exception cref="System.ArgumentNullException">InternalTextData or visualTextData</exception>
        public static bool ShiftShownTextToTheRight(TextBox internalTextData, Label visualTextData)
        {
#if DEBUG
            if (internalTextData == null)
            {
                throw new ArgumentNullException("internalTextData");
            }

            if (visualTextData == null)
            {
                throw new ArgumentNullException("visualTextData");
            }
#endif
            if (ShiftFirstCharShownToRight(internalTextData, visualTextData) == false)
            {
                return false;
            }

            if (ShiftLastCharShownRight(internalTextData, visualTextData) == false)
            {
                return false;
            }

            return true;
        }

        /// <summary>This will result that we show a extra character to the left side.</summary>
        /// <param name="visualTextData">The label with the text to shift to left.</param>
        /// <returns>True when succeeded , otherwise false</returns>
        public static bool ShiftFirstCharShownToLeft(Label visualTextData)
        {
#if DEBUG
            if (visualTextData == null)
            {
                throw new ArgumentNullException("visualTextData");
            }
#endif

            if (visualTextData.StateFirstCharacterIndexShown > 0)
            {
                visualTextData.StateFirstCharacterIndexShown--;
                return true;
            }

            return false;
        }

        /// <summary>This will result that we show one less character to the left.</summary>
        /// <param name="visualTextData">The visual Text Data.</param>
        /// <returns>true when succeeded , otherwise false</returns>
        public static bool ShiftLastCharShownToLeft(Label visualTextData)
        {
#if DEBUG
            if (visualTextData == null)
            {
                throw new ArgumentNullException("visualTextData");
            }
#endif
            if (visualTextData.StateLastCharacterIndexShown > 0)
            {
                visualTextData.StateLastCharacterIndexShown--;
                return true;
            }

            return false;
        }

        /// <summary>This will result that we shown one less character on the left side.</summary>
        /// <param name="internalTextData">The internal Text Data.</param>
        /// <param name="visualTextData">The visual Text Data.</param>
        /// <returns>true when succeeded , otherwise false</returns>
        public static bool ShiftFirstCharShownToRight(TextBox internalTextData, Label visualTextData)
        {
#if DEBUG
            if (internalTextData == null)
            {
                throw new ArgumentNullException("internalTextData");
            }

            if (visualTextData == null)
            {
                throw new ArgumentNullException("visualTextData");
            }
#endif

            if (visualTextData.StateFirstCharacterIndexShown < internalTextData.Text.Length)
            {
                visualTextData.StateFirstCharacterIndexShown++;
                return true;
            }

            return false;
        }

        /// <summary>This will result that we show a extra character to the right side.</summary>
        /// <param name="internalTextData">The internal Text Data.</param>
        /// <param name="visualTextData">The visual Text Data.</param>
        /// <returns>true when succeeded , otherwise false</returns>
        public static bool ShiftLastCharShownRight(TextBox internalTextData, Label visualTextData)
        {
#if DEBUG
            if (internalTextData == null)
            {
                throw new ArgumentNullException("internalTextData");
            }

            if (visualTextData == null)
            {
                throw new ArgumentNullException("visualTextData");
            }
#endif

            if (visualTextData.StateLastCharacterIndexShown < internalTextData.Text.Length)
            {
                visualTextData.StateLastCharacterIndexShown++;
                return true;
            }

            return false;
        }
    }
}
