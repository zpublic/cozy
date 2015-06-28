using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;

namespace CozyKxlol.Kxlol.Manager
{
    public class HappinessPlayerTextureManager
    {
        private Dictionary<uint, CozyTexture> TextureDictionary = new Dictionary<uint, CozyTexture>();

        public void Add(uint id, CozyTexture texture)
        {
            TextureDictionary[id] = texture;
        }

        public void Remove(uint id)
        {
            if(TextureDictionary.ContainsKey(id))
            {
                TextureDictionary.Remove(id);
            }
        }

        public CozyTexture Get(uint id)
        {
            if(TextureDictionary.ContainsKey(id))
            {
                return TextureDictionary[id];
            }
            return null;
        }

        public void Modify(uint id, CozyTexture texture)
        {
            if(TextureDictionary.ContainsKey(id))
            {
                TextureDictionary[id] = texture;
            }
        }
    }
}
