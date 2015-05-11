using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Input
{
    /// <summary>
    /// An EventArgs type that is used for mouse-based input events in Starbound.UI.
    /// </summary>
    public class MouseEventArgs : InputEventArgs
    {
        /// <summary>
        /// Gets or sets the previous mouse state for the given event. This is what the mouse looked like
        /// in the previous Update.
        /// </summary>
        public MouseState Previous { get; protected set; }

        /// <summary>
        /// Gets or sets the current mouse state for the given event. This is what the mouse looked like
        /// at the time the event occurred.
        /// </summary>
        public MouseState Current { get; protected set; }

        /// <summary>
        /// Creates a new MouseEventArgs object, based on a time for the event, and the previous and
        /// current mouse states.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="previous"></param>
        /// <param name="current"></param>
        public MouseEventArgs(TimeSpan time, MouseState previous, MouseState current)
            : base(time)
        {
            Previous = previous;
            Current = current;
        }
    }
}
