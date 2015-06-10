// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageControl.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Scalable image
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Images
{
    using GUI4UFramework.Colors;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Scalable image
    /// </summary>
    public class ImageControl : Control
    {
        /// <summary>
        /// The texture image to show.
        /// </summary>
        private string textureImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageControl"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ImageControl(string name) : base(name)
        {
            this.VerticalAlignment = VerticalAlignment.Top;
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.ImagePath = null;

            this.Config.AcceptsFocus = false;
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
        /// Gets or sets the horizontal alignment of the image inside the control.
        /// </summary>
        /// <value>
        /// The horizontal alignment.
        /// </value>
        public HorizontalAlignment HorizontalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the vertical alignment of the image inside the control.
        /// </summary>
        /// <value>
        /// The vertical alignment.
        /// </value>
        public VerticalAlignment VerticalAlignment { get; set; }

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
