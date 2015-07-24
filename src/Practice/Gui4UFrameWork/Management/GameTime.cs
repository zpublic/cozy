// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameTime.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the GameTime type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Management
{
    using System;

    /// <summary>
    /// The time in a object.
    /// </summary>
    public class GameTime
    {
        /// <summary>
        /// Gets the total game time passed since starting the program.
        /// </summary>
        /// <value>
        /// The total game time.
        /// </value>
        public TimeSpan TotalGameTime { get; private set; }

        /// <summary>
        /// Updates the specified total game time.
        /// </summary>
        /// <param name="totalGameTime">The total game time.</param>
        public void Update(TimeSpan totalGameTime)
        {
            this.TotalGameTime = totalGameTime;
        }
    }
}
