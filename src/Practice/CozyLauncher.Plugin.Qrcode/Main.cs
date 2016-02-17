using CozyLauncher.PluginBase;
using System.Collections.Generic;

namespace CozyLauncher.Plugin.Qrcode
{
    public class Main : BasePlugin
    {
        private PluginInitContext context_;

        public override PluginInfo Init(PluginInitContext context)
        {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "qr";
            return info;
        }

        public override List<Result> Query(Query query)
        {
            if (query.RawQuery.StartsWith("qr ") || query.RawQuery.StartsWith("qrcode "))
            {
                var rl = new List<Result>();
                var r = new Result();
                if (query.RawQuery.StartsWith("qr "))
                {
                    r.Title = query.RawQuery.Substring(3);
                }
                else
                {
                    r.Title = query.RawQuery.Substring(7);
                }
                r.SubTitle = "Show me the Qrcode";
                r.IcoPath = "[Res]:app";
                r.Score = 80;
                r.Action = e =>
                {
                    context_.Api.HideAndClear();
                    QrcodeForm window = new QrcodeForm(r.Title);
                    window.Show();
                    return true;
                };
                rl.Add(r);
                return rl;
            }
            return null;
        }
    }
}
