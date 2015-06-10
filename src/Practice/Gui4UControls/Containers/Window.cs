// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Window.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Window with sub-controls.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Containers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using GUI4UControls.Menu;

    using GUI4UFramework.EventArgs;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// Window with sub-controls.
    /// </summary>
    public class Window : Control
    {
        /// <summary>
        /// Occurs when gets shown
        /// </summary>
        public event EventHandler OnWindowShow;

        /// <summary>
        /// Occurs when gets hidden
        /// </summary>
        public event EventHandler OnWindowHide;

        /// <summary>
        /// The title of this window
        /// </summary>
        private string title;

        /// <summary>
        /// Initializes a new instance of the <see cref="Window"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Window(string name) : base(name)
        {
            this.Config.IsWindow = true;

            //// Config.UseInteriorClipping = true;
            //// Config.ApplyChildClipping = true;

            this.ChildWindows = new Dictionary<string, Window>();

            this.MenuBarHorizontal = new MenuBarHorizontal(this.Name + "-Menu Bar")
            {
                DebugLayout = false,
                ConfigAlwaysExpanded = true
            };

            this.ShowMenuBar = false;
        }

        /// <summary>
        /// Gets or sets the title bar control.
        /// </summary>
        /// <value>
        /// The title bar.
        /// </value>
        protected WindowTitleBar TitleBar { get; set; }

        /// <summary>
        /// Gets or sets the sizing corner control
        /// </summary>
        /// <value>
        /// The sizing corner.
        /// </value>
        protected WindowSizingCorner SizingCorner { get; set; }

        /// <summary>
        /// Gets or sets the menu bar control
        /// </summary>
        /// <value>
        /// The menu bar horizontal.
        /// </value>
        protected MenuBarHorizontal MenuBarHorizontal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we show a menu bar or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show menu bar]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowMenuBar { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we must redraw the size during Update().
        /// </summary>
        /// <value>
        ///   <c>true</c> if [redraw size control]; otherwise, <c>false</c>.
        /// </value>
        protected bool RedrawSizeControl
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether we must redraw the Title during Update().
        /// </summary>
        /// <value>
        ///   <c>true</c> if [redraw title control]; otherwise, <c>false</c>.
        /// </value>
        protected bool RedrawTitleControl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we must redraw the MenuBar during Update().
        /// </summary>
        /// <value>
        /// <c>true</c> if [redraw menu bar control]; otherwise, <c>false</c>.
        /// </value>
        protected bool RedrawMenuBarControl { get; set; }

        /// <summary>
        /// Gets or sets the title for the window.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.RedrawTitleControl = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Window"/> is shown.
        /// </summary>
        /// <value>
        ///   <c>true</c> if shown; otherwise, <c>false</c>.
        /// </value>
        public bool Shown { get; protected set; }

        /// <summary>
        /// Gets the windows that are children of me..
        /// </summary>
        /// <value>
        /// The child windows.
        /// </value>
        public Dictionary<string, Window> ChildWindows { get; private set; }

        /// <summary>
        /// Gets a value indicating whether we are [Dragging].
        /// </summary>
        /// <value>
        ///   <c>true</c> if dragging; otherwise, <c>false</c>.
        /// </value>
        public bool Dragging { get; private set; }

        /// <summary>
        /// Gets the location where the mouse started the drag. Relative to the parent.
        /// </summary>
        /// <value>
        /// The started drag location.
        /// </value>
        public DVector2 DragStartLocation { get; private set; }

        /// <summary>
        /// Gets the location where we are currently dragging with the mouse. Relative to the parent.
        /// </summary>
        /// <value>
        /// The current drag location.
        /// </value>
        public DVector2 DragCurrentLocation { get; private set; }

        /// <summary>
        /// Gets the location where we ended dragging with the mouse. Relative to the parent.
        /// </summary>
        /// <value>
        /// The drag end location.
        /// </value>
        public DVector2 DragEndLocation { get; private set; }

        /// <summary>
        /// Gets the location where we the window was at the start of dragging the window. Relative to the parent.
        /// </summary>
        /// <value>
        /// The window drag start location.
        /// </value>
        public DVector2 WindowDragStartLocation { get; private set; }

        /// <summary>
        /// Shows the window..
        /// </summary>
        public void ShowWindow()
        {
            this.Shown = true;
            if (this.OnWindowShow != null)
            {
                this.OnWindowShow(this, null);
            }
        }

        /// <summary>
        /// Hides the window.
        /// </summary>
        public void HideWindow()
        {
            this.Shown = false;
            if (this.OnWindowHide != null)
            {
                this.OnWindowHide(this, null);
            }
        }

        /// <summary>
        /// Shows the child window.
        /// </summary>
        /// <param name="formName">Name of the form.</param>
        public void ShowChildWindow(string formName)
        {
            foreach (var childFormPair in this.ChildWindows)
            {
                if (childFormPair.Key.Equals(formName))
                {
                    this.HideWindow();
                    childFormPair.Value.ShowWindow();
                    break;
                }
            }
        }

        /// <summary>
        /// Called when graphics resources need to be loaded.
        /// Use this for the usage of :
        /// - creation of the internal embedded controls.
        /// - setting of the variables and resources in this control
        /// - to load any game-specific graphics resources
        /// - take over the config width and height and use it into State
        /// - overriding how this item looks like , by settings its texture or theme
        /// Call base.LoadContent before you do your override code, this will cause :
        /// - State.SourceRectangle to be reset to the Config.Size
        /// </summary>
        public override void LoadContent()
        {
            // **** do the basic stuff
            base.LoadContent();

            // set the visual state using the current configuration
            this.State.DrawPosition = new DVector2(this.Config.PositionX, this.Config.PositionY);
            this.State.Width = this.Config.Width;
            this.State.Height = this.Config.Height;

            // make the background
            this.CurrentTextureName = this.Manager.ImageCompositor.CreateRectangleTexture(
                                                    this.Name,
                                                    (int)this.State.Width,
                                                    (int)this.State.Height,
                                                    this.Theme.BorderWidth,
                                                    this.Theme.WindowFillColor,
                                                    this.Theme.BorderColor);

            // make the title bar
            this.TitleBar = new WindowTitleBar(this.Name + "-TitleBar")
                                                        {
                                                            Config =
                                                            {
                                                                Width = this.Config.Width,
                                                                Height = this.Theme.ControlHeight,
                                                                PositionX = 0,
                                                                PositionY = 0
                                                            }
                                                        };
            this.TitleBar.CloseButton.Clicked += this.OnCloseClicked;
            this.AddControl(this.TitleBar);
            this.RedrawTitleControl = true;

            // make the sizer
            this.SizingCorner = new WindowSizingCorner(this.Name + "-Sizer")
                                                    {
                                                        Config =
                                                        {
                                                            PositionX = this.Config.Width - 16,
                                                            PositionY = this.Config.Height - 16
                                                        }
                                                    };
            this.AddControl(this.SizingCorner);
            this.RedrawSizeControl = true;

            // make the menu bar
            if (this.ShowMenuBar)
            {
                this.AddControl(this.MenuBarHorizontal);
                this.RedrawMenuBarControl = true;
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // do the basic stuff first
            base.Update(gameTime);

            // ? huh no code to take config into visual-state ?
            // get all the values from current configuration , 
            // and create a visual state that can be drawn from it
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();
            this.UpdateDrawSourceRectangleByConfig();

            // redraw the title if we must
            this.RedrawTitleControl = true;
            if (this.RedrawTitleControl == true)
            {
                this.RedrawTitleControl = false;
                this.TitleBar.ConfigText = this.title;
                this.TitleBar.Config.Width = this.Config.Width;
            }

            // redraw the sizer if we must
            this.RedrawSizeControl = true;
            if (this.RedrawSizeControl == true)
            {
                this.RedrawSizeControl = false;
                this.SizingCorner.Config.PositionX = this.Config.Width - this.SizingCorner.Config.Width;
                this.SizingCorner.Config.PositionY = this.Config.Height - this.SizingCorner.Config.Height;
            }

            // redraw the menu-bar if we must
            this.RedrawMenuBarControl = true;
            if (this.RedrawMenuBarControl == true)
            {
                this.RedrawMenuBarControl = false;
                this.MenuBarHorizontal.WindowModus = true;
                this.MenuBarHorizontal.Config.Width = this.Config.Width - 2;
                this.MenuBarHorizontal.Config.PositionX = 1;
                this.MenuBarHorizontal.Config.PositionY = this.TitleBar.Config.Height;
                this.MenuBarHorizontal.Config.Height = this.Theme.ControlHeight;
            }

            // read dragging
            this.ReadIfDragging();
            this.ReadIfEndDragging();
            if (this.Dragging)
            {
                // find out the movement, compared to from the point where we started dragging
                if (this.Parent != null)
                {
                    this.DragCurrentLocation = this.Parent.PointRelative(this.Manager.InputManager.ReadMouseLocation());
                }
                else
                {
                    this.DragCurrentLocation = this.Manager.InputManager.ReadMouseLocation();
                }

                var delta = this.DragCurrentLocation - this.DragStartLocation;
                var formCurrent = this.WindowDragStartLocation + delta;
                this.Config.PositionX = formCurrent.X;
                this.Config.PositionY = formCurrent.Y;
                // this.Title = string.Format("{0},{1}", this.Config.PositionX, this.Config.PositionY);
                this.UpdateDrawPositionByConfigAndParent();
            }
        }

        /// <summary>
        /// Draw the texture at DrawPosition combined with its offset
        /// </summary>
        public override void DrawMyData()
        {
            if (this.Config.Visible == false)
            {
                return;
            }

            base.DrawMyData();
        }

        /// <summary>
        /// Reads if we are dragging, and react to it if we are.
        /// </summary>
        public void ReadIfDragging()
        {
            if (this.TitleBar == null || !this.TitleBar.StateIsPressed || this.Dragging != false)
            {
                return;
            }

            if (this.Parent != null)
            {
                // remember the mouse location , relative to this control where we start dragging
                this.DragStartLocation = this.Parent.PointRelative(this.Manager.InputManager.ReadMouseLocation());

                // check where the indicator is, relative to this control where we start dragging
                this.WindowDragStartLocation = this.Parent.PointRelative(this.TitleBar.State.DrawPosition); 
            }
            else
            {
                this.DragStartLocation = this.Manager.InputManager.ReadMouseLocation();
                this.WindowDragStartLocation = this.TitleBar.State.DrawPosition;
            }

            this.Dragging = true;

            Debug.WriteLine("Dragging started at location " + this.DragStartLocation + " and form starts at location " + this.WindowDragStartLocation);
        }

        /// <summary>
        /// Reads if we ended dragging and react to it if we are.
        /// </summary>
        public void ReadIfEndDragging()
        {
            var mouseReleased = this.Manager.InputManager.ReadLeftMouseReleased();
            if (!mouseReleased)
            {
                return;
            }

            if (this.Dragging != true)
            {
                return;
            }

            if (this.Parent != null)
            {
                this.DragEndLocation = this.Parent.PointRelative(this.Manager.InputManager.ReadMouseLocation());
            }
            else
            {
                this.DragEndLocation = this.Manager.InputManager.ReadMouseLocation();
            }

            this.Dragging = false;
            Debug.WriteLine("Dragging ended at " + this.DragEndLocation);
        }

        /// <summary>
        /// Called when we clicked the close button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GameTimeEventArgs"/> instance containing the event data.</param>
        private void OnCloseClicked(object sender, GameTimeEventArgs e)
        {
            this.Parent.DestroyMe(this);
        }

        /// <summary>
        /// Creates a default menu to test the menu bar.
        /// </summary>
        public void CreateDefaultMenu()
        {
            this.ShowMenuBar = true;

            var file = this.MenuBarHorizontal.AddGroup("File", "File");
            var edit = this.MenuBarHorizontal.AddGroup("Edit", "Edit");
            var view = this.MenuBarHorizontal.AddGroup("View", "View");
            var project = this.MenuBarHorizontal.AddGroup("Project", "Project");
            var tools = this.MenuBarHorizontal.AddGroup("Tools", "Tools");
            var window = this.MenuBarHorizontal.AddGroup("Window", "Window");
            var help = this.MenuBarHorizontal.AddGroup("Help", "Help");

            //// the file menu
            file.AddButton("New", "New");
            file.AddButton("Open", "Open");
            file.AddSeparator("Seperator1");
            file.AddButton("Close", "Close");

            // the edit menu
            edit.AddButton("Undo", "Undo");
            edit.AddButton("Redo", "Redo");
            edit.AddSeparator("Seperator2");
            edit.AddButton("Cut", "Cut");
            edit.AddButton("Copy", "Cut");
            edit.AddButton("Paste", "Paste");

            // the view menu
            view.AddButton("PropWindow", "Property window");

            // the project menu
            project.AddButton("AddItem", "Add item");
            project.AddButton("ProjectProperties", "Project properties");

            // the tools menu
            tools.AddButton("Options", "Options");

            // window menu
            window.AddButton("CloseAll", "Close all");

            // help menu
            help.AddButton("ViewHelp", "View help");
            help.AddButton("About", "About");

            this.ConnectEventToButtons(this);
        }

        /// <summary>
        /// Utility function to create a default menu.
        /// </summary>
        /// <param name="node">The node.</param>
        private void ConnectEventToButtons(Node node)
        {
            var btn = node as MenuItemButton;
            if (btn != null)
            {
                btn.MouseClicked += this.OnButtonClicked;
            }

            foreach (var child in node.Children)
            {
                this.ConnectEventToButtons(child);
            }
        }

        /// <summary>
        /// Called when a button in the menu bar is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnButtonClicked(object sender, EventArgs e)
        {
            var btn = sender as MenuItemButton;
            if (btn == null)
            {
                return;
            }

            Debug.WriteLine(btn);
        }
    }
}
