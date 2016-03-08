using System.Collections.Generic;
using CozyLauncher.PluginBase;
using System.Diagnostics;

namespace CozyLauncher.Plugin.Core
{
    public class Main : BasePlugin
    {
        private PluginInitContext _context { get; set; }

        public override PluginInfo Init(PluginInitContext context)
        {
            _context = context;
            var info = new PluginInfo()
            {
                Keyword = "*",
            };
            return info;
        }

        public override List<Result> Query(Query query)
        {
            var rl = new List<Result>();

            if (query.RawQuery == "exit")
            {
                var r = new Result()
                {
                    Title       = "Exit",
                    SubTitle    = "关闭",
                    IcoPath     = "[Res]:exit",
                    Score       = 100,
                    Action      = e =>
                    {
                        _context.Api.CloseApp();
                        return true;
                    }
                };

                rl.Add(r);
            }
            else if(query.RawQuery == "config" || query.RawQuery == "setting")
            {
                var r = new Result()
                {
                    Title       = "Config / Setting",
                    SubTitle    = "设置",
                    IcoPath     = "[Res]:setting",
                    Score       = 100,
                    Action = e  =>
                    {
                        _context.Api.HideAndClear();
                        _context.Api.ShowPanel("config");
                        return true;
                    }
                };

                rl.Add(r);
            }
            else if (query.RawQuery == "about")
            {
                var r = new Result()
                {
                    Title       = "About",
                    SubTitle    = "关于",
                    IcoPath     = "[Res]:help",
                    Score       = 100,
                    Action      = e =>
                    {
                        _context.Api.HideAndClear();
                        _context.Api.ShowPanel("about");
                        _context.Api.Clear();
                        return true;
                    }
                };

                rl.Add(r);
            }
            else if (query.RawQuery == "guide" || query.RawQuery == "help")
            {
                var r = new Result()
                {
                    Title = "Guide / Help",
                    SubTitle = "向导",
                    IcoPath = "[Res]:help",
                    Score = 100,
                    Action = e =>
                    {
                        _context.Api.HideAndClear();
                        _context.Api.ShowPanel("guide");
                        _context.Api.Clear();
                        return true;
                    }
                };

                rl.Add(r);
            }
            else if(query.RawQuery == "cozy")
            {
                var r = new Result()
                {
                    Title = "Cozy",
                    SubTitle = "主页",
                    IcoPath = "[Res]:help",
                    Score = 100,
                    Action = e =>
                    {
                        Process.Start(@"http://cozy.laorouji.com");
                        _context.Api.HideAndClear();
                        return true;
                    }
                };

                rl.Add(r);
            }
            else if(query.RawQuery == "update")
            {
                var r = new Result()
                {
                    Title = "Update",
                    SubTitle = "更新",
                    IcoPath = "",
                    Score = 100,
                    Action = e =>
                    {
                        _context.Api.HideAndClear();
                        _context.Api.Update();
                        return true;
                    }
                };

                rl.Add(r);
            }

            return rl;
        }
    }
}
