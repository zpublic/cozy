// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScrollBarVertical.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ScrollBarVertical type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.ScrollBar
{
    using System.Diagnostics;
    using GUI4UControls.Buttons;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// A vertical scroll-bar
    /// </summary>
    public class ScrollBarVertical : ScrollBarBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollBarVertical"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ScrollBarVertical(string name) : base(name)
        {
            Config.Width = Theme.ControlHeight;
            Config.Height = Theme.ControlWidth;
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

            // min button
            this.MinButton.ConfigText = "^";
            this.MinButton.Config.Width = Config.Width;
            this.MinButton.Config.Height = Config.Width;
            this.MinButton.LoadContent();

            // max button
            this.MaxButton.ConfigText = "v";
            this.MaxButton.Config.PositionY = Config.Height - MinButton.Config.Height;
            this.MaxButton.Config.Width = Config.Width;
            this.MaxButton.Config.Height = Config.Width;
            this.MaxButton.LoadContent();

            // indicator
            this.ScrollIndicator = new Button(Name + "-indicator");
            this.RedrawSize();
            this.RedrawPosition();
            this.AddControl(this.ScrollIndicator);
            this.ScrollIndicator.LoadContent();

            this.SetVisibility();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (Config.Changed)
            {
                this.Config.ResetChanged();
                this.SetVisibility();
            }

            base.Update(gameTime);

            this.UpdateDrawPositionByConfigAndParent();

            if (!this.Dragging)
            {
                return;
            }

            // find out the movement, compared to from the point where we started dragging
            this.DragCurrent = this.PointRelative(Manager.InputManager.ReadMouseLocation());
            var delta = DragCurrent - DragStart;

            //// Debug.WriteLine("Dragging is now at " + DragCurrentLocation + " with Delta " + delta);

            // move the indicator the distance as the mouse moved.
            this.IndicatorCurrent = this.IndicatorStartPosition + delta;

            //// Debug.WriteLine("'New Indicator location " + IndicatorCurrent);

            // force the new location for the indicator in between the min and max button
            var indicatorLocationY = IndicatorCurrent.Y;
            if (indicatorLocationY < MinButton.Config.PositionY + MinButton.Config.Height)
            {
                indicatorLocationY = MinButton.Config.PositionY + MinButton.Config.Height;
            }

            if (indicatorLocationY + ScrollIndicator.Config.Height > MaxButton.Config.PositionY)
            {
                indicatorLocationY = MaxButton.Config.PositionY - ScrollIndicator.Config.Height;
            }

            // convert the new Indicator location into a new value , and use that value
            var scrollIndicatorDrawnCentre = indicatorLocationY + (ScrollIndicator.Config.Height / 2);
            var top = MinButton.Config.Height + (ScrollIndicator.Config.Height / 2); // get the minimum that the drawn center for the indicator can be set
            var bottom = Config.Height - MaxButton.Config.Height - (ScrollIndicator.Config.Height / 2); // get the maximum that the drawn center for the indicator can be set
            var percentage = (scrollIndicatorDrawnCentre - top) / (bottom - top);
            Debug.WriteLine((int)(percentage * 100) + "%");
            var value = ConfigMin + ((ConfigMax - ConfigMin) * percentage);
            Debug.WriteLine("message" + value);
            this.ConfigValue = value;
        }

        /// <summary>
        /// Draw the texture at DrawPosition combined with its offset
        /// </summary>
        public override void DrawMyData()
        {
            if (Config.Visible == false)
            {
                return;
            }

            base.DrawMyData();
        }

        /// <summary>
        /// Redraws the control using the new size
        /// </summary>
        public override void RedrawSize()
        {
            this.ValidateConfigValues();

            // update rapid step
            this.StateRapidStep = this.ConfigStep / 4;

            // validate if we should update the scrollBar indicator
            if (this.ScrollIndicator == null || !(this.ConfigMax > 0))
            {
                return;
            }

            // Calculate scroll indicator size
            var interiourSize = this.CalculateInteriorSize();

            // get the new size for the indicator
            var newLength = interiourSize / ConfigStep;

            // Minimum size
            if (newLength < State.Height / 4)
            {
                newLength = State.Height / 4;
            }

            // set the new size to ScrollIndicator
            ScrollIndicator.Config.Width = Config.Width;
            ScrollIndicator.Config.Height = newLength;
        }

        /// <summary>
        /// Redraws by using the new position
        /// </summary>
        public override void RedrawPosition()
        {
            // validate
            this.ValidateConfigValues();
            if (this.ScrollIndicator == null)
            {
                return;
            }

            // calculate where the indicator should be virtually
            var scrollPercent = ConfigValue / ConfigMax;
            var interiourSize = this.CalculateInteriorSize();
            var maxPhysicalRange = interiourSize - ScrollIndicator.Config.Height;
            ScrollIndicator.Config.PositionY = MinButton.Config.Height + (maxPhysicalRange * scrollPercent);
        }

        /// <summary>
        /// Sets the visibility of me and my children
        /// </summary>
        private void SetVisibility()
        {
            MinButton.Config.Visible = Config.Visible;
            MaxButton.Config.Visible = Config.Visible;
            ScrollIndicator.Config.Visible = Config.Visible;
        }

        /// <summary>
        /// Gets the size of the interior between the min and max button
        /// </summary>
        /// <returns>The interior size</returns>
        protected float CalculateInteriorSize()
        {
            return State.Height - MinButton.State.Height - MaxButton.State.Height;
        }
    }
}
