// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyFilter.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the KeyFilter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UWindows.Keyboard
{
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// A class that helps reading the keyboard.
    /// </summary>
    public class KeyFilter
    {
        /// <summary>The accepted keys.</summary>
        private readonly Keys[] acceptedKeys;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyFilter"/> class.
        /// </summary>
        public KeyFilter()
        {
            this.acceptedKeys = new[]
                                {
                                    Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.H, Keys.I, Keys.J, Keys.K,
                                    Keys.L, Keys.M, Keys.N, Keys.O, Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T, Keys.U, Keys.V,
                                    Keys.W, Keys.X, Keys.Y, Keys.Z, Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5,
                                    Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.Decimal, Keys.Add, Keys.Subtract,
                                    Keys.Multiply, Keys.Divide
                                };
        }

        /// <summary>
        /// Determines whether [is accepted key] [the specified key].
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>If the key is accepted.</returns>
        public bool IsAcceptedKey(Keys key)
        {
            foreach (var acceptedKey in this.acceptedKeys)
            {
                if (key.Equals(acceptedKey))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether we are selecting all.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>True when all selected.</returns>
        public static bool IsSelectAll(KeyboardState state)
        {
            return state.IsKeyDown(Keys.LeftControl) && state.IsKeyDown(Keys.A);
        }

        /// <summary>
        /// Determines whether we are pressing LeftControl and Shift.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>True when pressed.</returns>
        public static bool IsLeftShiftControl(KeyboardState state)
        {
            return state.IsKeyDown(Keys.LeftControl) || state.IsKeyDown(Keys.LeftAlt);
        }

        /// <summary>
        /// Determines whether we are pressing backspace.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>True when pressed.</returns>
        public static bool IsBackspace(KeyboardState state)
        {
            return state.IsKeyDown(Keys.Back);
        }

        /// <summary>
        /// Determines whether we are pressing delete.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>True if delete is pressed.</returns>
        public static bool IsDelete(KeyboardState state)
        {
            return state.IsKeyDown(Keys.Delete);
        }

        /// <summary>
        /// Determines whether we are pressing enter.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>True if enter is pressed.</returns>
        public static bool IsEnter(KeyboardState state)
        {
            return state.IsKeyDown(Keys.Enter);
        }

        /// <summary>
        /// Determines whether key left is pressed..
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if key left is pressed.</returns>
        public static bool IsKeyLeft(Keys key)
        {
            return key == Keys.Left;
        }

        /// <summary>
        /// Determines whether key right is pressed.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if key right is pressed.</returns>
        public static bool IsKeyRight(Keys key)
        {
            return key == Keys.Right;
        }

        /// <summary>
        /// Determines whether a number key is pressed..
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if a number key is pressed.</returns>
        public static bool IsKeyNumber(Keys key)
        {
            return ((key >= Keys.D0) && (key <= Keys.D9)) || ((key >= Keys.NumPad0) && (key <= Keys.NumPad9));
        }

        /// <summary>
        /// Determines whether the left shift key is pressed.
        /// </summary>
        /// <param name="state">The state to check.</param>
        /// <returns>True if the left shift key is pressed.</returns>
        public static bool IsKeyLeftShift(KeyboardState state)
        {
            return state.IsKeyUp(Keys.LeftShift);
        }

        /// <summary>
        /// Determines whether a alphabetic key is pressed.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True when a alfabetic key is pressed.</returns>
        public static bool IsKeyAlphabet(Keys key)
        {
            return key >= Keys.A && key <= Keys.Z;
        }

        /// <summary>
        /// Determines whether the spacebar is pressed.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True when the spacebar is pressed.</returns>
        public static bool IsKeySpacebar(Keys key)
        {
            return key == Keys.Space;
        }

        /// <summary>
        /// Determines whether a gramatical key is pressed.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if a gramatical key is pressed.</returns>
        public static bool IsKeyGrammatical(Keys key)
        {
            return key >= Keys.OemBackslash && key <= Keys.OemSemicolon;
        }

        /// <summary>
        /// Parses the numbers as their shifted character.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>Returns the shifted character.</returns>
        public static string ParseNumbersAsShifted(Keys key)
        {
            switch (key)
            {
                case Keys.D1: return "!";
                case Keys.D2: return "@";
                case Keys.D3: return "#";
                case Keys.D4: return "$";
                case Keys.D5: return "%";
                case Keys.D6: return "^";
                case Keys.D7: return "&";
                case Keys.D8: return "*";
                case Keys.D9: return "(";
                case Keys.D0: return ")";
            }

            return string.Empty;
        }

        /// <summary>
        /// Parses the special keys normally.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>Normally parsed special keys.</returns>
        public static string ParseSpecialAsNormal(Keys key)
        {
            // Non-shifted keys
            switch (key)
            {
                case Keys.OemMinus: return "-";
                case Keys.OemPlus: return "=";
                case Keys.OemOpenBrackets: return "[";
                case Keys.OemCloseBrackets: return "]";
                case Keys.OemSemicolon: return ";";
                case Keys.OemQuotes: return "'";
                case Keys.OemComma: return ",";
                case Keys.OemPeriod: return ".";
                case Keys.OemBackslash: return "/";
            }

            return string.Empty;
        }

        /// <summary>
        /// Parses the special keys as shifted keys.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>A string of shifted keys.</returns>
        public static string ParseSpecialAsShifted(Keys key)
        {
            // Shifted keys
            switch (key)
            {
                case Keys.OemMinus: return "_";
                case Keys.OemPlus: return "+";
                case Keys.OemOpenBrackets: return "{";
                case Keys.OemCloseBrackets: return "}";
                case Keys.OemSemicolon: return ":";
                case Keys.OemQuotes: return "\"";
                case Keys.OemComma: return "<";
                case Keys.OemPeriod: return ">";
                case Keys.OemBackslash: return "?";
            }

            return string.Empty;
        }
    }
}