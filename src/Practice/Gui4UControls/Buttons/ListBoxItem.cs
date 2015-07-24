// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListBoxItem.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Image and text item for the list-box
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Buttons
{
    using System;

    using GUI4UControls.Images;
    using GUI4UControls.Text;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    ///  Image and text item for the list-box.
    /// </summary>
    public class ListBoxItem : Control
    {
        /// <summary>
        /// The image position.
        /// </summary>
        private DVector2 imagePos;

        /// <summary>
        /// If this list-box item is selected.
        /// </summary>
        private bool selected;

        /// <summary>
        /// If we must redraw during Update().
        /// </summary>
        private bool mustRedraw;

        /// <summary>
        /// The control that shows the image.
        /// </summary>
        private ImageControl imageControl;

        /// <summary>
        /// The label in the list-box item.
        /// </summary>
        private Label label;

        /// <summary>
        /// The texture that is used when this item is selected.
        /// </summary>
        private string selectedTextureName;

        /// <summary>
        /// The texture that is used when this item is not selected.
        /// </summary>
        private string unselectedTextureName;

        /// <summary>
        /// Occurs when this item is pressed.
        /// </summary>
        public event EventHandler ItemPressed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListBoxItem"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="name">The name.</param>
        public ListBoxItem(string text, string name) : base(name)
        {
            this.ImagePath = null;
            this.Text = string.Empty;
            if (text != null)
            {
                this.Text = text;
            }

            this.Config.Width = this.Theme.ControlWidth;
            this.Config.Height = this.Theme.ControlHeight;
            this.Config.HoverColorsEnabled = true;
        }

        /// <summary>
        /// Gets or sets the image control.
        /// </summary>
        /// <value>
        /// The image control.
        /// </value>
        protected ImageControl ImageControl
        {
            get
            {
                return this.imageControl;
            }

            set
            {
                this.imageControl = value;
            }
        }

        /// <summary>
        /// Gets or sets the label control.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        protected Label Label
        {
            get
            {
                return this.label;
            }

            set
            {
                this.label = value;
            }
        }

        /// <summary>
        /// Gets or sets the image path used for the image shown.
        /// </summary>
        /// <value>
        /// The image path.
        /// </value>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the text for this control.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ListBoxItem"/> is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if selected; otherwise, <c>false</c>.
        /// </value>
        public bool Selected
        {
            get
            {
                return this.selected;
            }

            set
            {
                this.selected = value;
                this.mustRedraw = true;
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
            this.UpdateDrawSizeByConfig();
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSourceRectangleByConfig();

            this.imagePos = new DVector2(this.Theme.ControlSmallSpacing, this.Theme.ControlSmallSpacing);

            // create my image if i have one
            if (this.ImagePath != null)
            {
                this.imageControl = new ImageControl(this.Name + "-Image")
                {
                    Config =
                    {
                        PositionX = (int)this.imagePos.X,
                        PositionY = (int)this.imagePos.Y,
                        Width = (int)(this.State.Height - (2 * this.Theme.ControlSmallSpacing)),
                        Height = (int)(this.State.Height - (2 * this.Theme.ControlSmallSpacing))
                    },
                    ImagePath = this.ImagePath
                };
                this.AddControl(this.imageControl);
            }

            // Align text centered vertically
            if (this.imageControl != null)
            {
                //// _textPos = new DVector2(State.Height + Padding, State.Height/2);
            }

            // Create my label
            this.label = new Label(this.Name + "-Label")
                                {
                                    ConfigText = this.Text,
                                    ConfigHorizontalAlignment = HorizontalAlignment.Left,
                                    ConfigVerticalAlignment = VerticalAlignment.Center
                                };

            this.AddControl(this.label);

            this.mustRedraw = true;

            // create my textures
            this.selectedTextureName = this.Manager.ImageCompositor.CreateRectangleTexture(this.Name + "-selected", (int)this.Config.Width, (int)this.Config.Height, 0, this.Theme.HoverFillColor, this.Theme.BorderColor);
            this.unselectedTextureName = this.Manager.ImageCompositor.CreateRectangleTexture(this.Name + "-unselected", (int)this.Config.Width, (int)this.Config.Height, 0, this.Theme.FillColor, this.Theme.BorderColor);

            base.LoadContent();
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            if (this.imageControl != null)
            {
                this.imageControl.UnloadContent();
            }

            this.label.UnloadContent();
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            this.UpdateDrawSizeByConfig();
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSourceRectangleByConfig();

            // do change checks
            if (this.Config.Changed || this.mustRedraw)
            {
                this.mustRedraw = false;
                this.Config.ResetChanged();
                this.Redraw();
            }

            // do mouse stuff
            var leftMousePressed = this.Manager.InputManager.ReadLeftMousePressed();

            // Is mouse hovering over?
            if (!this.State.MouseHoveringOver || !this.CheckIsFocused())
            {
                return;
            }

            // Mouse click?
            if (!leftMousePressed)
            {
                return;
            }

            // then raise the event
            if (this.ItemPressed != null)
            {
                this.ItemPressed(this, new EventArgs());
            }
        }

        /// <summary>
        /// Draw the texture at DrawPosition combined with its offset
        /// </summary>
        public override void DrawMyData()
        {
            if (this.Selected)
            {
                this.Manager.ImageCompositor.Draw(this.selectedTextureName, this.State, GUIColor.White());
            }
            else
            {
                this.Manager.ImageCompositor.Draw(this.unselectedTextureName, this.State, GUIColor.White());
            }
        }

        /// <summary>
        /// Redraws this instance.
        /// </summary>
        private void Redraw()
        {
            this.Label.Config.Visible = this.Config.Visible;

            if (this.ImageControl != null)
            {
                this.ImageControl.Config.Visible = this.Config.Visible;
            }
        }
    }
}
