using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel.Tool
{
    public class PixelToolContainer
    {
        private static PixelToolContainer instance { get; set; } = new PixelToolContainer();
        public static PixelToolContainer Instance { get { return instance; } }

        private Dictionary<string, PixelToolBase> PixelToolDict { get; set; } 
            = new Dictionary<string, PixelToolBase>();

        public void RegisterPixelTool(string key, PixelToolBase tool)
        {
            PixelToolDict[key] = tool;
        }

        public void UnregisterPixelTool(string key)
        {
            if(PixelToolDict.ContainsKey(key))
            {
                PixelToolDict.Remove(key);
            }
        }

        public PixelToolBase GetPixelTool(string key)
        {
            if (PixelToolDict.ContainsKey(key))
            {
                return PixelToolDict[key];
            }
            return null;
        }
    }
}
