// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyboardReader.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Reads out the keyboard.
//   Checks for keys that have to be repeated and repeats them.
//   Uses the KeyFlags-class to communicate its new events to the outside world.
//   Starts reading the keyboard thanks to the UpdateKeyboardInput-function.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UWindows.Keyboard
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using GUI4UFramework.Management;

    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Reads out the keyboard.
    /// Checks for keys that have to be repeated and repeats them.
    /// Uses the KeyFlags-class to communicate its new events to the outside world.
    /// Starts reading the keyboard thanks to the UpdateKeyboardInput-function.
    /// </summary>
    public class KeyboardReader
    {
        /// <summary>The last pressed keys.</summary>
        private readonly LastPressedKeysDictionary lastPressedKeys;

        /// <summary>The fast repeat keys.</summary>
        private readonly FastRepeatKeysCollection fastRepeatKeys;

        /// <summary>The key repeat time.</summary>
        private readonly TimeSpan keyRepeatTime;

        /// <summary>The fast key repeat time.</summary>
        private readonly TimeSpan fastKeyRepeatTime;

        /// <summary>The keyboard state.</summary>
        private KeyboardState keyboardState;

        /// <summary>The last pressed keys count.</summary>
        private int lastPressedKeysCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardReader"/> class.
        /// </summary>
        public KeyboardReader()
        {
            this.keyRepeatTime = new TimeSpan(0, 0, 0, 0, 500); // 500 ms
            this.fastKeyRepeatTime = new TimeSpan(0, 0, 0, 0, 25); // 25 ms
            this.fastRepeatKeys = new FastRepeatKeysCollection();
            this.lastPressedKeys = new LastPressedKeysDictionary();
            this.Text = string.Empty;
            KeySwitches = new KeySwitches();
        }

        /// <summary>
        /// Gets the key switches.
        /// </summary>
        /// <value>
        /// The key switches.
        /// </value>
        public KeySwitches KeySwitches { get; private set; }

        /// <summary>Gets or sets the text.</summary>
        /// <value>The text read.</value>
        public string Text
        {
            get; 
            set;
        }

#if DEBUG
        /// <summary>
        /// Gets the last pressed keys.
        /// </summary>
        /// <value>
        /// The last pressed keys.
        /// </value>
        public LastPressedKeysDictionary LastPressedKeys
        {
            get
            {
                return this.lastPressedKeys;
            }
        }

        /// <summary>
        /// Gets the fast repeat keys.
        /// </summary>
        /// <value>
        /// The fast repeat keys.
        /// </value>
        public FastRepeatKeysCollection FastRepeatKeys
        {
            get
            {
                return this.fastRepeatKeys;
            }
        }

        /// <summary>Gets the pressed keys.</summary>
        /// <value>The pressed keys.</value>
        public string PressedKeys
        {
            get; 
            private set;
        }
#endif

        /// <summary>
        /// Gets or sets a value indicating whether we are in /[debug mode].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [debug mode]; otherwise, <c>false</c>.
        /// </value>
        public bool DebugMode { get; set; }

        /// <summary>
        /// Perform keyboard input.
        /// Includes insertion and deletion logic.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        public void UpdateKeyboardInput(GameTime gameTime)
        {
#if DEBUG
            if (gameTime == null)
            {
                throw new ArgumentNullException("gameTime", "Gametime is used here, can not be null.");
            }
#endif

            // read the keyboard state
            this.keyboardState = Keyboard.GetState();

            if (this.DebugMode)
            {
                var s = "Pressed keys";

                foreach (var pressedKey in this.keyboardState.GetPressedKeys())
                {
                    s = string.Format("{0} {1}", s, pressedKey);
                }

                Debug.WriteLine(s);

                this.PressedKeys = s;
            }

            // Remove any keys from last pressed keys if time has expired, or if they're released
            // Use a fast repeat keys list after the initial delay to input keys faster after being held down for a short time (windows textbox-like behavior)
            // What it does is catch the last pressed keys, and if they are pressed long enough, convert them to fast-repeat keys
            var culledKeys = new List<Keys>();

            // list of keys that can be repeated again because they have timed out or key is lifted
            foreach (var keyPair in this.lastPressedKeys)
            {
                if (this.keyboardState.IsKeyUp(keyPair.Key))
                {
                    if (this.fastRepeatKeys.Contains(keyPair.Key))
                    {
                        this.fastRepeatKeys.Remove(keyPair.Key);
                    }

                    culledKeys.Add(keyPair.Key);
                }
                else
                {
                    var keyTimeSpan = gameTime.TotalGameTime - keyPair.Value;

                    if (keyTimeSpan > this.keyRepeatTime && !this.fastRepeatKeys.Contains(keyPair.Key))
                    {
                        // Input key and then add to the fast list
                        culledKeys.Add(keyPair.Key);
                        this.fastRepeatKeys.Add(keyPair.Key);
                    }
                    else if (keyTimeSpan > this.fastKeyRepeatTime && this.fastRepeatKeys.Contains(keyPair.Key))
                    {
                        // Fast repeat
                        culledKeys.Add(keyPair.Key);
                    }
                }
            }

            foreach (var key in culledKeys)
            {
                this.lastPressedKeys.Remove(key);
            }

            if (this.lastPressedKeys.Count != this.lastPressedKeysCount)
            {
                KeySwitches.ChangeText = true;
                this.lastPressedKeysCount = this.lastPressedKeys.Count;
            }

            // ****************************************** do the commands ******************************************

            // Select All (Ctrl+A)
            if (KeyFilter.IsSelectAll(this.keyboardState))
            {
                this.RaiseSelectAll();
            }

            // Left Shift Control 
            if (KeyFilter.IsLeftShiftControl(this.keyboardState))
            {
                return;
            }

            // Backspace
            if (KeyFilter.IsBackspace(this.keyboardState) && !this.lastPressedKeys.ContainsKey(Keys.Back))
            {
                this.RaiseKeyBackSpace();
                this.lastPressedKeys.Add(Keys.Back, gameTime.TotalGameTime);
            }
            else if (KeyFilter.IsDelete(this.keyboardState) && !this.lastPressedKeys.ContainsKey(Keys.Delete))
            {
                // Delete
                this.RaiseKeyDelete();
                this.lastPressedKeys.Add(Keys.Delete, gameTime.TotalGameTime);
            }
            else if (KeyFilter.IsEnter(this.keyboardState) && this.Text.Length > 0 && !this.lastPressedKeys.ContainsKey(Keys.Enter))
            {
                // Enter
                this.lastPressedKeys.Add(Keys.Enter, gameTime.TotalGameTime);
                this.RaiseEnterPressed();
            }
            else
            {
                string textToInsert = null;
                var keys = this.keyboardState.GetPressedKeys();

                foreach (var key in keys)
                {
                    if (this.lastPressedKeys.ContainsKey(key))
                    {
                        continue;
                    }

                    // if key is left or if key is right
                    if (KeyFilter.IsKeyLeft(key))
                    {
                        this.RaiseGoLeft();
                    }
                    else if (KeyFilter.IsKeyRight(key))
                    {
                        this.RaiseGoRight();
                    }
                    
                    // if key is a number
                    if (KeyFilter.IsKeyNumber(key))
                    {
                        if (KeyFilter.IsKeyLeftShift(this.keyboardState))
                        {
                            textToInsert += key.ToString().Replace("D", string.Empty);
                        }
                        else
                        {
                            textToInsert += KeyFilter.ParseNumbersAsShifted(key);
                        }
                    }
                    else if (KeyFilter.IsKeyAlphabet(key))
                    {
                        // if key is a character from the alphabet
                        if (this.keyboardState.IsKeyUp(Keys.LeftShift) && this.keyboardState.IsKeyUp(Keys.CapsLock))
                        {
                            textToInsert += key.ToString().ToLower();
                        }
                        else
                        {
                            textToInsert += key;
                        }
                    }
                    else
                    {
                        // if the key is uppercase
                        if (KeyFilter.IsKeyLeftShift(this.keyboardState))
                        {
                            textToInsert += KeyFilter.ParseSpecialAsShifted(key);
                        }
                        else
                        {
                            textToInsert += KeyFilter.ParseSpecialAsNormal(key);
                        }

                        // if there is a spacebar
                        if (KeyFilter.IsKeySpacebar(key))
                        {
                            textToInsert += " ";
                        }
                    }

                    // if the key is grammatical
                    if (KeyFilter.IsKeyGrammatical(key))
                    {
                        textToInsert += key;
                    }

                    // If we have input a valid string
                    if (string.IsNullOrEmpty(textToInsert) == false)
                    {
                        this.RaiseInsertText(textToInsert);
                    }

                    this.lastPressedKeys.Add(key, gameTime.TotalGameTime);
                }
            }
        }

        /// <summary>
        /// Raises the select all flag.
        /// </summary>
        private void RaiseSelectAll()
        {
            KeySwitches.SelectAll = true;
        }

        /// <summary>Raises the enter pressed flag.</summary>
        private void RaiseEnterPressed()
        {
            KeySwitches.EnterPressed = true;
        }

        /// <summary>Raises the go left flag.</summary>
        private void RaiseGoLeft()
        {
            KeySwitches.DoCursorLeft = true;
        }

        /// <summary>Raises the go right flag.</summary>
        private void RaiseGoRight()
        {
            KeySwitches.DoCursorRight = true;
        }

        /// <summary>Raises the insert text flag.</summary>
        /// <param name="textToInsert">The text to insert.</param>
        private void RaiseInsertText(string textToInsert)
        {
            KeySwitches.TextToInsert = textToInsert;
            KeySwitches.InsertText = true;
        }

        /// <summary>Raises the key back space flag.</summary>
        private void RaiseKeyBackSpace()
        {
            KeySwitches.Backspace = true;
        }

        /// <summary>Raises the key delete flag.</summary>
        private void RaiseKeyDelete()
        {
            KeySwitches.KeyDelete = true;
        }
    }
}
