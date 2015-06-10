// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdobeColors.cs" company="Jarno Burger">
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
    using System;

    /// <summary>
    /// A utility class to help you out with color transforming.
    /// </summary>
    public static class AdobeColors
    {
        /// <summary>
        /// Sets the absolute brightness of a color.</summary>
        /// <param name="colorToChange">Original color.</param>
        /// <param name="brightness">The luminance level to impose.</param>
        /// <returns>an adjusted color.</returns>
        public static GUIColor SetBrightness(GUIColor colorToChange, double brightness)
        {
            var hsl = RGBToHSL(colorToChange);
            hsl.L = brightness;
            return HSLToRGB(hsl);
        }

        /// <summary>
        /// Modifies an existing brightness level. </summary>
        /// <remarks>
        /// To reduce brightness use a number smaller then 1. To increase brightness use a number larger then 1. </remarks>
        /// <param name="colorToModify">The original color.</param>
        /// <param name="brightness">The luminance delta.</param>
        /// <returns>An adjusted color.</returns>
        public static GUIColor ModifyBrightness(GUIColor colorToModify, double brightness)
        {
            var hsl = RGBToHSL(colorToModify);
            hsl.L *= brightness;
            return HSLToRGB(hsl);
        }

        /// <summary>
        /// Sets the absolute saturation level. </summary>
        /// <remarks>Accepted values 0-1.</remarks>
        /// <param name="colorToChange">An original color.</param>
        /// <param name="saturation">The saturation value to impose.</param>
        /// <returns>An adjusted color.</returns>
        public static GUIColor SetSaturation(GUIColor colorToChange, double saturation)
        {
            var hsl = RGBToHSL(colorToChange);
            hsl.S = saturation;
            return HSLToRGB(hsl);
        }

        /// <summary>
        /// Modifies an existing Saturation level. </summary>
        /// <remarks>
        /// To reduce Saturation use a number smaller then 1. To increase Saturation use a number larger then 1. </remarks>
        /// <param name="colorToModify">The original color.</param>
        /// <param name="saturation">The saturation delta.</param>
        /// <returns>An adjusted color.</returns>
        public static GUIColor ModifySaturation(GUIColor colorToModify, double saturation)
        {
            var hsl = RGBToHSL(colorToModify);
            hsl.S *= saturation;
            return HSLToRGB(hsl);
        }

        /// <summary>
        /// Sets the absolute Hue level. </summary>
        /// <remarks>Accepted values 0-1.</remarks>
        /// <param name="color">An original color.</param>
        /// <param name="hue">The Hue value to impose.</param>
        /// <returns>An adjusted color.</returns>
        public static GUIColor SetHue(GUIColor color, double hue)
        {
            var hsl = RGBToHSL(color);
            hsl.H = hue;
            return HSLToRGB(hsl);
        }

        /// <summary>
        /// Modifies an existing Hue level. </summary>
        /// <remarks>
        /// To reduce Hue use a number smaller than 1. To increase Hue use a number larger then 1. </remarks>
        /// <param name="color">The original color.</param>
        /// <param name="hue">The Hue delta.</param>
        /// <returns>An adjusted color.</returns>
        public static GUIColor ModifyHue(GUIColor color, double hue)
        {
            var hsl = RGBToHSL(color);
            hsl.H *= hue;
            return HSLToRGB(hsl);
        }

        /// <summary>
        /// Converts a color from HSL to RGB. </summary>
        /// <remarks>Adapted from the algorithm in Foley and Van-Dam.</remarks>
        /// <param name="colorToConvert">The HSL value.</param>
        /// <returns>A Color structure containing the equivalent RGB values.</returns>
        public static GUIColor HSLToRGB(HSL colorToConvert)
        {
#if DEBUG
            if (colorToConvert == null)
            {
                throw new ArgumentNullException("colorToConvert");
            }
#endif

            // ReSharper disable JoinDeclarationAndInitializer
            int max, mid, min;
            double q;

            // ReSharper restore JoinDeclarationAndInitializer
            max = Round(colorToConvert.L * 255);
            min = Round((1.0 - colorToConvert.S) * (colorToConvert.L / 1.0) * 255);
            q = (double)(max - min) / 255;

            if (colorToConvert.H >= 0 && colorToConvert.H <= (double)1 / 6)
            {
                mid = Round((((colorToConvert.H - 0) * q) * 1530) + min);
                return GUIColor.FromARGB(max, mid, min);
            }

            if (colorToConvert.H <= (double)1 / 3)
            {
                mid = Round((-((colorToConvert.H - ((double)1 / 6)) * q) * 1530) + max);
                return GUIColor.FromARGB(mid, max, min);
            }

            if (colorToConvert.H <= 0.5)
            {
                mid = Round((((colorToConvert.H - ((double)1 / 3)) * q) * 1530) + min);
                return GUIColor.FromARGB(min, max, mid);
            }

            if (colorToConvert.H <= (double)2 / 3)
            {
                mid = Round((-((colorToConvert.H - 0.5) * q) * 1530) + max);
                return GUIColor.FromARGB(min, mid, max);
            }

            if (colorToConvert.H <= (double)5 / 6)
            {
                mid = Round((((colorToConvert.H - ((double)2 / 3)) * q) * 1530) + min);
                return GUIColor.FromARGB(mid, min, max);
            }

            if (colorToConvert.H <= 1.0)
            {
                mid = Round((-((colorToConvert.H - ((double)5 / 6)) * q) * 1530) + max);
                return GUIColor.FromARGB(max, min, mid);
            }

            return GUIColor.FromARGB(0, 0, 0);
        }

        /// <summary>Converts RGB to HSL.</summary>
        /// <remarks>Takes advantage of what is already built in to .NET by using the Color.GetHue, Color.GetSaturation and Color.GetBrightness methods.</remarks>
        /// <param name="colorToConvert">A Color to convert.</param>
        /// <returns>An HSL value.</returns>
        public static HSL RGBToHSL(GUIColor colorToConvert)
        {
            var hsl = new HSL();

            // ReSharper disable JoinDeclarationAndInitializer
            int max, min, diff;
            // ReSharper restore JoinDeclarationAndInitializer

            //	Of our RGB values, assign the highest value to Max, and the Smallest to Min
            if (colorToConvert.R > colorToConvert.G)
            {
                max = colorToConvert.R;
                min = colorToConvert.G;
            }
            else
            {
                max = colorToConvert.G;
                min = colorToConvert.R;
            }

            if (colorToConvert.B > max)
            {
                max = colorToConvert.B;
            }
            else if (colorToConvert.B < min)
            {
                min = colorToConvert.B;
            }

            diff = max - min;

            //	Luminance - a.k.a. Brightness - Adobe photoshop uses the logic that the
            //	site VBspeed regards (regarded) as too primitive = superior decides the 
            //	level of brightness.
            hsl.L = (double)max / 255;

            //	Saturation
            if (max == 0)
            {
                hsl.S = 0; //	Protecting from the impossible operation of division by zero.
            }
            else
            {
                hsl.S = (double)diff / max; //	The logic of Adobe Photoshops is this simple.
            }

            //	Hue		R is situated at the angel of 360 degrees; 
            //			G 120 degrees
            //			B 240 degrees
            double q;
            if (diff == 0)
            {
                q = 0; // Protecting from the impossible operation of division by zero.
            }
            else
            {
                q = (double)60 / diff;
            }

            if (max == colorToConvert.R)
            {
                if (colorToConvert.G < colorToConvert.B)
                {
                    hsl.H = (360 + (q * (colorToConvert.G - colorToConvert.B))) / 360;
                }
                else
                {
                    hsl.H = q * (colorToConvert.G - colorToConvert.B) / 360;
                }
            }
            else if (max == colorToConvert.G)
            {
                hsl.H = (120 + (q * (colorToConvert.B - colorToConvert.R))) / 360;
            }
            else if (max == colorToConvert.B)
            {
                hsl.H = (240 + (q * (colorToConvert.R - colorToConvert.G))) / 360;
            }
            else
            {
                hsl.H = 0.0;
            }

            return hsl;
        }

        /// <summary>Converts RGB to CMYK.</summary>
        /// <param name="colorToConvert">A color to convert.</param>
        /// <returns>A CMYK object.</returns>
        public static CMYK RGBToCMYK(GUIColor colorToConvert)
        // ReSharper restore InconsistentNaming
        {
            var cmyk = new CMYK();
            var low = 1.0;

            cmyk.Cyan = (double)(255 - colorToConvert.R) / 255;
            if (low > cmyk.Cyan)
            {
                low = cmyk.Cyan;
            }

            cmyk.Magenta = (double)(255 - colorToConvert.G) / 255;
            if (low > cmyk.Magenta)
            {
                low = cmyk.Magenta;
            }

            cmyk.Yellow = (double)(255 - colorToConvert.B) / 255;
            if (low > cmyk.Yellow)
            {
                low = cmyk.Yellow;
            }

            if (low > 0.0)
            {
                cmyk.KeyBlack = low;
            }

            return cmyk;
        }

        /// <summary>Converts CMYK to RGB.</summary>
        /// <param name="colorToConvert">A color to convert.</param>
        /// <returns>A Color object.</returns>
        public static GUIColor CMYKToRGB(CMYK colorToConvert)
        {
#if DEBUG
            if (colorToConvert == null)
            {
                throw new ArgumentNullException("colorToConvert");
            }
#endif

            int red, green, blue;
            red = Round(255 - (255 * colorToConvert.Cyan));
            green = Round(255 - (255 * colorToConvert.Magenta));
            blue = Round(255 - (255 * colorToConvert.Yellow));

            return GUIColor.FromARGB(red, green, blue);
        }

        /// <summary>Custom rounding function.</summary>
        /// <param name="val">message to round.</param>
        /// <returns>Rounded value.</returns>
        private static int Round(double val)
        {
            var retVal = (int)val;

            var temp = (int)(val * 100);

            if ((temp % 100) >= 50)
            {
                retVal += 1;
            }

            return retVal;
        }

        /// <summary>
        /// Contrasts the color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The contrasted color.</returns>
        public static GUIColor ContrastColor(GUIColor color)
        {
            int d;

            // Counting the perceptive luminance - human eye favors green color... 
            var ar = 0.299 * color.R;
            var ag = 0.587 * color.G;
            var ab = 0.114 * color.B;
            var a = 1 - ((ar + ag + ab) / 255);

            if (a < 0.5)
            {
                d = 0; // bright colors - black font
            }
            else
            {
                d = 255; // dark colors - white font
            }

            return GUIColor.FromARGB(d, d, d);
        }

        /// <summary>
        /// Sit in between the start and end color by specified percentage.
        /// </summary>
        /// <param name="startColor">The start color.</param>
        /// <param name="endColor">The end color.</param>
        /// <param name="percentage">The percentage.</param>
        /// <returns>The interpolated color.</returns>
        /// <exception cref="System.ArgumentException">value must be between 0 and 1.</exception>
        public static GUIColor Lerp(GUIColor startColor, GUIColor endColor, float percentage)
        {
            if (percentage < 0 || percentage > 1)
            {
                throw new ArgumentException("Value must be between 0 and 1");
            }

            var r = (byte)(((endColor.R - startColor.R) * percentage) + startColor.R);
            var g = (byte)(((endColor.G - startColor.G) * percentage) + startColor.G);
            var b = (byte)(((endColor.B - startColor.B) * percentage) + startColor.B);
            var a = (byte)(((endColor.A - startColor.A) * percentage) + startColor.A);

            return new GUIColor(r, g, b, a);
        }
    }
}
