// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeViewIcon.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   I am the icon of a tree-view node
//   i could update at weird moments to show that my tree-view is in a new state.
//   i am in between the text and in between the collapse/expand button.
//   If i am don't have a resource-image , i am not shown. So text would be placed over me (instead of next to me)
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tree
{
    using GUI4UFramework.Colors;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// I am the icon of a tree-view node 
    /// i could update at weird moments to show that my tree-view is in a new state.
    /// i am in between the text and in between the collapse/expand button.
    /// If i am don't have a resource-image , i am not shown. So text would be placed over me (instead of next to me)
    /// </summary>
    public class TreeViewIcon : Control
    {
        /// <summary>
        /// The default icon for this control. it will start out with this.
        /// </summary>
        private const string DefaultIcon = @"Textures\file";

        /// <summary>
        /// The texture image to show.
        /// </summary>
        private string textureImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewIcon"/> class.
        /// </summary>
        /// <param name="name">The name for this node , will be used also as text.</param>
        public TreeViewIcon(string name) : base(name)
        {
            this.ImagePath = DefaultIcon;
        }

        /// <summary>
        /// Gets or sets a value indicating whether we debug this class.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [configuration debug]; otherwise, <c>false</c>.
        /// </value>
        public bool ConfigDebug { get; set; }

        /// <summary>
        /// Gets or sets the image path to the shown file.
        /// </summary>
        /// <value>
        /// The image path.
        /// </value>
        public string ImagePath { get; set; }

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

            this.ConfigDebug = false;

            if (this.ConfigDebug)
            {
                this.textureImage = this.Manager.ImageCompositor.CreateRectangleTexture(
                                                                                        this.Name + "-" + this.ImagePath,
                                                                                        (int)this.Config.Width,
                                                                                        (int)this.Config.Height,
                                                                                        0,
                                                                                        GUIColor.Gainsboro(),
                                                                                        GUIColor.White());
            }
            else
            {
                this.textureImage = Manager.ImageCompositor.CreateImageTexture(
                                                                               this.Name + "-" + this.ImagePath,
                                                                               this.ImagePath);
            }

            // get the used texture , and set the final size in state to the size of the texture
            var sourceSize = Manager.ImageCompositor.ReadSizeTexture(this.textureImage);
            State.SourceRectangle.Width = sourceSize.X;
            State.SourceRectangle.Height = sourceSize.Y;

            // scale to smallest 
            var shrinkVertical = sourceSize.X / Config.Height;
            var shrinkHorizontal = sourceSize.Y / Config.Width;

            // find the biggest shrink
            var sc = shrinkHorizontal > shrinkVertical ?
                shrinkHorizontal :
                shrinkVertical;

            // use this scale
            State.Width = sourceSize.X / sc;
            State.Height = sourceSize.Y / sc;

            var left = (Config.Width / 2) - (State.Width / 2);
            var top = (Config.Height / 2) - (State.Height / 2);
            State.Offset = new DVector2(left, top);
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            Manager.ImageCompositor.Delete(this.textureImage);

            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // set draw-position
            if (this.Parent == null)
            {
                return;
            }

            var newPosition = Parent.State.DrawPosition + new DVector2(Config.PositionX, Config.PositionY);
            State.DrawPosition = newPosition;
        }

        /// <summary>
        /// Draw the texture at DrawPosition combined with its offset
        /// </summary>
        public override void DrawMyData()
        {
            Manager.ImageCompositor.Draw(this.textureImage, this.State, Theme.TintColor);
        }
    }
}