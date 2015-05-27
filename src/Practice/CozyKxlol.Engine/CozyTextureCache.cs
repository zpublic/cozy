using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Engine
{
    public class CozyTextureCache : IDisposable
    {
        private Dictionary<string, CozyTexture> Textures = new Dictionary<string,CozyTexture>();

        public CozyTexture AddImage(string path)
        {
            CozyTexture result = null;
            if(Textures.ContainsKey(path))
            {
                result = Textures[path];
            }
            else
            {
                result = new CozyTexture(path);
                Textures[path] = result;
            }
            return result;
        }

        public void Dispose()
        {
            foreach(var obj in Textures)
            {
                obj.Value.Dispose();
            }
        }
    }
}
