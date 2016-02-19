using System.Collections.Generic;
using CozyLauncher.PluginBase;
using System.Diagnostics;

namespace CozyLauncher.Plugin.Command {

    public class Main : BasePlugin {

        private PluginInitContext context_;

        public override PluginInfo Init(PluginInitContext context) {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = ">";
            return info;
        }

        public override List<Result> Query(Query query) {
            if (query.RawQuery.StartsWith(">")) {
                var result = new List<Result>();
                var queryString = query.RawQuery.Substring(1);
                result.Add(new Result {
                    Title = queryString,
                    SubTitle = $"使用命令行打开 {queryString}",
                    IcoPath = "sys",
                    Action = e => {
                        Process.Start("cmd", $"/k {queryString}");
                        context_.Api.HideAndClear();
                        return true;
                    }
                });
                return result;
            }
            return null;
        }
    }
}
