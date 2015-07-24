// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScrollBarBase.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ScrollBarBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.ScrollBar
{
    using System;
    using System.Diagnostics;

    using GUI4UControls.Buttons;

    using GUI4UFramework.Colors;
    using GUI4UFramework.EventArgs;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Is the base class to draw scrollbars.
    /// </summary>
    public abstract class ScrollBarBase : Control
    {
        /// <summary>
        /// The default minimum value
        /// </summary>
        protected const float DefaultMin = 0;

        /// <summary>
        /// The default value
        /// </summary>
        protected const float DefaultValue = 50;

        /// <summary>
        /// The default maximum value
        /// </summary>
        protected const float DefaultMax = 100;

        /// <summary>
        /// The default step size
        /// </summary>
        protected const float DefaultStep = 20;

        /// <summary>
        /// Occurs when there is a event scrolling.
        /// </summary>
        public event EventHandler<FloatEventArgs> EventScrolling;

        /// <summary>
        /// True if we are rapidly scrolling
        /// </summary>
        private bool rapidScrollDelay = true;

        /// <summary>
        /// The maximum the value can get
        /// </summary>
        private float configMax;

        /// <summary>
        /// The minimum the value can get
        /// </summary>
        private float configMin;

        /// <summary>
        /// The value
        /// </summary>
        private float configValue;

        /// <summary>
        /// The step top grow or shrink the value
        /// </summary>
        private float configStep;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollBarBase"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected ScrollBarBase(string name) : base(name)
        {
            Theme.FillColor = GUIColor.White();

            this.ConfigStep = DefaultStep;
            this.ConfigValue = DefaultValue;
            this.ConfigMin = DefaultMin;
            this.ConfigMax = DefaultMax;
        }

        /// <summary>
        /// Gets or sets the minimum button control , to move the value towards minimum
        /// </summary>
        /// <value>
        /// The minimum button.
        /// </value>
        protected Button MinButton { get; set; }

        /// <summary>
        /// Gets or sets the maximum button control , to move the value towards maximum
        /// </summary>
        /// <value>
        /// The maximum button.
        /// </value>
        protected Button MaxButton { get; set; }

        /// <summary>
        /// Gets or sets the scroll indicator.
        /// </summary>
        /// <value>
        /// The scroll indicator.
        /// </value>
        protected Button ScrollIndicator { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this scrollbar is dragging.
        /// </summary>
        /// <value>
        ///   <c>true</c> if dragging; otherwise, <c>false</c>.
        /// </value>
        public bool Dragging { get; set; }

        /// <summary>
        /// Gets or sets the location where drag is started.
        /// </summary>
        /// <value>
        /// The drag start.
        /// </value>
        public DVector2 DragStart { get; set; }

        /// <summary>
        /// Gets or sets the location where drag is now.
        /// </summary>
        /// <value>
        /// The drag current.
        /// </value>
        public DVector2 DragCurrent { get; set; }

        /// <summary>
        /// Gets or sets the location where drag is ended.
        /// </summary>
        /// <value>
        /// The drag end.
        /// </value>
        public DVector2 DragEnd { get; set; }

        /// <summary>
        /// Gets or sets the indicator start position.
        /// </summary>
        /// <value>
        /// The indicator start.
        /// </value>
        public DVector2 IndicatorStartPosition { get; set; }

        /// <summary>
        /// Gets or sets the indicator current position.
        /// </summary>
        /// <value>
        /// The indicator current.
        /// </value>
        public DVector2 IndicatorCurrent { get; set; }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        public float ConfigMax
        {
            get
            {
                return this.configMax;
            }

            set
            {
                this.configMax = value;
                this.RedrawIndicatorPosition = true;
                this.RedrawIndicatorSize = true;
            }
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>
        /// The configuration minimum.
        /// </value>
        public float ConfigMin
        {
            get
            {
                return this.configMin;
            }

            set
            {
                this.configMin = value;
                this.RedrawIndicatorPosition = true;
                this.RedrawIndicatorSize = true;
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
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
                this.RedrawIndicatorPosition = true;
                this.RedrawIndicatorSize = true;
            }
        }

        /// <summary>
        /// Gets or sets the number of steps that the scrollbar indicator can move between min and max
        /// Example 4 Steps = four positions for the scrollbar indicator.
        /// </summary>
        /// <value>
        /// The configuration step.
        /// </value>
        public float ConfigStep
        {
            get
            {
                return this.configStep;
            }

            set
            {
                this.configStep = value;
                this.RedrawIndicatorPosition = true;
                this.RedrawIndicatorSize = true;
            }
        }

        /// <summary>
        /// Gets or sets the number of steps that the scrollbar indicator can move quickly between min and max
        /// </summary>
        /// <value>
        /// The configuration step.
        /// </value>
        public float StateRapidStep { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether were debug layout.
        /// </summary>
        /// <value>
        /// <c>true</c> if [configuration debug layout]; otherwise, <c>false</c>.
        /// </value>
        public bool ConfigDebugLayout { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we shall redraw the indicators size during update.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [redraw indicator size]; otherwise, <c>false</c>.
        /// </value>
        public bool RedrawIndicatorSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we shall redraw the indicators position during update.
        /// </summary>
        /// <value>
        /// <c>true</c> if [redraw indicator position]; otherwise, <c>false</c>.
        /// </value>
        public bool RedrawIndicatorPosition { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we are rapidly scrolling.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [rapid scroll delay]; otherwise, <c>false</c>.
        /// </value>
        public bool RapidScrollDelay
        {
            get { return this.rapidScrollDelay; }
            set { this.rapidScrollDelay = value; }
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
            // this needs to happen first
            base.LoadContent();
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();

            // min button
            this.MinButton = new Button(Name + "-MinButton")
            {
                Config =
                    {
                        Width = State.Width, Height = State.Height
                    },
                ConfigText = string.Empty
            };
            this.AddControl(this.MinButton);

            // max button
            this.MaxButton = new Button(Name + "-MaxButton") { ConfigText = string.Empty, Config = { Height = State.Height } };
            this.MaxButton.ConfigText = string.Empty;
            this.AddControl(this.MaxButton);

            // scroll indicator
            this.RedrawIndicatorPosition = true;
            this.RedrawIndicatorSize = true;

            // my background
            this.CurrentTextureName = Manager.ImageCompositor.CreateRectangleTexture(
                                                                    this.Name + "-background",
                                                                    (int)State.Width,
                                                                    (int)State.Height,
                                                                    1,
                                                                    Theme.ContainerFillColor,
                                                                    Theme.BorderColor);

            // set up event handlers
            this.MinButton.LeftMousePressed += this.MinButtonLeftMousePressed;
            this.MinButton.LeftMouseReleased += this.MinButtonLeftMouseReleased;
            this.MaxButton.LeftMousePressed += this.MaxButtonLeftMousePressed;
            this.MaxButton.LeftMouseReleased += this.MaxButtonLeftMouseReleased;
        }

        /// <summary>Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.</summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// <exception cref="System.ArgumentNullException">GameTime was null.</exception>
        public override void Update(GameTime gameTime)
        {
            this.ReadIfDragging();
            this.ReadIfEndDragging();
            this.UpdateIndicatorSize();
            this.UpdateIndicatorPosition();

            base.Update(gameTime);
        }

        /// <summary>
        /// Updates the size of the indicator.
        /// </summary>
        private void UpdateIndicatorSize()
        {
            // update the indicator size when we must
            if (this.RedrawIndicatorSize)
            {
                this.RedrawSize();
                this.RedrawIndicatorSize = false;
            }
        }

        /// <summary>
        /// Updates the indicator position.
        /// </summary>
        private void UpdateIndicatorPosition()
        {
            // update the indicator position when we must
            if (this.RedrawIndicatorPosition)
            {
                this.RedrawPosition();
                this.RedrawIndicatorPosition = false;
            }
        }

        /// <summary>
        /// Draw the texture
        /// </summary>
        public override void DrawMyData()
        {
            if (!Config.Visible)
            {
                return;
            }

            if (this.ConfigDebugLayout)
            {
                Manager.ImageCompositor.Draw(this.CurrentTextureName, this.State, new GUIColor(255, 0, 0));
            }
            else
            {
                Manager.ImageCompositor.Draw(this.CurrentTextureName, this.State, Theme.TintColor);
            }
        }

        /// <summary>
        /// Reads if there is a end in dragging.
        /// </summary>
        private void ReadIfEndDragging()
        {
            var mouseReleased = Manager.InputManager.ReadLeftMouseReleased();
            if (mouseReleased)
            {
                // System.Diagnostics.Debug.WriteLine("Mouse released !");
                if (this.Dragging == true)
                {
                    this.DragEnd = this.PointRelative(Manager.InputManager.ReadMouseLocation());
                    this.Dragging = false;
                    Debug.WriteLine("Dragging ended at " + this.DragEnd);
                }
            }
        }

        /// <summary>
        /// Reads if we are dragging.
        /// </summary>
        private void ReadIfDragging()
        {
            if (this.ScrollIndicator == null)
            {
                return;
            }

            if (!this.ScrollIndicator.StateIsPressed)
            {
                return;
            }

            //// System.Diagnostics.Debug.WriteLine("Pressed !");

            if (this.Dragging == false)
            {
                this.DragStart = this.PointRelative(Manager.InputManager.ReadMouseLocation()); // remember the mouse location , relative to this control where we start dragging
                this.IndicatorStartPosition = this.PointRelative(this.ScrollIndicator.State.DrawPosition); // check where the indicator is, relative to this control where we start dragging
                this.Dragging = true;

                // Debug.WriteLine("Dragging started at location " + DragStartLocation + " and indicator starts at location " + IndicatorStartPosition);
            }
        }

        /// <summary>
        /// Move list to a scroll position.
        /// </summary>
        /// <param name="value">The scroll position value.</param>
        public void Scroll(float value)
        {
            var diff = this.ConfigValue;
            this.ConfigValue += value;

            // validate value to minimum
            if (this.ConfigValue < this.ConfigMin)
            {
                this.ConfigValue = this.ConfigMin;
            }

            // validate value to maximum
            if (this.ConfigValue > this.ConfigMax)
            {
                this.ConfigValue = this.ConfigMax;
            }

            // get the resulting difference after validating
            diff = this.ConfigValue - diff;

            // if the difference is way too small , then do nothing
            if (Math.Abs(diff) < 0.0001f)
            {
                return;
            }

            // Move the indicator
            this.RedrawIndicatorPosition = true;

            if (this.EventScrolling != null)
            {
                this.EventScrolling(this, new FloatEventArgs(diff));
            }
        }

        /// <summary>
        /// React on when the mouse is up above the max button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="gameTimeEventArgs">The <see cref="GameTimeEventArgs"/> instance containing the event data.</param>
        private void MaxButtonLeftMouseReleased(object sender, GameTimeEventArgs gameTimeEventArgs)
        {
            this.rapidScrollDelay = true;
        }

        /// <summary>
        /// React on when the mouse down above the max button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="gameTimeEventArgs">The <see cref="GameTimeEventArgs"/> instance containing the event data.</param>
        private void MaxButtonLeftMousePressed(object sender, GameTimeEventArgs gameTimeEventArgs)
        {
            this.rapidScrollDelay = true;

            this.Scroll(this.ConfigStep);
        }

        /// <summary>
        /// React on when the mouse is up above the min button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="gameTimeEventArgs">The <see cref="GameTimeEventArgs"/> instance containing the event data.</param>
        private void MinButtonLeftMouseReleased(object sender, GameTimeEventArgs gameTimeEventArgs)
        {
            this.rapidScrollDelay = true;
        }

        /// <summary>
        /// React on when the mouse is down above the min button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="gameTimeEventArgs">The <see cref="GameTimeEventArgs"/> instance containing the event data.</param>
        private void MinButtonLeftMousePressed(object sender, GameTimeEventArgs gameTimeEventArgs)
        {
            this.rapidScrollDelay = true;

            this.Scroll(-this.ConfigStep);
        }

        /// <summary>
        /// Validates the important values and fixes them if needed.
        /// </summary>
        protected void ValidateConfigValues()
        {
            if (this.ConfigMin > this.ConfigMax)
            {
                this.ConfigMax = this.ConfigMin;
            }

            if (this.ConfigValue < this.ConfigMin)
            {
                this.ConfigValue = this.ConfigMin;
            }

            if (this.ConfigValue > this.ConfigMax)
            {
                this.ConfigMax = this.ConfigMax;
            }
        }

        /// <summary>
        /// Redraws the control to adjust for a new (value) size.
        /// </summary>
        public abstract void RedrawSize();

        /// <summary>
        /// Redraws the control to adjust for a new (value) position.
        /// </summary>
        public abstract void RedrawPosition();
    }
}