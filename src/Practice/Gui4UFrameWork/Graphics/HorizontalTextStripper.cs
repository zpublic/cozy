// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HorizontalTextStripper.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   This creates the visible text in a control,
//   by looking at the font size, the original text,
//   and the size of where this text needs to fit in.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Graphics
{
    using System.Diagnostics;

    using GUI4UFramework.Management;

    /// <summary>
    /// This creates the visible text in a control, 
    /// by looking at the font size, the original text, 
    /// and the size of where this text needs to fit in.
    /// </summary>
    public class HorizontalTextStripper
    {
        /// <summary>The alignment of the text left,center,right.</summary>
        private HorizontalAlignment alignment;

        /// <summary>The image compositor is the object that helps out with drawing stuff.</summary>
        private readonly ImageCompositor imageCompositor;

        /// <summary>The font name.</summary>
        private string fontName;

        /// <summary>
        /// Initializes a new instance of the <see cref="HorizontalTextStripper"/> class.
        /// </summary>
        /// <param name="imageCompositor">The image compositor.</param>
        public HorizontalTextStripper(ImageCompositor imageCompositor)
        {
            this.imageCompositor = imageCompositor;
        }

        /// <summary>
        /// Gets or sets the alignment. Left,center,right.
        /// </summary>
        /// <value>
        /// The alignment.
        /// </value>
        public HorizontalAlignment Alignment
        {
            get
            {
                return this.alignment;
            }

            set
            {
                this.alignment = value;
            }
        }

        /// <summary>Gets or sets the name of the font.</summary>
        /// <value>The font name.</value>
        public string FontName
        {
            get
            {
                return this.fontName;
            }

            set
            {
                this.fontName = value;
            }
        }

        /// <summary>
        /// Gets the index for the first character shown.
        /// </summary>
        /// <value>
        /// The index for the first character shown.
        /// </value>
        public int FirstCharacterIndexShown { get; private set; }

        /// <summary>
        /// Gets the index for the last character shown.
        /// </summary>
        /// <value>
        /// The index for the last character shown.
        /// </value>
        public int LastCharacterIndexShown { get; private set; }

        /// <summary>Calculates the visible string.</summary>
        /// <param name="originalText">The original text.</param>
        /// <param name="textControlWidth">Width of the text control.</param>
        /// <returns>a string that contains a clipped version of the total string.</returns>
        public string CalculateVisibleString(string originalText, float textControlWidth)
        {
            // validation
            if (string.IsNullOrEmpty(originalText))
            {
                Debug.WriteLine("The text to strip was null/empty , nothing to strip.");
                this.FirstCharacterIndexShown = 0;
                this.LastCharacterIndexShown = 0;
                return originalText;
            }

            // does the text fit in anyways ?
            var totalTextSize = this.imageCompositor.ReadSizeString(this.fontName, originalText);
            if (totalTextSize.X < textControlWidth)
            {
                // yes it totally fits , nothing to do , so return the original text
                this.FirstCharacterIndexShown = 0;
                this.LastCharacterIndexShown = originalText.Length;
                return originalText;
            }

            // it did not fit , so we have to go and start clipping
            var visibleString = string.Empty;
            switch (this.alignment)
            {
                case HorizontalAlignment.Left:
                    visibleString = this.ClipToAlignmentLeft(originalText, textControlWidth);
                    break;

                case HorizontalAlignment.Right:
                    visibleString = this.ClipToAlignmentRight(originalText, textControlWidth);
                    break;

                case HorizontalAlignment.Center:
                    visibleString = this.ClipToAlignmentCenter(originalText, textControlWidth);
                    break;
            }

            return visibleString;
        }

        /// <summary>We align to the left, so all characters out of boundary on the right have to be removed.</summary>
        /// <param name="originalText">The original text to align.</param>
        /// <param name="textControlWidth">The width of where the text must fit in.</param>
        /// <returns>clipped text without the right-side stuff.</returns>
        private string ClipToAlignmentLeft(string originalText, float textControlWidth)
        {
            var visibleString = originalText;

            // strip letters from the end until no more letters are left
            while (visibleString.Length >= 0)
            {
                // strip a letter at the end
                visibleString = RemoveLastChar(visibleString);

                // check if we are now smaller then the textControl
                var newSize = this.imageCompositor.ReadSizeString(this.fontName, visibleString);
                if (newSize.X < textControlWidth)
                {
                    // we are ! so we return this visibleString
                    return visibleString;
                }
            }

            this.FirstCharacterIndexShown = 0;
            this.LastCharacterIndexShown = visibleString.Length;

            // nothing left,  but still return this empty value
            return visibleString;
        }

        /// <summary>We align to the center, so all character out of boundary on the left and right have to be removed.
        /// First learn ClipToAlignmentLeft and -Right functions to understand this one !.</summary>
        /// <param name="originalText">The original text to align.</param>
        /// <param name="textControlWidth">The width of where the text must fit in.</param>
        /// <returns>text without the extra stuff outside the center.</returns>
        private string ClipToAlignmentCenter(string originalText, float textControlWidth)
        {
            var visibleString = originalText;
            
            // we use a tickTack trick : remove from the right , remove from the left, right, left , right etc
            var tickTack = false;

            var left = 0;
            var right = originalText.Length;

            // strip letters from the beginning until no more letters are left
            while (visibleString.Length >= 0)
            {
                if (tickTack)
                {
                    // tack ! remove from the right !
                    visibleString = RemoveLastChar(visibleString);
                    right = right - 1;
                }
                else
                {
                    // tick ! remove from the left !
                    visibleString = RemoveFirstChar(visibleString);
                    left = left + 1;
                }

                // flip the tick to tack , or tack to tick :).. whoof !
                tickTack = !tickTack;

                // check if we are now smaller then the textControl
                var newSize = this.imageCompositor.ReadSizeString(this.fontName, visibleString);
                if (newSize.X < textControlWidth)
                {
                    // we are ! so we return this visibleString
                    return visibleString;
                }
            }

            this.FirstCharacterIndexShown = left;
            this.LastCharacterIndexShown = right;

            // nothing left,  but still return this empty value
            return visibleString;
        }

        /// <summary>align to the right , so all character out of boundary on the left have to be removed.</summary>
        /// <param name="originalText">The original text to align.</param>
        /// <param name="textControlWidth">The width of where the text must fit in.</param>
        /// <returns>text without the extra stuff on the left.</returns>
        private string ClipToAlignmentRight(string originalText, float textControlWidth)
        {
            var visibleString = originalText;

            // strip letters from the beginning until no more letters are left
            while (visibleString.Length >= 0)
            {
                // strip a letter at the beginning
                visibleString = RemoveFirstChar(visibleString);

                // check if we are now smaller then the textControl
                var newSize = this.imageCompositor.ReadSizeString(this.fontName, visibleString);
                if (newSize.X < textControlWidth)
                {
                    // we are ! so we return this visibleString
                    return visibleString;
                }
            }

            this.FirstCharacterIndexShown = originalText.Length - visibleString.Length;
            this.LastCharacterIndexShown = originalText.Length;

            // nothing left,  but still return this empty value
            return visibleString;
        }

        /// <summary>Utility to removes the last character from a string.</summary>
        /// <param name="originalText">The original text.</param>
        /// <returns>stripped string.</returns>
        private static string RemoveLastChar(string originalText)
        {
            return originalText.Remove(originalText.Length - 1, 1);
        }

        /// <summary>Utility to removes the first character from a string.</summary>
        /// <param name="originalText">The original text.</param>
        /// <returns>stripped string.</returns>
        private static string RemoveFirstChar(string originalText)
        {
            return originalText.Remove(0, 1);
        }
    }
}