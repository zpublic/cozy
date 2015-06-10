// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorPickerRGBA.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the DColorPickerRGBA type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Color
{
    using GUI4UFramework.Colors;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Structural;

    /// <summary>
    /// ColorPicker that uses R,G,B,A sliders
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "RGBA")]
    public class ColorPickerRGBA : Control
    {
        /// <summary>
        /// The color that is being changed.
        /// </summary>
        private GUIColor color;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPickerRGBA"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ColorPickerRGBA(string name) : base(name)
        {
            Config.Height = Theme.ControlWidth + Theme.ControlHeight;
            Config.Width = Theme.ControlHeight * 4;
        }

        /// <summary>
        /// Gets the red slider.
        /// </summary>
        /// <value>
        /// The red.
        /// </value>
        public ColorSliderVertical Red { get; private set; }

        /// <summary>
        /// Gets the green slider
        /// </summary>
        /// <value>
        /// The green.
        /// </value>
        public ColorSliderVertical Green { get; private set; }

        /// <summary>
        /// Gets the blue slider
        /// </summary>
        /// <value>
        /// The blue.
        /// </value>
        public ColorSliderVertical Blue { get; private set; }

        /// <summary>
        /// Gets the alpha slider
        /// </summary>
        /// <value>
        /// The alpha.
        /// </value>
        public ColorSliderVertical Alpha { get; private set; }

        /// <summary>
        /// Gets the color box with a solid color that represents the color edited
        /// </summary>
        /// <value>
        /// The color box solid.
        /// </value>
        public ColorBoxSolid ColorBoxSolid { get; private set; }

        /// <summary>
        /// Gets or sets the color that is being edited.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public GUIColor Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

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

            // red
            this.Red = new ColorSliderVertical(Name + "-Red")
            {
                Config =
                    {
                        Width = Theme.ControlHeight, Height = Theme.ControlWidth
                    },
                DrawStyle = DrawStyle.Red,
                RGBA = this.Color
            };
            this.AddControl(this.Red);

            // green
            this.Green = new ColorSliderVertical(Name + "-Green")
            {
                Config = { PositionX = this.Red.Config.Width * 1 },
                DrawStyle = DrawStyle.Green,
                RGBA = this.Color
            };
            this.AddControl(this.Green);

            // blue
            this.Blue = new ColorSliderVertical(Name + "-Blue")
            {
                Config = { PositionX = this.Red.Config.Width * 2 },
                DrawStyle = DrawStyle.Blue,
                RGBA = this.Color
            };
            this.AddControl(this.Blue);

            // alpha
            this.Alpha = new ColorSliderVertical(Name + "-Alpha")
            {
                Config = { PositionX = this.Red.Config.Width * 3 },
                DrawStyle = DrawStyle.Brightness,
                RGBA = this.Color
            };
            this.AddControl(this.Alpha);

            // the box with the final color
            this.ColorBoxSolid = new ColorBoxSolid(Name + "-ColorBox")
            {
                Config =
                {
                    PositionX = 0,
                    PositionY = this.Red.Config.Height,
                    Width = this.Red.Config.Width * 4
                }
            };
            this.AddControl(this.ColorBoxSolid);
        }

        /// <summary>
        /// Draw the texture from CurrentTextureName at DrawPosition combined with its offset
        /// </summary>
        public override void DrawMyData()
        {
            // this control only has children, it doesn't have a  background itself.
            // so there is nothing to draw here. the children will do that.
        }
    }
}
