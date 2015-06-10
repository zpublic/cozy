// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWindowProgressBar.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the TestWindowProgressBar type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tests
{
    using System;

    using GUI4UControls.ProgressBar;

    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// A test-window for the ProgressBar-controls..
    /// </summary>
    public class TestWindowProgressBar : TestWindowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestWindowProgressBar"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TestWindowProgressBar(string name) : base(name)
        {
            this.Title = "Test progress-bar";
        }

        /// <summary>
        /// Gets or sets the vertical progress bar.
        /// </summary>
        /// <value>
        /// The progress bar vertical.
        /// </value>
        public ProgressBarVertical ProgressBarVertical { get; set; }

        /// <summary>
        /// Gets or sets the horizontal progress bar.
        /// </summary>
        /// <value>
        /// The progress bar horizontal.
        /// </value>
        public ProgressBarHorizontal ProgressBarHorizontal { get; set; }

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

            // horizontal progress-bar
            var y = Theme.ControlHeight + Theme.ControlLargeSpacing;
            this.ProgressBarHorizontal = new ProgressBarHorizontal("MyProgressBar")
            {
                ConfigMaximumValue = 10,
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing + Theme.ControlHeight + Theme.ControlLargeSpacing,
                    PositionY = y
                }
            };
            this.AddControl(this.ProgressBarHorizontal);

            // vertical progress-bar
            y = (int)(this.ProgressBarHorizontal.Config.Height + Theme.ControlLargeSpacing);
            this.ProgressBarVertical = new ProgressBarVertical("MyProgressBar2")
            {
                ConfigMaximumValue = 10,
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = y
                }
            };
            this.AddControl(this.ProgressBarVertical);

            Config.Height = this.ProgressBarVertical.Config.PositionY + this.ProgressBarHorizontal.Config.Width + Theme.ControlLargeSpacing;
            Config.Width = this.ProgressBarHorizontal.Config.PositionX + this.ProgressBarHorizontal.Config.Width + Theme.ControlLargeSpacing;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// <exception cref="System.ArgumentNullException">GameTime can not be null.</exception>
        public override void Update(GameTime gameTime)
        {
#if DEBUG
            if (gameTime == null)
            {
                throw new ArgumentNullException("gameTime");
            }
#endif 

            base.Update(gameTime);

            // update horizontal progress-bar
            if (this.ProgressBarHorizontal != null)
            {
                var t = gameTime.TotalGameTime.TotalMilliseconds / 1000f;
                var v = (float)(t % 10f);

                this.ProgressBarHorizontal.ConfigValue = v;
                if (this.ProgressBarHorizontal.ConfigValue > this.ProgressBarHorizontal.ConfigMaximumValue)
                {
                    this.ProgressBarHorizontal.ConfigValue = this.ProgressBarHorizontal.ConfigMinimumValue;
                }
            }

            // update vertical progress bar
            if (this.ProgressBarVertical != null)
            {
                var t = gameTime.TotalGameTime.TotalMilliseconds / 1000f;
                var v = (float)(t % 11f);

                this.ProgressBarVertical.ConfigValue = v;
                if (this.ProgressBarVertical.ConfigValue > this.ProgressBarVertical.ConfigMaximumValue)
                {
                    this.ProgressBarVertical.ConfigValue = this.ProgressBarVertical.ConfigMinimumValue;
                }
            }
        }
    }
}
