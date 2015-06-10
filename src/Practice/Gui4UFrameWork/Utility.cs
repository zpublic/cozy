// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Utility.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the Utility type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework
{
    using System;
    using System.Text;

    /// <summary>
    /// A class with random handy functions that can be used generally.
    /// </summary>
    public static class Utility
    {
        /// <summary>Creates random text.</summary>
        /// <param name="random">The random generator.</param>
        /// <param name="textLength">Length of the text to make.</param>
        /// <returns>Random text.</returns>
        /// <exception cref="System.ArgumentNullException">The random number instance.</exception>
        public static string CreateRandomText(Random random, int textLength)
        {
#if DEBUG
            if (random == null)
            {
                throw new ArgumentNullException("random", "A random number generator is needed to create random letters.");
            }
#endif

            // create some random text
            var builder = new StringBuilder();

            char ch;
            for (var i = 0; i < textLength; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor((26 * random.NextDouble()) + 65)));
                builder.Append(ch);
            }

            var randomText = builder.ToString();

            return randomText;
        }

        /// <summary>Reads the time and places that into a string.</summary>
        /// <returns>A time string.</returns>
        public static string ReadTime()
        {
            var now = DateTime.Now;
            var s = string.Format("{0:HH:MM:ss}", now);
            return s;
        }
    }
}
