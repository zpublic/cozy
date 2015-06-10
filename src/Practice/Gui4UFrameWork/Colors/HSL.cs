// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HSL.cs" company="Jarno Burger">
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
    using System.Diagnostics.CodeAnalysis;

    /// <summary>A representation of Color using the HSL-model.. Using double H, double S and double L. </summary>
    public sealed class HSL
    {
        /// <summary>The hue component of the color.</summary>
        private double hue;

        /// <summary>The saturation component of the color.</summary>
        private double saturation;

        /// <summary>The lighting component of the color.</summary>
        private double lighting;

        /// <summary>
        /// Initializes a new instance of the <see cref="HSL"/> class.
        /// </summary>
        public HSL()
        {
            this.hue = 0;
            this.saturation = 0;
            this.lighting = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HSL"/> class.
        /// </summary>
        /// <param name="hue">The hue component.</param>
        /// <param name="lighting">The lighting component.</param>
        /// <param name="saturation">The saturation component.</param>
        public HSL(double hue, double lighting, double saturation)
        {
            this.hue = hue;
            this.lighting = lighting;
            this.saturation = saturation;
        }

        /// <summary>Gets or sets the Hue component of the color.</summary>
        /// <value>Hue.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1632:DocumentationTextMustMeetMinimumCharacterLength", Justification = "Reviewed. Suppression is OK here."),
        SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed. Suppression is OK here.")]
        public double H
        {
            get
            {
                return this.hue;
            }

            set
            {
                this.hue = value;
                this.hue = this.hue > 1 ? 1 : this.hue < 0 ? 0 : this.hue;
            }
        }

        /// <summary>Gets or sets the Saturation component of the color.</summary>
        /// <value>Saturation.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed. Suppression is OK here.")]
        public double S
        {
            get
            {
                return this.saturation;
            }

            set
            {
                this.saturation = value;
                this.saturation = this.saturation > 1 ? 1 : this.saturation < 0 ? 0 : this.saturation;
            }
        }

        /// <summary>Gets or sets the lightness component of the color.</summary>
        /// <value>Lightness.</value>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed. Suppression is OK here.")]
        public double L
        {
            get
            {
                return this.lighting;
            }

            set
            {
                this.lighting = value;
                this.lighting = this.lighting > 1 ? 1 : this.lighting < 0 ? 0 : this.lighting;
            }
        }
    }
}