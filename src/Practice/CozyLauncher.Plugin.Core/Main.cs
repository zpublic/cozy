using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyLauncher.PluginBase;

namespace CozyLauncher.Plugin.Core
{
    public class Main : IPlugin
    {
        private PluginInitContext _context { get; set; }

        public PluginInfo Init(PluginInitContext context)
        {
            _context = context;
            var info = new PluginInfo()
            {
                Keyword = "*",
            };
            return info;
        }

        public List<Result> Query(Query query)
        {
            var rl = new List<Result>();

            if (query.RawQuery == "exit")
            {
                var r = new Result()
                {
                    Title       = "Exit",
                    SubTitle    = "关闭",
                    IcoPath     = "exe",
                    Score       = 100,
                    Action      = e =>
                    {
                        _context.Api.CloseApp();
                        return true;
                    }
                };

                rl.Add(r);
            }
            else if(query.RawQuery == "config")
            {
                var r = new Result()
                {
                    Title       = "Config",
                    SubTitle    = "设置",
                    IcoPath     = "exe",
                    Score       = 100,
                    Action = e  =>
                    {
                        _context.Api.Config();
                        return true;
                    }
                };
            }

            return rl;
        }
    }
}
