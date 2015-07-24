// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuItemText.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   :) lochinvar13@livecoding.tv
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Menu
{
    using GUI4UControls.Text;
    using GUI4UFramework.Management;

    /// <summary>
    /// Is a menu item that only shows some text.
    /// </summary>
    public class MenuItemText : MenuItemBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemText"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public MenuItemText(string name) : base(name)
        {
        }

        /// <summary>
        /// Gets or sets the text control that is shows text on this control.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        protected Label Text { get; set; }

        /// <summary>
        /// Gets or sets the text that is shown on this control.
        /// </summary>
        /// <value>
        /// The configuration text.
        /// </value>
        public string ConfigText { get; set; }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Config.Changed)
            {
                this.Text.Config.Visible = this.Config.Visible;
            }
        }

        /// <summary>
        /// Will redraw myself , and set the children values correctly (when collapsed/expanded)
        /// </summary>
        protected override void Redraw()
        {
        }
    }
}