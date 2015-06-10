// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ButtonStateEventArgs.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ButtonStateEventArgs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.EventArgs
{
    using GUI4UFramework.Graphics;

    /// <summary>
    /// Event arguments where we need to pass what the button state is.
    /// </summary>
    public class ButtonStateEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonStateEventArgs"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        public ButtonStateEventArgs(ButtonState state)
        {
            this.ButtonState = state;
        }

        /// <summary>
        /// Gets the state of the button.
        /// </summary>
        /// <value>
        /// The state of the button.
        /// </value>
        public ButtonState ButtonState { get; private set; }
    }
}