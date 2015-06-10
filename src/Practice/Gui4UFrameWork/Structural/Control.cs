// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Control.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   This is the base control, every item extends from this.
//   Has a configuration , and a current state
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Structural
{
    using System.Diagnostics;

    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;

    /// <summary>
    /// This is the base control, every item extends from this.
    /// Has a configuration , and a current state.
    /// </summary>
    public class Control : Node
    {
        /// <summary>The current texture name that is been used as a resource.</summary>
        private string currentTextureName;

        /// <summary>If we must UnLoad and Load when we are in the Update again.</summary>
        private bool mustReload;

        /// <summary>
        /// Initializes a new instance of the <see cref="Control"/> class.
        /// </summary>
        /// <param name="name">The name to u.</param>
        public Control(string name)
        {
            this.Name = name;
            this.State = new DrawState();
            this.Config = new ControlConfig();
            this.Theme = new Theme();
            this.TooltipText = "No toolkit text is set";
            this.DrawMeAndMyChildren = true;
            this.Config.Visible = true;

            this.IsLoaded = false;
            this.mustReload = false;
        }

        /// <summary>Gets the configuration that is set mostly during initialize.</summary>
        /// <value>The configuration. That contains all the information that can be used by the control to determine what to draw (set in this.State).</value>
        public ControlConfig Config { get; private set; }

        /// <summary>Gets the current state of the control.</summary>
        /// <value>The state , that contains the information to know how to draw on-screen.</value>
        public DrawState State { get; private set; }

        /// <summary>Gets or sets the theme for the control.</summary>
        /// <value>Gets or sets the theme , that defines how the control will look.</value>
        public Theme Theme { get; set; }

        /// <summary>Gets or sets the manager that does the heavy lifting of the nodes.</summary>
        /// <value>The manager, the object that can do stuff for controls.</value>
        public NodeManager Manager { get; set; }

        /// <summary>
        /// Gets or sets the name of the current texture used when we are using the standard Draw call.
        /// </summary>
        /// <value>
        /// The name of the current texture.
        /// </value>
        public string CurrentTextureName
        {
            get { return this.currentTextureName; }
            set { this.currentTextureName = value; }
        }

        /// <summary>Used to check if this control has focus.</summary>
        /// <returns>If this control is focused.</returns>
        public bool CheckIsFocused()
        {
            return this.Manager != null && this.Manager.FocusedNode == this;
        }

        /// <summary>
        /// Gets or sets the tool-tip text. This is optional and could be used be a optional tool-tip-control to show tips about this control.
        /// </summary>
        /// <value>
        /// The tool-tip text.
        /// </value>
        public string TooltipText { get; set; }

        /// <summary>Gets or sets a value indicating whether [to draw me and my children].
        /// When true, this control and all of its children will be skipped in draw by.</summary>
        /// <value>When true : will draw me and my children. When false : will not draw.</value>
        public bool DrawMeAndMyChildren { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loaded using the Load() function.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is loaded; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoaded { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [must reload].
        /// Use this sparingly. Because it will destroy the whole control.
        /// After you must rebuild it totally again too..
        /// </summary>
        /// <value>
        ///   <c>true</c> if [must reload]; otherwise, <c>false</c>.
        /// </value>
        public bool MustReload
        {
            get
            {
                return this.mustReload;
            }

            set
            {
                this.mustReload = value;
            }
        }

        /// <summary>Use this method to query for any required services, and load any non-graphics resources.
        /// Do not build your control here ! Build it in LoadContent !.</summary>
        public void Initialize()
        {
            if (string.IsNullOrEmpty(this.currentTextureName))
            {
                this.currentTextureName = "Control " + this.Name;
            }
        }

        /// <summary>Called when graphics resources need to be loaded.
        /// Use this for the usage of :
        /// - creation of the internal embedded controls.
        /// - setting of the variables and resources in this control
        /// - to load any game-specific graphics resources
        /// - take over the config width and height and use it into State
        /// - overriding how this item looks like , by settings its texture or theme
        /// Call base.LoadContent before you do your override code, this will cause :
        /// - State.SourceRectangle to be reset to the Config.Size.</summary>
        public virtual void LoadContent()
        {
            this.UpdateDrawSourceRectangleByConfig();
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public virtual void UnloadContent()
        {
            if (string.IsNullOrEmpty(this.currentTextureName))
            {
                Debug.WriteLine("Texture could not be removed, because it was null or empty " + this.Name);
                return;
            }

            this.Manager.ImageCompositor.Delete(this.currentTextureName);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime)
        {
            if (this.mustReload)
            {
                this.mustReload = false;
                this.UnloadContent();
                this.IsLoaded = false;
            }

            this.UpdateDrawPositionByConfigAndParent();
        }

        /// <summary>Draw the texture from CurrentTextureName at DrawPosition combined with its offset.</summary>
        public virtual void DrawMyData()
        {
            // Draw the control
            this.Manager.ImageCompositor.Draw(this.currentTextureName, this.State, this.Theme.TintColor);
        }

        /// <summary>
        /// Updates the draw position by using my current configuration and the position of my parent control.
        /// </summary>
        protected void UpdateDrawPositionByConfigAndParent()
        {
            var parentPosition = new DVector2();
            if (this.Parent != null)
            {
                parentPosition = Parent.State.DrawPosition;
            }

            var newPosition = parentPosition + new DVector2(this.Config.PositionX, this.Config.PositionY);

            // System.Diagnostics.Debug.WriteLine(string.Format("Setting control {0} draw-position to {1}", Name, newPosition));
            this.State.DrawPosition = newPosition;
        }

        /// <summary>
        /// Updates the draw size by looking at my current configuration size.
        /// </summary>
        protected void UpdateDrawSizeByConfig()
        {
            this.State.Width = this.Config.Width;
            this.State.Height = this.Config.Height;
        }

        /// <summary>
        ///  Take the rectangle to draw from source to be the same size of the whole control.
        /// </summary>
        protected void UpdateDrawSourceRectangleByConfig()
        {
            this.State.SourceRectangle = new Rectangle(0, 0, (int)this.Config.Width, (int)this.Config.Height);
        }

        /// <summary>
        /// Check where given location is relative to me.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>The given point in relation to this control.</returns>
        public DVector2 PointRelative(DVector2 location)
        {
            return location - this.State.DrawPosition;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var message = string.Format(
                "Name:{0},Config:{1},State:{2}",
                this.Name,
                this.Config,
                this.State);
            return message;
        }

        /// <summary>Writes down the position of this control in debug-output.</summary>
        public void DebugPositions()
        {
            Debug.WriteLine("*** Debugging positions ***");
            this.DebugPositionsRecursive();
            Debug.WriteLine(string.Empty);
        }

        /// <summary>Writes down the position of this control in debug-output, recursively.</summary>
        private void DebugPositionsRecursive()
        {
            var x = this.Config.PositionX;
            var y = this.Config.PositionY;
            var w = this.Config.Width;
            var h = this.Config.Height;

            string p;
            if (this.Parent == null)
            {
                p = "NULL";
            }
            else
            {
                p = Parent.Name;
            }

            var message = string.Format(
                                        "Name:{0},X:{1},Y:{2},W:{3},H:{4},P:{5}",
                                        Name,
                                        x,
                                        y,
                                        w,
                                        h,
                                        p);

            Debug.WriteLine(message);

            foreach (var child in this.Children)
            {
                var node = child as Control;
                if (node == null)
                {
                    continue;
                }

                node.DebugPositionsRecursive();
            }
        }

        /// <summary>When mouse enters this control.</summary>
        public virtual void HoverEnter()
        {
        }

        /// <summary>When mouse leaves this control.</summary>
        public virtual void HoverExit()
        {
        }

        /// <summary>
        /// Centers this control to the parents canvas.
        /// </summary>
        public void CentreToParent()
        {
            var childW = this.Config.Width;
            var childH = this.Config.Height;

            var parent = Parent;
            var parentW = parent.Config.Width;
            var parentH = parent.Config.Height;

            var locx = (parentW / 2) - (childW / 2);
            var locy = (parentH / 2) - (childH / 2);

            this.Config.PositionX = locx;
            this.Config.PositionY = locy;
        }

        /// <summary>
        /// Destroys me.
        /// </summary>
        /// <param name="control">The control.</param>
        public void DestroyMe(Control control)
        {
            for (var i = Children.Count - 1; i >= 0; i--)
            {
                var child = Children[i];
                if (child != control)
                {
                    continue;
                }

                Children.Remove(child);
                var cnt = child as Control;
                if (cnt == null)
                {
                    continue;
                }

                cnt.UnloadContent();
            }
        }
    }
}
