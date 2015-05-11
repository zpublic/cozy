using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Input
{
    /// <summary>
    /// Represents a polar coordinate, defined by a distance from the origin and an angle from the
    /// polar axis. 
    /// </summary>
    public class PolarCoordinate
    {
        /// <summary>
        /// Stores the angle of the polar coordinate from the polar axis (usually the equivalent of the
        /// x-axis).
        /// </summary>
        public float Angle { get; private set; }

        /// <summary>
        /// Stores the distance from the coordinate system's origin.
        /// </summary>
        public float Distance { get; private set; }

        /// <summary>
        /// Creates a new PolarCoordinate from an angle and distance.
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="distance"></param>
        public PolarCoordinate(float angle, float distance)
        {
            Angle = angle;
            Distance = distance;
        }

        /// <summary>
        /// Converts a Cartesian x-, y- coordinate pair to polar coordinates.
        /// </summary>
        /// <param name="cartesian"></param>
        /// <returns></returns>
        public static PolarCoordinate FromCartesian(Vector2 cartesian)
        {
            float angle = (float)Math.Atan2(cartesian.Y, cartesian.X);
            float distance = cartesian.Length();

            return new PolarCoordinate(angle, distance);
        }
    }
}
