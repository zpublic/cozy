// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DVector3.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the DVector3 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Graphics
{
    using System;

    /// <summary>
    /// Contains a 3D representation of a vector.
    /// </summary>
    public struct DVector3
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DVector3"/> struct.
        /// </summary>
        /// <param name="positionX">The position x.</param>
        /// <param name="positionY">The position y.</param>
        /// <param name="positionZ">The position z.</param>
        public DVector3(float positionX, float positionY, float positionZ) : this()
        {
            this.X = positionX;
            this.Y = positionY;
            this.Z = positionZ;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DVector3"/> struct.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="depth">The depth.</param>
        public DVector3(DVector2 position, float depth) : this()
        {
            this.X = position.X;
            this.Y = position.Y;
            this.Z = depth;
        }

        /// <summary>Gets or sets the x component of the vector.</summary>
        /// <value>The x component.</value>
        public float X { get; set; }

        /// <summary>Gets or sets the y component of the vector.</summary>
        /// <value>The y component.</value>
        public float Y { get; set; }

        /// <summary>Gets or sets the z component of the vector.</summary>
        /// <value>The z component.</value>
        public float Z { get; set; }

        /// <summary>Gets a new vector where X=0 , Y=0 , Z=0.</summary>
        /// <value>The new zero-vector.</value>
        public static DVector3 Zero
        {
            get
            {
                return new DVector3(0, 0, 0);
            }
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="compareMe">The compare me.</param>
        /// <param name="compareTo">The compare to.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(DVector3 compareMe, DVector3 compareTo)
        {
            const float Tolerance = 0.0000001f;
            return Math.Abs(compareMe.X - compareTo.X) < Tolerance && 
                Math.Abs(compareMe.Y - compareTo.Y) < Tolerance &&
                Math.Abs(compareMe.Z - compareTo.Z) < Tolerance;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="compareMe">The compare me.</param>
        /// <param name="compareTo">The compare to.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(DVector3 compareMe, DVector3 compareTo)
        {
            return !(compareMe == compareTo);
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
            if (!(obj is DVector3))
            {
                return false;
            }

            return this.Equals((DVector3)obj);
        }

        /// <summary>
        /// EDetermines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="other">The other vector.</param>
        /// <returns>True when object equals.</returns>
        public bool Equals(DVector2 other)
        {
            const float Tolerance = 0.001f;

            return
                Math.Abs(this.X - other.X) < Tolerance &&
                Math.Abs(this.Y - other.Y) < Tolerance;
        }

        /// <summary>
        /// Returns the hash-code for this instance.
        /// </summary>
        /// <returns>A integer containing the unique hash-code for this instance.</returns>
        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="left">The left side of the operator.</param>
        /// <param name="right">The right side of the operator.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static DVector3 operator +(DVector3 left, DVector3 right)
        {
            return new DVector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="left">The left side of the operator.</param>
        /// <param name="right">The right side of the operator.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static DVector3 operator /(DVector3 left, DVector3 right)
        {
            return new DVector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="left">The left side of the operator.</param>
        /// <param name="right">The right side of the operator.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static DVector3 operator /(DVector3 left, float right)
        {
            return new DVector3(left.X / right, left.Y / right, left.Z / right);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="left">The left side of the operator.</param>
        /// <param name="right">The right side of the operator.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static DVector3 operator *(DVector3 left, DVector3 right)
        {
            return new DVector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="left">The left side of the operator.</param>
        /// <param name="right">The right side of the operator.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static DVector3 operator *(DVector3 left, float right)
        {
            return new DVector3(left.X * right, left.Y * right, left.Z * right);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0},{1},{2}", this.X, this.Y, this.Z);
        }

        /// <summary>
        /// Adds the specified vector3 to me.
        /// </summary>
        /// <param name="vector3">The vector3.</param>
        public void Add(DVector3 vector3)
        {
            this = this + vector3;
        }

        /// <summary>
        /// Divides the specified vector3 to me.
        /// </summary>
        /// <param name="vector3">The vector3.</param>
        public void Divide(DVector3 vector3)
        {
            this = this / vector3;
        }

        /// <summary>
        /// Divides the specified strength trough me.
        /// </summary>
        /// <param name="strength">The strength.</param>
        public void Divide(float strength)
        {
            this = this / strength;
        }

        /// <summary>
        /// Multiplies the specified vector3 with me.
        /// </summary>
        /// <param name="vector3">The vector3.</param>
        public void Multiply(DVector3 vector3)
        {
            this = this * vector3;
        }

        /// <summary>
        /// Multiplies the specified strength with me.
        /// </summary>
        /// <param name="strength">The strength.</param>
        public void Multiply(float strength)
        {
            this = this * strength;
        }
    }
}