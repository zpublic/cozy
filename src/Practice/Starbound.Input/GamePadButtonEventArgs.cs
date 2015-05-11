using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Input
{
    /// <summary>
    /// An event args object for button presses on the game pad. This includes all of the standard buttons,
    /// plus the thumbsticks and triggers moving past certain thresholds.
    /// </summary>
    public class GamePadButtonEventArgs : GamePadEventArgs 
    {
        /// <summary>
        /// The button that was involved in the button press.
        /// </summary>
        public Buttons Button { get; set; }

        /// <summary>
        /// Creates a new GamePadButtonEventArgs object.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="logicalIndex"></param>
        /// <param name="button"></param>
        /// <param name="current"></param>
        public GamePadButtonEventArgs(TimeSpan gameTime, PlayerIndex logicalIndex, Buttons button, GamePadState current)
            : base(gameTime, logicalIndex, current)
        {
            Button = button;
        }
    }
}
