// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuBuilder.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the MenuBuilder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tests
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;

    using GUI4UControls.Buttons;

    using GUI4UFramework.EventArgs;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Structural;

    using Window = GUI4UControls.Containers.Window;

    /// <summary>
    /// Create buttons that open a window
    /// </summary>
    public class MenuBuilder
    {
        /// <summary>
        /// The theme to use in the menu.
        /// </summary>
        private readonly Theme theme;

        /// <summary>
        /// The parent control where the menu items will be placed in.
        /// </summary>
        private readonly Control parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuBuilder"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="parentControl">The control where we are a buttons to.</param>
        public MenuBuilder(NodeManager manager, Control parentControl)
        {
            this.Windows = new Collection<Window>();
            this.ButtonNames = new Collection<string>();
            this.theme = new Theme();
            this.Manager = manager;
            this.parent = parentControl;
        }

        /// <summary>
        /// Gets the manager, to use for editing the menu.
        /// </summary>
        /// <value>
        /// The manager.
        /// </value>
        public NodeManager Manager { get; private set; }

        /// <summary>
        /// Adds a test window to the menu.
        /// </summary>
        /// <param name="window">The window.</param>
        public void AddWindow(Window window)
        {
            this.Windows.Add(window);
        }

        /// <summary>
        /// Gets all the windows that can be opened by the menu.
        /// </summary>
        /// <value>
        /// The tests.
        /// </value>
        public Collection<Window> Windows { get; private set; }

        /// <summary>
        /// Gets all the button names that we added to the menu.
        /// </summary>
        /// <value>
        /// The button names.
        /// </value>
        public Collection<string> ButtonNames { get; private set; }

        /// <summary>
        /// Create buttons in the given control.
        ///  </summary>
        /// <param name="parentControl">The parent control.</param>
        public void CreateButtonsInControl(Control parentControl)
        {
            var y = this.theme.ControlLargeSpacing + (this.theme.ControlHeight * 2);
            foreach (var testWindow in this.Windows)
            {
               // create a button to go and show the window
                var btn = new Button(testWindow.Title)
                {
                    ConfigText = testWindow.Name,
                    Tag = testWindow.GetType().FullName,
                    Config =
                    {
                        PositionY = y, 
                        PositionX = this.theme.ControlLargeSpacing
                    }
                };
                btn.Clicked += this.OnButtonClicked;
                y = y + this.theme.ControlHeight + this.theme.ControlSmallSpacing;

                // add the button to the window
                parentControl.AddControl(btn);
            }
        }

        /// <summary>
        /// Called when a menu button was clicked to open a menu.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="GameTimeEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.InvalidOperationException">Button tag was null</exception>
        private void OnButtonClicked(object sender, GameTimeEventArgs e)
        {
            // get the button that started this event
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            // get the tag out of the button , cause it contains the type of window that i want to show
            var typeName = button.Tag as string;

            if (string.IsNullOrEmpty(typeName))
            {
                throw new InvalidOperationException("Button tag was null");
            }

            var windowType = Type.GetType(typeName);

            // validate that we really have the type of window
            if (windowType == null)
            {
                Debug.WriteLine("Could not extract the type of window i should open.");
                return;
            }

            // create the window , by using the given type
            var shortName = windowType.Name;
            object[] par = { shortName };
            var obj = Activator.CreateInstance(windowType, par);

            // validate that we really have the window
            var window = obj as Window;
            if (window == null)
            {
                Debug.WriteLine("Could not create the given type of window");
                return;
            }

            // show the window
            this.parent.AddControl(window);
            window.Config.PositionX = 100;
            window.Config.PositionY = 100;
            window.CentreToParent();
        }
    }
}
