using CozyLauncher.PluginBase;
using FluentScheduler;

namespace CozyLauncher.Plugin.InfoCollect
{
    public class Main : BasePlugin
    {
        private PluginInitContext context_;

        public override PluginInfo Init(PluginInitContext context)
        {
            TaskManager.Initialize(new TaskRegistry());

            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "-";
            return info;
        }
    }
}
