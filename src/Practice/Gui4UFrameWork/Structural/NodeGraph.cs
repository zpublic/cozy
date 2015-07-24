// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NodeGraph.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines a scene graph.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Structural
{
    using System.Diagnostics;

    using GUI4UFramework.Management;

    /// <summary>
    /// Defines a scene graph.
    /// </summary>
    public class NodeGraph
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeGraph"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public NodeGraph(NodeManager manager)
        {
            this.RootNode = new RootNode("RootNode");
            this.Manager = manager;
        }

        /// <summary>
        /// Gets or sets the <see cref="T:SceneNode"/> that is the root of the graph.
        /// </summary>
        /// <value>
        /// The first node of the tree. The root.
        /// </value>
        public RootNode RootNode { get; set; }

        /// <summary>
        /// Gets or sets the manager.
        /// The manager is the object that gives you control to drawing/input/etc.. Your main access point to the outside world.
        /// </summary>
        /// <value>
        /// The manager.
        /// </value>
        public NodeManager Manager { get; set; }

        /// <summary>
        /// Use this method to query for any required services, and load any non-graphics resources. 
        /// </summary>
        public void Initialize()
        {
            this.InitializeRecursive(RootNode);
        }

        /// <summary>
        /// walk trough every node recursive , and if a node is a control , then initialize it.
        /// Use this method to query for any required services, and load any non-graphics resources. 
        /// </summary>
        /// <param name="rootNode">The node where we start to initialize recursive.</param>
        private void InitializeRecursive(Node rootNode)
        {
            var control = rootNode as Control;
            if (control != null)
            {
                control.Manager = this.Manager;
                control.Initialize();
            }

            // Update children recursively
            for (var i = 0; i < rootNode.Children.Count; i++)
            {
                this.InitializeRecursive(rootNode.Children[i]);
            }
        }

        /// <summary>
        /// 1 reset root-node to origin
        /// 2 calculate the positions for every control recursive
        /// 3 call the update on each control recursive
        /// Called when the game has determined that game logic needs to be processed. 
        /// This might include the management of the game state, the processing of user input, or the updating of simulation data.
        /// Use this method with game-specific logic.
        /// </summary>
        /// <param name="gameTime">The time that will be used by the nodes.</param>
        public void Update(GameTime gameTime)
        {
            var updateIndex = 0;
            this.UpdateRecursive(gameTime, RootNode, ref updateIndex);
        }

        /// <summary>Call the UpdateCall on each node recursively
        /// Called when the game has determined that game logic needs to be processed. 
        /// This might include the management of the game state, the processing of user input, or the updating of simulation data. 
        /// Use this method with game-specific logic.</summary>
        /// <param name="time">The time used for updating animated objects thanks to time differences.</param>
        /// <param name="currentNode">The current Node to update.</param>
        /// <param name="updateIndex">The update Index , used for height calculations.</param>
        private void UpdateRecursive(GameTime time, Node currentNode, ref int updateIndex)
        {
            // Update node
            var currentControl = currentNode as Control;
            if (currentControl != null)
            {
                if (currentControl.DrawMeAndMyChildren)
                {
                    // lazy initialization
                    if (currentControl.IsLoaded == false)
                    {
                        Debug.WriteLine("Loading : " + currentControl.Name);
                        if (currentControl.Manager == null)
                        {
                            currentControl.Manager = this.Manager;
                        }

                        currentControl.LoadContent();
                        currentControl.IsLoaded = true;
                    }

                    // do the monogame version of update
                    currentControl.Update(time);

                    // Set update index, for height calculations
                    updateIndex++;
                    currentControl.State.UpdateIndex = updateIndex;
                }
            }

            // Update children recursively
            for (var i = 0; i < currentNode.Children.Count; i++)
            {
                if (currentControl != null && currentControl.DrawMeAndMyChildren == false)
                {
                    if (currentNode.Children[i] is Control)
                    {
                        continue;
                    }
                }

                this.UpdateRecursive(time, currentNode.Children[i], ref updateIndex);
            }
        }

        /// <summary>
        /// Allows the scene graph to render.
        /// Called when the game determines it is time to draw a frame. Use this method with game-specific rendering code.
        /// </summary>
        /// <param name="gameTime">
        /// The game Time. Used by objects to change  what they draw , because time  is changed.
        /// </param>
        public void Draw(GameTime gameTime)
        {
            this.DrawRecursive(gameTime, RootNode);
        }

        /// <summary>Draws each node recursive. But does not draw it is not visible.
        /// Your code now already knows where the draw and how to draw each control , thanks to your update implementation.</summary>
        /// <param name="gameTime">The game Time. Used by objects to change  what they draw , because time  is changed.</param>
        /// <param name="currentNode">The current Node to draw.</param>
        // ReSharper disable once UnusedParameter.Local
        private void DrawRecursive(GameTime gameTime, Node currentNode)
        {
            // if current node is drawable
            var currentControl = currentNode as Control;
            if (currentControl != null)
            {
                // and if "me and my children should be drawn" (like tab-sheets)
                if (currentControl.DrawMeAndMyChildren)
                {
                    // and if i should be drawn
                    if (currentControl.Config.Visible)
                    {
                        currentControl.DrawMyData();
                    }
                }
            }

            // and go recursive
            for (var i = 0; i < currentNode.Children.Count; i++)
            {
                var currentChildNode = currentNode.Children[i];

                // if currentControl is not null, then we are a Control. 
                // if  currentControl is a Control, then maybe we should not draw her or its children
                if (currentControl != null && currentControl.DrawMeAndMyChildren == false)
                {
                    if (currentChildNode is Control)
                    {
                        continue;
                    }
                }

                // draw my child
                this.DrawRecursive(gameTime, currentChildNode);
            }
        }

        /// <summary>Called when graphics resources need to be unloaded. 
        /// Override this method to unload any game-specific graphics resources.
        /// Use this for the :
        /// - destruction of the internal embedded controls.
        /// - cleaning up of variables and resource in this control.</summary>
        public void UnloadContent()
        {
            this.UnloadContentRecursive(RootNode);
        }

        /// <summary>Tells each control to unload content recursive.</summary>
        /// <param name="currentNode">The current Node to unload.</param>
        private void UnloadContentRecursive(Node currentNode)
        {
            var currentControl = currentNode as Control;
            if (currentControl != null)
            {
                currentControl.UnloadContent();
            }

            // Update children recursively
            for (var i = 0; i < currentNode.Children.Count; i++)
            {
                this.UnloadContentRecursive(currentNode.Children[i]);
            }
        }
    }
}
