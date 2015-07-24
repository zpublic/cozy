// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressBarHorizontal.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Progress bar/meter
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.ProgressBar
{
    using System;

    using GUI4UFramework.Structural;

    /// <summary>
    /// Progress bar/meter
    /// </summary>
    public class ProgressBarHorizontal : ProgressBarBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBarHorizontal"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ProgressBarHorizontal(string name) : base(name)
        {
            this.Config.Width = Theme.ControlWidth;
            this.Config.Height = Theme.ControlHeight;

            this.HorizontalFill = new ProgressBarHorizontalFill(Name + "-ProgressBar")
            {
                Config =
                {
                    PositionX = Theme.ControlSmallSpacing,
                    PositionY = Theme.ControlSmallSpacing,
                    Width = (int)Config.Width - (Theme.ControlSmallSpacing * 2),
                    Height = (int)Config.Height - (Theme.ControlSmallSpacing * 2)
                }
            };
        }

        /// <summary>
        /// Gets the fill control.
        /// </summary>
        /// <value>
        /// The fill control.
        /// </value>
        protected ProgressBarHorizontalFill HorizontalFill { get; private set; }

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

            // Small control inside this one
            this.HorizontalFill.Config.Width = Config.Width - (2 * Theme.ControlSmallSpacing);
            this.HorizontalFill.Config.Height = Config.Height - (2 * Theme.ControlSmallSpacing);
            this.HorizontalFill.Theme.BorderColor = this.ConfigValueBarColor;
            this.HorizontalFill.Theme.FillColor = this.ConfigValueBarColor;
            this.HorizontalFill.Theme.BorderWidth = 0;
            this.AddControl(this.HorizontalFill);
            this.HorizontalFill.LoadContent();

            // set my appearance values
            this.CurrentTextureName = Manager.ImageCompositor.CreateRectangleTexture(this.Name + "-background", (int)State.Width, (int)State.Height, 1, Theme.ContainerFillColor, Theme.BorderColor);
        }

        /// <summary>
        /// Redraws this instance.
        /// </summary>
        protected override void Redraw()
        {
            // Get bar value percentage
            if (this.ConfigMaximumValue != 0)
            {
                this.HorizontalFill.Percentage = (this.ConfigValue / this.ConfigMaximumValue) * 100f;
            }
            else
            {
                this.HorizontalFill.Percentage = 100f;
            }

            const float Tolerance = 0.001f;
            if (Math.Abs(this.ConfigValue) < Tolerance && this.HorizontalFill.Config.Visible)
            {
                this.HorizontalFill.Config.Visible = false;
                Children.Remove(this.HorizontalFill);
            }
            else if (this.ConfigValue > 0 && !this.HorizontalFill.Config.Visible)
            {
                this.HorizontalFill.Config.Visible = true;
                Children.Add(this.HorizontalFill);
            }
        }
    }
}
