// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonitorSetting.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the MonitorSetting type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Graphics
{
    using System;

    /// <summary>
    /// The settings for a monitor connected to this device.
    /// </summary>
    public struct MonitorSetting : IEquatable<MonitorSetting>
    {
        /// <summary>
        /// Gets or sets the width of the monitor.
        /// </summary>
        /// <value>
        /// The width of the monitor.
        /// </value>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the monitor.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the name of the device.
        /// </summary>
        /// <value>
        /// The name of the device.
        /// </value>
        public string DeviceName { get; set; }

        /// <summary>Gets or sets the pixel format of the device.</summary>
        /// <value>The pixel format of the device.</value>
        public PixelFormat PixelFormat { get; set; }

        /// <summary>
        /// Gets or sets the left side of this monitor on the multi-monitor screen-space.
        /// </summary>
        /// <value>
        /// The left of this monitor on the multi-monitor screen-space.
        /// </value>
        public int Left { get; set; }

        /// <summary>
        /// Gets or sets the top of this monitor on the multi-monitor screen-space.
        /// </summary>
        /// <value>
        /// The top of this monitor on the multi-monitor screen-space.
        /// </value>
        public int Top { get; set; }

        /// <summary>
        /// Gets or sets the refresh rate of this monitor on the multi-monitor screen-space.
        /// </summary>
        /// <value>
        /// The refresh rate of this monitor on the multi-monitor screen-space.
        /// </value>
        public int RefreshRate { get; set; }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return 
                this.Width ^
                this.Height ^
                this.DeviceName.GetHashCode() ^
                this.PixelFormat.GetHashCode() ^
                this.Left ^
                this.Top ^
                this.RefreshRate;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is MonitorSetting))
            {
                return false;
            }

            return this.Equals((MonitorSetting)obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(MonitorSetting other)
        {
            if (
                this.Width != other.Width ||
                this.Height != other.Height ||
                this.DeviceName != other.DeviceName ||
                this.PixelFormat != other.PixelFormat ||
                this.Left != other.Left ||
                this.Top != other.Top ||
                this.RefreshRate != other.RefreshRate)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(MonitorSetting point1, MonitorSetting point2)
        {
            return point1.Equals(point2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="point1">The point1.</param>
        /// <param name="point2">The point2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(MonitorSetting point1, MonitorSetting point2)
        {
            return !point1.Equals(point2);
        }
    }
}