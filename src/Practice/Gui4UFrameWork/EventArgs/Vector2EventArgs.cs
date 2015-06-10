// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Vector2EventArgs.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the Vector2EventArgs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.EventArgs
{
    using GUI4UFramework.Graphics;

    /// <summary>Used by events that need to tell a Vector2.</summary>
    public class Vector2EventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2EventArgs"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Vector2EventArgs(DVector2 value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public DVector2 Value { get; private set; }
    }
}