// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourcePool.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   uses the crud method..
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Management
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>uses the crud method..</summary>
    /// <remarks>http://en.wikipedia.org/wiki/Create,_read,_update_and_delete.</remarks>
    /// <typeparam name="T">The resource type contained in this resource-pool.</typeparam>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed. Suppression is OK here.")]
    public abstract class ResourcePool<T> where T : class
    {
        /// <summary>
        /// The dictionary that contains the resources.
        /// </summary>
        private readonly Dictionary<string, T> resources;

        /// <summary>The private field that contains the singleton of the unique name creator.</summary>
        private readonly UniqueNameCreator uniqueNameCreator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcePool{T}"/> class.
        /// </summary>
        protected ResourcePool()
        {
            this.resources = new Dictionary<string, T>();
            this.uniqueNameCreator = UniqueNameCreator.CreateInstance();
        }

        /// <summary>
        /// Gets the dictionary that contains the resources.
        /// </summary>
        /// <value>
        /// The resources.
        /// </value>
        protected Dictionary<string, T> Resources
        {
            get
            {
                return this.resources;
            }
        }

        /// <summary>
        /// Gets the unique name creator.
        /// </summary>
        /// <value>
        /// The unique name creator.
        /// </value>
        protected UniqueNameCreator UniqueNameCreator
        {
            get
            {
                return this.uniqueNameCreator;
            }
        }

        /// <summary>Creates a resource in the resource pool.
        /// The specified prefferedName is used to make a name that is unique , using the unique-name-creator.
        /// That unique name is used in the resource-pool to know where it is , and that named is returned to the user.</summary>
        /// <param name="preferredName">the preferred name for the new resource.</param>
        /// <param name="value">The value to add to the resources.</param>
        /// <returns>A special unique prefferedName to tell where the object is in the resource pool.</returns>
        public string Create(string preferredName, T value)
        {
            var key = this.uniqueNameCreator.GetUniqueName(preferredName);
            this.resources.Add(key, value);
            return key;
        }

        /// <summary>
        /// Updates the specified the object a specified prefferedName-location with the new specified object.
        /// </summary>
        /// <param name="name">The named location in the resource pool.</param>
        /// <param name="newObject">The new object.</param>
        public void Update(string name, T newObject)
        {
            this.resources[name] = newObject;
        }

        /// <summary>Reads the object at a location with specified name.</summary>
        /// <param name="name">The item to find using its name, in my resource pool.</param>
        /// <returns>The object at a location with specified name.</returns>
        public T Read(string name)
        {
#if DEBUG
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "If the prefferedName variable is NULL, then we already know for sure we will NOT get the resource with THAT.");
            }
#endif
            T obj;
            var success = this.resources.TryGetValue(name, out obj);
            if (success != false)
            {
                return obj;
            }

            System.Diagnostics.Debug.WriteLine("Trying to get [" + name + "] but can not find it");
            this.Debug();
            Debugger.Break();
            return null;
        }

        /// <summary>
        /// Deletes the object at a location with specified name. 
        /// </summary>
        /// <param name="name">The name of the location where the object is to delete.</param>
        public void Delete(string name)
        {
            var success = this.resources.Remove(name);
            if (success == false)
            {
                System.Diagnostics.Debug.WriteLine("Could not delete " + name + " in library, cannot find it.");
                Debugger.Break();
                this.Debug();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Deleted " + name);
            }
        }

        /// <summary>
        /// Determines whether there is a object at the location with [the specified resource name].
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>True if we have the resource, otherwise false.</returns>
        public bool Contains(string resourceName)
        {
            if (string.IsNullOrEmpty(resourceName))
            {
                return false;
            }

            return this.resources.ContainsKey(resourceName);
        }

        /// <summary>
        /// Debugs this instance. Let this class spit out debug info.
        /// </summary>
        public abstract void Debug();
    }
}