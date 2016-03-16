using CozyLauncher.PluginBase;
using System.Collections.Generic;

namespace CozyLauncher.Core.Plugin
{
    public class CppPluginWrapper : IPlugin
    {
        CppPluginLoader plugins_;
        int id_;

        public CppPluginWrapper(CppPluginLoader plugins, int id)
        {
            plugins_ = plugins;
            id_ = id;
        }

        public PluginInfo Init(PluginInitContext context)
        {
            return plugins_.Init(id_, context);
        }

        public List<Result> Query(Query query)
        {
            return plugins_.Query(id_, query);
        }

        public void ShowPanel(string command)
        {
            plugins_.ShowPanel(id_, command);
        }

        public void RunCommand(string command)
        {
            plugins_.RunCommand(id_, command);
        }
    }
}
