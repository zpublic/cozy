using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyLauncher.PluginBase;
using CozyLauncher.Infrastructure.Http;

namespace CozyLauncher.Plugin.GFW
{
    public class Main : BasePlugin
    {
        private PluginInitContext context_;

        public override PluginInfo Init(PluginInitContext context)
        {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "gfw";
            return info;
        }

        public override List<Result> Query(Query query)
        {
            if(query.RawQuery == "gfw")
            {
                var rl = new List<Result>();

                bool canGoogle = false;
                bool canBaidu = false;

                try
                {
                    var htmlgoogle = HttpDownload.HttpGetString(@"https://www.google.com");
                    canGoogle = true;
                }
                catch(Exception e)
                {

                }

                try
                {
                    var htmlbaidu = HttpDownload.HttpGetString(@"https://www.baidu.com");
                    canBaidu = true;
                }
                catch (Exception e)
                {
                }

                rl.Add(new Result()
                {
                    Title = "Where am i?",
                    IcoPath = "app",
                    Action = x => { return true; }
                });
                return rl;
            }

            return null;
        }
    }
}
