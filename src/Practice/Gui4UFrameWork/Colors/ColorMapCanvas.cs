// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorMapCanvas.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   This is a color-map where coordinate 0,0 is the center (in stead of top-left).
//   Left = minus
//   Right = plus
//   Top = minus
//   Bottom = plus
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Colors
{
    using System;

    /// <summary>This is a color-map where coordinate 0,0 is the center (in stead of top-left).
    /// Left = minus 
    /// Right = plus
    /// Top = minus
    /// Bottom = plus.</summary>
    public class ColorMapCanvas
    {
        /// <summary>The color map array that contains the actual data.</summary>
        private readonly ColorMap colorMap;

        /// <summary>The total width of the color map.</summary>
        private readonly int totalwidth;

        /// <summary>The total height of the color map.</summary>
        private readonly int totalheight;

        /// <summary>
        /// The left side distance when canvas has a origin in the center of the color-map.
        /// </summary>
        private readonly float left;

        /// <summary>
        /// The right side distance when canvas has a origin in the center of the color-map.
        /// </summary>
        private readonly float right;

        /// <summary>
        /// The top side distance when canvas has a origin in the center of the color-map.
        /// </summary>
        private readonly float top;

        /// <summary>
        /// The bottom side distance when canvas has a origin in the center of the color-map.
        /// </summary>
        private readonly float bottom;

        /// <summary>Initializes a new instance of the <see cref="ColorMapCanvas"/> class.</summary>
        /// <param name="colorMap">The color map.</param>
        public ColorMapCanvas(ColorMap colorMap)
        {
#if DEBUG
            if (colorMap == null)
            {
                throw new ArgumentNullException("colorMap", "ColorMap can not be NULL");
            }
#endif
            this.colorMap = colorMap;
            this.totalwidth = colorMap.Width;
            this.totalheight = colorMap.Height;
            this.left = -(this.totalwidth * 0.5f);
            this.right = +(this.totalwidth * 0.5f);
            this.top = -(this.totalheight * 0.5f);
            this.bottom = +(this.totalheight * 0.5f);
        }

        /// <summary>Gets the specified color at given location. Point 0,0 is at center of the map.</summary>
        /// <param name="positionX">The x position.</param>
        /// <param name="positionY">The y position.</param>
        /// <returns>The color at given location.</returns>
        public GUIColor Get(int positionX, int positionY)
        {
            var locationX = (int)(positionX + (this.TotalWidth / 2));
            var locationY = (int)(positionY + (this.TotalHeight / 2));

            return this.colorMap.Get(locationX, locationY);
        }

        /// <summary>
        /// Gets the left side distance when canvas has a origin in the center of the color-map.
        /// </summary>
        /// <value>
        /// The left side..
        /// </value>
        public float Left
        {
            get
            {
                return this.left;
            }
        }

        /// <summary>
        /// Gets the right side distance when canvas has a origin in the center of the color-map.
        /// </summary>
        /// <value>
        /// The right side.
        /// </value>
        public float Right
        {
            get
            {
                return this.right;
            }
        }

        /// <summary>
        /// Gets the top side distance when canvas has a origin in the center of the color-map.
        /// </summary>
        /// <value>
        /// The top side.
        /// </value>
        public float Top
        {
            get
            {
                return this.top;
            }
        }

        /// <summary>
        /// Gets the bottom side distance when canvas has a origin in the center of the color-map.
        /// </summary>
        /// <value>
        /// The bottom side.
        /// </value>
        public float Bottom
        {
            get
            {
                return this.bottom;
            }
        }

        /// <summary>Gets the total width of the color-map.</summary>
        /// <value>The total width.</value>
        public float TotalWidth
        {
            get
            {
                return this.totalwidth;
            }
        }

        /// <summary>Gets the total height of the color-map.</summary>
        /// <value>The total height.</value>
        public float TotalHeight
        {
            get
            {
                return this.totalheight;
            }
        }
    }
}
