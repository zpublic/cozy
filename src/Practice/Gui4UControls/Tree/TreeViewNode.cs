// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeViewNode.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   This node the base class where each tree-node should build upon..
//   You could use this as a new TreeViewNode , and you pass along a text or a icon
//   Or you could use this as your base like DGroupNode : TreeViewNode , and do some smarter stuff
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tree
{
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;

    using GUI4UControls.Text;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// This node the base class where each tree-node should build upon..
    /// You could use this as a new TreeViewNode , and you pass along a text or a icon 
    /// Or you could use this as your base like DGroupNode : TreeViewNode , and do some smarter stuff
    /// </summary>
    public class TreeViewNode : Control
    {
        /// <summary>
        /// Contains the text shown
        /// </summary>
        private string text;

        /// <summary>
        /// When true , will force to rebuild the text
        /// </summary>
        private bool redrawText;

        /// <summary>
        /// Contains the path to the texture n the resource pool, to show as ICON
        /// </summary>
        private string iconName;

        /// <summary>
        /// When true , will force to rebuild the icon
        /// </summary>
        private bool redrawIcon;

        /// <summary>
        /// When its true, this class will collapse its children visually 
        /// </summary>
        private bool collapsed;

        /// <summary>
        /// When true/false, will calculate the new sizes when there is less/more to see
        /// </summary>
        private bool redrawCollapse;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewNode"/> class.
        /// </summary>
        /// <param name="name">The name for this node , will be used also as text.</param>
        public TreeViewNode(string name) : base(name)
        {
            this.ConfigText = name;
        }

        /// <summary>
        /// Gets the path to the texture n the resource pool, to show as ICON visually
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public TreeViewIcon Icon { get; private set; }

        /// <summary>
        /// Gets : The expander control. When collapsed is true, this class will collapse its children visually
        /// </summary>
        /// <value>
        /// The expander.
        /// </value>
        public TreeViewExpander Expander { get; private set; }

        /// <summary>
        /// Gets the text shown
        /// </summary>
        /// <value>
        /// The text label.
        /// </value>
        public Label TextLabel { get; private set; }

        /// <summary>
        /// Gets or sets the text that is shown for this tree-node in the tree-view
        /// </summary>
        /// <value>
        /// The configuration text.
        /// </value>
        public string ConfigText
        {
            get
            {
                return this.text;
            }

            set
            {
                if (this.text == value)
                {
                    return;
                }

                this.text = value;
                this.redrawText = true;
            }
        }

        /// <summary>
        /// Gets or sets the resource name of the icon for this tree-node in the tree-view
        /// </summary>
        /// <value>
        /// The name of the icon.
        /// </value>
        public string IconName
        {
            get
            {
                return this.iconName;
            }

            set
            {
                if (this.iconName == value)
                {
                    return;
                }

                this.iconName = value;
                this.redrawIcon = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TreeViewNode"/> is a collapsed tree-node in the tree-view
        /// </summary>
        /// <value>
        ///   <c>true</c> if collapsed; otherwise, <c>false</c>.
        /// </value>
        public bool Collapsed
        {
            get
            {
                return this.collapsed;
            }

            set
            {
                if (this.collapsed == value)
                {
                    return;
                }

                this.collapsed = value;
                this.redrawCollapse = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [debug location].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [debug location]; otherwise, <c>false</c>.
        /// </value>
        public bool DebugLayout { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether we must rebuild positions during Update()
        /// </summary>
        /// <value>
        /// <c>true</c> if [must rebuild positions]; otherwise, <c>false</c>.
        /// </value>
        public bool MustRebuildPositions { get; set; }

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

            // take my position and size , 
            // and calculate the position where i should be drawn (State)
            this.UpdateDrawSizeByConfig();

            // fill the label control with my text
            this.TextLabel = new Label(this.Name + "-Label") { ConfigText = this.ConfigText };
            this.AddControl(this.TextLabel);

            // expander
            this.Expander = new TreeViewExpander(this.Name + "-Expander")
                                {
                                    Config = { Visible = false }
                                };
            this.AddControl(this.Expander);

            // icon
            this.Icon = new TreeViewIcon(this.Name + "-Icon") { Config = { Visible = false } };
            this.AddControl(this.Icon);

            // lets start out with some simple box for this tree-node
            // the looks are not important yet
            this.CurrentTextureName = this.Manager.ImageCompositor.CreateRectangleTexture(
                                                                                this.Name + "-BackGround",
                                                                                (int)this.State.Width,
                                                                                (int)this.State.Height,
                                                                                1,
                                                                                this.Theme.FillColor,
                                                                                this.Theme.BorderColor);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // the state location (the location shown on screen) of each tree-view-node is calculated in treeview.RebuildPositions()
            if (this.redrawText == true)
            {
                this.RedrawText();
                this.redrawText = false;
            }

            if (this.redrawIcon == true)
            {
                this.RedrawIcon();
                this.redrawIcon = false;
            }

            if (this.redrawCollapse == true)
            {
                this.RedrawCollapse();
            }
        }

        /// <summary>
        /// Draw the texture from CurrentTextureName at DrawPosition combined with its offset
        /// </summary>
        public override void DrawMyData()
        {
            this.Manager.ImageCompositor.Draw(this.CurrentTextureName, this.State, new GUIColor(255, 255, 255));
        }

        /// <summary>
        /// If i would be collapsed , i would not show my children anymore.
        /// And i would have to show a open me icon or something.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private void RedrawCollapse()
        {
        }

        /// <summary>
        /// Redraws the icon.my icon would be next to the collapse/expand-button.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private void RedrawIcon()
        {
            // load the new icon

            // place the text next to the icon
        }

        /// <summary>
        /// My text is always next to the collapse/expand-button.
        /// But sometimes there is also a icon , and then i should be next to that.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private void RedrawText()
        {
            // update my text

            // reposition my text
        }

        /// <summary>
        /// Adds a new child tree node to me with specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The newly made and added (as child) tree-node.</returns>
        public TreeViewNode AddTreeNode(string name)
        {
            var newNode = new TreeViewNode(name);

            this.AddControl(newNode);

            this.ParentRebuildPositions();

            return newNode;
        }

        /// <summary>
        /// Adds a new child tree node to me with specified name and icon.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="newIconName">The icon name.</param>
        /// <returns>The newly made and added (as child) tree-node.</returns>
        public TreeViewNode AddTreeNode(string name, string newIconName)
        {
            var newNode = new TreeViewNode(name)
            {
                IconName = newIconName
            };

            this.AddControl(newNode);

            this.ParentRebuildPositions();

            return newNode;
        }

        /// <summary>
        /// Adds a new child tree node to me with specified name and icon.
        /// We also tell directly if this new child will be collapsed or not.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="newIconName">The icon name.</param>
        /// <param name="newCollapsed">If the new tree-node is collapsed.</param>
        /// <returns>The newly made and added (as child) tree-node.</returns>
        public TreeViewNode AddTreeNode(string name, string newIconName, bool newCollapsed)
        {
            var newNode = new TreeViewNode(name)
            {
                IconName = newIconName, 
                Collapsed = newCollapsed
            };

            this.AddControl(newNode);

            this.ParentRebuildPositions();

            return newNode;
        }

        /// <summary>
        /// Tells the parent (recursively) to flag that it needs to rebuild positions.
        /// </summary>
        public void ParentRebuildPositions()
        {
            var parentNode = this.Parent as TreeViewNode;
            if (parentNode != null)
            {
                parentNode.MustRebuildPositions = true;
            }
        }

        /// <summary>
        /// Flattens the TreeView nodes into a flat list of TreeViewNodeFlat-items
        /// </summary>
        /// <returns>The flattened tree.</returns>
        public Collection<TreeViewNodeFlat> FlattenTreeViewNodes()
        {
            var flattenNodes = new Collection<TreeViewNodeFlat>();
            const int Depth = 0;
            this.FlattenTree(ref flattenNodes, Depth);

            return flattenNodes;
        }

        /// <summary>
        /// Flattens the tree recursively.
        /// </summary>
        /// <param name="flattendNodes">The flattened nodes.</param>
        /// <param name="depth">The depth.</param>
        private void FlattenTree(ref Collection<TreeViewNodeFlat> flattendNodes, int depth)
        {
            var flat = new TreeViewNodeFlat
            {
                Node = this, 
                Depth = depth
            };
            flattendNodes.Add(flat);

            depth = depth + 1;
            foreach (var child in this.Children)
            {
                var treeViewNode = child as TreeViewNode;
                if (treeViewNode == null)
                {
                    continue;
                }

                treeViewNode.FlattenTree(ref flattendNodes, depth);
            }
        }
    }
}