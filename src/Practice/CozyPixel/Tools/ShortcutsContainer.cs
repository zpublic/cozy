using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CozyPixel.Tools
{
    public class ShortcutsContainer
    {
        private Dictionary<Keys, PixelToolBase> ToolShortcutsDict { get; set; } 
            = new Dictionary<Keys, PixelToolBase>();

        public PixelToolBase GetToolByShortcuts(Keys key)
        {
            if (ToolShortcutsDict.ContainsKey(key))
            {
                return ToolShortcutsDict[key];
            }
            return null;
        }

        public void RegisterShortcutsKey(Keys key, PixelToolBase tool)
        {
            ToolShortcutsDict[key] = tool;
        }

        public void UnregisterShortcutsKey(Keys key)
        {
            if(ToolShortcutsDict.ContainsKey(key))
            {
                ToolShortcutsDict.Remove(key);
            }
        }
    }
}
