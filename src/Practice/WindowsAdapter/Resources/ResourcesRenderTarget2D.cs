// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourcesRenderTarget2D.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the ResourcesRenderTarget2D type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UWindows.Resources
{
    using GUI4UFramework.Management;

    using Microsoft.Xna.Framework.Graphics;

    /// <summary>Is a resource pool containing render-targets.</summary>
    public class ResourcesRenderTarget2D : ResourcePool<RenderTarget2D>
    {
        /// <summary>
        /// The graphics device.
        /// </summary>
        private readonly GraphicsDevice device;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcesRenderTarget2D"/> class.
        /// </summary>
        /// <param name="device">The device.</param>
        public ResourcesRenderTarget2D(GraphicsDevice device)
        {
            this.device = device;
        }

        /// <summary>Creates the render target.</summary>
        /// <param name="preferredName">Name of the preferred.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="mipMap">If the rendertarget will use MipMapping.</param>
        /// <returns>The name of the location of the created render-target.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "mip", Justification = "MipMap is a normal function.")]
        public string CreateRenderTarget(string preferredName, int width, int height, bool mipMap)
        {
            var renderTarget = new RenderTarget2D(
                                                this.device,
                                                width,
                                                height,
                                                mipMap,          
                                                SurfaceFormat.Color, 
                                                DepthFormat.Depth24);
            
            var resourceName = UniqueNameCreator.GetUniqueName(preferredName);

            Resources.Add(resourceName, renderTarget);

            return resourceName;
        }

        /// <summary>
        /// Debugs this instance. Let this class spit out debug info.
        /// </summary>
        public override void Debug()
        {
            System.Diagnostics.Debug.WriteLine("Library for RenderTargets contains : ");
            foreach (var resource in this.Resources)
            {
                var value = resource.Value;
                var s = string.Format("{0},{1}x{2},{3},{4},{5}", value.Name, value.Width, value.Height, value.Format, value.Bounds, value.LevelCount);
                System.Diagnostics.Debug.WriteLine("[" + resource.Key + "] : " + s);
            }
        }
    }
}