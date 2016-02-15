using CozyLauncher.PluginBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Plugin.Guide
{
    public class Main : BasePlugin
    {
        private PluginInitContext _context { get; set; }

        public override PluginInfo Init(PluginInitContext context)
        {
            _context = context;
            var info = new PluginInfo()
            {
                Keyword = "-",
            };
            return info;
        }

        public override void ShowPanel(string command)
        {
            if (command == "guide")
            {
                GuideWindow w = new GuideWindow();
                w.Show();
            }
        }
    }
}
