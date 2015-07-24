// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DialogBase.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the DialogBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Dialogs
{
    using GUI4UControls.Text;
    using Window = GUI4UControls.Containers.Window;

    /// <summary>
    /// The base class for dialog-windows.
    /// </summary>
    public abstract class DialogBase : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogBase"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected DialogBase(string name) : base(name)
        {
            Config.Width = 300;
            Config.Height = 160;
        }

        /// <summary>
        /// Gets or sets the text that could be shown on the dialog.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        protected Label Text { get; set; }

        /// <summary>
        /// Gets or sets the dialog text that can be shown on the Text-Control.
        /// </summary>
        /// <value>
        /// The dialog text.
        /// </value>
        public string DialogText { get; set; }
    }
}
