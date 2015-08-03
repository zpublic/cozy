using CozyAnywhere.Protocol;
using System;
using System.Collections.Generic;
using System.Reflection;
using PluginHelper;

namespace CozyAnywhere.ClientCore
{
    public partial class AnywhereClient
    {
        public List<string> EnumPluginFolder()
        {
            string path         = @"./Plugins/";
            return PluginFileHelper.EnumPluginFolder(path);
        }

        public List<Tuple<string, Assembly>> LoadPlugins(List<string> files)
        {
            var result = new List<Tuple<string, Assembly>>();
            foreach (var filename in files)
            {
                if (filename.StartsWith("CozyAnywhere.Plugin.") && filename.EndsWith(".dll"))
                {
                    string ns                   = filename.Substring(0, filename.Length - 4);
                    Assembly assembly           = Assembly.LoadFrom(filename);
                    Type loadhelper             = assembly.GetType(ns + ".LoadHelper");
                    IPluginLoadHelper helper    = (IPluginLoadHelper)Activator.CreateInstance(loadhelper, null);
                    string pluginName           = helper.PluginName;
                    result.Add(Tuple.Create(ns + "." + pluginName, assembly));
                }
            }
            return result;
        }
    }
}
