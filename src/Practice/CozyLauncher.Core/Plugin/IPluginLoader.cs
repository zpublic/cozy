using CozyLauncher.PluginBase;
using System.Collections.Generic;

namespace CozyLauncher.Core.Plugin
{
    public interface IPluginLoader
    {
        List<IPlugin> GetPlugins(PluginInitContext context);
    }
}
