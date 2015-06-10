// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DVector2.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the FloatEventArgs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

/*
Copyright (c) 2006 - 2008 The Open Toolkit library.

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

namespace GUI4UFramework.Graphics
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents a 2D vector using two single-precision floating-point numbers.
    /// </summary>
    /// <remarks>
    /// The DVector2 structure is suitable for inter-operation with unmanaged code requiring two consecutive floats.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct DVector2 : IEquatable<DVector2>
    {
        /// <summary>The x component of the vector.</summary>
        private float positionX;

        /// <summary>The y component of the vector.</summary>
        private float positionY;

        /// <summary>
        /// Initializes a new instance of the <see cref="DVector2"/> struct. 
        /// </summary>
        /// <param name="value">
        /// The value that will initialize this instance.
        /// </param>
        public DVector2(float value)
        {
            this.positionX = value;
            this.positionY = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DVector2"/> struct. 
        /// </summary>
        /// <param name="vector">
        /// The DVector2 to copy components from.
        /// </param>
        public DVector2(DVector2 vector)
        {
            this.positionX = vector.X;
            this.positionY = vector.Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DVector2"/> struct. 
        /// </summary>
        /// <param name="positionX">
        /// The x coordinate of the net DVector2.
        /// </param>
        /// <param name="positionY">
        /// The y coordinate of the net DVector2.
        /// </param>
        public DVector2(float positionX, float positionY) : this()
        {
            this.positionX = positionX;
            this.positionY = positionY;
        }

        /// <summary>Gets or sets the X component of the DVector2.</summary>
        /// <value>The x component.</value>
        public float X
        {
            get { return this.positionX; }
            set { this.positionX = value; }
        }

        /// <summary>Gets or sets the Y component of the DVector2.</summary>
        /// <value>The y component.</value>
        public float Y
        {
            get { return this.positionY; }
            set { this.positionY = value; }
        }

        /// <summary>
        /// Gets the length (magnitude) of the vector.
        /// </summary>
        /// <value>
        /// The length / magnitude.
        /// </value>
        public float Length
        {
            get
            {
                return (float)Math.Sqrt(
                                        (this.positionX * this.positionX) + 
                                        (this.positionY * this.positionY));
            }
        }

        /// <summary>
        /// Gets the square of the vector length (magnitude).
        /// </summary>
        /// <remarks>
        /// This property avoids the costly square root operation required by the Length property. This makes it more suitable for comparisons.
        /// </remarks>
        /// <value>
        /// The length squared.
        /// </value>
        public float LengthSquared
        {
            get
            {
                return (this.X * this.X) + (this.Y * this.Y);
            }
        }

        /// <summary>
        /// Gets the perpendicular vector on the right side of this vector.
        /// </summary>
        /// <value>
        /// The perpendicular right.
        /// </value>
        public DVector2 PerpendicularRight
        {
            get
            {
                return new DVector2(+this.Y, -this.X);
            }
        }

        /// <summary>
        /// Gets the perpendicular vector on the left side of this vector.
        /// </summary>
        /// <value>
        /// The perpendicular left.
        /// </value>
        public DVector2 PerpendicularLeft
        {
            get
            {
                return new DVector2(-this.Y, +this.X);
            }
        }

        /// <summary>
        /// Scales the DVector2 to unit length.
        /// </summary>
        public void Normalize()
        {
            var scale = 1.0f / this.Length;
            this.X *= scale;
            this.Y *= scale;
        }

        /// <summary>
        /// Negates the specified vector2.
        /// </summary>
        /// <param name="vector2">The vector2.</param>
        public void Negate(DVector2 vector2)
        {
            this = this - vector2;
        }

        /// <summary>
        /// Defines a unit-length DVector2 that points towards the X-axis.
        /// </summary>
        public static readonly DVector2 UnitX = new DVector2(1, 0);

        /// <summary>
        /// Defines a unit-length DVector2 that points towards the Y-axis.
        /// </summary>
        public static readonly DVector2 UnitY = new DVector2(0, 1);

        /// <summary>
        /// Defines a zero-length DVector2.
        /// </summary>
        public static readonly DVector2 Zero = new DVector2(0, 0);

        /// <summary>
        /// Defines an instance with all components set to 1.
        /// </summary>
        public static readonly DVector2 One = new DVector2(1, 1);

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="vectorA">
        /// Left operand.
        /// </param>
        /// <param name="vectorB">
        /// Right operand.
        /// </param>
        /// <returns>
        /// The <see cref="DVector2"/>.
        /// </returns>
        public static DVector2 Add(DVector2 vectorA, DVector2 vectorB)
        {
            return new DVector2(vectorA.X + vectorB.X, vectorA.Y + vectorB.Y);
        }

        /// <summary>
        /// Subtract one Vector from another.
        /// </summary>
        /// <param name="vectorA">First operand.</param>
        /// <param name="vectorB">Second operand.</param>
        /// <returns>Result of subtraction.</returns>
        public static DVector2 Subtract(DVector2 vectorA, DVector2 vectorB)
        {
            DVector2 result;
            result = new DVector2(vectorA.X - vectorB.X, vectorA.Y - vectorB.Y);
            return result;
        }

        /// <summary>
        /// Multiplies a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static DVector2 Multiply(DVector2 vector, float scale)
        {
            DVector2 result;
            result = new DVector2(vector.X * scale, vector.Y * scale);
            return result;
        }

        /// <summary>
        /// Multiplies a vector by the components a vector (scale).
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static DVector2 Multiply(DVector2 vector, DVector2 scale)
        {
            return new DVector2(vector.X * scale.X, vector.Y * scale.Y);
        }

        /// <summary>
        /// Divides a vector by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static DVector2 Divide(DVector2 vector, float scale)
        {
            return Multiply(vector, 1 / scale);
        }

        /// <summary>
        /// Divides a vector by the components of a vector (scale).
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the operation.</returns>
        public static DVector2 Divide(DVector2 vector, DVector2 scale)
        {
            return new DVector2(vector.X / scale.X, vector.Y / scale.Y);
        }

        /// <summary>Calculate the component-wise minimum of two vectors.</summary>
        /// <param name="vectorA">First operand.</param>
        /// <param name="vectorB">Second operand.</param>
        /// <returns>The component-wise minimum.</returns>
        public static DVector2 ComponentMin(DVector2 vectorA, DVector2 vectorB)
        {
            vectorA.X = vectorA.X < vectorB.X ? vectorA.X : vectorB.X;
            vectorA.Y = vectorA.Y < vectorB.Y ? vectorA.Y : vectorB.Y;
            return vectorA;
        }

        /// <summary>Calculate the component-wise maximum of two vectors.</summary>
        /// <param name="vectorA">First operand.</param>
        /// <param name="vectorB">Second operand.</param>
        /// <returns>The component-wise maximum.</returns>
        public static DVector2 ComponentMax(DVector2 vectorA, DVector2 vectorB)
        {
            vectorA.X = vectorA.X > vectorB.X ? vectorA.X : vectorB.X;
            vectorA.Y = vectorA.Y > vectorB.Y ? vectorA.Y : vectorB.Y;
            return vectorA;
        }

        /// <summary>Returns the Vector3 with the minimum magnitude.</summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>The minimum Vector3.</returns>
        public static DVector2 Min(DVector2 left, DVector2 right)
        {
            return left.LengthSquared < right.LengthSquared ? left : right;
        }

        /// <summary>Returns the Vector3 with the minimum magnitude.</summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>The minimum Vector3.</returns>
        public static DVector2 Max(DVector2 left, DVector2 right)
        {
            return left.LengthSquared >= right.LengthSquared ? left : right;
        }

        /// <summary>Clamp a vector to the given minimum and maximum vectors.</summary>
        /// <param name="vectorToClamp">Input vector.</param>
        /// <param name="minimum">Minimum vector.</param>
        /// <param name="maximum">Maximum vector.</param>
        /// <returns>The clamped vector.</returns>
        public static DVector2 Clamp(DVector2 vectorToClamp, DVector2 minimum, DVector2 maximum)
        {
            vectorToClamp.X = vectorToClamp.X < minimum.X ? minimum.X : vectorToClamp.X > maximum.X ? maximum.X : vectorToClamp.X;
            vectorToClamp.Y = vectorToClamp.Y < minimum.Y ? minimum.Y : vectorToClamp.Y > maximum.Y ? maximum.Y : vectorToClamp.Y;
            return vectorToClamp;
        }

        /// <summary>Scale a vector to unit length.</summary>
        /// <param name="vectorToNormalize">The input vector.</param>
        /// <returns>The normalized vector.</returns>
        public static DVector2 Normalize(DVector2 vectorToNormalize)
        {
            var scale = 1.0f / vectorToNormalize.Length;
            vectorToNormalize.X *= scale;
            vectorToNormalize.Y *= scale;
            return vectorToNormalize;
        }

        /// <summary>Calculate the dot (scalar) product of two vectors.</summary>
        /// <param name="left">First operand.</param>
        /// <param name="right">Second operand.</param>
        /// <returns>The dot product of the two inputs.</returns>
        public static float Dot(DVector2 left, DVector2 right)
        {
            return (left.X * right.X) + (left.Y * right.Y);
        }

        /// <summary>Returns a new Vector that is the linear blend of the 2 given Vectors.</summary>
        /// <param name="vectorA">First input vector.</param>
        /// <param name="vectorB">Second input vector.</param>
        /// <param name="blendFactor">The blend factor. a when blend=0, b when blend=1.</param>
        /// <returns>a when blend=0, b when blend=1, and a linear combination otherwise.</returns>
        public static DVector2 Lerp(DVector2 vectorA, DVector2 vectorB, float blendFactor)
        {
            vectorA.X = (blendFactor * (vectorB.X - vectorA.X)) + vectorA.X;
            vectorA.Y = (blendFactor * (vectorB.Y - vectorA.Y)) + vectorA.Y;
            return vectorA;
        }

        /// <summary>
        /// Adds the specified instances.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Result of addition.</returns>
        public static DVector2 operator +(DVector2 left, DVector2 right)
        {
            left.X += right.X;
            left.Y += right.Y;
            return left;
        }

        /// <summary>
        /// Subtracts the specified instances.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Result of subtraction.</returns>
        public static DVector2 operator -(DVector2 left, DVector2 right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            return left;
        }

        /// <summary>
        /// Negates the specified instance.
        /// </summary>
        /// <param name="vector">The Operand.</param>
        /// <returns>Result of negation.</returns>
        public static DVector2 operator -(DVector2 vector)
        {
            vector.X = -vector.X;
            vector.Y = -vector.Y;
            return vector;
        }

        /// <summary>
        /// Multiplies the specified instance by a scalar.
        /// </summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of multiplication.</returns>
        public static DVector2 operator *(DVector2 vector, float scale)
        {
            vector.X *= scale;
            vector.Y *= scale;
            return vector;
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static DVector2 operator *(DVector2 left, DVector2 right)
        {
            return new DVector2(left.X * right.X, left.Y * right.Y);
        }

        /// <summary>
        /// Multiplies the specified instance by a scalar.
        /// </summary>
        /// <param name="scale">Left operand.</param>
        /// <param name="vector">Right operand.</param>
        /// <returns>Result of multiplication.</returns>
        public static DVector2 operator *(float scale, DVector2 vector)
        {
            vector.X *= scale;
            vector.Y *= scale;
            return vector;
        }

        /// <summary>Divides the specified instance by a scalar.</summary>
        /// <param name="vector">Left operand.</param>
        /// <param name="scale">Right operand.</param>
        /// <returns>Result of the division.</returns>
        public static DVector2 operator /(DVector2 vector, float scale)
        {
            var divide = 1.0f / scale;
            vector.X *= divide;
            vector.Y *= divide;
            return vector;
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static DVector2 operator /(DVector2 left, DVector2 right)
        {
            return new DVector2(left.X / right.X, left.Y / right.Y);
        }

        /// <summary>
        /// Compares the specified instances for equality.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>True if both instances are equal; false otherwise.</returns>
        public static bool operator ==(DVector2 left, DVector2 right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares the specified instances for inequality.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>True if both instances are not equal; false otherwise.</returns>
        public static bool operator !=(DVector2 left, DVector2 right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Returns a System.String that represents the current DVector2.
        /// </summary>
        /// <returns>Representation of this class.</returns>
        public override string ToString()
        {
            return string.Format("({0}, {1})", this.positionX, this.positionY);
        }

        /// <summary>Indicates whether the current vector is equal to another vector.</summary>
        /// <param name="other">A vector to compare with this vector.</param>
        /// <returns>true if the current vector is equal to the vector parameter; otherwise, false.</returns>
        public bool Equals(DVector2 other)
        {
            const float Tolerance = 0.001f;

            return
                Math.Abs(this.positionX - other.X) < Tolerance &&
                Math.Abs(this.positionY - other.Y) < Tolerance;
        }

        /// <summary>
        /// Returns the hash-code for this instance.
        /// </summary>
        /// <returns>A integer containing the unique hash-code for this instance.</returns>
        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode();
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>True if the instances are equal; false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is DVector2))
            {
                return false;
            }

            return this.Equals((DVector2)obj);
        }
    }
}