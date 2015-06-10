// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NodeCollection.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   this is a list of nodes , look at it as the branches from the main branch
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Structural
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>this is a list of nodes , look at it as the branches from the main branch.</summary>
    public class NodeCollection : ICollection<Node>
    {
        /// <summary>The nodes.</summary>
        private readonly List<Node> nodes = new List<Node>();

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the entry to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        public Node this[int index]
        {
            get
            {
                if (index < 0 || index > this.nodes.Count - 1)
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                return this.nodes[index];
            }

            set
            {
                if (index < 0 || index > this.nodes.Count - 1)
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                this.nodes[index] = value;
            }
        }

        /// <summary>
        /// Adds an item to the collection.
        /// </summary>
        /// <param name="item">The item to add to the collection.</param>
        public void Add(Node item)
        {
            this.nodes.Add(item);
        }

        /// <summary>
        /// Insert an item at a specified position to the collection.
        /// </summary>
        /// <param name="index">The index of the item in the list to insert next to.</param>
        /// <param name="item">The item to add to the collection.</param>
        public void Insert(int index, Node item)
        {
            this.nodes.Insert(index, item);
        }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        public void Clear()
        {
            this.nodes.Clear();
        }

        /// <summary>
        /// Determines whether the collection contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the collection.</param>
        /// <returns>true if item is found in the collection; otherwise, false.</returns>
        public bool Contains(Node item)
        {
            return this.nodes.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the collection to an <see cref="System.Array"/>, starting at a particular index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from the collection. The System.Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(Node[] array, int arrayIndex)
        {
            this.nodes.CopyTo(array, arrayIndex);
        }

        /// <summary>Gets the number of elements contained in the collection.</summary>
        /// <value>The number of nodes i have.</value>
        public int Count
        {
            get { return this.nodes.Count; }
        }

        /// <summary>Gets a value indicating whether the collection is read-only.</summary>
        /// <value>True when i am only readable.</value>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the collection.
        /// </summary>
        /// <param name="item">The object to remove from the collection.</param>
        /// <returns>true if item was successfully removed from the collection; otherwise, false. This method also returns false if item is not found in the collection.</returns>
        public bool Remove(Node item)
        {
            return this.nodes.Remove(item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="System.Collections.Generic.IEnumerator{SceneNode}"/> that can be used to iterate through the collection.</returns>
        public IEnumerator<Node> GetEnumerator()
        {
            return this.nodes.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="System.Collections.IEnumerator"/> that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.nodes.GetEnumerator();
        }
    }
}
