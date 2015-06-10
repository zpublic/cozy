// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorBoxGradient.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ColorBoxGradient type.
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
    /// Shows a gradient color box with a indicator on top that changes color by changes in the HSL property
    /// </summary>
    public class ColorBoxGradient : Control
    {
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
        /// If we are dragging the indicator over this control
        /// </summary>
        private bool dragging;

        /// <summary>
        /// The gradient value
        /// </summary>
        private DVector2 gradientValue;

        /// <summary>
        /// If we must redraw this control during update
        /// </summary>
        private bool redrawControlFlag;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorBoxGradient"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ColorBoxGradient(string name) : base(name)
        {
            this.hsl = new HSL { H = 0.3, S = 0.6, L = 0.8 };
            this.rgba = AdobeColors.HSLToRGB(this.hsl);
            this.drawStyle = DrawStyle.Hue;
            Config.Width = Theme.ControlWidth;
            Config.Height = Theme.ControlWidth;
            this.redrawControlFlag = true;
        }

        /// <summary>
        /// Gets the indicator. The indicator points out which value is picked by this control.
        /// </summary>
        /// <value>
        /// The indicator.
        /// </value>
        public ColorBoxGradientIndicator Indicator { get; private set; }

        /// <summary>
        /// Gets or sets the color that we are showing on the control
        /// </summary>
        /// <value>
        /// The color that we are showing.
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
                this.redrawControlFlag = true;
            }
        }

        /// <summary>
        /// Gets or sets the color that we are showing on the control
        /// </summary>
        /// <value>
        /// The color that we are showing.
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
                this.redrawControlFlag = true;
            }
        }

        /// <summary>
        /// Gets or sets how we show data on the control for the used color
        /// </summary>
        /// <value>
        /// The style to use to draw the control.
        /// </value>
        public DrawStyle DrawStyle
        {
            get
            {
                return this.drawStyle;
            }

            set
            {
                this.drawStyle = value;
                this.redrawControlFlag = true;
            }
        }

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
            this.Indicator = new ColorBoxGradientIndicator(Name + "-Indicator");
            this.AddControl(this.Indicator);

            this.SetIndicatorPosition(new DVector2(0, 0));

            this.redrawControlFlag = true;

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

            this.UpdateDrawPositionByConfigAndParent();
            this.UpdateDrawSizeByConfig();
            this.UpdateDrawSourceRectangleByConfig();

            var leftMousePressed = Manager.InputManager.ReadLeftMousePressed();
            var mouseLocation = Manager.InputManager.ReadMouseLocation();

            // check the redraw FLAG !
            if (this.redrawControlFlag == true)
            {
                this.RedrawControl();
                this.redrawControlFlag = false;
            }

            if (this.CheckIsFocused() && leftMousePressed)
            {
                this.dragging = true;
            }

            if (this.dragging && leftMousePressed)
            {
                var relativeLocation = PointRelative(mouseLocation);
                this.gradientValue = new DVector2(relativeLocation.X / Config.Width, relativeLocation.Y / Config.Height);
                this.redrawControlFlag = true;
                this.TakeValue(this.gradientValue);
                Debug.WriteLine("Rel " + relativeLocation + " val " + this.gradientValue);
            }

            if (this.dragging && leftMousePressed == false)
            {
                this.dragging = false;
            }
        }

        /// <summary>
        /// Takes the value that came from the relative position of the mouse , to update color.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        private void TakeValue(DVector2 newValue)
        {
            switch (this.drawStyle)
            {
                case DrawStyle.Hue:
                    HSL.S = newValue.X;
                    HSL.L = 1 - newValue.Y;
                    this.RGBA = AdobeColors.HSLToRGB(HSL);
                    break;
                case DrawStyle.Saturation:
                    HSL.H = newValue.X;
                    HSL.L = 1 - newValue.Y;
                    this.RGBA = AdobeColors.HSLToRGB(HSL);
                    break;
                case DrawStyle.Brightness:
                    HSL.H = newValue.X;
                    HSL.S = 1 - newValue.Y;
                    this.RGBA = AdobeColors.HSLToRGB(HSL);
                    break;
                case DrawStyle.Red:
                    this.RGBA.UpdateG((byte)(1 - newValue.Y));
                    this.RGBA.UpdateB((byte)newValue.X);
                    HSL = AdobeColors.RGBToHSL(this.RGBA);
                    break;
                case DrawStyle.Green:
                    this.RGBA.UpdateB((byte)newValue.X);
                    this.RGBA.UpdateR((byte)(1 - newValue.Y));
                    HSL = AdobeColors.RGBToHSL(this.RGBA);
                    break;
                case DrawStyle.Blue:
                    this.RGBA.UpdateR((byte)newValue.X);
                    this.RGBA.UpdateG((byte)(1 - newValue.Y));
                    HSL = AdobeColors.RGBToHSL(this.RGBA);
                    break;
            }
        }

        /// <summary>
        /// Redraws the control using a chosen style.
        /// </summary>
        private void RedrawControl()
        {
            this.SetIndicatorPosition(this.gradientValue);

            var width = (int)Config.Width;
            var heigth = (int)Config.Height;

            if (Manager.ImageCompositor.Contains(this.CurrentTextureName) == false)
            {
                this.CurrentTextureName = Manager.ImageCompositor.CreateFlatTexture(this.Name + "-Background", width, heigth, GUIColor.Gainsboro());
            }

            var array = new ColorMap(width, heigth);
            switch (this.drawStyle)
            {
                case DrawStyle.Hue:
                    this.DrawStyleHue(ref array);
                    break;
                case DrawStyle.Saturation:
                    this.DrawStyleSaturation(ref array);
                    break;
                case DrawStyle.Brightness:
                    this.DrawStyleLuminance(ref array);
                    break;
                case DrawStyle.Red:
                    this.DrawStyleRed(ref array);
                    break;
                case DrawStyle.Green:
                    this.DrawStyleGreen(ref array);
                    break;
                case DrawStyle.Blue:
                    this.DrawStyleBlue(ref array);
                    break;
            }

            Manager.ImageCompositor.UpdateTexture(this.CurrentTextureName, array);
        }

        /// <summary>
        /// Draws this control in the style blue.
        /// </summary>
        /// <param name="map">The map to update.</param>
        private void DrawStyleBlue(ref ColorMap map)
        {
            var blue = this.rgba.B;
            for (var x = 0; x < map.Width; x++)
            {
                for (var y = 0; y < map.Height; y++)
                {
                    // red = x, green = y , blue is constant
                    var red = Round((x / map.Width) * 255);
                    var green = Round(255 - (255 * (double)y / (map.Height - 4)));

                    map.Set(x, y, new GUIColor((byte)red, (byte)green, blue));
                }
            }
        }

        /// <summary>
        /// Draws this control in the style green.
        /// </summary>
        /// <param name="map">The map to update.</param>
        private void DrawStyleGreen(ref ColorMap map)
        {
            var green = this.rgba.G;
            for (var x = 0; x < map.Width; x++)
            {
                for (var y = 0; y < map.Height; y++)
                {
                    // red = x, green = constant , blue = y
                    var red = Round((x / map.Width) * 255);
                    var blue = Round(255 - (255 * (double)y / (map.Height - 4)));

                    map.Set(x, y, new GUIColor((byte)red, green, (byte)blue));
                }
            }
        }

        /// <summary>
        /// Draws this control in the style red.
        /// </summary>
        /// <param name="map">The map to update.</param>
        private void DrawStyleRed(ref ColorMap map)
        {
            var red = this.rgba.G;
            for (var x = 0; x < map.Width; x++)
            {
                for (var y = 0; y < map.Height; y++)
                {
                    // red = constant, green = x , blue = y
                    var green = Round((x / map.Width) * 255);
                    var blue = Round(255 - (255 * (double)y / (map.Height - 4)));

                    map.Set(x, y, new GUIColor(red, (byte)green, (byte)blue));
                }
            }
        }

        /// <summary>
        /// Draws this control in the style luminance.
        /// </summary>
        /// <param name="map">The map to update.</param>
        private void DrawStyleLuminance(ref ColorMap map)
        {
            var hslStart = new HSL();
            var hslEnd = new HSL();
            hslStart.L = this.hsl.L;
            hslEnd.L = this.hsl.L;
            hslStart.S = 1.0;
            hslEnd.S = 0.0;

            for (var x = 0; x < map.Width; x++)
            {
                for (var y = 0; y < map.Height; y++)
                {
                    // Calculate Hue at this line (Saturation and Luminance are constant)
                    hslStart.H = (double)x / map.Width;
                    hslEnd.H = hslStart.H;

                    var rgbStart = AdobeColors.HSLToRGB(hslStart);
                    var rgbEnd = AdobeColors.HSLToRGB(hslEnd);

                    var lerpValue = y / map.Height;

                    var rgbValue = AdobeColors.Lerp(rgbStart, rgbEnd, lerpValue);
                    map.Set(x, y, rgbValue);
                }
            }
        }

        /// <summary>
        /// Draws this control in the style saturation.
        /// </summary>
        /// <param name="map">The map to update.</param>
        private void DrawStyleSaturation(ref ColorMap map)
        {
            var hslStart = new HSL();
            var hslEnd = new HSL();
            hslStart.S = this.hsl.S;
            hslEnd.S = this.hsl.S;
            hslStart.L = 1.0;
            hslEnd.L = 0.0;

            for (var x = 0; x < map.Width; x++)
            {
                for (var y = 0; y < map.Height; y++)
                {
                    // Calculate Hue at this line (Saturation and Luminance are constant)
                    hslStart.H = (double)x / map.Width;
                    hslEnd.H = hslStart.H;

                    var rgbStart = AdobeColors.HSLToRGB(hslStart);
                    var rgbEnd = AdobeColors.HSLToRGB(hslEnd);

                    var lerpValue = y / map.Height;

                    var rgbValue = AdobeColors.Lerp(rgbStart, rgbEnd, lerpValue);
                    map.Set(x, y, rgbValue);
                }
            }
        }

        /// <summary>
        /// Draws this control in the style hue.
        /// </summary>
        /// <param name="map">The map to update.</param>
        private void DrawStyleHue(ref ColorMap map)
        {
            var hslStart = new HSL();
            var hslEnd = new HSL();

            hslStart.H = this.hsl.H;
            hslEnd.H = this.hsl.H;

            hslStart.S = 0;
            hslEnd.S = 1;

            for (var y = 0; y < map.Height; y++)
            {
                hslStart.L = (float)y / map.Height;
                hslEnd.L = hslStart.L;

                var rgbStart = AdobeColors.HSLToRGB(hslStart);
                var rgbEnd = AdobeColors.HSLToRGB(hslEnd);

                for (var x = 0; x < map.Width; x++)
                {
                    var lerpValue = 1 - (x / (float)map.Width);

                    // System.Diagnostics.Debug.WriteLine(lerpValue);
                    var rgbValue = AdobeColors.Lerp(rgbStart, rgbEnd, lerpValue);
                    map.Set(x, y, rgbValue);
                }
            }
        }

        /// <summary>
        /// Sets the indicator position.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        private void SetIndicatorPosition(DVector2 newValue)
        {
            var x = Config.Width * newValue.X;
            if (x < 0)
            {
                x = 0;
            }

            if (x > Config.Width)
            {
                x = Config.Width;
            }

            var y = Config.Height * newValue.Y;
            if (y < 0)
            {
                y = 0;
            }

            if (y > Config.Height)
            {
                y = Config.Height;
            }

            this.Indicator.Config.PositionX = x;
            this.Indicator.Config.PositionY = y;
        }

        /// <summary>
        /// Kind of self explanatory, I really need to look up the .NET function that does this.
        /// </summary>
        /// <param name="val">Double value to be rounded to an integer.</param>
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
    }
}
