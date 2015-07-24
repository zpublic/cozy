// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DrawState.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the DrawState type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Structural
{
    using GUI4UFramework.Graphics;

    /// <summary>
    /// Contains all the info for drawing a control on the screen.
    /// </summary>
    public class DrawState
    {
        /// <summary>
        /// The source rectangle , the part shown.
        /// </summary>
        private Rectangle sourceRectangle = new Rectangle(0, 0, 0, 0);

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawState"/> class.
        /// </summary>
        public DrawState()
        {
            this.MouseHoveringOver = false;
            this.UseHovering = false;
        }

        /// <summary>Gets or sets the location in local space to move and center the scene node when displayed.</summary>
        /// <value>The offset to start the draw.</value>
        public DVector2 Offset { get; set; }

        /// <summary>
        /// Gets or sets the SourceRectangle. Each control has its rectangle , some controls have stuff over them (clipped)
        /// This contains the part that will be shown.
        /// </summary>
        /// <value>
        /// The source rectangle.
        /// </value>
        public Rectangle SourceRectangle
        {
            get { return this.sourceRectangle; }
            set { this.sourceRectangle = value; }
        }

        /// <summary>Gets or sets the update index.
        /// Each node has a moment of update.
        /// On each update of a node , updateIndex is incremented
        /// Making each node have a unique updateIndex.
        /// You can use this for depth sorting , or finding out which control is on top
        /// If ControlA.UpdateIndex is smaller then ControlB.UpdateIndex , then ControlA is underneath ControlB.</summary>
        /// <value>When i update.</value>
        public long UpdateIndex { get; set; }

        /// <summary>Gets or sets the position where the object is drawn relative to the total 3d canvas.
        /// The scene node's , calculated world transform.</summary>
        /// <value>The root position where i start drawing.</value>
        public DVector2 DrawPosition { get; set; }

        /// <summary>Gets or sets the scene node's transformed width.</summary>
        /// <value>The width of the part that i draw.</value>
        public float Width { get; set; }

        /// <summary>Gets or sets the scene node's transformed height.</summary>
        /// <value>The height of the part that i draw.</value>
        public float Height { get; set; }

        /// <summary>Gets or sets a value indicating whether the mouse is hovering over the control.</summary>
        /// <value>Whether the mouse is hovering over me.</value>
        public bool MouseHoveringOver { get; set; }

        /// <summary>Gets or sets a value indicating whether we use hovering behavior for this control.</summary>
        /// <value><c>true</c> if [use hovering]; otherwise, <c>false</c>.</value>
        public bool UseHovering { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                                "Pos{0} W{1},H{2}",
                                this.DrawPosition,
                                this.Width,
                                this.Height);
        }
    }
}