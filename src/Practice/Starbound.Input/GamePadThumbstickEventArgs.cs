using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Input
{
    /// <summary>
    /// An event args class for events related to a game pad's thumbsticks.
    /// </summary>
    public class GamePadThumbstickEventArgs : GamePadEventArgs
    {
        /// <summary>
        /// The thumbstick involved in the event.
        /// </summary>
        public Thumbsticks Thumbstick { get; set; }

        /// <summary>
        /// The current position of the thumbstick as a Cartesian coordinate.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The angle that the thumbstick is currently making. This may be unreliable or inaccurate if the
        /// Amount property reports a very small or 0 value.
        /// </summary>
        public float Angle { get; set; }

        /// <summary>
        /// The amount that the thumbstick is being pushed in the current direction.
        /// </summary>
        public float Amount { get; set; }

        /// <summary>
        /// Creates a new GamePadThumbstickEventArgs object.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="logicalIndex"></param>
        /// <param name="thumbstick"></param>
        /// <param name="position"></param>
        /// <param name="current"></param>
        public GamePadThumbstickEventArgs(TimeSpan gameTime, PlayerIndex logicalIndex, Thumbsticks thumbstick, Vector2 position, GamePadState current)
            : base(gameTime, logicalIndex, current)
        {
            Thumbstick = thumbstick;
            Position = position;
            PolarCoordinate polar = PolarCoordinate.FromCartesian(position);
            Angle = polar.Angle;
            Amount = polar.Distance;
        }
    }
}
