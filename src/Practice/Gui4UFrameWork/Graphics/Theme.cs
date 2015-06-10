// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Theme.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   GUIColor themes for Control drawing
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Graphics
{
    using GUI4UFramework.Colors;

    /// <summary>GUIColor themes for Control drawing.</summary>
    public class Theme
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Theme"/> class.
        /// </summary>
        public Theme()
        {
            this.BorderWidth = 1;
            this.ContainerFillColor = new GUIColor(190, 190, 190);
            this.BorderColor = new GUIColor(20, 20, 20);
            this.FillColor = new GUIColor(120, 120, 120);
            this.FontName = @"Fonts\LucidaConsole";
            this.InputFontColor = new GUIColor(0, 0, 0);
            this.FontColor = new GUIColor(0, 0, 0);
            this.HoverBorderColor = new GUIColor(10, 10, 10);
            this.HoverFillColor = new GUIColor(170, 170, 170);
            this.ClickedBorderColor = new GUIColor(0, 0, 0);
            this.ClickedFillColor = new GUIColor(190, 190, 190);
            this.InputFillColor = new GUIColor(255, 255, 255);
            this.WindowFillColor = new GUIColor(200, 200, 200, 80);
            this.TintColor = new GUIColor(255, 255, 255);
            this.ControlWidth = 120;
            this.ControlHeight = 28;
            this.ControlSmallSpacing = 3;
            this.ControlLargeSpacing = 10;
        }

        /// <summary>Gets or sets the alpha part of the windows background.</summary>
        /// <value>The window alpha.</value>
        public byte WindowAlpha { get; set; }

        /// <summary>Gets or sets the alpha part of the controls background.</summary>
        /// <value>The control alpha.</value>
        public byte ControlAlpha { get; set; }

        /// <summary>
        /// Gets or sets the color of the tint to re-tint the textures into when drawn.
        /// </summary>
        /// <value>
        /// The color of the tint.
        /// </value>
        public GUIColor TintColor { get; set; }

        /// <summary>Gets or sets the Fill GUIColor for forms, used instead of FillColor when Control._isWindow is true.</summary>
        /// <value>The window fill color.</value>
        public GUIColor WindowFillColor { get; set; }

        /// <summary>Gets or sets the Fill GUIColor for input objects (text-box, combo-box, list-box).</summary>
        /// <value>The input-control fill color.</value>
        public GUIColor InputFillColor { get; set; }

        /// <summary>Gets or sets the Fill GUIColor for click-able/selectable objects, when clicked.</summary>
        /// <value>The fill color for click events.</value>
        public GUIColor ClickedFillColor { get; set; }

        /// <summary>Gets or sets the Border GUIColor for click able/selectable objects, when clicked.</summary>
        /// <value>The clicked border color.</value>
        public GUIColor ClickedBorderColor { get; set; }

        /// <summary>Gets or sets the Fill GUIColor to be used when mouse is hovering over, when Control.UseHoverColor is true.</summary>
        /// <value>The hover fill color.</value>
        public GUIColor HoverFillColor { get; set; }

        /// <summary>Gets or sets the Border GUIColor to be used when mouse is hovering over, when Control.UseHoverColor is true.</summary>
        /// <value>The hover border color.</value>
        public GUIColor HoverBorderColor { get; set; }

        /// <summary>Gets or sets the Main font GUIColor for all controls.</summary>
        /// <value>The font color.</value>
        public GUIColor FontColor { get; set; }

        /// <summary>Gets or sets the GUIColor for editable text.</summary>
        /// <value>The input font color.</value>
        public GUIColor InputFontColor { get; set; }

        /// <summary>Gets or sets the Fill GUIColor.</summary>
        /// <value>The fill color.</value>
        public GUIColor FillColor { get; set; }

        /// <summary>Gets or sets the Border GUIColor.</summary>
        /// <value>The border color.</value>
        public GUIColor BorderColor { get; set; }

        /// <summary>Gets or sets the color to be used when filling a control that behaves like a container of other controls.</summary>
        /// <value>The fill-color of the container.</value>
        public GUIColor ContainerFillColor { get; set; }

        /// <summary>Gets or sets the name of the font to be used in the controls.</summary>
        /// <value>The name of the font.</value>
        public string FontName { get; set; }

        /// <summary>Gets or sets the width of the border for the controls.</summary>
        /// <value>The width of the border.</value>
        public int BorderWidth { get; set; }

        /// <summary>
        /// Gets or sets the default width of the control when created.
        /// Can be multiplied with a integer for bigger sized controls.
        /// </summary>
        /// <value>
        /// The width of the control.
        /// </value>
        public int ControlWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the control when created.
        /// Can be multiplied with a integer for bigger sized controls.
        /// </summary>
        /// <value>
        /// The height of the control.
        /// </value>
        public int ControlHeight { get; set; }

        /// <summary>
        /// Gets or sets the small spacing in between controls or related.
        /// </summary>
        /// <value>
        /// The control small spacing.
        /// </value>
        public int ControlSmallSpacing { get; set; }

        /// <summary>
        /// Gets or sets the large spacing in between controls or related.
        /// </summary>
        /// <value>
        /// The control large spacing.
        /// </value>
        public int ControlLargeSpacing { get; set; }

        /// <summary>Clones this instance.</summary>
        /// <returns>a clone of this object.</returns>
        public object Clone()
        {
            var ct = (Theme)MemberwiseClone();
            return ct;
        }
    }
}
