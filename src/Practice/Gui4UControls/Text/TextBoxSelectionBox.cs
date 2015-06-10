// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextBoxSelectionBox.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the TextBoxSelectionBox type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Text
{
    using System.Diagnostics.CodeAnalysis;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Structural;

    /// <summary>
    /// The selection-box that will tell you that you have selected a part of the text
    /// </summary>
    public class TextBoxSelectionBox : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextBoxSelectionBox"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TextBoxSelectionBox(string name) : base(name)
        {
        }

        /// <summary>
        /// Gets or sets the begin index of the selection
        /// </summary>
        /// <value>
        /// The configuration begin.
        /// </value>
        public int ConfigBegin { get; set; }

        /// <summary>
        /// Gets or sets the end index of the selection
        /// </summary>
        /// <value>
        /// The configuration end.
        /// </value>
        public int ConfigEnd { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the text selection is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [configuration active]; otherwise, <c>false</c>.
        /// </value>
        public bool ConfigActive { get; set; }

        /// <summary>
        /// Gets the count in between the first and last character selected.
        /// </summary>
        /// <returns>The number of characters.</returns>
        public int ConfigGetCharacterSelectionLength()
        {
            return this.ConfigEnd - this.ConfigBegin;
        }

        /// <summary>
        /// Recreate the selection texture rectangle to fit the current selection size.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="textBoxSelectionBox">The text box selection box.</param>
        /// <param name="color">The color.</param>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private static void SetSelectionRectangle(Rectangle rectangle, TextBoxSelectionBox textBoxSelectionBox, GUIColor color)
        {
            if (rectangle.Width >= 1 && rectangle.Height >= 1)
            {
                textBoxSelectionBox.CurrentTextureName = textBoxSelectionBox.Manager.ImageCompositor.CreateRectangleTexture(textBoxSelectionBox.Name, (int)rectangle.Width, (int)rectangle.Height, 0, GUIColor.White(), GUIColor.White());
                var size = textBoxSelectionBox.Manager.ImageCompositor.ReadSizeTexture(textBoxSelectionBox.CurrentTextureName);
                var count = (int)size.X * (int)size.Y;

                var colorArray = new GUIColor[count];
                for (var i = 0; i < count; i++)
                {
                    colorArray[i] = color;
                }

                var map = new ColorMap(colorArray, (int)size.X, (int)size.Y);

                textBoxSelectionBox.Manager.ImageCompositor.UpdateTexture(textBoxSelectionBox.CurrentTextureName, map);

                textBoxSelectionBox.ConfigActive = true;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            var s = string.Format(
                                "{0} Config: {1} {2} {3}",
                                base.ToString(),
                                this.ConfigBegin,
                                this.ConfigEnd,
                                this.ConfigActive);

            return base.ToString() + " " + s;
        }
    }
}
