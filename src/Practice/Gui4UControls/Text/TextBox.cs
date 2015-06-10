// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextBox.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   you give me text , and i show it as a editable box
//   string ConfigText is read out by DShownText to find what text should be shown
//   Text shows ShownText , and align it into the box
//   _shownCursorIndex is calculate by using DShownText and CursorIndex
//   _cursorPosition is calculated by using _shownCursorIndex
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
    /// You give me text , and i show it as a editable box
    /// string ConfigText is read out by DShownText to find what text should be shown
    /// Text shows ShownText , and align it into the box
    ///
    /// _shownCursorIndex is calculate by using DShownText and CursorIndex
    /// _cursorPosition is calculated by using _shownCursorIndex
    /// </summary>
    public class TextBox : Control
    {
        /// <summary>
        /// The text box utility , that helps to do action on the text that is shown.
        /// </summary>
        private readonly TextBoxUtility textBoxUtility;

        /// <summary>
        /// The do rebuild
        /// </summary>
        private bool doRebuild;

        /// <summary>
        /// Whether we debug the cursor.
        /// </summary>
        private bool debugCursor;

        /// <summary>
        /// Whether we debug the layout.
        /// </summary>
        private bool configDebugLayout;

        /// <summary>
        /// The maximum length of the text.
        /// </summary>
        private int maxLength;

        /// <summary>
        /// If this control is read-only.
        /// </summary>
        private bool readOnly;

        /// <summary>
        /// The horizontal alignment
        /// </summary>
        private HorizontalAlignment horizontalAlignment;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBox"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TextBox(string name) : base(name)
        {
            Config.Width = Theme.ControlWidth;
            Config.Height = Theme.ControlHeight;

            this.MaxLength = int.MaxValue;

            // create the cursor to show where you are editing text
            this.TextBoxCursor = new TextBoxCursor(Name + "TextBoxCursor")
            {
                Config =
                {
                    PositionX = 0, 
                    PositionY = 0, 
                    Width = 10, 
                    Height = 10
                }
            };

            // this shows the text
            this.Label = new Label(Name + "-TextItem");

            // this draws a square over the text that is being edited
            this.TextBoxSelection = new TextBoxSelectionBox(Name + "-TextBoxSelection");

            this.textBoxUtility = new TextBoxUtility
                                      {
                                          Debug = true
                                      };
        }

        /// <summary>
        /// Gets or sets the selection box control.
        /// </summary>
        /// <value>
        /// The text box selection.
        /// </value>
        protected internal TextBoxSelectionBox TextBoxSelection { get; set; }

        /// <summary>
        /// Gets or sets the text box cursor.
        /// </summary>
        /// <value>
        /// The text box cursor.
        /// </value>
        protected internal TextBoxCursor TextBoxCursor { get; set; }

        /// <summary>
        /// Gets or sets the label that shows the text on this control.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        protected internal Label Label { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we are debugging cursor.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [debug cursor]; otherwise, <c>false</c>.
        /// </value>
        public bool DebugCursor
        {
            get
            {
                return this.debugCursor;
            }

            set
            {
                this.debugCursor = value;
                this.doRebuild = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether we are debugging the layout.
        /// </summary>
        /// <value>
        /// <c>true</c> if [configuration debug layout]; otherwise, <c>false</c>.
        /// </value>
        public bool ConfigDebugLayout
        {
            get
            {
                return this.configDebugLayout;
            }

            set
            {
                this.configDebugLayout = value;
                this.doRebuild = true;
            }
        }

        /// <summary>
        /// Gets or sets the maximum length of the characters shown.
        /// </summary>
        /// <value>
        /// The maximum length.
        /// </value>
        public int MaxLength
        {
            get
            {
                return this.maxLength;
            }

            set
            {
                this.maxLength = value;
                this.doRebuild = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this text-box is only [read only].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [read only]; otherwise, <c>false</c>.
        /// </value>
        public bool ReadOnly
        {
            get
            {
                return this.readOnly;
            }

            set
            {
                this.readOnly = value;
                this.doRebuild = true;
            }
        }

        /// <summary>
        /// Gets or sets the text that will be shown on the control.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get
            {
                return this.Label.ConfigText;
            }

            set
            {
                this.Label.ConfigText = value;
                this.doRebuild = true;
            }
        }

        /// <summary>
        /// Gets or sets the horizontal text alignment.
        /// </summary>
        /// <value>
        /// The horizontal text alignment.
        /// </value>
        public HorizontalAlignment HorizontalAlignment
        {
            get
            {
                return this.horizontalAlignment;
            }

            set
            {
                this.horizontalAlignment = value;
                this.doRebuild = true;
            }
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
            // **** do the basic stuff
            base.LoadContent();
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();

            // **** make the cursor , and let it fit inside me
            this.TextBoxCursor.Config.PositionX = Theme.ControlSmallSpacing;
            this.TextBoxCursor.Config.PositionY = Theme.ControlSmallSpacing;
            this.TextBoxCursor.Config.Width = Config.Width - (Theme.ControlSmallSpacing * 2);
            this.TextBoxCursor.Config.Height = Config.Height - (Theme.ControlSmallSpacing * 2);
            this.AddControl(this.TextBoxCursor);
            this.TextBoxCursor.LoadContent();

            // **** make the text, and let it fit inside me
            this.Label.ConfigHorizontalAlignment = HorizontalAlignment.Left;
            this.Label.ConfigVerticalAlignment = VerticalAlignment.Center;
            this.Label.ConfigText = this.Text;
            this.Label.Config.PositionX = Theme.ControlSmallSpacing;
            this.Label.Config.PositionY = Theme.ControlSmallSpacing;
            this.Label.Config.Width = Config.Width - (Theme.ControlSmallSpacing * 2);
            this.Label.Config.Height = Config.Height - (Theme.ControlSmallSpacing * 2);
            this.Label.Manager = this.Manager;
            this.Label.Theme.FontColor = Theme.InputFontColor;
            this.AddControl(this.Label);
            this.Label.LoadContent();

            // **** make the selection box
            this.TextBoxSelection.Manager = this.Manager;
            this.TextBoxSelection.Theme.FontColor = Theme.InputFontColor;
            this.TextBoxSelection.Config.PositionX = Theme.ControlSmallSpacing;
            this.TextBoxSelection.Config.PositionY = Theme.ControlSmallSpacing;
            this.TextBoxSelection.Config.Width = Config.Width - (Theme.ControlSmallSpacing * 2);
            this.TextBoxSelection.Config.Height = Config.Height - (Theme.ControlSmallSpacing * 2);

            // **** make the background
            this.CurrentTextureName = Manager.ImageCompositor.CreateRectangleTexture(
                                                                                this.Name + "-text",
                                                                                (int)State.Width,
                                                                                (int)State.Height,
                                                                                1,
                                                                                Theme.InputFillColor,
                                                                                Theme.BorderColor);

            // tell to rebuild all visually
            this.doRebuild = true;

            this.DebugCursor = true;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.doRebuild)
            {
                this.doRebuild = false;
                this.textBoxUtility.ValidateAndRepair(this);
            }

            if (this.CheckIsFocused() && Config.Enabled && !this.ReadOnly)
            {
                // do keyboard
                Manager.InputManager.UpdateKeyboardInput(gameTime);
                this.ReadKeyboardMessages();

                // show cursor
                this.TextBoxCursor.ConfigShowCursor = true;
            }
            else
            {
                // hide cursor
                this.TextBoxCursor.ConfigShowCursor = false;
            }
        }

        /// <summary>
        /// Draw the texture at DrawPosition combined with its offset
        /// </summary>
        public override void DrawMyData()
        {
            if (!Config.Visible)
            {
                return;
            }

            if (this.ConfigDebugLayout)
            {
                Manager.ImageCompositor.Draw(this.CurrentTextureName, this.State, new GUIColor(255, 0, 0));
            }
            else
            {
                Manager.ImageCompositor.Draw(this.CurrentTextureName, this.State, Theme.TintColor);
            }
        }

        /// <summary>
        /// Reads the keyboard messages and does action with that.
        /// </summary>
        private void ReadKeyboardMessages()
        {
            // we use flags here , because then we only have to check booleans
            // we don't use events for that because keyboard could be on a different thread
            // todo check the order of handling the keyboard messages
            // todo make the key-flags protected against double threading
            var flags = Manager.InputManager.ReadKeySwitchState();

            if (flags.Changed == false)
            {
                return;
            }

            // check all the flags
            if (flags.ChangeText)
            {
                Debug.WriteLine("Change text");
            }

            if (flags.SelectAll)
            {
                Debug.WriteLine("Do select-all");
                this.DoSelectAll();
            }

            if (flags.EnterPressed)
            {
                Debug.WriteLine("Do enter-pressed");
                this.DoKeyEnter();
            }

            if (flags.DoCursorLeft)
            {
                Debug.WriteLine("Do cursor-left");
                this.DoCursorLeft();
            }

            if (flags.DoCursorRight)
            {
                Debug.WriteLine("Do cursor-right");
                this.DoCursorRight();
            }

            if (flags.InsertText)
            {
                Debug.WriteLine("Do insert-text");
                var changeInto = flags.TextToInsert;
                this.DoInsertText(changeInto);
            }

            if (flags.Backspace)
            {
                Debug.WriteLine("Do backspace");
                this.DoKeyBackspace();
            }

            if (flags.KeyDelete)
            {
                Debug.WriteLine("Do key delete");
                this.DoKeyDelete();
            }

            // reset the keyboard flags , to tell that we have read all and we are ready for new messages
            flags.Reset();
        }

        /// <summary>
        /// Select everything in the text-box.
        /// Override to perform text selection
        /// </summary>
        public void DoSelectAll()
        {
            TextBoxUtility.DoSelectAll(this);
        }

        /// <summary>
        /// Delete only the selection.
        /// </summary>
        public void DeleteSelectedText()
        {
            TextBoxUtility.DeleteSelection(this);
        }

        /// <summary>
        /// Does the insert text action.
        /// </summary>
        /// <param name="changeInto">The change into.</param>
        public void DoInsertText(string changeInto)
        {
            this.textBoxUtility.DoInsertText(this, changeInto);
        }

        /// <summary>
        /// Does the cursor left action.
        /// </summary>
        public void DoCursorLeft()
        {
            this.textBoxUtility.DoCursorLeft(this);
        }

        /// <summary>
        /// Does the cursor right action.
        /// </summary>
        public void DoCursorRight()
        {
            this.textBoxUtility.DoCursorRight(this);
        }

        /// <summary>
        /// Does the key delete action.
        /// </summary>
        public void DoKeyDelete()
        {
            this.textBoxUtility.DoKeyDelete(this);            
        }

        /// <summary>
        /// Does the key enter action.
        /// </summary>
        public void DoKeyEnter()
        {
            this.textBoxUtility.DoKeyEnter();
        }

        /// <summary>
        /// Does the key backspace action.
        /// </summary>
        public void DoKeyBackspace()
        {
            this.textBoxUtility.DoKeyBackspace(this);
        }
    }
}
