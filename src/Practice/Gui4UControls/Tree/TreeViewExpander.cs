// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeViewExpander.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   I am like a switch button.
//   I can be on , i can be off
//   I only use a two images instead of a button up/down look.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tree
{
    using GUI4UFramework.Colors;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// I am like a switch button.
    /// I can be on , i can be off
    /// I only use a two images instead of a button up/down look.
    /// </summary>
    public class TreeViewExpander : Control
    {
        /// <summary>Initializes a new instance of the <see cref="TreeViewExpander"/> class.</summary>
        /// <param name="name">The name.</param>
        public TreeViewExpander(string name) : base(name)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether we debug the location.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [debug location]; otherwise, <c>false</c>.
        /// </value>
        public bool DebugLocation { get; set; }

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

            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();
            this.UpdateDrawSourceRectangleByConfig();

            const int Width = 10;
            const int Height = 10;
            var color = new GUIColor(0, 0, 255);
            this.CurrentTextureName = this.Manager.ImageCompositor.CreateFlatTexture(this.Name + "-Background", Width, Height, color);
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
            this.UpdateDrawSourceRectangleByConfig();
        }
    }
}