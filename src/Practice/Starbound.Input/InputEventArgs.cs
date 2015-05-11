using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Input
{
    /// <summary>
    /// A base class for all Starbound.Input event args to derive from.
    /// </summary>
    public abstract class InputEventArgs : EventArgs
    {
        /// <summary>
        /// Stores the time of the event as a TimeSpan since the game began.
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Creates a new InputEventArgs with the time that it occurred at.
        /// </summary>
        /// <param name="time"></param>
        public InputEventArgs(TimeSpan time)
        {
            Time = time;
        }
    }
}
