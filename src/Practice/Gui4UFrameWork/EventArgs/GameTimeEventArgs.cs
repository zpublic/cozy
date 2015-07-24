// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameTimeEventArgs.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the GameTimeEventArgs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.EventArgs
{
    using GUI4UFramework.Management;

    /// <summary>
    /// Contains the time for events that need to have time.
    /// </summary>
    public class GameTimeEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameTimeEventArgs"/> class.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public GameTimeEventArgs(GameTime gameTime)
        {
            this.GameTime = gameTime;
        }

        /// <summary>
        /// Gets the game time.
        /// </summary>
        /// <value>
        /// The game time.
        /// </value>
        public GameTime GameTime { get; private set; }
    }
}