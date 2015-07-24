// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlConfig.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   This configuration doesn't change a lot.
//   Configuration is normally set at creation time.
//   Don't expect that ControlConfig will change during runtime.
//   For changes during runtime, use NodeState
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Structural
{
    /// <summary>This configuration doesn't change a lot.
    /// Configuration is normally set at creation time.
    /// Don't expect that ControlConfig will change during runtime.
    /// For changes during runtime, use NodeState.</summary>
    public class ControlConfig
    {
        /// <summary>
        /// The width to start out with for the control.
        /// </summary>
        protected const int InitialWidth = 140;

        /// <summary>
        /// The height to start out with for the control.
        /// </summary>
        protected const int InitialHeight = 40;

        /// <summary>
        /// When one of the properties has changed.
        /// </summary>
        private bool changed;

        /// <summary>
        /// If this control will react to focus changes.
        /// </summary>
        private bool acceptsFocus;

        /// <summary>
        /// If this control behaves like a window.
        /// </summary>
        private bool isWindow;

        /// <summary>
        /// If this control is enabled.
        /// </summary>
        private bool enabled;

        /// <summary>
        /// If the control is visible on the canvas.
        /// </summary>
        private bool visible;

        /// <summary>
        /// If we should use different colors when the mouse is hovering over.
        /// </summary>
        private bool hoverColorsEnabled;

        /// <summary>
        /// The x position for this control , relative to my parent.
        /// </summary>
        private float positionX;

        /// <summary>
        /// The y position for this control , relative to my parent.
        /// </summary>
        private float positionY;

        /// <summary>
        /// The width for this control.
        /// </summary>
        private float width;

        /// <summary>
        /// The height for this control.
        /// </summary>
        private float height;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlConfig"/> class.
        /// </summary>
        public ControlConfig()
        {
            this.enabled = true;
            this.visible = true;
            this.hoverColorsEnabled = true;
            this.isWindow = false;
            this.acceptsFocus = true;
            this.width = InitialWidth;
            this.height = InitialHeight;
            this.positionX = 0;
            this.positionY = 0;
            this.changed = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this control will react to focus changes.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [accepts focus]; otherwise, <c>false</c>.
        /// </value>
        public bool AcceptsFocus
        {
            get
            {
                return this.acceptsFocus;
            }

            set
            {
                this.acceptsFocus = value;
                this.changed = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is window If this control behaves like a window.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is window; otherwise, <c>false</c>.
        /// </value>
        public bool IsWindow
        {
            get
            {
                return this.isWindow;
            }

            set
            {
                this.isWindow = value;
                this.changed = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ControlConfig"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled
        {
            get
            {
                return this.enabled;
            }

            set
            {
                this.enabled = value;
                this.changed = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is visible on the canvas.
        /// </summary>
        /// <value>
        /// Control visibility.
        /// </value>
        public bool Visible
        {
            get
            {
                return this.visible;
            }

            set
            {
                this.visible = value;
                this.changed = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [hover colors enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [hover colors enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool HoverColorsEnabled
        {
            get
            {
                return this.hoverColorsEnabled;
            }

            set
            {
                this.hoverColorsEnabled = value;
                this.changed = true;
            }
        }

        /// <summary>Gets or sets the location relative to its parent (from left to right). </summary>
        /// <value>The x position of the node.</value>
        public float PositionX
        {
            get
            {
                return this.positionX;
            }

            set
            {
                this.positionX = value;
                this.changed = true;
            }
        }

        /// <summary>Gets or sets the location relative to its parent {from top to bottom).</summary>
        /// <value>The y position of the node.</value>
        public float PositionY
        {
            get
            {
                return this.positionY;
            }

            set
            {
                this.positionY = value;
                this.changed = true;
            }
        }

        /// <summary>Gets or sets the width of the control.</summary>
        /// <value>The width of the node.</value>
        public float Width
        {
            get
            {
                return this.width;
            }

            set
            {
                this.width = value;
                this.changed = true;
            }
        }

        /// <summary>Gets or sets the height of the control.</summary>
        /// <value>The height of the node.</value>
        public float Height
        {
            get
            {
                return this.height;
            }

            set
            {
                this.height = value;
                this.changed = true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ControlConfig"/> is changed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if changed; otherwise, <c>false</c>.
        /// </value>
        public bool Changed
        {
            get
            {
                return this.changed;
            }
        }

        /// <summary>Resets the changed property to false.</summary>
        public void ResetChanged()
        {
            this.changed = false;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                                "X{0},Y{1} W{2},H{3} V{4}",
                                this.positionX,
                                this.positionY,
                                this.width,
                                this.height,
                                this.visible);
        }
    }
}