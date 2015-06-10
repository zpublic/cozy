// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeySwitches.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Contains the keyboard actions that happened
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Management
{
    /// <summary>Contains the keyboard actions that happened.</summary>
    public class KeySwitches
    {
        /// <summary>There is change in text.</summary>
        private bool changeText;

        /// <summary>There is selection of all.</summary>
        private bool selectAll;

        /// <summary>There is a enter pressed.</summary>
        private bool enterPressed;

        /// <summary>There is a cursor movement to the left.</summary>
        private bool doCursorLeft;

        /// <summary>There is a cursor movement to the right.</summary>
        private bool doCursorRight;

        /// <summary>There is a text insertion.</summary>
        private bool insertText;

        /// <summary>There is a backspace.</summary>
        private bool backspace;

        /// <summary>There is key delete.</summary>
        private bool keyDelete;

        /// <summary>The text to insert.</summary>
        private string textToInsert;

        /// <summary>
        /// Gets or sets a value indicating whether there is [change text].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [change text]; otherwise, <c>false</c>.
        /// </value>
        public bool ChangeText
        {
            get
            {
                return this.changeText;
            }

            set
            {
                this.changeText = value;
                this.Changed = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether there is [[select all].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [select all]; otherwise, <c>false</c>.
        /// </value>
        public bool SelectAll
        {
            get
            {
                return this.selectAll;
            }

            set
            {
                this.selectAll = value;
                this.Changed = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether there is [enter pressed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enter pressed]; otherwise, <c>false</c>.
        /// </value>
        public bool EnterPressed
        {
            get
            {
                return this.enterPressed;
            }

            set
            {
                this.enterPressed = value;
                this.Changed = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether there is [do cursor left].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [do cursor left]; otherwise, <c>false</c>.
        /// </value>
        public bool DoCursorLeft
        {
            get
            {
                return this.doCursorLeft;
            }

            set
            {
                this.doCursorLeft = value;
                this.Changed = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether there is [do cursor right].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [do cursor right]; otherwise, <c>false</c>.
        /// </value>
        public bool DoCursorRight
        {
            get
            {
                return this.doCursorRight;
            }

            set
            {
                this.doCursorRight = value;
                this.Changed = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether there is [insert text].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [insert text]; otherwise, <c>false</c>.
        /// </value>
        public bool InsertText
        {
            get
            {
                return this.insertText;
            }

            set
            {
                this.insertText = value;
                this.Changed = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether there is <see cref="KeySwitches"/> is backspace.
        /// </summary>
        /// <value>
        ///   <c>true</c> if backspace; otherwise, <c>false</c>.
        /// </value>
        public bool Backspace
        {
            get
            {
                return this.backspace;
            }

            set
            {
                this.backspace = value;
                this.Changed = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether there is [key delete].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [key delete]; otherwise, <c>false</c>.
        /// </value>
        public bool KeyDelete
        {
            get
            {
                return this.keyDelete;
            }

            set
            {
                this.keyDelete = value;
                this.Changed = true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="KeySwitches"/> has changed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if changed; otherwise, <c>false</c>.
        /// </value>
        public bool Changed
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the text to insert.
        /// </summary>
        /// <value>
        /// The text to insert.
        /// </value>
        public string TextToInsert
        {
            get
            {
                return this.textToInsert;
            }

            set
            {
                this.textToInsert = value;
                this.Changed = true;
            }
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            this.changeText = false;
            this.selectAll = false;
            this.enterPressed = false;
            this.doCursorLeft = false;
            this.doCursorRight = false;
            this.insertText = false;
            this.backspace = false;
            this.keyDelete = false;
            this.textToInsert = string.Empty;

            this.Changed = false;
        }
    }
}