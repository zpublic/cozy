using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Input
{
    /// <summary>
    /// An EventArgs object that represents mouse wheel rotation events.
    /// </summary>
    public class MouseWheelEventArgs : MouseEventArgs
    {
        /// <summary>
        /// Gets or sets the change in mouse wheel position. While most mice will tend to consistently
        /// return the same value depending on the number of "clicks" of the mouse wheel, you should not
        /// assume that all mice use the same amount. Different mice will produce different deltas for 
        /// each notch of the mouse wheel, while others don't have notches, but rather, a continuous rotation.
        /// </summary>
        public int Delta { get; protected set; }

        public int Value {get; protected set;}

        /// <summary>
        /// Creates a new MouseWheelEventArgs object given a time, the previous and current mouse states, and
        /// the delta and value of the mouse wheel.
        /// </summary>
        public MouseWheelEventArgs(TimeSpan time, MouseState previous, MouseState current, int delta, int value)
            : base(time, previous, current)
        {
            Delta = delta;
            Value = value;
        }
    }
}
