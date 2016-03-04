using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyLauncher.PluginBase;

namespace CozyLauncher.Plugin.About
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
            if(command == "about")
            {
                var w = new AboutWindow();
                w.ShowDialog();
            }
        }
    }
}
