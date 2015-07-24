// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TabMenu.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the TabMenu type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Containers
{
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Contains the tab buttons.
    /// </summary>
    public class TabMenu : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabMenu"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TabMenu(string name) : base(name)
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
        /// - State.SourceRectangle to be reset to the Config.Size .
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();

            this.CurrentTextureName = Manager.ImageCompositor.CreateRectangleTexture(
                                                                                    this.Name + "-background",
                                                                                    (int)Config.Width, 
                                                                                    (int)Config.Height,
                                                                                    1,
                                                                                    Theme.ContainerFillColor, 
                                                                                    Theme.BorderColor);
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
    }
}
