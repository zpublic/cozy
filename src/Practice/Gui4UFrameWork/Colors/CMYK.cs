// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CMYK.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the CMYK type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Colors
{
    /// <summary>Represents a color using the CMYK color-model.</summary>
    public sealed class CMYK
    {
        /// <summary>The cyan part of the color.</summary>
        private double cyan;

        /// <summary>The magenta part of the color.</summary>
        private double magenta;

        /// <summary>The yellow part of the color.</summary>
        private double yellow;

        /// <summary>The blackness of the color.</summary>
        private double keyBlack;

        /// <summary>
        /// Initializes a new instance of the <see cref="CMYK"/> class.
        /// </summary>
        public CMYK()
        {
            this.cyan = 0;
            this.magenta = 0;
            this.yellow = 0;
            this.keyBlack = 0;
        }

        /// <summary>Gets or sets the cyan part of the color.</summary>
        /// <value>The cyan part of the color.</value>
        public double Cyan
        {
            get
            {
                return this.cyan;
            }

            set
            {
                this.cyan = value;
                this.cyan = this.cyan > 1 ? 1 : this.cyan < 0 ? 0 : this.cyan;
            }
        }

        /// <summary>Gets or sets the magenta part of the color.</summary>
        /// <value>The magenta part of the color.</value>
        public double Magenta
        {
            get
            {
                return this.magenta;
            }

            set
            {
                this.magenta = value;
                this.magenta = this.magenta > 1 ? 1 : this.magenta < 0 ? 0 : this.magenta;
            }
        }

        /// <summary>Gets or sets the yellow part of the color.</summary>
        /// <value>The yellow part of the color.</value>
        public double Yellow
        {
            get
            {
                return this.yellow;
            }

            set
            {
                this.yellow = value;
                this.yellow = this.yellow > 1 ? 1 : this.yellow < 0 ? 0 : this.yellow;
            }
        }

        /// <summary>Gets or sets the blackness of the color.</summary>
        /// <value>The blackness of the color.</value>
        public double KeyBlack
        {
            get
            {
                return this.keyBlack;
            }

            set
            {
                this.keyBlack = value;
                this.keyBlack = this.keyBlack > 1 ? 1 : this.keyBlack < 0 ? 0 : this.keyBlack;
            }
        }
    }
}