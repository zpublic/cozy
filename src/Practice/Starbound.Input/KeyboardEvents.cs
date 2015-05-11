using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Input
{
    /// <summary>
    /// An abstraction around keyboard input that turns XNA's underlying polling model into an event-based
    /// model for keyboard input.
    /// </summary>
    public class KeyboardEvents : GameComponent
    {
        /// <summary>
        /// Represents the amount of time between a key being pressed, and the time that key typed events
        /// start repeating. This is measured in milliseconds. The initial delay is traditionally 
        /// significantly longer than other delays. The default is 800 milliseconds.
        /// </summary>
        public static int InitialDelay { get; set; }

        /// <summary>
        /// Represents the amount of time delay between key typed events after the first repeat. This 
        /// "normal" repeat delay is typically much faster than the initial. The default is 50 milliseconds
        /// (20 times per second).
        /// </summary>
        public static int RepeatDelay { get; set; }

        /// <summary>
        /// A list of key sequences that should be monitored and maintained.
        /// </summary>
        private static List<KeySequence> keySequences;
        
        /// <summary>
        /// Stores the last keyboard state from the previous update.
        /// </summary>
        private KeyboardState previous;

        /// <summary>
        /// Stores the last key that was pressed. Used in tracking when keys are held down for
        /// a prolonged time and need to repeatedly raise key typed events.
        /// </summary>
        private Keys lastKey;

        /// <summary>
        /// Stores the last time that a key was pressed. Used in tracking when keys are held down for
        /// a prolonged time and need to repeatedly raise key typed events.
        /// </summary>
        private TimeSpan lastPress;

        /// <summary>
        /// Indicates whether the last key press was an initial press or not.
        /// </summary>
        private bool isInitial;

        /// <summary>
        /// Sets up the class with defaults.
        /// </summary>
        static KeyboardEvents()
        {
            InitialDelay = 800;
            RepeatDelay = 50;
            keySequences = new List<KeySequence>();
            KeyTyped += KeyTypedHandler;
        }

        /// <summary>
        /// An event handler for when a key is typed. This is set up to listen for key presses and update
        /// active key sequences. While anybody could handle KeySequence objects on their own, for most
        /// people, this will be the preferred method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void KeyTypedHandler(object sender, KeyboardEventArgs e)
        {
            foreach(KeySequence sequence in keySequences)
            {
                sequence.HandleKey(e.Key);
            }
        }

        /// <summary>
        /// Creates a new KeyboardEvents object.
        /// </summary>
        /// <param name="game"></param>
        public KeyboardEvents(Game game)
            : base(game)
        {
        }

        /// <summary>
        /// Adds a KeySequence to the list of key sequences that are being monitored.
        /// </summary>
        /// <param name="keySequence"></param>
        public static void AddKeySequence(KeySequence keySequence)
        {
            keySequences.Add(keySequence);
        }

        /// <summary>
        /// Removes a KeySequence from the list of key sequences that are being monitored.
        /// </summary>
        /// <param name="keySequence"></param>
        public static void RemoveKeySequence(KeySequence keySequence)
        {
            keySequences.Remove(keySequence);
        }

        /// <summary>
        /// Updates the component, turning XNA's polling model into an event-based model, raising
        /// events as they happen.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState current = Keyboard.GetState();

            // Build the modifiers that currently apply to the current situation.
            Modifiers modifiers = Modifiers.None;
            if (current.IsKeyDown(Keys.LeftControl) || current.IsKeyDown(Keys.RightControl)) { modifiers |= Modifiers.Control; }
            if (current.IsKeyDown(Keys.LeftShift) || current.IsKeyDown(Keys.RightShift)) { modifiers |= Modifiers.Shift; }
            if (current.IsKeyDown(Keys.LeftAlt) || current.IsKeyDown(Keys.RightAlt)) { modifiers |= Modifiers.Alt; }
            
            // Key pressed and initial key typed events for all keys.
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (current.IsKeyDown(key) && previous.IsKeyUp(key))
                {
                    OnKeyPressed(this, new KeyboardEventArgs(gameTime.TotalGameTime, key, modifiers, current));
                    OnKeyTyped(this, new KeyboardEventArgs(gameTime.TotalGameTime, key, modifiers, current));

                    // Maintain the state of last key pressed.
                    lastKey = key;
                    lastPress = gameTime.TotalGameTime;
                    isInitial = true;
                }
            }

            // Key released events for all keys.
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (current.IsKeyUp(key) && previous.IsKeyDown(key)) { OnKeyReleased(this, new KeyboardEventArgs(gameTime.TotalGameTime, key, modifiers, current)); }
            }

            // Handle keys being held down and getting multiple KeyTyped events in sequence.
            double elapsedTime = (gameTime.TotalGameTime - lastPress).TotalMilliseconds;

            if (current.IsKeyDown(lastKey) && ((isInitial && elapsedTime > InitialDelay) || (!isInitial && elapsedTime > RepeatDelay)))
            {
                OnKeyTyped(this, new KeyboardEventArgs(gameTime.TotalGameTime, lastKey, modifiers, current));
                lastPress = gameTime.TotalGameTime;
                isInitial = false;
            }

            previous = current;
        }

        /// <summary>
        /// Raises the KeyPressed event. This is done automatically by a correctly configured component,
        /// but this is exposed publicly to allow programmatic key press events to occur.
        /// </summary>
        public void OnKeyPressed(object sender, KeyboardEventArgs args)
        {
            if (KeyPressed != null) { KeyPressed(sender, args); }
        }

        /// <summary>
        /// Raises the KeyReleased event. This is done automatically by a correctly configured component,
        /// but this is exposed publicly to allow programmatic key release events to occur.
        /// </summary>
        public void OnKeyReleased(object sender, KeyboardEventArgs args)
        {
            if (KeyReleased != null) { KeyReleased(sender, args); }
        }

        /// <summary>
        /// Raises the KeyTyped event. This is done automatically by a correctly configured component,
        /// but this is exposed publicly to allow programmatic key typed events to occur.
        /// </summary>
        public void OnKeyTyped(object sender, KeyboardEventArgs args)
        {
            if (KeyTyped != null) { KeyTyped(sender, args); }
        }

        /// <summary>
        /// An event that is raised when a key is first pressed.
        /// </summary>
        public static event EventHandler<KeyboardEventArgs> KeyPressed;

        /// <summary>
        /// An event that is raised when a key is released.
        /// </summary>
        public static event EventHandler<KeyboardEventArgs> KeyReleased;

        /// <summary>
        /// An event that is raised when a key is first pressed, and then periodically again afterwards
        /// until the key is released. There is a longer initial delay, determined by 
        /// KeyboardEvents.InitialDelay, and then subsequent repeats happen at regular intervals as 
        /// determined by KeyboardEvents.RepeatDelay.
        /// </summary>
        public static event EventHandler<KeyboardEventArgs> KeyTyped;
    }
}