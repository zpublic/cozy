using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Input
{
    /// <summary>
    /// Represents an EventArgs object that is for all keyboard events in Starbound.UI.
    /// </summary>
    public class KeyboardEventArgs : InputEventArgs
    {
        /// <summary>
        /// The current state of they keyboard.
        /// </summary>
        public KeyboardState State { get; protected set; }

        /// <summary>
        /// The current set of modifiers that are in use.
        /// </summary>
        public Modifiers Modifiers { get; protected set; }

        /// <summary>
        /// The key that is involved in the event.
        /// </summary>
        public Keys Key { get; set; }

        /// <summary>
        /// The character that represents the key that is involved in the event.
        /// This uses KeyboardUtil.ToChar to make the conversion.
        /// </summary>
        public char? Character { get; set; }

        /// <summary>
        /// Creates a new KeyboardEventArgs, given a time for the event, the key that was pressed, and
        /// the modifiers that were applied at the time of the press, as well as the keyboard state at 
        /// the time the event occurred.
        /// </summary>
        public KeyboardEventArgs(TimeSpan time, Keys key, Modifiers modifiers, KeyboardState state)
            : base(time)
        {
            Character = KeyboardUtil.ToChar(key, modifiers);
            State = state;
            Modifiers = modifiers;
            Key = key;
        }
    }
}
