using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Engine
{
    public class CozyContentManager : ContentManager
    {
        public CozyContentManager(IServiceProvider serviceProvider)
            :base(serviceProvider)
        {

        }

        public CozyContentManager(IServiceProvider serviceProvider, string rootDirectory)
            :base(serviceProvider, rootDirectory)
        {

        }

        public T TryLoad<T>(string filename)
        {
            T result            = default(T);

            try
            {
                result = (T)ReadAsset<T>(filename, null);
            }
            catch (ContentLoadException e)
            {
                string assetPath = Path.Combine(RootDirectory, filename);

                if (typeof(T) == typeof(string))
                {
                    using (var streamContent = File.OpenRead(assetPath))
                    {
                        using (StreamReader reader = new StreamReader(streamContent, Encoding.UTF8))
                        {
                            result = (T)(object)reader.ReadToEnd();
                        }
                    }
                }
                else if (typeof(T) == typeof(Texture2D) && Path.HasExtension(assetPath))
                {
                    var service = (IGraphicsDeviceService)ServiceProvider.GetService(typeof(IGraphicsDeviceService));
                    using (var streamContent = File.OpenRead(assetPath))
                    {
                        result = (T)(object)Texture2D.FromStream(service.GraphicsDevice, streamContent);
                    }
                }
                else
                {
                    throw;
                }
            }
            return result;
        }
    }
}
