// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuItemButton.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Show a button with a triangle , when pressed , shows my children vertically next to me.
//   has a option to stay always expanded (for a button column system)
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Menu
{
    using System;

    using GUI4UControls.Images;
    using GUI4UControls.Text;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Show a button with a triangle , when pressed , shows my children vertically next to me.
    /// has a option to stay always expanded (for a button column system)
    /// </summary>
    public class MenuItemButton : MenuItemBase
    {
        /// <summary>
        /// Occurs when the button is clicked.
        /// </summary>
        public event EventHandler MouseClicked;

        /// <summary>
        /// Whether the mouse clicked on me.
        /// </summary>
        private bool mouseClicked;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemButton"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public MenuItemButton(string name) : base(name)
        {
        }

        /// <summary>
        /// Gets or sets the text shown on me.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public Label Text { get; set; }

        /// <summary>
        /// Gets or sets the triangle that tells if i can expand or not.
        /// </summary>
        /// <value>
        /// The triangle.
        /// </value>
        public ImageControl Triangle { get; set; }

        /// <summary>
        /// Gets or sets the text that is shown on the Text-label
        /// </summary>
        /// <value>
        /// The configuration text.
        /// </value>
        public string ConfigText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [configuration show triangle].
        /// </summary>
        /// <value>
        /// <c>true</c> if [configuration show triangle]; otherwise, <c>false</c>.
        /// </value>
        public bool ConfigShowTriangle { get; set; }

        /// <summary>
        /// Gets or sets the texture name for when there is a mouse-hover.
        /// </summary>
        /// <value>
        /// The texture name hover.
        /// </value>
        public string TextureNameHover { get; set; }

        /// <summary>
        /// Gets or sets the texture name for when we go (or revert back) to default mode.
        /// </summary>
        /// <value>
        /// The texture name default.
        /// </value>
        public string TextureNameDefault { get; set; }

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

            // create some text to show on top of me
            this.Text = new Label(Name + "-Button")
            {
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = 0,
                    Width = Theme.ControlWidth - (Theme.ControlLargeSpacing * 2),
                    Height = Theme.ControlHeight,
                },
                ConfigText = this.ConfigText
            };
            this.AddControl(this.Text);

            // create some texture colors
            var clr = Theme.FillColor;
            this.TextureNameHover = Manager.ImageCompositor.CreateRectangleTexture(this.Name + "-Background", (int)Config.Width, (int)Config.Height, 0, Theme.ContainerFillColor, Theme.BorderColor);
            this.TextureNameDefault = Manager.ImageCompositor.CreateRectangleTexture(this.Name + "-Background", (int)Config.Width, (int)Config.Height, 0, clr, Theme.HoverBorderColor);
            this.CurrentTextureName = this.TextureNameDefault;

            // copy all the configuration values towards current visual state
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();
            this.UpdateDrawSourceRectangleByConfig();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // use all current configuration towards current visual state
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();

            // if we have a change in config.. then take it to the text-box too
            if (Config.Changed)
            {
                this.Text.Config.Visible = this.Config.Visible;
            }

            // if the mouse is over me
            var hover = State.MouseHoveringOver;
            if (hover)
            {
                // show that the mouse is over me
                this.CurrentTextureName = this.TextureNameHover;

                // read the mouse
                var mousePress = Manager.InputManager.ReadLeftMousePressed();
                if (mousePress == true && this.mouseClicked == false)
                {
                    this.mouseClicked = true;
                    this.RaiseMouseClick();
                }

                var mouseReleased = Manager.InputManager.ReadLeftMouseReleased();
                if (mouseReleased)
                {
                    this.mouseClicked = false;
                }
            }
            else
            {
                // show that the mouse is not over me
                this.CurrentTextureName = this.TextureNameDefault;
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

            if (this.DebugLayout)
            {
                Manager.ImageCompositor.Draw(this.CurrentTextureName, this.State, new GUIColor(255, 0, 0));
            }
            else
            {
                Manager.ImageCompositor.Draw(this.CurrentTextureName, this.State, Theme.TintColor);
            }
        }

        /// <summary>
        /// Will redraw myself , and set the children values correctly (when collapsed/expanded)
        /// </summary>
        protected override void Redraw()
        {
        }

        /// <summary>
        /// Raises the mouse clicked event.
        /// </summary>
        private void RaiseMouseClick()
        {
            if (this.MouseClicked != null)
            {
                this.MouseClicked(this, null);
            }
        }
    }
}