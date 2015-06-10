// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlEventArgs.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ControlEventArgs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.EventArgs
{
    using GUI4UFramework.Structural;

    /// <summary>Contains arguments to tell about a control.</summary>
    public class ControlEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlEventArgs"/> class.
        /// </summary>
        /// <param name="control">The Control.</param>
        public ControlEventArgs(Control control)
        {
            this.Control = control;
        }

        /// <summary>
        /// Gets the Control.
        /// </summary>
        /// <value>
        /// The Control.
        /// </value>
        public Control Control { get; private set; }
    }
}