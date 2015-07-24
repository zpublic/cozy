// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LastPressedKeysDictionary.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the LastPressedKeysDictionary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UWindows.Keyboard
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.Xna.Framework.Input;

    /// <summary>A class containing the last pressed keys.</summary>
    [SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "We do not serialize this.")]
    public class LastPressedKeysDictionary : Dictionary<Keys, TimeSpan>
    {
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var s = string.Empty;
            foreach (var key in this.Keys)
            {
                s += " " + key;
            }

            return s;
        }
    }
}