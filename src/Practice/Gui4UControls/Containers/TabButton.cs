// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TabButton.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the TabButton type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Containers
{
    using GUI4UControls.Buttons;

    /// <summary>
    /// The button in the tab-menu .
    /// </summary>
    public class TabButton : ToggleButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabButton"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TabButton(string name) : base(name)
        {
            this.ConfigText = "Tab";
            this.Config.Width = Theme.ControlWidth / 3.0f;
            this.Config.Height = Theme.ControlHeight * 0.8f;
        }
    }
}
