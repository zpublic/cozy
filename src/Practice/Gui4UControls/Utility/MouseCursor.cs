// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MouseCursor.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the MouseCursor type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Utility
{
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Shows a mouse cursor image underneath the mouse location
    /// </summary>
    public class MouseCursor : Control
    {
        /// <summary>
        /// The texture size of the mouse cursor image
        /// </summary>
        private DVector2 mouseCursorImageSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseCursor"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public MouseCursor(string name) : base(name)
        {
            Config.Width = 15;
            Config.Height = 15;
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
            // make the background
            this.CurrentTextureName = Manager.ImageCompositor.CreateImageTexture(this.Name + "-mouse cursor", "Textures\\Kcng6e9cq");

            // CurrentTextureName = Manager.ImageCompositor.CreateRectangleColorMap(this.Name, (int)this.Config.Width, (int)this.Config.Height, 1,GUIColor.Gainsboro(), GUIColor.Wheat());
            this.mouseCursorImageSize = Manager.ImageCompositor.ReadSizeTexture(this.CurrentTextureName);
            Config.Width = this.mouseCursorImageSize.X;
            Config.Height = this.mouseCursorImageSize.Y;

            // do the basic stuff
            base.LoadContent();
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();
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

            State.DrawPosition = new DVector2(pos.X, pos.Y);
            State.Offset = new DVector2(-Config.Width / 8, -Config.Height / 8);
            State.Width = Config.Width / 4;
            State.Height = Config.Height / 4;
        }
    }
}
