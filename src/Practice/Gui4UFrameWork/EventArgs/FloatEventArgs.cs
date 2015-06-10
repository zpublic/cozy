// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FloatEventArgs.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the FloatEventArgs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.EventArgs
{
    /// <summary>Arguments to be used for events that need to carry a float around.</summary>
    public class FloatEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatEventArgs"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public FloatEventArgs(float value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the float value of the event.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public float Value { get; private set; }
    }
}