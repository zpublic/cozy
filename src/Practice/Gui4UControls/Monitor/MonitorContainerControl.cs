// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonitorContainerControl.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the MonitorContainerControl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Monitor
{
    using System.Collections.ObjectModel;

    using GUI4UFramework.Structural;

    /// <summary>
    /// Creates a container look , where you will see the monitor-controls inside.
    /// </summary>
    public class MonitorContainerControl : Control
    {
        /// <summary>
        /// The monitor controls
        /// </summary>
        private readonly Collection<MonitorControl> monitorControls;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorContainerControl"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public MonitorContainerControl(string name) : base(name)
        {
            Config.Width = Theme.ControlWidth;
            Config.Height = Theme.ControlHeight;
            this.monitorControls = new Collection<MonitorControl>();
        }

        /// <summary>
        /// Gets the monitor controls.
        /// </summary>
        /// <value>
        /// The monitor controls.
        /// </value>
        public Collection<MonitorControl> MonitorControls
        {
            get { return this.monitorControls; }
        }
    }
}
