using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Input
{
    /// <summary>
    /// Provides a set of utility methods for keyboard input. Of particular note is the ToChar method, which
    /// converts a key to a character.
    /// </summary>
    public static class KeyboardUtil
    {
        /// <summary>
        /// Returns a character that represents the key that was pressed, including accounting for pressing
        /// the shift key. In the event that they supplied key doesn't translate into a character, null
        /// is returned instead.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        public static char? ToChar(Keys key, Modifiers modifiers = Modifiers.None)
        {
            if (key == Keys.A) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'A' : 'a'; }
            if (key == Keys.B) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'B' : 'b'; }
            if (key == Keys.C) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'C' : 'c'; }
            if (key == Keys.D) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'D' : 'd'; }
            if (key == Keys.E) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'E' : 'e'; }
            if (key == Keys.F) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'F' : 'f'; }
            if (key == Keys.G) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'G' : 'g'; }
            if (key == Keys.H) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'H' : 'h'; }
            if (key == Keys.I) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'I' : 'i'; }
            if (key == Keys.J) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'J' : 'j'; }
            if (key == Keys.K) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'K' : 'k'; }
            if (key == Keys.L) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'L' : 'l'; }
            if (key == Keys.M) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'M' : 'm'; }
            if (key == Keys.N) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'N' : 'n'; }
            if (key == Keys.O) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'O' : 'o'; }
            if (key == Keys.P) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'P' : 'p'; }
            if (key == Keys.Q) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'Q' : 'q'; }
            if (key == Keys.R) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'R' : 'r'; }
            if (key == Keys.S) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'S' : 's'; }
            if (key == Keys.T) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'T' : 't'; }
            if (key == Keys.U) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'U' : 'u'; }
            if (key == Keys.V) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'V' : 'v'; }
            if (key == Keys.W) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'W' : 'w'; }
            if (key == Keys.X) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'X' : 'x'; }
            if (key == Keys.Y) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'Y' : 'y'; }
            if (key == Keys.Z) { return ((modifiers & Modifiers.Shift) == Modifiers.Shift) ? 'Z' : 'z'; }

            if ((key == Keys.D0 && !ShiftDown(modifiers)) || key == Keys.NumPad0) { return '0'; }
            if ((key == Keys.D1 && !ShiftDown(modifiers)) || key == Keys.NumPad1) { return '1'; }
            if ((key == Keys.D2 && !ShiftDown(modifiers)) || key == Keys.NumPad2) { return '2'; }
            if ((key == Keys.D3 && !ShiftDown(modifiers)) || key == Keys.NumPad3) { return '3'; }
            if ((key == Keys.D4 && !ShiftDown(modifiers)) || key == Keys.NumPad4) { return '4'; }
            if ((key == Keys.D5 && !ShiftDown(modifiers)) || key == Keys.NumPad5) { return '5'; }
            if ((key == Keys.D6 && !ShiftDown(modifiers)) || key == Keys.NumPad6) { return '6'; }
            if ((key == Keys.D7 && !ShiftDown(modifiers)) || key == Keys.NumPad7) { return '7'; }
            if ((key == Keys.D8 && !ShiftDown(modifiers)) || key == Keys.NumPad8) { return '8'; }
            if ((key == Keys.D9 && !ShiftDown(modifiers)) || key == Keys.NumPad9) { return '9'; }

            if (key == Keys.D0 && ShiftDown(modifiers)) { return ')'; }
            if (key == Keys.D1 && ShiftDown(modifiers)) { return '!'; }
            if (key == Keys.D2 && ShiftDown(modifiers)) { return '@'; }
            if (key == Keys.D3 && ShiftDown(modifiers)) { return '#'; }
            if (key == Keys.D4 && ShiftDown(modifiers)) { return '$'; }
            if (key == Keys.D5 && ShiftDown(modifiers)) { return '%'; }
            if (key == Keys.D6 && ShiftDown(modifiers)) { return '^'; }
            if (key == Keys.D7 && ShiftDown(modifiers)) { return '&'; }
            if (key == Keys.D8 && ShiftDown(modifiers)) { return '*'; }
            if (key == Keys.D9 && ShiftDown(modifiers)) { return '('; }

            if (key == Keys.Space) { return ' '; }
            if (key == Keys.Tab) { return '\t'; }
            if (key == Keys.Enter) { return '\n'; }

            if (key == Keys.Add) { return '+'; }
            if (key == Keys.Decimal) { return '.'; }
            if (key == Keys.Divide) { return '/'; }
            if (key == Keys.Multiply) { return '*'; }
            if (key == Keys.OemBackslash) { return '\\'; }
            if (key == Keys.OemComma && !ShiftDown(modifiers)) { return ','; }
            if (key == Keys.OemComma &&  ShiftDown(modifiers)) { return '<'; }
            if (key == Keys.OemOpenBrackets && !ShiftDown(modifiers)) { return '['; }
            if (key == Keys.OemOpenBrackets &&  ShiftDown(modifiers)) { return '{'; }
            if (key == Keys.OemCloseBrackets && !ShiftDown(modifiers)) { return ']'; }
            if (key == Keys.OemCloseBrackets &&  ShiftDown(modifiers)) { return '}'; }
            if (key == Keys.OemPeriod && !ShiftDown(modifiers)) { return '.'; }
            if (key == Keys.OemPeriod &&  ShiftDown(modifiers)) { return '>'; }
            if (key == Keys.OemPipe && !ShiftDown(modifiers)) { return '\\'; }
            if (key == Keys.OemPipe &&  ShiftDown(modifiers)) { return '|'; }
            if (key == Keys.OemPlus && !ShiftDown(modifiers)) { return '='; }
            if (key == Keys.OemPlus &&  ShiftDown(modifiers)) { return '+'; }
            if (key == Keys.OemMinus && !ShiftDown(modifiers)) { return '-'; }
            if (key == Keys.OemMinus &&  ShiftDown(modifiers)) { return '_'; }
            if (key == Keys.OemQuestion && !ShiftDown(modifiers)) { return '/'; }
            if (key == Keys.OemQuestion &&  ShiftDown(modifiers)) { return '?'; }
            if (key == Keys.OemQuotes && !ShiftDown(modifiers)) { return '\''; }
            if (key == Keys.OemQuotes &&  ShiftDown(modifiers)) { return '"'; }
            if (key == Keys.OemSemicolon && !ShiftDown(modifiers)) { return ';'; }
            if (key == Keys.OemSemicolon &&  ShiftDown(modifiers)) { return ':'; }
            if (key == Keys.OemTilde && !ShiftDown(modifiers)) { return '`'; }
            if (key == Keys.OemTilde &&  ShiftDown(modifiers)) { return '~'; }
            if (key == Keys.Subtract) { return '-'; }

            return null;
        }

        /// <summary>
        /// Returns whether the given modifiers includes the Alt key.
        /// </summary>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        public static bool AltDown(Modifiers modifiers)
        {
            return ((modifiers & Modifiers.Alt) == Modifiers.Alt);
        }

        /// <summary>
        /// Returns whether the given modifiers includes the Control key.
        /// </summary>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        public static bool ControlDown(Modifiers modifiers)
        {
            return ((modifiers & Modifiers.Control) == Modifiers.Control);
        }

        /// <summary>
        /// Returns whether given modifiers include the Shift key.
        /// </summary>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        public static bool ShiftDown(Modifiers modifiers)
        {
            return ((modifiers & Modifiers.Shift) == Modifiers.Shift);
        }

        /// <summary>
        /// Returns whether the key represents a whitespace character.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsWhitespace(Keys key)
        {
            switch(key)
            {
                case Keys.Tab:
                case Keys.Space:
                case Keys.Enter:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Returns whether the key represents an alphabetical symbol.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsAlpha(Keys key)
        {
            return ((int)key >= (int)Keys.A && (int)key <= (int)Keys.Z);
        }

        /// <summary>
        /// Returns whether the key represents a numeric symbol.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsNumeric(Keys key)
        {
            switch(key)
            {
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                case Keys.NumPad0:
                case Keys.NumPad1:
                case Keys.NumPad2:
                case Keys.NumPad3:
                case Keys.NumPad4:
                case Keys.NumPad5:
                case Keys.NumPad6:
                case Keys.NumPad7:
                case Keys.NumPad8:
                case Keys.NumPad9:
                    return true;
                default:
                    return false;                
            }
        }
    }
}
