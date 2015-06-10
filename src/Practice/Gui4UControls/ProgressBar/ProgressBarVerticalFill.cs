// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressBarVerticalFill.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ProgressBarVerticalFill type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.ProgressBar
{
    using GUI4UFramework.Colors;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Shows a vertical progress bar.
    /// </summary>
    public class ProgressBarVerticalFill : Control
    {
        /// <summary>
        /// If i must redraw myself, checked during update.
        /// </summary>
        private bool mustRedraw;

        /// <summary>
        /// The percentage of fill
        /// </summary>
        private float percentage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBarVerticalFill"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ProgressBarVerticalFill(string name) : base(name)
        {
        }

        /// <summary>
        /// Gets or sets the percentage of fill.
        /// </summary>
        /// <value>
        /// The percentage.
        /// </value>
        public float Percentage
        {
            get
            {
                return this.percentage;
            }

            set
            {
                this.percentage = value;
                this.mustRedraw = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether we are debugging layout.
        /// </summary>
        /// <value>
        /// <c>true</c> if [configuration debug layout]; otherwise, <c>false</c>.
        /// </value>
        public bool ConfigDebugLayout { get; set; }

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

             // set my appearance values
            this.CurrentTextureName = Manager.ImageCompositor.CreateRectangleTexture(this.Name + "-background", (int)State.Width, (int)State.Height, 0, Theme.FillColor, Theme.BorderColor);

            this.Redraw();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.LoadContent();
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();

            if (this.mustRedraw)
            {
                this.Redraw();
                this.mustRedraw = false;
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

            // System.Diagnostics.Debug.WriteLine(State.Width);
            if (this.ConfigDebugLayout)
            {
                State.Width = Config.Width;
                State.Height = Config.Height;
                Manager.ImageCompositor.Draw(this.CurrentTextureName, this.State, new GUIColor(128, 0, 0));
            }
            else
            {
                Manager.ImageCompositor.Draw(this.CurrentTextureName, this.State, Theme.TintColor);
            } 
        }

        /// <summary>
        /// Redraws the progress bar using the new percentage... 
        /// </summary>
        private void Redraw()
        {
            // set my values
            var modPercentage = this.Percentage / 100f;

            //// System.Diagnostics.Debug.WriteLine("Percentage is " + modPercentage);

            var maxBarHeight = (int)Config.Height;

            var barHeigth = modPercentage * maxBarHeight;

            // So we don't draw an empty box.
            if (barHeigth < 1f) 
            {
                barHeigth = 1f;
            }

            State.Height = barHeigth;
            State.Offset = new DVector2(0, -State.Height + Config.Height);
        }
    }
}
