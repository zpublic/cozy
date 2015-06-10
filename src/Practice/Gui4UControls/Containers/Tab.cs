// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tab.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Tabs are added to the Tab-Control
//   A tab has a button , that is shown on top..
//   A tab has a container , where all the controls should end up.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Containers
{
    /// <summary>
    /// Tabs are added to the Tab-Control
    /// A tab has a button , that is shown on top..
    /// A tab has a container , where all the controls should end up.
    /// </summary>
    public class Tab
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tab"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Tab(string name)
        {
            this.TabName = name;
        }

        /// <summary>
        /// Gets the name of the tab.
        /// </summary>
        /// <value>
        /// The name of the tab.
        /// </value>
        public string TabName { get; private set; }

        /// <summary>
        /// Gets or sets the button control
        /// </summary>
        /// <value>
        /// The button.
        /// </value>
        public TabButton Button { get; set; }

        /// <summary>
        /// Gets or sets the container control.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public TabContainer Container { get; set; }
    }
}
