// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScrollBarHorizontal.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ScrollBarHorizontal type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.ScrollBar
{
    using System.Diagnostics;

    using GUI4UControls.Buttons;

    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// A horizontal scroll-bar
    /// </summary>
    public class ScrollBarHorizontal : ScrollBarBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollBarHorizontal"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ScrollBarHorizontal(string name) : base(name)
        {
            Config.Width = Theme.ControlWidth;
            Config.Height = Theme.ControlHeight;
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
            MinButton.ConfigText = "<";
            MinButton.Config.Width = Config.Height;
            MinButton.Config.Height = Config.Height;
            MinButton.LoadContent();

            // max button
            MaxButton.ConfigText = ">";
            MaxButton.Config.Width = Config.Height;
            MaxButton.Config.Height = Config.Height;
            MaxButton.Config.PositionX = Config.Width - MaxButton.Config.Width;
            MaxButton.LoadContent();

            // indicator
            this.ScrollIndicator = new Button(Name + "-indicator");
            this.RedrawSize();
            this.RedrawPosition();
            this.AddControl(this.ScrollIndicator);
            ScrollIndicator.LoadContent();

            this.SetVisibility();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // do the basic stuff first
            base.Update(gameTime);

            if (Config.Changed)
            {
                Config.ResetChanged();
                this.SetVisibility();
            }

            // take the config position , and stick it in the current visual state
            this.UpdateDrawPositionByConfigAndParent();

            // the next code will be all about dragging
            if (!this.Dragging)
            {
                return;
            }

            // find out the movement, compared to from the point where we started dragging
            this.DragCurrent = this.PointRelative(Manager.InputManager.ReadMouseLocation());
            var delta = DragCurrent - DragStart;

            // Debug.WriteLine("Dragging is now at " + DragCurrentLocation + " with Delta " + delta);

            // move the indicator the distance as the mouse moved.
            this.IndicatorCurrent = this.IndicatorStartPosition + delta;

            // Debug.WriteLine("'New Indicator location " + IndicatorCurrent);
            // force the new location for the indicator in between the min and max button
            var scrollIndicatorLocationX = IndicatorCurrent.X;
            if (scrollIndicatorLocationX < MinButton.Config.PositionX + MinButton.Config.Width)
            {
                scrollIndicatorLocationX = MinButton.Config.PositionX + MinButton.Config.Width;
            }

            if (scrollIndicatorLocationX + ScrollIndicator.Config.Width > MaxButton.Config.PositionX)
            {
                scrollIndicatorLocationX = MaxButton.Config.PositionX - ScrollIndicator.Config.Width;
            }

            // convert the new Indicator location into a new value , and use that value
            var scrollIndicatorDrawnCentre = scrollIndicatorLocationX + (ScrollIndicator.Config.Width / 2);

            // get the minimum that the drawn center for the indicator can be set
            var left = MinButton.Config.Width + (ScrollIndicator.Config.Width / 2);

            // get the maximum that the drawn center for the indicator can be set
            var right = Config.Width - MaxButton.Config.Width - (ScrollIndicator.Config.Width / 2);

            var percentage = (scrollIndicatorDrawnCentre - left) / (right - left);
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
        /// Redraws the control, using the new size.
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
            var interiourSize = this.GetInteriourSize();

            // get the new size for the indicator
            var newLength = interiourSize / ConfigStep;

            // Minimum size
            if (newLength < State.Width / 4)
            {
                newLength = State.Width / 4;
            }

            // Set the new size to ScrollIndicator
            ScrollIndicator.Config.Width = newLength;
            ScrollIndicator.Config.Height = Config.Height;
        }

        /// <summary>
        /// Redraws the control , by using the new position.
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
            var scrollPercent = this.ConfigValue / this.ConfigMax;
            var interiourSize = this.GetInteriourSize();
            var maxPhysicalRange = interiourSize - ScrollIndicator.Config.Width;
            ScrollIndicator.Config.PositionX = MinButton.Config.Width + (maxPhysicalRange * scrollPercent);
        }

        /// <summary>
        /// Sets the visibility for me and my children.
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
        /// <returns>The interior size.</returns>
        private float GetInteriourSize()
        {
            return State.Width - MinButton.State.Width - MaxButton.State.Width;
        }
    }
}
