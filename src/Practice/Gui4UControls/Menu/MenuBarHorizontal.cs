// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuBarHorizontal.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   This is a menu bar..
//   Its position should be under the title bar.
//   Its parent should be Window
//   The menu-bar has a global edit mode. Its either shown , or not shown.
//   Only one menu-item group is shown. The current branch will close to give way for the new branch.
//   Shows my children horizontally , my children are DMenuBarButtons..
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Menu
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// This is a menu bar.. 
    /// Its position should be under the title bar.
    /// Its parent should be Window
    /// The menu-bar has a global edit mode. Its either shown , or not shown.
    /// Only one menu-item group is shown. The current branch will close to give way for the new branch.
    /// 
    /// Shows my children horizontally , my children are DMenuBarButtons.. 
    /// </summary>
    public class MenuBarHorizontal : MenuContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuBarHorizontal"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public MenuBarHorizontal(string name) : base(name)
        {
            this.WindowModus = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether i will fit my children inside my boundaries or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fit children]; otherwise, <c>false</c>.
        /// </value>
        public bool WindowModus { get; set; }

        /// <summary>
        /// Redraws this instance.
        /// </summary>
        protected override void Redraw()
        {
            if (this.WindowModus == true)
            {
                // get the left top location and the width to fill in the horizontal menu items
                var parentY = Theme.ControlSmallSpacing;
                var parentX = Theme.ControlSmallSpacing * 1.25f;
                var interriourHeight = Config.Height - (2 * Theme.ControlSmallSpacing);

                // take the height and make some room on the top and bottom
                var interriourWidth = Config.Width - (2 * Theme.ControlSmallSpacing);

                var controlWidth = interriourWidth / this.Children.Count;
                for (var i = 0; i < Children.Count; i++)
                {
                    // cast and validate
                    var menuItemBase = Children[i] as MenuItemBase;
                    if (menuItemBase == null)
                    {
                        continue;
                    }

                    // place the horizontal menu-items
                    menuItemBase.Config.PositionX = parentX + (Theme.ControlSmallSpacing * 0.5f);
                    menuItemBase.Config.PositionY = parentY;
                    menuItemBase.Config.Width = controlWidth - Theme.ControlSmallSpacing;
                    menuItemBase.Config.Height = interriourHeight;
                    parentX = parentX + controlWidth;
                }
            }
            else
            {
                // get the left top location
                var parentY = Theme.ControlSmallSpacing;
                var parentX = Theme.ControlSmallSpacing;

                for (var i = 0; i < Children.Count; i++)
                {
                    // cast and validate
                    var menuItemBase = Children[i] as MenuItemBase;
                    if (menuItemBase == null)
                    {
                        continue;
                    }

                    // place the horizontal menu-items
                    menuItemBase.Config.PositionX = parentX;
                    menuItemBase.Config.PositionY = parentY;
                    menuItemBase.Config.Height = Config.Height - (Theme.ControlSmallSpacing * 2);
                    parentX = (int)(parentX + menuItemBase.Config.Width + Theme.ControlSmallSpacing);
                }
            }
        }

        /// <summary>
        /// Called when a button in the menu-bar was [clicked].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnMouseClicked(object sender, EventArgs e)
        {
            var menuBarButton = sender as MenuItemButton;
            if (menuBarButton == null)
            {
                return;
            }

            Debug.WriteLine("Sub-menu button pressed fr " + menuBarButton.Name);

            // not going to do the smart thing yet..
            // first test the basics
            // SwitchActiveTree(menuBarButton);
            // GoIntoActiveMode(menuBarButton);

            // the tag of the button should contain the vertical-menu-bar
            var menuBarVertical = menuBarButton.Tag as MenuBarVertical;
            if (menuBarVertical == null)
            {
                return;
            }

            var expanded = menuBarVertical.ConfigExpanded;

            menuBarVertical.ConfigExpanded = !expanded;
        }
    }
}
