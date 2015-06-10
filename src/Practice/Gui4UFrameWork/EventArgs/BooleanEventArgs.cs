// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BooleanEventArgs.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the BooleanEventArgs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.EventArgs
{
    /// <summary>
    /// Event arguments for events with a boolean switch.
    /// </summary>
    public class BooleanEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanEventArgs"/> class.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public BooleanEventArgs(bool value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="BooleanEventArgs"/> is value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if value; otherwise, <c>false</c>.
        /// </value>
        public bool Value { get; private set; }
    }
}