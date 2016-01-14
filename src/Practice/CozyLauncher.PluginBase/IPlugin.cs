using System.Collections.Generic;

namespace CozyLauncher.PluginBase
{
    public interface IPlugin
    {
        PluginInfo Init(PluginInitContext context);
        List<Result> Query(Query query);
    }
}
