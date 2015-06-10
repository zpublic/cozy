// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DialogDisplayPicker.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the DialogDisplayPicker type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Dialogs
{
    /// <summary>
    /// Shows a dialog that gives the option to pick a display
    /// </summary>
    public class DialogDisplayPicker : DialogBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogDisplayPicker"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public DialogDisplayPicker(string name) : base(name)
        {
            this.Title = "Display picker";
        }
    }
}
