// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FastRepeatKeysCollection.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   A list of keys that were self repeated.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UWindows.Keyboard
{
    using System.Collections.ObjectModel;

    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// A list of keys that were self repeated.
    /// </summary>
    public class FastRepeatKeysCollection : Collection<Keys>
    {
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            var s = string.Empty;
            foreach (var key in this)
            {
                s += " " + key;
            }

            return s;
        }
    }
}