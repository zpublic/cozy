// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressBarBase.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ProgressBarBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.ProgressBar
{
    using GUI4UFramework.Colors;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>The base class for the progress bar</summary>
    public abstract class ProgressBarBase : Control
    {
        /// <summary>
        /// The default minimum bar value
        /// </summary>
        protected const int DefaultBarValueMax = 100;

        /// <summary>
        /// The default maximum bar value
        /// </summary>
        protected const int DefaultBarValueMin = 0;

        /// <summary>
        /// The default tick for the bar-value
        /// </summary>
        protected const int DefaultBarValueTick = 5;

        /// <summary>
        /// If we must redraw during Update()
        /// </summary>
        private bool mustRedraw;

        /// <summary>
        /// The value that is shown
        /// </summary>
        private float configValue;

        /// <summary>
        /// The maximum that the value can get, otherwise its clipped to maximum.
        /// </summary>
        private int configMaximumValue;

        /// <summary>
        /// The minimum that the value can get, otherwise its clipped to minimum.
        /// </summary>
        private int configMinimumValue;

        /// <summary>
        /// If we must check if value is still between minimum and maximum..
        /// </summary>
        private bool mustRevalidate;

        /// <summary>
        /// The bar color that represents the value.
        /// </summary>
        private GUIColor configValueBarColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBarBase"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected ProgressBarBase(string name)
            : base(name)
        {
            this.ConfigMinimumValue = DefaultBarValueMin;
            this.ConfigMaximumValue = DefaultBarValueMax;
            this.ConfigValue = DefaultBarValueMax;
            this.ConfigValueBarColor = new GUIColor(255, 255, 0);
        }

        /// <summary>
        /// Gets or sets the color of the value bar.
        /// </summary>
        /// <value>
        /// The color of the configuration bar.
        /// </value>
        public GUIColor ConfigValueBarColor
        {
            get
            {
                return this.configValueBarColor;
            }

            set
            {
                this.configValueBarColor = value;
                this.mustRedraw = true;
            }
        }

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        /// <value>
        /// The current value.
        /// </value>
        public float ConfigValue
        {
            get
            {
                return this.configValue;
            }

            set
            {
                this.configValue = value;
                this.mustRevalidate = true;
                this.mustRedraw = true;
            }
        }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public int ConfigMaximumValue
        {
            get
            {
                return this.configMaximumValue;
            }

            set
            {
                this.configMaximumValue = value;
                this.mustRevalidate = true;
                this.mustRedraw = true;
            }
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public int ConfigMinimumValue
        {
            get
            {
                return this.configMinimumValue;
            }

            set
            {
                this.configMinimumValue = value;
                this.mustRevalidate = true;
                this.mustRedraw = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether we are debugging the layout.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [debug layout]; otherwise, <c>false</c>.
        /// </value>
        public bool DebugLayout { get; set; }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.mustRevalidate)
            {
                this.Revalidate();
                this.mustRevalidate = false;
            }

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
        /// Revalidates the important values for this instance
        /// </summary>
        public void Revalidate()
        {
            if (this.configMaximumValue < this.configMinimumValue)
            {
                this.configMaximumValue = this.configMinimumValue;
                this.mustRedraw = true;
            }

            if (this.configValue < this.configMinimumValue)
            {
                this.configValue = this.configMinimumValue;
                this.mustRedraw = true;
            }

            if (this.configValue > this.configMaximumValue)
            {
                this.configValue = this.configMaximumValue;
                this.mustRedraw = true;
            }
        }

        /// <summary>
        /// Redraws this instance.
        /// </summary>
        protected abstract void Redraw();
    }
}
