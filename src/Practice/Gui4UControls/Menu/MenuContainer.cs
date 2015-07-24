// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuContainer.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   this is the container for menu-bar and the vertical-menu
//   A container contains the items that will be shown or not
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Menu
{
    using System;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// This is the container for menu-bar and the vertical-menu
    /// A container contains the items that will be shown or not
    /// </summary>
    public abstract class MenuContainer : Control
    {
        /// <summary>
        /// Whether this control is expanded or not.
        /// </summary>
        private bool configExpanded;

        /// <summary>
        /// Whether this container is always expanded.
        /// </summary>
        private bool configAlwaysExpanded;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuContainer"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected MenuContainer(string name) : base(name)
        {
            this.ConfigExpanded = true;
        }

        /// <summary>
        /// Gets or sets the parent button that will toggle expansion.
        /// </summary>
        /// <value>
        /// The parent button.
        /// </value>
        public MenuItemButton ParentButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it will show all children in the container.
        /// When set to false, it will not.
        /// </summary>
        /// <value>
        /// <c>true</c> if [configuration expanded]; otherwise, <c>false</c>.
        /// </value>
        public bool ConfigExpanded
        {
            get
            {
                return this.configExpanded;
            }

            set
            {
                this.configExpanded = value;
                this.MustRedraw = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this control is always expanded.
        /// </summary>
        /// <value>
        /// <c>true</c> if [configuration always expanded]; otherwise, <c>false</c>.
        /// </value>
        public bool ConfigAlwaysExpanded
        {
            get
            {
                return this.configAlwaysExpanded;
            }

            set
            {
                this.configAlwaysExpanded = value;
                this.MustRedraw = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether we are debugging the layout.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [debug layout]; otherwise, <c>false</c>.
        /// </value>
        public bool DebugLayout
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether we must redraw during Update().
        /// </summary>
        /// <value>
        ///   <c>true</c> if [must redraw]; otherwise, <c>false</c>.
        /// </value>
        public bool MustRedraw
        {
            get;
            set;
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
            // **** do the basic stuff
            base.LoadContent();

            // set the visual state using the current configuration
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();

            // make the background
            this.CurrentTextureName = Manager.ImageCompositor.CreateRectangleTexture(
                                                    this.Name,
                                                    (int)State.Width,
                                                    (int)State.Height,
                                                    0,
                                                    Theme.ContainerFillColor,
                                                    Theme.BorderColor);

            // force me to do a "redraw check and fix" when i am in the Update()-function
            this.MustRedraw = true;
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

            if (this.MustRedraw)
            {
                this.MustRedraw = false;
                this.Redraw();
            }
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
        /// Redraws this instance during Update()
        /// </summary>
        protected abstract void Redraw();

        /// <summary>
        /// Called when the expand/collapse button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected abstract void OnMouseClicked(object sender, EventArgs e);

        /// <summary>
        /// Adds a group menu item.
        /// </summary>
        /// <param name="name">The name for the instance.</param>
        /// <param name="text">The text to show on the instance.</param>
        /// <returns>The group menu item.</returns>
        public MenuContainer AddGroup(string name, string text)
        {
            // create a sub menu that will be shown when this button is pressed, users can add items to this menu
            var menuVertical = new MenuBarVertical(name + "-Menu")
            {
                ConfigExpanded = false
            };

            // create the button
            var menuBarButton = new MenuItemButton(name + "-Button")
            {
                ConfigText = text, 
                Tag = menuVertical
            };
            menuBarButton.MouseClicked += this.OnMouseClicked;
            menuBarButton.AddControl(menuVertical);
            menuVertical.ParentButton = menuBarButton;

            // add that button to me
            this.AddControl(menuBarButton);
            this.MustRedraw = true;

            return menuVertical;
        }

        /// <summary>
        /// Adds a button menu item.
        /// </summary>
        /// <param name="name">The name for the instance.</param>
        /// <param name="text">The text to show on the instance.</param>
        /// <returns>The button menu item.</returns>
        public MenuItemButton AddButton(string name, string text)
        {
            var item = new MenuItemButton(name)
                           {
                               ConfigText = text
                           };

            this.AddControl(item);
            this.MustRedraw = true;

            return item;
        }

        /// <summary>
        /// Adds a separator menu item.
        /// </summary>
        /// <param name="name">The name for the instance.</param>
        /// <returns>The separator menu item.</returns>
        public MenuItemSeparator AddSeparator(string name)
        {
            var item = new MenuItemSeparator(name);

            this.AddControl(item);
            this.MustRedraw = true;

            return item;
        }

        /// <summary>
        /// Adds a text menu item.
        /// </summary>
        /// <param name="name">The name for the instance.</param>
        /// <param name="text">The text to show on the instance.</param>
        /// <returns>The text menu item.</returns>
        public MenuItemText AddMenuItemText(string name, string text)
        {
            var item = new MenuItemText(name)
                           {
                               ConfigText = text
                           };

            this.AddControl(item);
            this.MustRedraw = true;

            return item;
        }
    }
}