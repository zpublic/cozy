using CozyLauncher.PluginBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Plugin.Ip
{
    public class Main : IPlugin
    {
        private PluginInitContext context_;

        public PluginInfo Init(PluginInitContext context)
        {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "ip";
            return info;
        }

        public List<Result> Query(Query query)
        {
            if (query.RawQuery == "ip")
            {

                string hostname = Dns.GetHostName();
                IPHostEntry localhost = Dns.GetHostByName(hostname);
                IPAddress localaddr = localhost.AddressList[0];

                if (localhost.AddressList.Count() > 0)
                {
                    var rl = new List<Result>();
                    foreach (var ip in localhost.AddressList)
                    {
                        var r = new Result();
                        r.Title = "Local IP Address";
                        r.SubTitle = ip.ToString();
                        r.IcoPath = "sys";
                        r.Score = 60;
                        r.Action = e =>
                        {
                            context_.Api.HideApp();
                            return true;
                        };
                        rl.Add(r);
                    }
                    return rl;
                }
            }
            return null;
        }
    }
}
