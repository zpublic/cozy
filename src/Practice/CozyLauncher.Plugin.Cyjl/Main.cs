using CozyLauncher.Infrastructure;
using CozyLauncher.PluginBase;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;

namespace CozyLauncher.Plugin.Cyjl
{
    public class Main : BasePlugin
    {
        private PluginInitContext context_;
        private List<string> CyList { get; set; } = new List<string>();

        public override PluginInfo Init(PluginInitContext context)
        {
            string cur = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var fp = Path.Combine(cur, "成语词典.txt");
            StreamReader sr = File.OpenText(fp);
            string str = "";
            while ((str = sr.ReadLine()) != null)
            {
                var cy = str.Split('】')[0].Substring(1);
                CyList.Add(cy);
            }
            sr.Close();

            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "cyjl ";
            return info;
        }

        public override List<Result> Query(Query query)
        {
            if (query.RawQuery.StartsWith("cyjl "))
            {
                //var matcher = FuzzyMatcher.Create(query.RawQuery.Substring(5));
                var ret = new List<Result>();
                var p = query.RawQuery.Substring(5);
                foreach (var cy in CyList)
                {
                    if (cy.Unidecode().ToLower().StartsWith(p))
                        ret.Add(CreateResult(cy, 99));
                    /*var pyans = matcher.EvaluatePinYin(cy);
                    if (pyans.Success)
                    {
                        ret.Add(CreateResult(cy, pyans.Score));
                    }*/
                }
                return ret;
            }
            return null;
        }

        private Result CreateResult(string cy, int source)
        {
            var r = new Result();
            r.Title = cy;
            r.SubTitle = "Copy this text to the clipboard";
            r.IcoPath = "[Res]:txt";
            r.Score = 99;
            r.Action = e =>
            {
                context_.Api.HideAndClear();
                try
                {
                    Clipboard.SetText(r.Title);
                    return true;
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {
                    return false;
                }
            };
            return r;
        }
    }
}
