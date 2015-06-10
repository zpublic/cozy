// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GuiColor.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the FloatEventArgs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

/******************************************************************/
/*****                                                        *****/
/*****     Project:           Adobe Color Picker Clone 1      *****/
/*****     Filename:          AdobeColors.cs                  *****/
/*****     Original Author:   Danny Blanchard                 *****/
/*****                        - scrabcakes@gmail.com          *****/
/*****     Updates:	                                          *****/
/*****      3/28/2005 - Initial Version : Danny Blanchard     *****/
/*****                                                        *****/
/******************************************************************/

namespace GUI4UFramework.Colors
{
    /// <summary>
    /// A color represented in R,G,B,A each is a Byte..
    /// </summary>
    public struct GUIColor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GUIColor"/> struct.
        /// </summary>
        /// <param name="red">The red component.</param>
        /// <param name="green">The green component.</param>
        /// <param name="blue">The blue component.</param>
        public GUIColor(byte red, byte green, byte blue) : this()
        {
            this.R = red;
            this.G = green;
            this.B = blue;
            this.A = 255;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GUIColor"/> struct.
        /// </summary>
        /// <param name="red">The red component.</param>
        /// <param name="green">The green component.</param>
        /// <param name="blue">The blue component.</param>
        /// <param name="alpha">The alpha component.</param>
        public GUIColor(byte red, byte green, byte blue, byte alpha)
            : this(red, green, blue)
        {
            this.A = alpha;
        }

        /// <summary>
        /// Create a instance of GUIColor by using the given red green and blue..
        /// </summary>
        /// <param name="red">The red component.</param>
        /// <param name="green">The green component.</param>
        /// <param name="blue">The blue component.</param>
        /// <returns>A new created color, made with specified components.</returns>
        public static GUIColor FromARGB(int red, int green, int blue)
        {
            return new GUIColor((byte)red, (byte)green, (byte)blue);
        }

        /// <summary>Gets or sets the Red component of the color.</summary>
        /// <value>The red component.</value>
        public byte R { get; set; }

        /// <summary>Gets or sets the Green component of the color.</summary>
        /// <value>The green component.</value>
        public byte G { get; set; }

        /// <summary>Gets or sets the Blue component of the color.</summary>
        /// <value>The blue component.</value>
        public byte B { get; set; }

        /// <summary>Gets or sets the Alpha component of the color.</summary>
        /// <value>The alpha component.</value>
        public byte A { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is R,G,B and A are empty (zero).
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmpty
        {
            get
            {
                return this.A == 0 && this.R == 0 && this.G == 0 && this.B == 0;
            }
        }

        /// <summary>Updates the Red component.</summary>
        /// <param name="value">The value.</param>
        public void UpdateR(byte value)
        {
            this.R = value;
        }

        /// <summary>Updates the Green component.</summary>
        /// <param name="value">The value.</param>
        public void UpdateG(byte value)
        {
            this.G = value;
        }

        /// <summary>Updates the Blue component.</summary>
        /// <param name="value">The value.</param>
        public void UpdateB(byte value)
        {
            this.B = value;
        }

        /// <summary>Updates the Alpha component.</summary>
        /// <param name="value">The value.</param>
        public void UpdateA(byte value)
        {
            this.A = value;
        }

        /// <summary>Returns a instance of a Black GUIColor.</summary>
        /// <returns>A black color.</returns>
        public static GUIColor Black()
        {
            return new GUIColor(0, 0, 0);
        }

        /// <summary>Returns a instance of a White GUIColor.</summary>
        /// <returns>A white color.</returns>
        public static GUIColor White()
        {
            return new GUIColor(255, 255, 255);
        }

        /// <summary>Returns a instance of Black Fully Transparent GUIColor.</summary>
        /// <returns>A transparent color.</returns>
        public static GUIColor Transparent()
        {
            return new GUIColor(0, 0, 0, 255);
        }

        /// <summary>Returns a instance of a MidNightBlue GUIColor.</summary>
        /// <returns>A midnight-blue color.</returns>
        public static GUIColor MidnightBlue()
        {
            return new GUIColor(25, 25, 112);
        }

        /// <summary>Returns a instance of a Wheat GUIColor.</summary>
        /// <returns>A wheat color.</returns>
        public static GUIColor Wheat()
        {
            return new GUIColor(245, 222, 179);
        }

        /// <summary>Returns a instance of a Dodger Blue GUIColor.</summary>
        /// <returns>A dodger-blue color.</returns>
        public static GUIColor DodgerBlue()
        {
            return new GUIColor(30, 144, 255);
        }

        /// <summary>Returns a instance of a Gainsboro GUIColor.</summary>
        /// <returns>A gainsboro color.</returns>
        public static GUIColor Gainsboro()
        {
            return new GUIColor(20, 220, 220);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="compareMe">The compare me.</param>
        /// <param name="compareTo">The compare to.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(GUIColor compareMe, GUIColor compareTo)
        {
            // Return true if the fields match:
            return 
                (compareMe.R == compareTo.R) && 
                (compareMe.G == compareTo.G) && 
                (compareMe.B == compareTo.B) && 
                (compareMe.A == compareTo.A);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="compareMe">The compare me.</param>
        /// <param name="compareTo">The compare to.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(GUIColor compareMe, GUIColor compareTo)
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
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is GUIColor && this.Equals((GUIColor)obj);
        }

        /// <summary>Does a Equals check by checking the values of A,R,G and B.</summary>
        /// <param name="other">The other.</param>
        /// <returns>true if all are the same.</returns>
        public bool Equals(GUIColor other)
        {
            return (this.R == other.R) && (this.G == other.G) && (this.B == other.B) && (this.A == other.A);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.R.GetHashCode();
                hashCode = (hashCode * 397) ^ this.G.GetHashCode();
                hashCode = (hashCode * 397) ^ this.B.GetHashCode();
                hashCode = (hashCode * 397) ^ this.A.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("A{0},R{1},G{2},B{3}", this.A, this.R, this.G, this.B);
        }

        /// <summary>Adds the specified left color to the right and returns the result.</summary>
        /// <param name="left">The left item to operate.</param>
        /// <param name="right">The right item to operate.</param>
        /// <returns>Left color added to the Right color.</returns>
        public static GUIColor Add(GUIColor left, GUIColor right)
        {
            return left + right;
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="left">The left item to operate.</param>
        /// <param name="right">The right item to operate.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static GUIColor operator +(GUIColor left, GUIColor right)
        {
            var newClr = new GUIColor
                             {
                                 A = (byte)(left.A + right.A),
                                 R = (byte)(left.R + right.R),
                                 G = (byte)(left.G + right.G),
                                 B = (byte)(left.B + right.B)
                             };
            return newClr;
        }

        /// <summary>
        /// Subtracts the specified left color with the right color and returns the result.
        /// </summary>
        /// <param name="left">The left item to operate.</param>
        /// <param name="right">The right item to operate.</param>
        /// <returns>The result.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public GUIColor Subtract(GUIColor left, GUIColor right)
        {
            return left - right;
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="left">The left item to operate.</param>
        /// <param name="right">The right item to operate.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static GUIColor operator -(GUIColor left, GUIColor right)
        {
            var newClr = new GUIColor
            {
                A = (byte)(left.A - right.A),
                R = (byte)(left.R - right.R),
                G = (byte)(left.G - right.G),
                B = (byte)(left.B - right.B)
            };
            return newClr;
        }

        /// <summary>
        /// Multiplies the specified left color with the right color and returns the result.
        /// </summary>
        /// <param name="left">The left item to operate.</param>
        /// <param name="right">The right item to operate.</param>
        /// <returns>The result.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public GUIColor Multiply(GUIColor left, GUIColor right)
        {
            return left * right;
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="left">The left item to operate.</param>
        /// <param name="right">The right item to operate.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static GUIColor operator *(GUIColor left, GUIColor right)
        {
            var newClr = new GUIColor
            {
                A = (byte)(left.A * right.A),
                R = (byte)(left.R * right.R),
                G = (byte)(left.G * right.G),
                B = (byte)(left.B * right.B)
            };
            return newClr;
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="left">The left item to operate.</param>
        /// <param name="right">The right item to operate.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static GUIColor operator /(GUIColor left, GUIColor right)
        {
            var newClr = new GUIColor
            {
                A = (byte)(left.A / right.A),
                R = (byte)(left.R / right.R),
                G = (byte)(left.G / right.G),
                B = (byte)(left.B / right.B)
            };
            return newClr;
        }

        /// <summary>
        /// Divides the specified left color with the right color , and returns the result.
        /// </summary>
        /// <param name="left">The left item to operate.</param>
        /// <param name="right">The right item to operate.</param>
        /// <returns>The result.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public GUIColor Divide(GUIColor left, GUIColor right)
        {
            return left / right;
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="left">The left item to operate.</param>
        /// <param name="value">The value to operate.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static GUIColor operator /(GUIColor left, float value)
        {
            var newClr = new GUIColor
            {
                A = (byte)(left.A / value),
                R = (byte)(left.R / value),
                G = (byte)(left.G / value),
                B = (byte)(left.B / value)
            };
            return newClr;
        }
    }
}
