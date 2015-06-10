// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuBarVertical.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the MenuBarVertical type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Menu
{
    using System;

    using GUI4UFramework.Colors;

    /// <summary>
    /// Shows a vertical menu like photo-shop does
    /// </summary>
    public class MenuBarVertical : MenuContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuBarVertical"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public MenuBarVertical(string name) : base(name)
        {
            this.ConfigExpanded = true;
        }

        /// <summary>
        /// Draw the texture at DrawPosition combined with its offset
        /// </summary>
        public override void DrawMyData()
        {
            if (!Config.Visible)
            {
                return;
            }

            if (this.DebugLayout)
            {
                Manager.ImageCompositor.Draw(this.CurrentTextureName, this.State, new GUIColor(255, 0, 0));
            }
            else
            {
                Manager.ImageCompositor.Draw(this.CurrentTextureName, this.State, Theme.TintColor);
            }
        }

        /// <summary>
        /// Will redraw myself , and set the children values correctly (when collapsed/expanded)
        /// </summary>
        protected override void Redraw()
        {
            if (this.ConfigExpanded)
            {
                this.DrawAllExpanded();
            }
            else
            {
                this.DrawAllCollapsed();
            }
        }

        /// <summary>
        /// Called when a button was [clicked].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnMouseClicked(object sender, EventArgs e)
        {
            this.ConfigExpanded = !this.ConfigExpanded;
        }

        /// <summary>
        /// Draws all expanded.
        /// </summary>
        private void DrawAllExpanded()
        {
            // place the first child on the right of me at the same height
            // the other children will be underneath me
            const int X = 0;
            var y = this.ParentButton.Config.Height;

            foreach (var child in this.Children)
            {
                // check if the child is a menu-item
                var menuItemBase = child as MenuItemBase;
                if (menuItemBase == null)
                {
                    return;
                }

                // lets draw it
                menuItemBase.Config.Visible = true;
                menuItemBase.Config.PositionX = X;
                menuItemBase.Config.PositionY = y;
                menuItemBase.Config.Width = Config.Width;
                menuItemBase.Config.Height = Theme.ControlHeight * 0.9f;
                y = (int)(y + menuItemBase.Config.Height);
            }
        }

        /// <summary>
        /// Draws all collapsed.
        /// </summary>
        private void DrawAllCollapsed()
        {
            // place the first child on the right of me at the same height
            // the other children will be underneath me
            this.Config.Visible = false;

            foreach (var child in this.Children)
            {
                // check if the child is a menu-item
                var menuItemBase = child as MenuItemBase;
                if (menuItemBase == null)
                {
                    return;
                }

                menuItemBase.Config.Visible = false;
            }
        }
    }
}