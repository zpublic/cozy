// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NodeManager.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the NodeManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Structural
{
    using System.Diagnostics;
    using GUI4UFramework.Management;

    /// <summary>Manages the nodes.</summary>
    public class NodeManager
    {
        /// <summary>
        /// The scene graph contains a simple framework to walk by every node, contains the root node.
        /// </summary>
        private readonly NodeGraph sceneGraph;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeManager"/> class.
        /// </summary>
        /// <param name="imageCompositor">The image compositor.</param>
        /// <param name="inputManager">The input manager.</param>
        public NodeManager(ImageCompositor imageCompositor, InputManager inputManager)
        {
            this.sceneGraph = new NodeGraph(this);

            this.SceneNodes = new NotDrawnNode("SceneNodes");

            this.ForegroundSceneNodes = new Control("ForegroundSceneNodes") { Config = { Visible = false } };

            this.sceneGraph.RootNode.Children.Add(this.SceneNodes);
            this.sceneGraph.RootNode.Children.Add(this.ForegroundSceneNodes);

            ImageCompositor = imageCompositor;
            InputManager = inputManager;
        }

        /// <summary>Gets or sets the nodes that will be used to create the user-interface.</summary>
        /// <value>The scene nodes.</value>
        public NotDrawnNode SceneNodes { get; set; }

        /// <summary>Gets or sets the foreground scene nodes the nodes that will always be drawn in front of the user-interface.</summary>
        /// <value>The foreground scene nodes.</value>
        public Control ForegroundSceneNodes { get; set; }

        /// <summary>
        /// Gets the focused node. The node where the mouse is over.
        /// </summary>
        /// <value>
        /// The focused node.
        /// </value>>
        public Control FocusedNode { get; private set; }

        /// <summary>
        /// Gets the image compositor. A class that tells how to create/read/update/delete the textures.
        /// </summary>
        /// <value>
        /// The image compositor.
        /// </value>
        public ImageCompositor ImageCompositor { get; private set; }

        /// <summary>
        /// Gets the input manager. A class that grabs information from input-devices.
        /// </summary>
        /// <value>The input manager.</value>>
        public InputManager InputManager { get; private set; }

        /// <summary>Initializes the scene graph.</summary>
        public void Initialize()
        {
            this.sceneGraph.Initialize();
        }

        /// <summary>
        /// Updates the scene-graph.
        /// And finds the control that has focus.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            // update the scene graph
            Debug.Assert(this.sceneGraph != null, "SceneGraph can never be null ! Fix this.");
            this.sceneGraph.Update(gameTime);

            // Calculate focus based on update index
            this.FindFocusedControl();
        }

        /// <summary>
        /// Finds the focused control.
        /// </summary>
        private void FindFocusedControl()
        {
            Control focusControl = null;

            var nodeCollection = this.sceneGraph.RootNode.FlattenChildrenRecursive();

            var mouseLocation = InputManager.ReadMouseLocation();

            // walk by every control
            foreach (var sceneNode in nodeCollection)
            {
                var control = sceneNode as Control;
                if (control == null)
                {
                    continue;
                }

                // reset the control focus state first
                control.State.MouseHoveringOver = false;

                // is the mouse hovering over the control ?
                if (control.IsPointInside((int)mouseLocation.X, (int)mouseLocation.Y))
                {
                    control.State.MouseHoveringOver = true;
                    if (!control.State.UseHovering)
                    {
                        control.HoverEnter();
                    }

                    if (!control.Config.AcceptsFocus)
                    {
                        continue;
                    }

                    if (focusControl == null)
                    {
                        focusControl = control;
                    }

                    if (focusControl.State.UpdateIndex < control.State.UpdateIndex)
                    {
                        focusControl = control;
                    }
                }
                else
                {
                    control.State.MouseHoveringOver = false;
                    if (control.State.UseHovering)
                    {
                        control.HoverExit();
                    }
                }
            }

            // we have found a focused control , lets use it
            if (focusControl != null && this.FocusedNode != focusControl)
            {
                this.FocusedNode = focusControl;
            }
        }

        /// <summary>Tells that we start drawing , draws the scene-graph ,  and them stops the drawing.</summary>
        /// <param name="gameTime">The game time.</param>
        public void Draw(GameTime gameTime)
        {
            Debug.Assert(this.sceneGraph != null, "SceneGraph can never be null ! Fix this.");

            ImageCompositor.BeginDraw();
            this.sceneGraph.Draw(gameTime);
            ImageCompositor.EndDraw();
        }

        /// <summary>Unloads the content in the scene-graph.</summary>
        public void UnloadContent()
        {
            this.sceneGraph.UnloadContent();
        }

        /// <summary>
        /// Add a control to the manager's main scene graph.
        /// Typically you would add a window item with this method and attach controls to the control itself.
        /// </summary>
        /// <param name="control">The control that we add.</param>
        public void AddControl(Control control)
        {
            if (this.sceneGraph != null)
            {
                this.SceneNodes.Children.Add(control);
            }
        }

        /// <summary>
        /// Removes a control from the user-interface.
        /// </summary>
        /// <param name="control">The control.</param>
        public void RemoveControl(Control control)
        {
            if (this.sceneGraph != null)
            {
                this.SceneNodes.Children.Remove(control);
            }
        }

        /// <summary>
        /// Add a control to the scene-graph as a foreground item which will be rendered after all controls.
        /// </summary>
        /// <param name="control">The control that we add to the foreground.</param>
        public void AddForegroundControl(Control control)
        {
            if (this.sceneGraph != null)
            {
                this.ForegroundSceneNodes.Children.Add(control);
            }
        }

        /// <summary>
        /// Sets the focus to given control.
        /// </summary>
        /// <param name="control">The control.</param>
        public void SetFocusedControl(Control control)
        {
            if (this.FocusedNode != null && this.FocusedNode != control)
            {
                this.FocusedNode = control;
            }
        }

        /// <summary>
        /// Debugs me.
        /// </summary>
        public void DebugMe()
        {
            Debug.WriteLine("Control Scene nodes");
            var controlSceneNodes = this.SceneNodes.FlattenChildrenRecursive();
            foreach (var node in controlSceneNodes)
            {
                Debug.WriteLine(node.ToString());
            }

            Debug.WriteLine("Foreground Scene nodes");
            var foregroundNodes = this.ForegroundSceneNodes.FlattenChildrenRecursive();
            foreach (var node in foregroundNodes)
            {
                Debug.WriteLine(node.ToString());
            }
        }
    }
}
