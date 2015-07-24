// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorMapDrawer.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ColorMapDrawer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Colors
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using GUI4UFramework.Graphics;

    /// <summary>Draws things inside ColorMaps like :
    /// - lines
    /// - rectangles
    /// - filled rectangles
    /// - corners
    /// - circles.</summary>
    public static class ColorMapDrawer
    {
        /// <summary>Creates the circle texture.</summary>
        /// <param name="radius">The radius.</param>
        /// <param name="borderWidth">Width of the border.</param>
        /// <param name="borderInnerTransitionWidth">Width of the border inner transition.</param>
        /// <param name="borderOuterTransitionWidth">Width of the border outer transition.</param>
        /// <param name="color">The color.</param>
        /// <param name="borderColor">Color of the border.</param>
        /// <returns>A array with a representation of a circle inside.</returns>
        public static GUIColor[] CreateCircleTexture(int radius, int borderWidth, int borderInnerTransitionWidth, int borderOuterTransitionWidth, GUIColor color, GUIColor borderColor)
        {
            int x;
            var y = -1;
            var diameter = (radius + 2) * 2;

            // DVector2 center = new DVector2(0, 0);
            var center = new DVector2((diameter - 1) / 2f, (diameter - 1) / 2f);

            var colors = new GUIColor[diameter * diameter];

            for (var i = 0; i < colors.Length; i++)
            {
                if (i % diameter == 0)
                {
                    y += 1;
                }

                x = i % diameter;

                // var distance = new DVector2(Math.Abs(center.X - x), Math.Abs(center.Y - y));
                var diff = new DVector2(x, y) - center;
                var length = diff.Length; // distance.Length();

                if (length > radius)
                {
                    colors[i] = GUIColor.Transparent();
                }
                else if (length >= radius - borderOuterTransitionWidth)
                {
                    var transitionAmount = (length - (radius - borderOuterTransitionWidth)) / borderOuterTransitionWidth;
                    transitionAmount = 255 * (1 - transitionAmount);
                    colors[i] = new GUIColor(borderColor.R, borderColor.G, borderColor.B, (byte)transitionAmount);
                }
                else if (length > radius - (borderWidth + borderOuterTransitionWidth))
                {
                    colors[i] = borderColor;
                }
                else if (length >= radius - (borderWidth + borderOuterTransitionWidth + borderInnerTransitionWidth))
                {
                    var transitionAmount = (length
                                            - (radius
                                               - (borderWidth + borderOuterTransitionWidth + borderInnerTransitionWidth)))
                                           / (borderInnerTransitionWidth + 1);
                    colors[i] = new GUIColor(
                        (byte)Lerp(color.R, borderColor.R, transitionAmount),
                        (byte)Lerp(color.G, borderColor.G, transitionAmount),
                        (byte)Lerp(color.B, borderColor.B, transitionAmount));
                }
                else
                {
                    colors[i] = color;
                }
            }

            return colors;
        }

        /// <summary>Creates a line with given height and width.</summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="color">The color.</param>
        /// <returns>A color array containing a representation of a line.</returns>
        public static GUIColor[] CreateFlat(int width, int height, GUIColor color)
        {
            // Texture2D texture2D = new Texture2D(graphicsDevice, 2, lineWidth + 2);
            var count = width * height;
            var colorArray = new GUIColor[count];

            for (var i = 0; i < count; i++)
            {
                colorArray[i] = color;
            }

            return colorArray;
        }

        /// <summary>Creates the a texture with a rectangle with given color and border.</summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="borderWidth">Width of the border.</param>
        /// <param name="color">The color.</param>
        /// <param name="borderColor">Color of the border.</param>
        /// <returns>A color map with a rectangle image in the colorMap.</returns>
        public static ColorMap CreateRectangleColorMap(int width, int height, int borderWidth, GUIColor color, GUIColor borderColor)
        {
            var colorMap = new ColorMap(width, height);

            var left = borderWidth;
            var right = width - borderWidth;
            var top = borderWidth;
            var bottom = height - borderWidth;

            GUIColor colorToUse;

            for (var wY = 0; wY < height; wY++)
            {
                for (var wX = 0; wX < width; wX++)
                {
                    colorToUse = color;

                    if (wX < left || wX >= right)
                    {
                        colorToUse = borderColor;
                    }

                    if (wY < top || wY >= bottom)
                    {
                        colorToUse = borderColor;
                    }

                    colorMap.Set(wX, wY, colorToUse);
                }
            }

            return colorMap;
        }

        /// <summary>Creates a rounded rectangle texture with given color and border.</summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="borderWidth">Width of the border.</param>
        /// <param name="cornerSize">Size of the corner.</param>
        /// <param name="fillColor">Color of the fill.</param>
        /// <param name="borderColor">Color of the border.</param>
        /// <returns>A color-map with a rounded rectangle inside.</returns>
        public static ColorMap CreateRoundedRectangleColorMap(int width, int height, int borderWidth, int cornerSize, GUIColor fillColor, GUIColor borderColor)
        {
            var colorMap = new ColorMap(width, height);

            if (cornerSize > width / 2)
            {
                cornerSize = width / 2;
            }

            if (cornerSize > height / 2)
            {
                cornerSize = height / 2;
            }

            // draw top horizontal lines
            for (var y = 0; y < cornerSize; y++)
            {
                if (y < borderWidth)
                {
                    DrawHorizontalLineOnColorMap(ref colorMap, y, cornerSize, width - cornerSize, borderColor);
                }
                else
                {
                    DrawHorizontalLineOnColorMap(ref colorMap, y, cornerSize, width - cornerSize, fillColor);
                }
            }

            // draw bottom horizontal lines
            for (var y = height - cornerSize; y < height; y++)
            {
                if (y < height - borderWidth)
                {
                    DrawHorizontalLineOnColorMap(ref colorMap, y, cornerSize, width - cornerSize, fillColor);
                }
                else
                {
                    DrawHorizontalLineOnColorMap(ref colorMap, y, cornerSize, width - cornerSize, borderColor);
                }
            }

            // draw left vertical lines
            for (var x = 0; x < cornerSize; x++)
            {
                if (x < borderWidth)
                {
                    DrawVerticalLineOnColorMap(ref colorMap, x, cornerSize, height - cornerSize, borderColor);
                }
                else
                {
                    DrawVerticalLineOnColorMap(ref colorMap, x, cornerSize, height - cornerSize, fillColor);
                }
            }

            // draw right vertical lines
            for (var x = width - cornerSize; x < width; x++)
            {
                if (x < width - borderWidth)
                {
                    DrawVerticalLineOnColorMap(ref colorMap, x, cornerSize, height - cornerSize, fillColor);
                }
                else
                {
                    DrawVerticalLineOnColorMap(ref colorMap, x, cornerSize, height - cornerSize, borderColor);
                }
            }

            // draw internal rectangle
            DrawRectangleOnColorMap(ref colorMap, cornerSize, cornerSize, width - cornerSize, height - cornerSize, fillColor);

            // create the bottom right corner
            var bottomRightCorner = CreateHighQualityRoundedCornerColorMap(cornerSize - borderWidth, cornerSize, fillColor, borderColor);
            PlaceSourceColorMapIntoTargetColorMap(bottomRightCorner, ref colorMap, width - cornerSize, height - cornerSize);

            // create the bottom left corner
            var bottomLeftCorner = FlipColorMapHorizontal(bottomRightCorner);
            PlaceSourceColorMapIntoTargetColorMap(bottomLeftCorner, ref colorMap, 0, height - cornerSize);

            // create the top right corner
            var topRightCorner = FlipColorMapHVertical(bottomRightCorner);
            PlaceSourceColorMapIntoTargetColorMap(topRightCorner, ref colorMap, width - cornerSize, 0);

            // create the bottom left corner
            var topLeftCorner = FlipColorMapHorizontal(topRightCorner);
            PlaceSourceColorMapIntoTargetColorMap(topLeftCorner, ref colorMap, 0, 0);

            return colorMap;
        }

        /// <summary>
        /// Creates a empty rectangle with given color.
        /// </summary>
        /// <param name="colorMap">The color array.</param>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image.</param>
        /// <param name="colorToUse">The color to use.</param>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private static void CreateEmptyRectangleColorMap(ref ColorMap colorMap, int imageWidth, int imageHeight, GUIColor colorToUse)
        {
            for (var wY = 0; wY < imageHeight; wY++)
            {
                for (var wX = 0; wX < imageWidth; wX++)
                {
                    colorMap.Set(wX, wY, colorToUse);
                }
            }
        }

        /// <summary>Returns a value between start and end by look at the percentage to determine how far we are towards end.</summary>
        /// <param name="start">The start value.</param>
        /// <param name="end">The end value.</param>
        /// <param name="percentage">The percentage to sit in between.</param>
        /// <returns>A value between start and end , using percentage.</returns>
        public static float Lerp(float start, float end, float percentage)
        {
            return start + ((end - start) * percentage);
        }

        /// <summary>
        /// Sets the pixel in (x, y) coordinates, where (0,0) is at the center of the image.
        /// Using the code from https://banu.com/blog/7/drawing-circles/ .
        /// </summary>
        /// <param name="colorMap">The array to change a pixel in.</param>
        /// <param name="imageWidth">Width of the array.</param>
        /// <param name="imageHeight">Height of the array.</param>
        /// <param name="x">The x position in the array.</param>
        /// <param name="y">The y position in the array.</param>
        /// <param name="colorToUse">The color to set.</param>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private static void SetPixel(ref ColorMap colorMap, int imageWidth, int imageHeight, float x, float y, GUIColor colorToUse)
        {
            // all is centered , so shift it back t bitmap coordinates

            // shift x to bitmap location
            var tempX = (imageWidth / 2) + (int)x;
            if (tempX >= imageWidth)
            {
                return;
            }

            if (tempX <= 0)
            {
                return;
            }

            // shift y to bitmap location
            var tempY = (imageHeight / 2) + (int)y;
            if (tempY >= imageHeight)
            {
                return;
            }

            if (tempY <= 0)
            {
                return;
            }

            // get the location into my array
            colorMap.Set(tempX, tempY, colorToUse);
        }

        /// <summary>Resizes the color-map to the given target size.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="targetWidth">Width of the target.</param>
        /// <param name="targetHeight">Height of the target.</param>
        /// <returns>A color-map with given target size.</returns>
        private static ColorMap ResizeImage(ColorMap sourceArray, int targetWidth, int targetHeight)
        {
            var targetArray = new ColorMap(targetWidth, targetHeight);

            // walk trough every pixel on the target
            // find out the area that the a pixel on target is covering on the source
            // average the source area into one pixel to be used on the target
            //
            // the way i am looking at it is that you have a target (emitting) rectangle standing on a table
            // and somewhere further in the distance you have a larger source (receiving) rectangle standing on a table
            // we are shooting beams trough the target (emitting) rectangle
            // those hit the source (receiving) rectangle at a given position
            // we use those hit-points to calculate what the color will be in the target rectangle
            for (int targetX = 0; targetX < targetWidth; targetX++)
            {
                for (int targetY = 0; targetY < targetHeight; targetY++)
                {
                    // we walk trough every point in the target rectangle

                    // find out, how big the source rectangle is
                    var sourceAreaWidth = (float)sourceArray.Width / targetWidth;
                    var sourceAreaHeight = (float)sourceArray.Height / targetHeight;

                    // find the center of the source rectangle (receiver) on the table
                    var percentageTargetX = (float)targetX / targetWidth;
                    var percentageTargetY = (float)targetY / targetHeight;
                    var sourceCentreX = Lerp(0, sourceArray.Width, percentageTargetX);
                    var sourceCentreY = Lerp(0, sourceArray.Height, percentageTargetY);

                    // get the array of pixels on the source rectangle , that we will use for averaging my location on the target rectangle
                    var sourceLeft = Math.Floor(sourceCentreX - (sourceAreaWidth * 0.5f));
                    var sourceRight = Math.Ceiling(sourceCentreX + (sourceAreaWidth * 0.5f));
                    var sourceTop = Math.Floor(sourceCentreY - (sourceAreaHeight * 0.5f));
                    var sourceBottom = Math.Ceiling(sourceCentreY + (sourceAreaHeight * 0.5f));

                    // get all the colors in the source rectangle that are targeted
                    var colorToAverage = new List<GUIColor>();
                    for (var parseX = (int)sourceLeft; parseX < sourceRight; parseX++)
                    {
                        for (var parseY = (int)sourceTop; parseY < sourceBottom; parseY++)
                        {
                            if (parseX < 0 || parseX > sourceArray.Width || parseY < 0 || parseY > sourceArray.Height)
                            {
                                continue;
                            }

                            var sourceColor = sourceArray.Get(parseX, parseY);
                            colorToAverage.Add(sourceColor);
                        }
                    }

                    // average those colors into the color that will be used in the target rectangle
                    float r = 0;
                    float g = 0;
                    float b = 0;
                    float a = 0;
                    for (var index = 0; index < colorToAverage.Count; index++)
                    {
                        var clr = colorToAverage[index];
                        r = r + clr.R;
                        g = g + clr.G;
                        b = b + clr.B;
                        a = a + clr.A;
                    }

                    r = r / colorToAverage.Count;
                    g = g / colorToAverage.Count;
                    b = b / colorToAverage.Count;
                    a = a / colorToAverage.Count;

                    targetArray.Set(targetX, targetY, new GUIColor((byte)r, (byte)g, (byte)b, (byte)a));
                }
            }

            return targetArray;
        }

        /// <summary>Creates a rounded corner.
        /// And of course every day greets from  !.</summary>
        /// <param name="innerRadius">The inner radius.</param>
        /// <param name="outerRadius">The outer radius.</param>
        /// <param name="innerColor">Color of the inner.</param>
        /// <param name="outerColor">Color of the outer.</param>
        /// <returns>A color-map with a rounded corner inside.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private static ColorMap CreateRoundedCornerColorMap(int innerRadius, int outerRadius, GUIColor innerColor, GUIColor outerColor)
        {
            int x;
            int y;
            int width = outerRadius;
            int height = outerRadius;

            var array = new ColorMap(width, height);
            for (x = 0; x < +width; x++)
            {
                for (y = 0; y < +height; y++)
                {
                    // get the distance to x,y
                    var r = Math.Sqrt((x * x) + (y * y));

                    if (r < innerRadius)
                    {
                        array.Set(x, y, innerColor);
                    }
                    else if (r >= innerRadius && r < outerRadius)
                    {
                        array.Set(x, y, outerColor);
                    }
                }
            }

            return array;
        }

        /// <summary>Creates a high quality rounded corner color-map.</summary>
        /// <param name="innerRadius">The inner radius.</param>
        /// <param name="outerRadius">The outer radius.</param>
        /// <param name="innerColor">Color of the inner.</param>
        /// <param name="outerColor">Color of the outer.</param>
        /// <returns>A color-map with a high quality rounded corner inside.</returns>
        private static ColorMap CreateHighQualityRoundedCornerColorMap(int innerRadius, int outerRadius, GUIColor innerColor, GUIColor outerColor)
        {
            int x;
            int y;
            const int GrowFactor = 4;
            int width = outerRadius * GrowFactor;
            int height = outerRadius * GrowFactor;

            // create a grown round corner
            var array = new ColorMap(width, height);
            for (x = 0; x < +width; x++)
            {
                for (y = 0; y < +height; y++)
                {
                    // get the distance to x,y
                    var r = Math.Sqrt((x * x) + (y * y));

                    if (r < innerRadius * GrowFactor)
                    {
                        array.Set(x, y, innerColor);
                    }
                    else if (r >= innerRadius * GrowFactor && r < outerRadius * GrowFactor)
                    {
                        array.Set(x, y, outerColor);
                    }
                }
            }

            // and shrink it
            array = ResizeImage(array, outerRadius, outerRadius);

            return array;
        }

        /// <summary>Places the source array into target array.
        /// Its like copy a bitmap into a bitmap at a specific location.</summary>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="targetArray">The target array.</param>
        /// <param name="placeAtX">The place at x.</param>
        /// <param name="placeAtY">The place at y.</param>
        private static void PlaceSourceColorMapIntoTargetColorMap(ColorMap sourceArray, ref ColorMap targetArray, int placeAtX, int placeAtY)
        {
            // get the source pixel
            for (var x = 0; x < sourceArray.Width; x++)
            {
                for (var y = 0; y < sourceArray.Height; y++)
                {
                    var sourcePixel = sourceArray.Get(x, y);

                    targetArray.TrySet(placeAtX + x, placeAtY + y, sourcePixel);
                }
            }
        }

        /// <summary>
        /// Draws a horizontal line on the given color-map.
        /// </summary>
        /// <param name="colorMap">The color map to draw into.</param>
        /// <param name="y">The y position to draw the line.</param>
        /// <param name="startX">The start x to draw the line.</param>
        /// <param name="endX">The end x to draw the line.</param>
        /// <param name="colorToUse">The color to use.</param>
        private static void DrawHorizontalLineOnColorMap(ref ColorMap colorMap, int y, int startX, int endX, GUIColor colorToUse)
        {
            for (var x = startX; x < endX; x++)
            {
                colorMap.Set(x, y, colorToUse);
            }
        }

        /// <summary>
        /// Draws the vertical line on the given color-map.
        /// </summary>
        /// <param name="colorMap">The color map to draw into.</param>
        /// <param name="x">The x position to draw the line.</param>
        /// <param name="startY">The start y to draw the line.</param>
        /// <param name="endY">The end y to draw the line.</param>
        /// <param name="colorToUse">The color to use.</param>
        private static void DrawVerticalLineOnColorMap(ref ColorMap colorMap, int x, int startY, int endY, GUIColor colorToUse)
        {
            for (int y = startY; y < endY; y++)
            {
                colorMap.Set(x, y, colorToUse);
            }
        }

        /// <summary>
        /// Draws a color filled rectangle on given color-map.
        /// </summary>
        /// <param name="colorMap">The color map.</param>
        /// <param name="startX">The start x.</param>
        /// <param name="startY">The start y.</param>
        /// <param name="endX">The end x.</param>
        /// <param name="endY">The end y.</param>
        /// <param name="colorToUse">The color to use.</param>
        private static void DrawRectangleOnColorMap(ref ColorMap colorMap, int startX, int startY, int endX, int endY, GUIColor colorToUse)
        {
            for (var x = startX; x < endX; x++)
            {
                for (var y = startY; y < endY; y++)
                {
                    colorMap.Set(x, y, colorToUse);
                }
            }
        }

        /// <summary>Create a new color-map with a horizontal flip of given color-map.</summary>
        /// <param name="source">The source.</param>
        /// <returns>A horizontal flipped color-map.</returns>
        private static ColorMap FlipColorMapHorizontal(ColorMap source)
        {
            var target = new ColorMap(source.Width, source.Height);
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    var sourcePixel = source.Get(x, y);
                    target.Set((source.Width - 1) - x, y, sourcePixel);
                }
            }

            return target;
        }

        /// <summary>Create a new color-map with a vertical flip of given color-map.</summary>
        /// <param name="source">The source.</param>
        /// <returns>A vertical flipped color-map.</returns>
        private static ColorMap FlipColorMapHVertical(ColorMap source)
        {
            var target = new ColorMap(source.Width, source.Height);
            for (int x = 0; x < source.Width; x++)
            {
                for (int y = 0; y < source.Height; y++)
                {
                    var sourcePixel = source.Get(x, y);
                    target.Set(x, (source.Height - 1) - y, sourcePixel);
                }
            }

            return target;
        }
    }
}
