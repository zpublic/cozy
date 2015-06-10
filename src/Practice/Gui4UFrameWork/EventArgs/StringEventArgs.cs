// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringEventArgs.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the StringEventArgs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.EventArgs
{
    /// <summary>To be used by events that want to tell a message.</summary>
    public class StringEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public StringEventArgs(string message)
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; private set; }
    }
}