// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ButtonBase.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Base class for buttons , like toggle button or standard button
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Buttons
{
    using GUI4UControls.Text;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Base class for buttons , like toggle button or standard button.
    /// </summary>
    public abstract class ButtonBase : Control
    {
        /// <summary>
        /// The  text shown on the button.
        /// </summary>
        private string configText;

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonBase"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected ButtonBase(string name) : base(name)
        {
            this.ButtonState = ButtonState.Off;
            Config.HoverColorsEnabled = true;
        }

        /// <summary>
        /// Gets the label that represent the text on the button.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public Label Label { get; private set; }

        /// <summary>
        /// Gets or sets the text shown on the button.
        /// </summary>
        /// <value>
        /// The text shown on the button.
        /// </value>
        public string ConfigText
        {
            get
            {
                return this.configText;
            }

            set
            {
                this.configText = value;
            }
        }

        /// <summary>
        /// Gets or sets the state of the button. (on/off)
        /// </summary>
        /// <value>
        /// The state of the button.
        /// </value>
        public ButtonState ButtonState { get; set; }

        /// <summary>
        /// Gets the hover texture name.
        /// </summary>
        /// <value>
        /// The hover texture name.
        /// </value>
        protected string TextureNameHover { get; private set; }

        /// <summary>
        /// Gets the default texture name.
        /// </summary>
        /// <value>
        /// The default texture name.
        /// </value>
        protected string TextureNameDefault { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether we are debugging the layout of the control.
        /// </summary>
        /// <value>
        /// <c>true</c> if [configuration debug layout]; otherwise, <c>false</c>.
        /// </value>
        public bool ConfigDebugLayout { get; set; }

        /// <summary>
        /// When mouse enters this control
        /// </summary>
        public override void HoverEnter()
        {
            State.UseHovering = true;

            if (Config.HoverColorsEnabled)
            {
                this.CurrentTextureName = this.TextureNameHover;
            }

            base.HoverEnter();
        }

        /// <summary>
        /// When mouse leaves this control
        /// </summary>
        public override void HoverExit()
        {
            State.UseHovering = false;

            if (Config.HoverColorsEnabled)
            {
                this.CurrentTextureName = this.TextureNameDefault;
            }

            base.HoverExit();
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
            // this needs to happen first
            base.LoadContent();
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();

            // make my text that i show
            this.Label = new Label(Name + "-ConfigText")
            {
                ConfigText = this.configText,
                ConfigHorizontalAlignment = HorizontalAlignment.Center,
                ConfigVerticalAlignment = VerticalAlignment.Center,
                Manager = Manager,
                Config =
                    {
                        Width = Config.Width - Theme.BorderWidth, Height = Config.Height - Theme.BorderWidth
                    },
            };

            // this.Label.DebugLayout = true;
            this.Label.Config.PositionX = (Config.Width - this.Label.Config.Width) / 2;
            this.Label.Config.PositionY = (Config.Height - this.Label.Config.Height) / 2;
            this.AddControl(this.Label);

            // set my appearance values
            this.TextureNameDefault = Manager.ImageCompositor.CreateRectangleTexture(this.Name + " default", (int)State.Width, (int)State.Height, Theme.BorderWidth, Theme.FillColor, Theme.BorderColor);
            this.TextureNameHover = Manager.ImageCompositor.CreateRectangleTexture(this.Name + " hover", (int)State.Width, (int)State.Height, Theme.BorderWidth, Theme.HoverFillColor, Theme.HoverBorderColor);
            this.CurrentTextureName = this.TextureNameDefault;
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            this.Label.UnloadContent();
            Manager.ImageCompositor.Delete(this.TextureNameDefault);
            Manager.ImageCompositor.Delete(this.TextureNameHover);
            base.UnloadContent();
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
    }
}
