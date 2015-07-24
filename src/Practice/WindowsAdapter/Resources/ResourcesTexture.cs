// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourcesTexture.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ResourcesTexture type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UWindows.Resources
{
    using System;

    using GUI4UFramework.Colors;
    using GUI4UFramework.Management;

    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// The resource pool containing textures.
    /// </summary>
    public class ResourcesTexture : ResourcePool<Texture2D>
    {
        /// <summary>
        /// The graphics device.
        /// </summary>
        private readonly GraphicsDevice device;

        /// <summary>
        /// The content manager.
        /// </summary>
        private readonly ContentManager contentManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcesTexture"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="contentManager">The content manager.</param>
        public ResourcesTexture(GraphicsDevice device, ContentManager contentManager)
        {
            this.device = device;
            this.contentManager = contentManager;
        }

        /// <summary>Creates the texture.</summary>
        /// <param name="name">The name of the texture.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="color">The color of texture.</param>
        /// <returns>The created texture.</returns>
        /// <exception cref="ArgumentNullException">Name is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">width can not be smaller then 1 and height can not be smaller then 1. </exception>
        public string Create(string name, int width, int height, GUIColor color)
        {
#if DEBUG
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (width < 1)
            {
                throw new ArgumentOutOfRangeException("width", "width can not be smaller then 1");
            }

            if (height < 1)
            {
                throw new ArgumentOutOfRangeException("height", "height can not be smaller then 1");
            }
#endif

            var texture = new Texture2D(this.device, width, height, false, SurfaceFormat.Color);
            var count = width * height;
            var array = new GUIColor[count];

            for (var i = 0; i < array.Length; i++)
            {
                array[i] = color;
            }

            texture.SetData(array);

            var resourceName = UniqueNameCreator.GetUniqueName(name);
            texture.Name = resourceName;

            Resources.Add(resourceName, texture);

            return resourceName;
        }

        /// <summary>Creates a image from given URL.</summary>
        /// <param name="preferredName">Name of the preferred.</param>
        /// <param name="imageLocation">The image location on disk.</param>
        /// <returns>The location of the created resource.</returns>
        /// <exception cref="ArgumentNullException">PreferredName or imageLocation. </exception>
        public string CreateFromUrl(string preferredName, string imageLocation)
        {
#if DEBUG
            if (string.IsNullOrEmpty(preferredName))
            {
                throw new ArgumentNullException("preferredName");
            }

            if (imageLocation == null)
            {
                throw new ArgumentNullException("imageLocation");
            }

            if (string.IsNullOrEmpty(imageLocation.ToString()))
            {
                throw new ArgumentNullException("imageLocation");
            }
#endif

            var texture = this.contentManager.Load<Texture2D>(imageLocation.ToString());
            var resourceName = UniqueNameCreator.GetUniqueName(preferredName);
            Resources.Add(resourceName, texture);

            return resourceName;
        }

        /// <summary>
        /// Updates the item with given name with new color data.
        /// </summary>
        /// <param name="name">The name of the resource.</param>
        /// <param name="colorData">The colorData to set.</param>
        public void Update(string name, GUIColor[] colorData)
        {
            var texture = Resources[name];
            texture.SetData(colorData);
        }

        /// <summary>
        /// Debugs this instance. Let this class spit out debug info.
        /// </summary>
        public override void Debug()
        {
            System.Diagnostics.Debug.WriteLine("Library for Texture2D contains : ");
            foreach (var resource in this.Resources)
            {
                var value = resource.Value;
                var s = string.Format("{0},{1}x{2},{3},{4},{5}", value.Name, value.Width, value.Height, value.Format, value.Bounds, value.LevelCount);
                System.Diagnostics.Debug.WriteLine("[" + resource.Key + "] : " + s);
            }
        }
    }
}