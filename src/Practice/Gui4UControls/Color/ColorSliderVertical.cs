// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorSliderVertical.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   A vertical slider control that shows a range for a color property (a.k.a. Hue, Saturation, Brightness, Red, Green, Blue) and sends an event when the slider is changed.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Color
{
    using System.Diagnostics;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;
    using GUI4UFramework.Structural;

    /// <summary>
    /// A vertical slider control that shows a range for a color property (a.k.a. Hue, Saturation, Brightness, Red, Green, Blue) and sends an event when the slider is changed.
    /// </summary>
    public class ColorSliderVertical : Control
    {
        /// <summary>
        /// Where the marker starts
        /// </summary>
        private float sliderValue;

        /// <summary>
        /// How we show data on the control for the used color
        /// </summary>
        private DrawStyle drawStyle = DrawStyle.Hue;

        /// <summary>
        /// The color that we are showing on the control
        /// </summary>
        private HSL hsl;

        /// <summary>
        /// The color that we are showing on the control
        /// </summary>
        private GUIColor rgba;

        /// <summary>
        /// Whether the mouse is dragging the indicator.
        /// </summary>
        private bool dragging;

        /// <summary>
        /// If we must redraw during update.
        /// </summary>
        private bool mustRedraw;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorSliderVertical"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ColorSliderVertical(string name) : base(name)
        {
            // Initialize Colors
            this.hsl = new HSL { H = 1.0, S = 1.0, L = 1.0 };
            this.rgba = AdobeColors.HSLToRGB(this.hsl);

            // pick a format to show
            this.drawStyle = DrawStyle.Hue;

            this.Padding = 9;

            Config.Width = Theme.ControlHeight;
            Config.Height = Theme.ControlWidth;
        }

        /// <summary>
        /// Gets or sets the indicator on the left-side.
        /// </summary>
        /// <value>
        /// The indicator left.
        /// </value>
        public ColorSliderVerticalIndicator IndicatorLeft { get; set; }

        /// <summary>
        /// Gets or sets the indicator on the right-side.
        /// </summary>
        /// <value>
        /// The indicator right.
        /// </value>
        public ColorSliderVerticalIndicator IndicatorRight { get; set; }

        /// <summary>
        /// Gets or sets the color that we are showing on the control.
        /// </summary>
        /// <value>
        /// The color used.
        /// </value>
        public HSL HSL
        {
            get
            {
                return this.hsl;
            }

            set
            {
                this.hsl = value;
                this.mustRedraw = true;
            }
        }

        /// <summary>
        /// Gets or sets the color that we are showing on the control.
        /// </summary>
        /// <value>The color used.
        /// </value>
        public GUIColor RGBA
        {
            get
            {
                return this.rgba;
            }
 
            set 
            { 
                this.rgba = value;
                this.mustRedraw = true;
            }
        }

        /// <summary>
        /// Gets or sets how we show data on the control for the used color.
        /// </summary>
        /// <value> The draw-style for this control.</value>
        public DrawStyle DrawStyle
        {
            get
            {
                return this.drawStyle;
            }

            set
            {
                this.drawStyle = value;
                this.mustRedraw = true;
            }
        }

        /// <summary>
        /// Gets or sets the padding. the spacing between the outside border and the rest of the controls.
        /// </summary>
        /// <value>
        /// The padding.
        /// </value>
        public int Padding { get; set; }

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
            if (this.hsl == null)
            {
                this.hsl = new HSL { H = 1.0, S = 1.0, L = 1.0 };
                this.rgba = AdobeColors.HSLToRGB(this.hsl);
            }

            Config.Width = Theme.ControlHeight;
            Config.Height = Theme.ControlWidth;

            // create left indicator
            this.IndicatorLeft = new ColorSliderVerticalIndicator(Name + "-indicatorLeft")
            {
                Side = Side.Left,
                Manager = Manager
            };
            this.AddControl(this.IndicatorLeft);

            // create right indicator
            this.IndicatorRight = new ColorSliderVerticalIndicator(Name + "-indicatorRight")
            {
                Side = Side.Right,
                Manager = Manager
            };
            this.AddControl(this.IndicatorRight);

            this.SetIndicatorPosition(0);

            this.RedrawControl();

            // do the basic stuff
            base.LoadContent();
            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.mustRedraw)
            {
                this.RedrawControl();
            }

            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();
            this.UpdateDrawSourceRectangleByConfig();

            var leftMousePressed = Manager.InputManager.ReadLeftMousePressed();
            var mouseLocation = Manager.InputManager.ReadMouseLocation();

            if (this.CheckIsFocused() && leftMousePressed)
            {
                this.dragging = true;
            }

            if (this.dragging && leftMousePressed)
            {
                var relativeLocation = PointRelative(mouseLocation);
                this.sliderValue = relativeLocation.Y / Config.Height;
                this.RedrawControl();
                this.TakeValue(this.sliderValue);
                Debug.WriteLine("Rel " + relativeLocation + " val " + this.sliderValue);
            }

            if (this.dragging && leftMousePressed == false)
            {
                this.dragging = false;
            }
        }

        /// <summary>
        /// Draw the texture at DrawPosition combined with its offset
        /// </summary>
        public override void DrawMyData()
        {
            Manager.ImageCompositor.Draw(this.CurrentTextureName, this.State, Theme.TintColor);
        }

        /// <summary>
        /// Calls all the functions necessary to redraw the entire control.
        /// </summary>
        private void RedrawControl()
        {
            this.SetIndicatorPosition(this.sliderValue);

            var width = (int)Config.Width;
            var heigth = (int)Config.Height;

            if (Manager.ImageCompositor.Contains(this.CurrentTextureName) == false)
            {
                this.CurrentTextureName = Manager.ImageCompositor.CreateFlatTexture(this.Name + "-Background", width, heigth, GUIColor.Gainsboro());
            }

            var colorMap = new ColorMap(width, heigth);
            switch (this.drawStyle)
            {
                case DrawStyle.Hue:
                    DrawStyleHue(ref colorMap);
                    break;
                case DrawStyle.Saturation:
                    this.DrawStyleSaturation(ref colorMap);
                    break;
                case DrawStyle.Brightness:
                    this.DrawStyleLuminance(ref colorMap);
                    break;
                case DrawStyle.Red:
                    this.DrawStyleRed(ref colorMap);
                    break;
                case DrawStyle.Green:
                    this.DrawStyleGreen(ref colorMap);
                    break;
                case DrawStyle.Blue:
                    this.DrawStyleBlue(ref colorMap);
                    break;
            }

            Manager.ImageCompositor.UpdateTexture(this.CurrentTextureName, colorMap);
        }

        /// <summary>
        /// Fills in the content of the control showing all values of Hue (from 0 to 360) , a rainbow
        /// </summary>
        /// <param name="map">The map to update.</param>
        private static void DrawStyleHue(ref ColorMap map)
        {
            // S and L will both be at 100% for this DrawStyle
            var hsl = new HSL
            {
                S = 1.0,
                L = 1.0
            };

            // i represents the current line of pixels we want to draw horizontally
            for (var i = 0; i < map.Height; i++)
            {
                // H (hue) is based on the current vertical position
                hsl.H = 1.0 - ((double)i / map.Height);

                // Get the Color for this line
                var rgb = AdobeColors.HSLToRGB(hsl);
                for (var l = 0; l < map.Width; l++)
                {
                    map.Set(l, i, rgb);
                }
            }
        }

        /// <summary>Fills in the content of the control showing all values of Saturation (0 to 100%) for the given Hue and Luminance.</summary>
        /// <param name="map">The map to update.</param>
        private void DrawStyleSaturation(ref ColorMap map)
        {
            // Use the H and L values of the current color (m_hsl)
            var colorToConvert = new HSL
            {
                H = this.hsl.H,
                L = this.hsl.L
            };

            // i represents the current line of pixels we want to draw horizontally
            for (var i = 0; i < map.Height; i++)
            {
                // S (Saturation) is based on the current vertical position
                colorToConvert.S = 1.0 - ((double)i / map.Height);

                // Get the Color for this line
                var rgb = AdobeColors.HSLToRGB(colorToConvert);
                for (var l = 0; l < map.Width; l++)
                {
                    map.Set(l, i, rgb);
                }
            }
        }

        /// <summary>Fills in the content of the control showing all values of Luminance (0 to 100%) for the given
        /// Hue and Saturation.</summary>
        /// <param name="map">The map to update.</param>
        private void DrawStyleLuminance(ref ColorMap map)
        {
            // Use the H and S values of the current color (m_hsl)
            var colorToConvert = new HSL
            {
                H = this.hsl.H,
                S = this.hsl.S
            };

            // i represents the current line of pixels we want to draw horizontally
            for (var i = 0; i < map.Height; i++)
            {
                // L (Luminance) is based on the current vertical position
                colorToConvert.L = 1.0 - ((double)i / map.Height);

                // Get the Color for this line
                var rgb = AdobeColors.HSLToRGB(colorToConvert);
                for (var l = 0; l < map.Width; l++)
                {
                    map.Set(l, i, rgb);
                }
            }
        }

        /// <summary>Fills in the content of the control showing all values of Red (0 to 255) for the given
        /// Green and Blue.</summary>
        /// <param name="map">The map to update.</param>
        private void DrawStyleRed(ref ColorMap map)
        {
            // i represents the current line of pixels we want to draw horizontally
            for (var i = 0; i < map.Height; i++)
            {
                // red is based on the current vertical position
                var red = 255 - Round(255 * (double)i / map.Height);

                // Get the Color for this line
                var rgb = GUIColor.FromARGB(red, this.rgba.G, this.rgba.B);
                for (var l = 0; l < map.Width; l++)
                {
                    map.Set(l, i, rgb);
                }
            }
        }

        /// <summary>Fills in the content of the control showing all values of Green (0 to 255) for the given
        /// Red and Blue.</summary>
        /// <param name="map">The map to update.</param>
        private void DrawStyleGreen(ref ColorMap map)
        {
            // i represents the current line of pixels we want to draw horizontally
            for (var i = 0; i < map.Height; i++)
            {
                // red is based on the current vertical position
                var green = 255 - Round(255 * (double)i / map.Height);

                // Get the Color for this line
                var rgb = GUIColor.FromARGB(this.rgba.R, green, this.rgba.B);
                for (var l = 0; l < map.Width; l++)
                {
                    map.Set(l, i, rgb);
                }
            }
        }

        /// <summary>Fills in the content of the control showing all values of Blue (0 to 255) for the given
        /// Red and Green.</summary>
        /// <param name="map">The map to update.</param>
        private void DrawStyleBlue(ref ColorMap map)
        {
            // i represents the current line of pixels we want to draw horizontally
            for (var i = 0; i < map.Height; i++)
            {
                // red is based on the current vertical position
                var blue = 255 - Round(255 * (double)i / map.Height);

                // Get the Color for this line
                var rgb = GUIColor.FromARGB(this.rgba.R, this.rgba.G, blue);
                for (var l = 0; l < map.Width; l++)
                {
                    map.Set(l, i, rgb);
                }
            }
        }

        /// <summary>
        /// Kind of self explanatory, I really need to look up the .NET function that does this.
        /// </summary>
        /// <param name="val">Double value to be rounded to an integer</param>
        /// <returns>The rounded value.</returns>
        private static int Round(double val)
        {
            var retVal = (int)val;

            var temp = (int)(val * 100);

            if ((temp % 100) >= 50)
            {
                retVal += 1;
            }

            return retVal;
        }

        /// <summary>
        /// Set the position of the indicator between 0 and 1
        /// </summary>
        /// <param name="newIndicatorPositionValue">The new indicator position.</param>
        private void SetIndicatorPosition(float newIndicatorPositionValue)
        {
            var y = Config.Height * newIndicatorPositionValue;
            if (y < 0)
            {
                y = 0;
            }

            if (y > Config.Height)
            {
                y = Config.Height;
            }

            // left 
            this.IndicatorLeft.Config.PositionX = 0;
            this.IndicatorLeft.Config.PositionY = y;

            // right
            this.IndicatorRight.Config.PositionX = Config.Width - this.IndicatorRight.Config.Width;
            this.IndicatorRight.Config.PositionY = y;
        }

        /// <summary>
        /// Takes the value to use and update the slider with it.
        /// </summary>
        /// <param name="valueToUse">The value to use.</param>
        private void TakeValue(float valueToUse)
        {
            switch (this.drawStyle)
            {
                case DrawStyle.Hue:
                    this.HSL.H = valueToUse;
                    this.RGBA = AdobeColors.HSLToRGB(HSL);
                    break;

                case DrawStyle.Saturation:
                    this.HSL.S = valueToUse;
                    this.RGBA = AdobeColors.HSLToRGB(HSL);
                    break;

                case DrawStyle.Brightness:
                    this.HSL.L = valueToUse;
                    this.RGBA = AdobeColors.HSLToRGB(HSL);
                    break;

                case DrawStyle.Red:
                    this.RGBA.UpdateR((byte)valueToUse);
                    this.HSL = AdobeColors.RGBToHSL(this.RGBA);
                    break;

                case DrawStyle.Green:
                    this.RGBA.UpdateG((byte)valueToUse);
                    this.HSL = AdobeColors.RGBToHSL(this.RGBA);
                    break;

                case DrawStyle.Blue:
                    this.RGBA.UpdateB((byte)valueToUse);
                    this.HSL = AdobeColors.RGBToHSL(this.RGBA);
                    break;
            }
        }
    }
}
