using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Input
{
    /// <summary>
    /// Represents a key sequence that a player can enter. Provides an event for when the sequence
    /// has been triggered.
    /// </summary>
    public class KeySequence
    {
        /// <summary>
        /// The index of the character that should be expected in the code next.
        /// </summary>
        private int awaitingKey;

        /// <summary>
        /// The full sequence of keys that makes up the sequence.
        /// </summary>
        public List<Keys> Sequence { get; set; }

        /// <summary>
        /// Creates a new KeySequence with a specific list of keys to be pressed.
        /// </summary>
        /// <param name="keys"></param>
        public KeySequence(params Keys[] keys)
        {
            if (keys.Length == 0) { throw new ArgumentException("Must supply at least one key to the KeySequence constructor."); }

            Sequence = new List<Keys>(keys);
        }

        /// <summary>
        /// Causes the KeySequence to process the supplied key. If it is not the same as the expected key
        /// in the sequence, the sequence is restarted to nothing. If it is the right key, the KeySequence
        /// advances internally to be expecting the next key in the sequence. If it has reached the end
        /// of the sequence, the KeySequenceEntered event will be raised.
        /// </summary>
        /// <param name="key"></param>
        public void HandleKey(Keys key)
        {
            if (key == Sequence[awaitingKey])
            {
                awaitingKey++;
                if (awaitingKey >= Sequence.Count)
                {
                    awaitingKey = 0;
                    OnKeySequenceEntered(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Raises the KeySequenceEntered event. This automatically is raised when key presses
        /// are relayed through HandleKey and the sequence is matched, but this method allows
        /// for programmatically raising the event at any time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnKeySequenceEntered(object sender, EventArgs args)
        {
            if (KeySequenceEntered != null)
            {
                KeySequenceEntered(sender, args);
            }
        }

        /// <summary>
        /// An event that is raised whenever the correct key sequence is entered.
        /// </summary>
        public event EventHandler<EventArgs> KeySequenceEntered;

        /// <summary>
        /// A pre-built key sequence that represents the Konami Code: http://en.wikipedia.org/wiki/Konami_Code
        /// </summary>
        public static readonly KeySequence KonamiCode = new KeySequence(Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Left, Keys.Right, Keys.B, Keys.A);
    }
}
