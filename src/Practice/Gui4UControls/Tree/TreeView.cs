// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeView.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   I am the canvas where the tree-view should fit inside
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tree
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// I am the canvas where the tree-view should fit inside
    /// </summary>
    public class TreeView : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeView"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TreeView(string name) : base(name)
        {
            this.RootNode = new TreeViewNode(this.Name + "-RootNode") { ConfigText = "Root" };
            this.Config.Width = this.Theme.ControlWidth * 2;
            this.Config.Height = 300;
        }

        /// <summary>
        /// Gets or sets a value indicating whether we show a background to show where i will be on my parent
        /// </summary>
        /// <value>
        ///   <c>true</c> if [debug location]; otherwise, <c>false</c>.
        /// </value>
        public bool DebugLocation { get; set; }

        /// <summary>
        /// Gets or sets the root node.
        /// </summary>
        /// <value>
        /// The root node.
        /// </value>
        public TreeViewNode RootNode { get; set; }

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

            // lets start out with some simple box for this tree-node
            // the looks are not important yet
            this.CurrentTextureName = this.Manager.ImageCompositor.CreateRectangleTexture(
                                                                                this.Name + "-BackGround",
                                                                                (int)this.Config.Width,
                                                                                (int)this.Config.Height,
                                                                                1,
                                                                                this.Theme.ContainerFillColor,
                                                                                this.Theme.BorderColor);

            if (this.Children.Contains(this.RootNode) == false)
            {
                this.AddControl(this.RootNode);
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            this.UpdateDrawPositionByConfigAndParent();

            // check if we need to rebuild positions
            if (this.RootNode.MustRebuildPositions)
            {
                this.RebuildPositions();
                this.RootNode.MustRebuildPositions = false;
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
        /// Gets the tree node with given name from my tree.
        /// </summary>
        /// <param name="treeNodeName">Name of the tree node.</param>
        /// <returns>The tree-node with given name.</returns>
        public TreeViewNode GetTreeNode(string treeNodeName)
        {
            // go and find the tree-node recursive
            var node = this.GetNodeRecursive(this.RootNode, treeNodeName);
            return node;
        }

        /// <summary>
        /// Gets the given node by name , by crawling recursive trough the tree.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="treeNodeName">Name of the tree node.</param>
        /// <returns>The tree-node with given name.</returns>
        /// <exception cref="System.ArgumentNullException">Node can not be null.</exception>
        public TreeViewNode GetNodeRecursive(Node node, string treeNodeName)
        {
#if DEBUG
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
#endif

            TreeViewNode nodeToReturn = null;
            foreach (var child in node.Children)
            {
                var treeviewchild = child as TreeViewNode;
                if (treeviewchild != null)
                {
                    if (treeviewchild.Name.Equals(treeNodeName))
                    {
                        nodeToReturn = treeviewchild;
                        break;
                    }
                }

                var recursedNode = this.GetNodeRecursive(child, treeNodeName);
                if (recursedNode != null)
                {
                    return recursedNode;
                }
            }

            return nodeToReturn;
        }

        /// <summary>
        /// Rebuilds the positions for each tree-node in the tree.
        /// </summary>
        private void RebuildPositions()
        {
            // flatten the tree
            var flat = this.RootNode.FlattenTreeViewNodes();

            // calculate positions for the flattened tree
            for (var index = 0; index < flat.Count; index++)
            {
                var treeviewNodeFlattend = flat[index];
                var depth = treeviewNodeFlattend.Depth;
                var node = treeviewNodeFlattend.Node;
                var spacer = string.Concat(Enumerable.Repeat("-", treeviewNodeFlattend.Depth));
                var name = node.Name;

                // calculate the position of each control , relative to TreeView
                var locX = depth * this.Theme.ControlLargeSpacing;
                var locY = index * (node.Config.Height + this.Theme.ControlSmallSpacing);
                Debug.WriteLine("{0}{1}: {2},{3}", spacer, name, locX, locY);

                node.State.DrawPosition = new DVector2(locX, locY) + this.State.DrawPosition;
                node.State.Width = node.Config.Width;
                node.State.Height = node.Config.Height;
            }
        }
    }
}