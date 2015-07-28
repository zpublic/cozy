using System.Collections.Generic;
using PluginHelper;

namespace CozyAnywhere.ServerCore
{
    public partial class AnywhereServer
    {
        public List<string> EnumPluginFolder()
        {
            string path = @"./Plugins/";
            return PluginFileHelper.EnumPluginFolder(path);
        }
    }
}