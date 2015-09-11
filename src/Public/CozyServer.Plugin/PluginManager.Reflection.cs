using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace CozyServer.Plugin
{
    public partial class PluginManager
    {
        public void AddPlugin(IPlugin plugin)
        {
            PluginSet.Add(plugin);
        }

        public bool TryAddPluginWithFilename(string filename)
        {
            bool result = false;

            if(filename != null && filename.Length > 0)
            {
                if(Path.GetExtension(filename) == @".dll")
                {
                    if (PluginFilter != null )
                    {
                        if(!PluginFilter(filename))
                        {
                            return false;
                        }
                    }

                    Assembly assembly = Assembly.LoadFrom(filename);
                    if(assembly != null)
                    {
                        var pluginTypes = assembly.GetTypes();
                        foreach(var obj in pluginTypes)
                        {
                            if(typeof(IPlugin).IsAssignableFrom(obj))
                            {
                                var plugin = (IPlugin)Activator.CreateInstance(obj);
                                AddPlugin(plugin);
                                result = true;
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
