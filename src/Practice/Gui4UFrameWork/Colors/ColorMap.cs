// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorMap.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ColorMap type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Colors
{
    using System.Dynamic;

    /// <summary>
    /// Gives the user a 2d representation of the 1d color-array carried inside me.
    /// </summary>
    public class ColorMap
    {
        /// <summary>The colors contained in the color-map.</summary>
        private readonly GUIColor[] colors;

        /// <summary>The width of the color-map.</summary>
        private readonly int width;

        /// <summary>The height of the color-map.</summary>
        private readonly int height;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorMap"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public ColorMap(int width, int height)
        {
            this.width = width;
            this.height = height;

            this.colors = new GUIColor[this.width * this.height];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorMap"/> class.
        /// </summary>
        /// <param name="colors">The colors in array.</param>
        /// <param name="width">The width of the array.</param>
        /// <param name="height">The height of the array.</param>
        public ColorMap(GUIColor[] colors, int width, int height)
        {
            this.colors = colors;
            this.width = width;
            this.height = height;
        }

        /// <summary>Gets the width of the color map.</summary>
        /// <value>The width.</value>
        public int Width
        {
            get
            {
                return this.width;
            }
        }

        /// <summary>Gets the height of the color map.</summary>
        /// <value>The height.</value>
        public int Height
        {
            get
            {
                return this.height;
            }
        }

        /// <summary>
        /// Gets the length of the array contained.
        /// </summary>
        /// <value>
        /// The length of the internal array.
        /// </value>
        public long Length
        {
            get
            {
                return this.width * this.height;
            }
        }

        /// <summary>
        /// Gets the internal color-array.
        /// </summary>
        /// <returns>The map converted to a array.</returns>
        public GUIColor[] ToArray()
        {
            return this.colors;
        }

        /// <summary>Gets the specified color at location x,y.</summary>
        /// <param name="locationX">The location x.</param>
        /// <param name="locationY">The location y.</param>
        /// <returns>The color at given location.</returns>
        public GUIColor Get(int locationX, int locationY)
        {
            return this.colors[locationX + (locationY * this.width)];
        }

        /// <summary>Sets the specified color at location x,y.</summary>
        /// <param name="locationX">The location x.</param>
        /// <param name="locationY">The location y.</param>
        /// <param name="color">The color.</param>
        public void Set(int locationX, int locationY, GUIColor color)
        {
            this.colors[locationX + (locationY * this.width)] = color;
        }

        /// <summary>Tries to set the specified color at location x,y.</summary>
        /// <param name="locationX">The location x.</param>
        /// <param name="locationY">The location y.</param>
        /// <param name="color">The color.</param>
        /// <returns>true when there is success setting it.</returns>
        public bool TrySet(int locationX, int locationY, GUIColor color)
        {
            var pos = locationX + (locationY * this.width);
            if (pos > this.colors.Length)
            {
                return false;
            }

            this.colors[pos] = color;
            return true;
        }

        /// <summary>
        /// Reverses this color-array in this instance.
        /// </summary>
        /// <returns>A reversed color-map.</returns>
        public ColorMap Reverse()
        {
            var reverseData = new GUIColor[this.Width * this.Height];
            var r = reverseData.Length - 1;
            foreach (var guiColor in this.colors)
            {
                reverseData[r] = guiColor;
                r--;
            }

            var map = new ColorMap(reverseData, this.width, this.height);

            return map;
        }
    }
}