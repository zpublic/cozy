// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UniqueNameCreator.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Creates unique names.. It will add a number to given name , if name is already used.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Management
{
    using System.Collections.Generic;

    /// <summary>Creates unique names.. It will add a number to given name, if name is already used.</summary>
    /// <remarks>This uses the singleton pattern. http://en.wikipedia.org/wiki/Singleton_pattern .</remarks>
    public class UniqueNameCreator
    {
        /// <summary>The private static instance (like specified in the singleton pattern).</summary>
        private static UniqueNameCreator instance;

        /// <summary>The names that were already used / created.</summary>
        private readonly List<string> names;

        /// <summary>
        /// The counter is incremented every that there is a new object.. 
        /// This counter can be added to a string to make the string unique.
        /// </summary>
        private static int counter;

        /// <summary>
        /// Prevents a default instance of the <see cref="UniqueNameCreator"/> class from being created.
        /// </summary>
        private UniqueNameCreator()
        {
            this.names = new List<string>();
        }

        /// <summary>Creates the instance using the singleton pattern.</summary>
        /// <returns>A singleton instance of the unique name creator.</returns>
        public static UniqueNameCreator CreateInstance()
        {
            if (instance == null)
            {
                instance = new UniqueNameCreator();
            }

            return instance;
        }

        /// <summary>Creates a unique name , using preferred name.</summary>
        /// <param name="preferredName">Name of the preferred.</param>
        /// <returns>A unique name.</returns>
        public string GetUniqueName(string preferredName)
        {
            var name = preferredName;

            // check if the name is in our dictionary.
            // if it is , get a counter that is higher then the current one.
            if (this.names.Contains(preferredName))
            {
                counter++;
                name = preferredName + " " + counter;
            }

            this.names.Add(name);
            return name;
        }
    }
}
