// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlExtensions.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ControlExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Structural
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using GUI4UFramework.Graphics;

    /// <summary>
    /// Contains extra extensions for the Control class..
    /// </summary>
    public static class ControlExtensions
    {
        /// <summary>
        /// Determines whether given location is inside the control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="positionX">The positionX of the location.</param>
        /// <param name="positionY">The positionY of the location.</param>
        /// <returns>Whether the given location is inside.</returns>
        public static bool IsPointInside(this Control control, int positionX, int positionY)
        {
#if DEBUG
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
#endif

            var absPos = new DVector2(control.State.DrawPosition.X, control.State.DrawPosition.Y);

            return absPos.X < positionX && positionX < (absPos.X + control.State.Width) &&
                   absPos.Y < positionY && positionY < (absPos.Y + control.State.Height);
        }

        /// <summary>
        /// Adds the child control to the parent. Passes along the manager and parent props too.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="child">The child.</param>
        public static void AddControl(this Control parent, Control child)
        {
#if DEBUG
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }

            if (child == null)
            {
                throw new ArgumentNullException("child");
            }
#endif

            child.Parent = parent;
            child.Manager = parent.Manager;

            parent.Children.Add(child);
        }

        /// <summary>
        /// Removes the child control from the parent. Disconnect the manager too..
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="child">The child.</param>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void RemoveControl(this Control parent, Control child)
        {
#if DEBUG
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }

            if (child == null)
            {
                throw new ArgumentNullException("child");
            }
#endif

            child.Parent = null;
            child.Manager = null;

            parent.Children.Remove(child);
        }
    }
}