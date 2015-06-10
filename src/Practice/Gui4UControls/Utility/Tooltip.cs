// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tooltip.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   The tool-tip is attached to a control.
//   It will read out the tool-tip property of its parent-control , when hovered over the parent-control.
//   The tool-tip follows the mouse , until the mouses moves out of its parent focus-box
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Utility
{
    using GUI4UControls.Text;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// The tool-tip is attached to a control.
    /// It will read out the tool-tip property of its parent-control , when hovered over the parent-control.
    /// The tool-tip follows the mouse , until the mouse moves out of its parent focus-box
    /// </summary>
    public class Tooltip : Control
    {
        /// <summary>
        /// If the parent has focus
        /// </summary>
        private bool parentHasFocus;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tooltip"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Tooltip(string name) : base(name)
        {
            Config.Height = Theme.ControlHeight / 2.0f;
        }

        /// <summary>
        /// Gets or sets the label that contains the text with info for the user.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        protected Label Label { get; set; }

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

            // make the background
            this.CurrentTextureName = Manager.ImageCompositor.CreateRectangleTexture(
                                                                                    this.Name + "-Tool-tip Background",
                                                                                    (int)Config.Width,
                                                                                    (int)Config.Height,
                                                                                    1,
                                                                                    new GUIColor(252, 252, 220),
                                                                                    Theme.BorderColor);

            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();

            this.Label = new Label(Name + "-Label")
                                                     {
                                                         ConfigText = this.GetParentTooltipText(),
                                                         Config =
                                                             {
                                                                 Height = Config.Height
                                                             }
                                                     };
            this.AddControl(this.Label);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var pos = Manager.InputManager.ReadMouseLocation();
            Config.PositionX = pos.X;
            Config.PositionY = pos.Y;
            State.DrawPosition = new DVector2(pos.X, pos.Y) + new DVector2(10, 0);
            this.Label.ConfigText = this.GetParentTooltipText();

            // check if parent has focus , if not: turn off drawing
            if (this.Parent != null)
            {
                this.parentHasFocus = Parent.CheckIsFocused();
            }
        }

        /// <summary>
        /// Draw the texture from CurrentTextureName at DrawPosition combined with its offset
        /// Draw the label too if needed.
        /// </summary>
        public override void DrawMyData()
        {
            if (this.parentHasFocus)
            {
                this.Label.Config.Visible = true;
                base.DrawMyData();
            }
            else
            {
                this.Label.Config.Visible = false;
            }
        }

        /// <summary>
        /// Gets the parents it's tool-tip text.
        /// </summary>
        /// <returns>The tool-tip text if it exists</returns>
        private string GetParentTooltipText()
        {
            var parent = Parent;
            if (parent != null)
            {
                return parent.TooltipText;
            }

            return "could not get parent";
        }
    }
}
