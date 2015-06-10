// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuItemSeparator.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the MenuItemSeparator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Menu
{
    using GUI4UFramework.Colors;
    using GUI4UFramework.Management;

    /// <summary>
    /// Creates a separator line in the menu.
    /// </summary>
    public class MenuItemSeparator : MenuItemBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemSeparator"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public MenuItemSeparator(string name) : base(name)
        {
        }

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

            Config.Height = Theme.ControlLargeSpacing;

            var clr = Theme.HoverFillColor;
            this.CurrentTextureName = Manager.ImageCompositor.CreateRectangleTexture(
                                                                            this.Name + "-Separator",
                                                                            (int)Config.Width, 
                                                                            (int)Config.Height,
                                                                            0,
                                                                            clr, 
                                                                            Theme.BorderColor);

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

            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();
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
    }
}