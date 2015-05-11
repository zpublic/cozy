using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Input
{
    /// <summary>
    /// A base class for game pad event args.
    /// </summary>
    public class GamePadEventArgs : InputEventArgs
    {
        /// <summary>
        /// The logical index that this event occurred for.
        /// </summary>
        public PlayerIndex LogicalIndex { get; set; }

        /// <summary>
        /// The game pad state for the player at the time the associated event occurred at.
        /// </summary>
        public GamePadState Current { get; set; }

        /// <summary>
        /// Creates a new GamePadEventArgs object at a specific time, for a specific logical player index.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="logicalIndex"></param>
        /// <param name="current"></param>
        public GamePadEventArgs(TimeSpan time, PlayerIndex logicalIndex, GamePadState current)
            : base(time)
        {
            LogicalIndex = logicalIndex;
            Current = current;
        }
    }
}
