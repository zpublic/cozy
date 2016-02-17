using CozyLauncher.PluginBase;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace CozyLauncher.Plugin.Ip
{
    public class Main : BasePlugin
    {
        private PluginInitContext context_;

        public override PluginInfo Init(PluginInitContext context)
        {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "ip";
            return info;
        }

        public override List<Result> Query(Query query)
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
                        r.Title = ip.ToString();
                        r.SubTitle = "Copy this number to the clipboard";
                        r.IcoPath = "[Res]:sys";
                        r.Score = 100;
                        r.Action = e =>
                        {
                            context_.Api.HideAndClear();
                            try
                            {
                                Clipboard.SetText(ip.ToString());
                                return true;
                            }
                            catch (System.Runtime.InteropServices.ExternalException)
                            {
                                return false;
                            }
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
