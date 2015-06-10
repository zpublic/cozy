// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TabControl.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   The tab-control gives you tab switching.
//   It will start out with three tabs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Containers
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// The tab-control gives you tab switching.
    /// It will start out with three tabs.
    /// </summary>
    public class TabControl : Control
    {
        /// <summary>
        /// The tabs that can be shown
        /// </summary>
        private readonly List<Tab> tabs;

        /// <summary>
        /// The current tab shown.
        /// </summary>
        private Tab currentTab;

        /// <summary>
        /// Initializes a new instance of the <see cref="TabControl"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TabControl(string name) : base(name)
        {
            Config.Width = 3 * Theme.ControlWidth;
            Config.Height = 3 * Theme.ControlWidth;

            this.tabs = new List<Tab>();
            this.AddTab("Tab1");
            this.AddTab("Tab2");
            this.AddTab("Tab3");
        }

        /// <summary>
        /// Gets or sets the menu control where the tap buttons are shown.
        /// </summary>
        /// <value>
        /// The menu.
        /// </value>
        protected TabMenu Menu { get; set; }

        /// <summary>
        /// Gets the current tab shown.
        /// </summary>
        /// <value>
        /// The current tab.
        /// </value>
        public Tab CurrentTab
        {
            get
            {
                return this.currentTab;
            }
        }

        /// <summary>
        /// Gets the number of tabs.
        /// </summary>
        /// <value>
        /// The tab count.
        /// </value>
        public int TabCount
        {
            get
            {
                return this.tabs.Count;
            }
        }

        /// <summary>
        /// Called when graphics resources need to be loaded. 
        /// 
        /// Use this for the usage of :
        /// - creation of the internal embedded controls.
        /// - setting of the variables and resources in this control
        /// - to load any game-specific graphics resources
        /// - take over the config width and height and use it into State
        /// - overriding how this item looks like , by settings its texture or theme
        /// 
        /// Call base.LoadContent before you do your override code, this will cause :
        /// - State.SourceRectangle to be reset to the Config.Size 
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            this.Menu = new TabMenu(Name + "-Menu");
            this.AddControl(this.Menu);

            this.RedrawSizePositions();
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();

            this.CurrentTextureName = Manager.ImageCompositor.CreateRectangleTexture(
                this.Name + "-background",
                (int)Config.Width,
                (int)Config.Height,
                1,
                Theme.ContainerFillColor,
                Theme.BorderColor);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();
        }

        /// <summary>
        /// Adds a tab with specified name. And returns it too.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The just created tab.</returns>
        public Tab AddTab(string name)
        {
            var tab = new Tab(name);
            this.tabs.Add(tab);

            return tab;
        }

        /// <summary>
        /// Gets the tab at given index from internal list of tabs
        /// </summary>
        /// <param name="tabIndex">Index of the tab.</param>
        /// <returns>The tab at given index</returns>
        public Tab GetTab(int tabIndex)
        {
            return this.tabs[tabIndex];
        }

        /// <summary>
        /// Gets the tab with given name from internal list of tabs
        /// </summary>
        /// <param name="name">The name f the tab to get.</param>
        /// <returns>The tab with given name.</returns>
        public Tab GetTab(string name)
        {
            for (var index = 0; index < this.tabs.Count; index++)
            {
                var tab = this.tabs[index];
                if (tab.TabName == name)
                {
                    return tab;
                }
            }

            return null;
        }

        /// <summary>
        /// Redraws the size positions.
        /// </summary>
        private void RedrawSizePositions()
        {
            // the width of tab-menu is the width of tab-control
            // the height of tab-menu is 1.2 times the theme height
            this.Menu.Config.PositionX = 0;
            this.Menu.Config.PositionY = 0;
            this.Menu.Config.Width = Config.Width;
            this.Menu.Config.Height = Theme.ControlHeight * 1.2f;

            // tab buttons will be aligned on the left side
            // there will be small spacing in between each tab-button
            // tab buttons will be added to the TabMenu
            for (var index = 0; index < this.tabs.Count; index++)
            {
                var tab = this.tabs[index];
                var button = tab.Button;
                if (button == null)
                {
                    button = new TabButton(Name + "-" + tab.TabName);
                    this.AddControl(button);
                }

                var w = Theme.ControlWidth;
                var h = Theme.ControlHeight;
                button.Config.PositionX = ((index * w) * 0.8f) + (w * 0.1f) - (w * 0.1f);
                button.Config.PositionY = this.Menu.Config.Height - h;
                button.Config.Width = w * 0.8f;
                button.Config.Height = h;
            }

            // tab containers will placed underneath the tab-menu
            // they fill up all the rest space of the tab-control
            for (var index = 0; index < this.tabs.Count; index++)
            {
                var tab = this.tabs[index];
                var container = tab.Container;
                if (container == null)
                {
                    container = new TabContainer(Name + "-" + tab.Container);
                    this.AddControl(container);
                }

                container.Config.PositionX = 0;
                container.Config.PositionY = this.Menu.Config.Height;
                container.Config.Width = this.Config.Width;
                container.Config.Height = this.Config.Height - this.Menu.Config.Height;
            }
        }

        /// <summary>
        /// Pop up the tab at given index location from internal list of tabs
        /// </summary>
        /// <param name="tab">The tab.</param>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private void PopupTab(int tab)
        {
            this.currentTab = this.GetTab(tab);
            this.RedrawSizePositions();
        }
    }
}
