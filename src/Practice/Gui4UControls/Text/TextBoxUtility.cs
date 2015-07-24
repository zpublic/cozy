// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextBoxUtility.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the TextBoxUtility type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Text
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using GUI4UFramework.Graphics;

    /// <summary>
    /// A utility class to help out with the logic of the text-box-control.
    /// </summary>
    public class TextBoxUtility
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TextBoxUtility"/> is in debug mode.
        /// </summary>
        /// <value>
        ///   <c>true</c> if debug; otherwise, <c>false</c>.
        /// </value>
        public bool Debug { get; set; }

        /// <summary>
        /// Select all text in the text box.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        public static void DoSelectAll(TextBox textBox)
        {
#if DEBUG
            if (textBox == null)
            {
                throw new ArgumentNullException("textBox");
            }
#endif

            var selectionBox = textBox.TextBoxSelection;
            var cursor = textBox.TextBoxCursor;

            selectionBox.ConfigBegin = 0;
            selectionBox.ConfigEnd = textBox.Text.Length;
            selectionBox.ConfigActive = true;

            cursor.ConfigIndex = textBox.Text.Length;
            cursor.ConfigShowCursor = true;
            cursor.ConfigBlinking = true;
        }

        /// <summary>Deletes the selection that was made.</summary>
        /// <param name="textBox">The text box.</param>
        /// <exception cref="System.ArgumentNullException">TextBox was null.</exception>
        public static void DeleteSelection(TextBox textBox)
        {
#if DEBUG
            if (textBox == null)
            {
                throw new ArgumentNullException("textBox");
            }
#endif

            var selectionBox = textBox.TextBoxSelection;
            var cursor = textBox.TextBoxCursor;

            var textToModify = textBox.Text;
            textToModify = textToModify.Remove(selectionBox.ConfigBegin, selectionBox.ConfigGetCharacterSelectionLength());

            selectionBox.ConfigBegin = selectionBox.ConfigBegin;
            selectionBox.ConfigEnd = selectionBox.ConfigEnd;
            selectionBox.ConfigActive = false;

            cursor.ConfigIndex = selectionBox.ConfigBegin;
            cursor.ConfigShowCursor = true;
            cursor.ConfigBlinking = true;

            textBox.Text = textToModify;
        }

        /// <summary>
        /// Set the cursor to given location.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <param name="cursorIndex">Index of the cursor.</param>
        /// <exception cref="System.ArgumentNullException">TextBox was null.</exception>
        public void DoCursorLocation(TextBox textBox, int cursorIndex)
        {
#if DEBUG
            if (textBox == null)
            {
                throw new ArgumentNullException("textBox");
            }
#endif

            var selectionBox = textBox.TextBoxSelection;
            var cursor = textBox.TextBoxCursor;

            if (cursorIndex < 0)
            {
                cursorIndex = 0;
            }

            var text = textBox.Text;
            if (string.IsNullOrEmpty(text))
            {
                cursorIndex = 0;
            }
            else if (cursorIndex > textBox.Text.Length)
            {
                cursorIndex = textBox.Text.Length;
            }

            cursor.ConfigIndex = cursorIndex;
            selectionBox.ConfigActive = false;

            this.ValidateAndRepair(textBox);
        }

        /// <summary>
        /// Does the key enter action.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void DoKeyEnter()
        {
            // Make DoKeyEnter work
        }

        /// <summary>
        /// Does the insert text action.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <param name="textToInsert">The text to insert.</param>
        /// <exception cref="System.ArgumentNullException">TextBox can not be null.</exception>
        /// <exception cref="System.InvalidOperationException">The cursor for given text-box is null</exception>
        public void DoInsertText(TextBox textBox, string textToInsert)
        {
            if (string.IsNullOrEmpty(textToInsert))
            {
                // nothing to do.. return.
                return;
            }

#if DEBUG
            if (textBox == null)
            {
                throw new ArgumentNullException("textBox");
            }
#endif

            var cursor = textBox.TextBoxCursor;

#if DEBUG
            if (cursor == null)
            {
                throw new InvalidOperationException("The cursor for given text-box is null");
            }
#endif 

            //// var changed = false;

            // remove text that was selected
            var allSelected = CheckIfAllTextIsSelected(textBox);
            if (allSelected)
            {
                DeleteSelection(textBox);
                //// changed = true;
            }

            // if there is already text, then insert it at the cursor-position
            var cursorIndex = cursor.ConfigIndex;
            var textToModify = textBox.Text;
            if (!string.IsNullOrEmpty(textToModify))
            {
                textToModify = textToModify.Insert(cursorIndex, textToInsert);
                //// changed = true;
            }
            else
            {
                textToModify = textToInsert;
                //// changed = true;
            }

            textBox.Text = textToModify;

            // we must move the cursor because we just inserted some text
            cursor.ConfigIndex = cursor.ConfigIndex + textToInsert.Length;

            this.ValidateAndRepair(textBox);
        }

        /// <summary>
        /// Does the cursor left action.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <exception cref="System.ArgumentNullException">TextBox can not be null.</exception>
        public void DoCursorLeft(TextBox textBox)
        {
#if DEBUG
            if (textBox == null)
            {
                throw new ArgumentNullException("textBox");
            }
#endif

            var cursor = textBox.TextBoxCursor;
            var selectionBox = textBox.TextBoxSelection;

            if (cursor.ConfigIndex <= 0)
            {
                return;
            }

            cursor.ConfigIndex = cursor.ConfigIndex - 1;
            selectionBox.ConfigActive = false;
            this.ValidateAndRepair(textBox);
        }

        /// <summary>
        /// Does the cursor right action.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <exception cref="System.ArgumentNullException">TextBox can not be null.</exception>
        public void DoCursorRight(TextBox textBox)
        {
#if DEBUG
            if (textBox == null)
            {
                throw new ArgumentNullException("textBox");
            }
#endif

            var selection = textBox.TextBoxSelection;
            var cursor = textBox.TextBoxCursor;

            if (cursor.ConfigIndex >= textBox.Text.Length)
            {
                return;
            }

            cursor.ConfigIndex = cursor.ConfigIndex + 1;
            selection.ConfigActive = false;
            this.ValidateAndRepair(textBox);
        }

        /// <summary>
        /// Does the key delete action.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <exception cref="System.ArgumentNullException">TextBox can not be null.</exception>
        public void DoKeyDelete(TextBox textBox)
        {
#if DEBUG
            if (textBox == null)
            {
                throw new ArgumentNullException("textBox");
            }
#endif

            var cursor = textBox.TextBoxCursor;
            var changed = false;

            // if there is no text to look at. then we have nothing to do
            var textboxLength = textBox.Text.Length;
            if (textboxLength <= 0)
            {
                return;
            }

            // remove text that was selected
            var allSelected = CheckIfAllTextIsSelected(textBox);
            if (allSelected)
            {
                DeleteSelection(textBox);
                changed = true;
            }
            else if (cursor.ConfigIndex < textBox.Text.Length)
            {
                // or else remove a character at the right of cursor position
                // if the cursor is already at the end , we can't delete
                // left part is on the left of the cursor
                var leftPart = textBox.Text.Substring(0, cursor.ConfigIndex);

                // the right part is on the right of the cursor , but without the first character
                var rightPartMinusACharacter = textBox.Text.Substring(cursor.ConfigIndex + 1, textBox.Text.Length - (cursor.ConfigIndex + 1));

                // join left and right , resulting in a Delete-action
                textBox.Text = leftPart + rightPartMinusACharacter;

                changed = true;
            }

            if (changed)
            {
                this.ValidateAndRepair(textBox);
            }
        }

        /// <summary>
        /// Does the key backspace action.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <exception cref="System.ArgumentNullException">TextBox can not be null.</exception>
        public void DoKeyBackspace(TextBox textBox)
        {
#if DEBUG
            if (textBox == null)
            {
                throw new ArgumentNullException("textBox");
            }
#endif

            var selectionBox = textBox.TextBoxSelection;
            var cursor = textBox.TextBoxCursor;

            // if there is no text , then do nothing
            if (textBox.Text.Length > 0 == false)
            {
                return;
            }

            // if there is no selection and the cursor is not already at 0
            if (selectionBox.ConfigActive == false && cursor.ConfigIndex > 0)
            {
                var cursorLocation = cursor.ConfigIndex;
                var textToModify = textBox.Text;
                textToModify = textToModify.Substring(0, cursorLocation - 1) + textToModify.Substring(cursorLocation, textToModify.Length - cursorLocation);
                cursor.ConfigIndex = cursorLocation - 1;
                textBox.Text = textToModify;
            }
            else
            {
                DeleteSelection(textBox);
            }

            this.ValidateAndRepair(textBox);
        }

        /// <summary>
        /// Validates the data in the text-box.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        public void ValidateAndRepair(TextBox textBox)
        {
            this.FixEveryConfigInTheirBoundaries(textBox);
            this.MakeVisualDataFromConfig(textBox);
            this.FixConfigByCheckingVisual();
        }

        /// <summary>
        /// Fixes everything in their boundaries.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        private void FixEveryConfigInTheirBoundaries(TextBox textBox)
        {
            var label = textBox.Label;
            var cursor = textBox.TextBoxCursor;
            var selectionBox = textBox.TextBoxSelection;

            // place last-char inside text
            var lastShownCharIsAt = WhereIs(0, label.StateLastCharacterIndexShown, textBox.Text.Length);
            switch (lastShownCharIsAt)
            {
                case Side.Left:
                    label.StateLastCharacterIndexShown = 0;
                    break;
                case Side.Right:
                    label.StateLastCharacterIndexShown = textBox.Text.Length;
                    break;
            }

            // place first-char inside text
            var firstShownCharIsAt = WhereIs(0, label.StateFirstCharacterIndexShown, textBox.Text.Length);
            switch (firstShownCharIsAt)
            {
                case Side.Left:
                    label.StateFirstCharacterIndexShown = 0;
                    break;
                case Side.Right:
                    label.StateFirstCharacterIndexShown = textBox.Text.Length;
                    break;
            }

            // place cursor index inside text
            var cursorIsAt = WhereIs(label.StateFirstCharacterIndexShown, cursor.ConfigIndex, label.StateLastCharacterIndexShown);
            switch (cursorIsAt)
            {
                case Side.Left:
                    cursor.ConfigIndex = label.StateFirstCharacterIndexShown;
                    break;
                case Side.Right:
                    cursor.ConfigIndex = label.StateFirstCharacterIndexShown;
                    break;
            }

            // make sure selection box is inside the boundaries of text
            if (selectionBox.ConfigBegin < 0)
            {
                selectionBox.ConfigBegin = 0;
            }

            if (selectionBox.ConfigEnd > textBox.Text.Length)
            {
                selectionBox.ConfigEnd = selectionBox.ConfigGetCharacterSelectionLength();
            }

            if (this.Debug)
            {
                System.Diagnostics.Debug.WriteLine(label.ToString());
                System.Diagnostics.Debug.WriteLine(cursor.ToString());
                System.Diagnostics.Debug.WriteLine(selectionBox.ToString());
            }
        }

        /// <summary>
        /// Makes visual data from the configuration data.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private void MakeVisualDataFromConfig(TextBox textBox)
        {
            var textControl = textBox.Label;
            var text = textBox.Label;
            var cursor = textBox.TextBoxCursor;

            // populate visual text-data
            text.StateMaximumSize = new DVector2(textControl.StateCalculatedWidth, textControl.StateCalculatedHeight);
            text.State.DrawPosition = textControl.State.DrawPosition;
            text.State.Offset = new DVector2(textControl.StateCalculatedOffsetX, textControl.StateCalculatedOffsetY);
            var firstCharacterShown = text.StateFirstCharacterIndexShown;
            var shownCharacterLength = text.StateShownCharacterCount;
            text.StateTextShown = textBox.Text.Substring(firstCharacterShown, shownCharacterLength);

            // populate visual cursor data
            var partBetweenFirstCharAndCursorLength = cursor.ConfigIndex - firstCharacterShown;
            var partBetweenFirstCharAndCursorText = text.StateTextShown.Substring(0, partBetweenFirstCharAndCursorLength);
            var fontName = textBox.Theme.FontName;
            var sizeOfFirstPart = textControl.Manager.ImageCompositor.ReadSizeString(fontName, partBetweenFirstCharAndCursorText);
            var sizeOfAGiantLetter = textControl.Manager.ImageCompositor.ReadSizeString(fontName, "M");
            cursor.State.DrawPosition = textBox.State.DrawPosition;
            cursor.State.Offset = new DVector2(sizeOfFirstPart.X, 0);
            cursor.State.Width = 1;
            cursor.State.Height = sizeOfAGiantLetter.Y;
        }

        /// <summary>
        /// Fixes the configuration data by checking visual.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private void FixConfigByCheckingVisual()
        {
        }

        /// <summary>
        /// Utility function that tells on which side is the value.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="value">The value.</param>
        /// <param name="right">The right.</param>
        /// <returns>On which side the value is</returns>
        private static Side WhereIs(int left, int value, int right)
        {
            if (value < left)
            {
                return Side.Left;
            }

            if (value > right)
            {
                return Side.Right;
            }

            return Side.Center;
        }

        /// <summary>
        /// Checks if all text is selected.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <returns>True if all text is selected</returns>
        /// <exception cref="System.ArgumentNullException">TextBox can not be null.</exception>
        public static bool CheckIfAllTextIsSelected(TextBox textBox)
        {
#if DEBUG
            if (textBox == null)
            {
                throw new ArgumentNullException("textBox");
            }
#endif

            var selectionControl = textBox.TextBoxSelection;
            if (string.IsNullOrEmpty(textBox.Text) || selectionControl.ConfigBegin != 0 || selectionControl.ConfigBegin != textBox.Text.Length || selectionControl.ConfigActive == false)
            {
                return false;
            }

            return true;
        }
    }
}
