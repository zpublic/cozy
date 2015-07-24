// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWindowTreeview.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the TestWindowTreeView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tests
{
    using GUI4UControls.Text;

    using GUI4UFramework.Structural;

    using TreeView = GUI4UControls.Tree.TreeView;

    /// <summary>
    /// Creates a test window with a tree-view control inside.
    /// </summary>
    public class TestWindowTreeView : TestWindowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestWindowTreeView"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TestWindowTreeView(string name) : base(name)
        {
            this.Title = "Test tree-view";
            this.Config.Width = 400;
            this.Config.Height = 400;
        }

        /// <summary>
        /// Gets or sets the TreeView.
        /// </summary>
        /// <value>
        /// The TreeView.
        /// </value>
        private TreeView TreeView { get; set; }

        /// <summary>
        /// Gets or sets the information label.
        /// </summary>
        /// <value>
        /// The information label.
        /// </value>
        private Label InfoLabel { get; set; }

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
            base.LoadContent();

            // TreeView
            this.TreeView = new TreeView("TreeView test")
            {
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = Theme.ControlLargeSpacing + Theme.ControlHeight,
                },
                DebugLocation = true
            };
            this.AddControl(this.TreeView);

            // add some child nodes to RootNode
            var root = this.TreeView.RootNode;

            root.AddTreeNode("Child1"); // tree-node text/name
            root.AddTreeNode("Child2", "IconUrl"); // tree-node text/name and icon
            root.AddTreeNode("Child3", "IconUrl", true); // tree-node text/name and icon and collapsed

            // add some child nodes to Child2
            var node = this.TreeView.GetTreeNode("Child2");
            node.AddTreeNode("Child2.1", "IconUrl", true);
            node.AddTreeNode("Child2.2", "IconUrl", true);

            // InfoLabel
            this.InfoLabel = new Label("MyButton")
            {
                ConfigText = "Info text",
                Config =
                {
                    PositionX = Theme.ControlLargeSpacing,
                    PositionY = Theme.ControlLargeSpacing + this.TreeView.Config.Height + this.TreeView.Config.PositionY,
                }
            };
            this.AddControl(this.InfoLabel);

            Config.Height = this.InfoLabel.Config.PositionY + this.InfoLabel.Config.Height + Theme.ControlLargeSpacing;
            Config.Width = this.TreeView.Config.Width + (Theme.ControlLargeSpacing * 2);
        }
    }
}
