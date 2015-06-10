// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuItemBase.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   greets from lochinvar13@livecoding.tv !
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Menu
{
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Is the base class to create menu items.
    /// </summary>
    public abstract class MenuItemBase : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemBase"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected MenuItemBase(string name) : base(name)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether we are debugging the layout.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [debug layout]; otherwise, <c>false</c>.
        /// </value>
        public bool DebugLayout { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we must redraw during Update();
        /// </summary>
        /// <value>
        ///   <c>true</c> if [must redraw]; otherwise, <c>false</c>.
        /// </value>
        public bool MustRedraw { get; set; }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.MustRedraw)
            {
                this.MustRedraw = false;
                this.Redraw();
            }
        }

        /// <summary>
        /// Will redraw myself , and set the children values correctly (when collapsed/expanded)
        /// </summary>
        protected abstract void Redraw();
    }
}

// new way of looking at it
// the HorizontalMenuBar contains its own MenuBarButtons
// a MenuBarButtons opens up a VerticalMenuBar
// a VerticalMenuBar contains its own MenuBarButtons
// a VerticalMenuBar-MenuBarButton can open up even more VerticalMenuBars etc... 