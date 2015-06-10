// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressEventArgs.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ProgressEventArgs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.EventArgs
{
    /// <summary>Is for events that need to tell about current progress.</summary>
    public class ProgressEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressEventArgs"/> class.
        /// </summary>
        /// <param name="currentValue">The current.</param>
        public ProgressEventArgs(float currentValue)
        {
            this.CurrentValue = currentValue;
        }

        /// <summary>Gets the current progress.</summary>
        /// <value>The current.</value>
        public float CurrentValue { get; private set; }
    }
}