using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Input
{
    /// <summary>
    /// An EventArgs object that represents mouse events specific to buttons, their presses, clicks, and releases.
    /// </summary>
    public class MouseButtonEventArgs : MouseEventArgs
    {
        /// <summary>
        /// Gets or sets the mouse button that the event occurred for.
        /// </summary>
        public MouseButton Button { get; protected set; }

        /// <summary>
        /// Creates a new MouseButtonEventArgs object given a time, the previous and current mouse states, and
        /// the button that the event occurred with.
        /// </summary>
        public MouseButtonEventArgs(TimeSpan time, MouseState previous, MouseState current, MouseButton button)
            : base(time, previous, current)
        {
            Button = button;
        }
    }
}
