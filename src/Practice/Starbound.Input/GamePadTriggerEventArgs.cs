using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Input
{
    /// <summary>
    /// An EventArgs object for game pad trigger events.
    /// </summary>
    public class GamePadTriggerEventArgs : GamePadEventArgs
    {
        /// <summary>
        /// The trigger that is involved in the event.
        /// </summary>
        public Triggers Trigger { get; set; }

        /// <summary>
        /// The value of the trigger at the time of the event.
        /// </summary>
        public float Value { get; set; }

        /// <summary>
        /// Creates a new GamePadTriggerEventArgs.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="logicalIndex"></param>
        /// <param name="trigger"></param>
        /// <param name="value"></param>
        /// <param name="current"></param>
        public GamePadTriggerEventArgs(TimeSpan gameTime, PlayerIndex logicalIndex, Triggers trigger, float value, GamePadState current)
            : base(gameTime, logicalIndex, current)
        {
            Trigger = trigger;
            Value = value;
        }
    }
}
