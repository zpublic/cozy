using CozyLauncher.PluginBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace CozyLauncher.Plugin.ManualRun
{
    public class Main : BasePlugin
    {
        const string configFile = "CozyLauncher.Plugin.ManualRun.config.json";
        private PluginInitContext context_;
        private ActionData ad_;

        public override PluginInfo Init(PluginInitContext context)
        {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "*";
            LoadActionData();
            return info;
        }

        public override List<Result> Query(Query query)
        {
            if (query.RawQuery == "manual")
            {
                LoadActionData();
            }

            ActionOpenDirctory acDir;
            if (ad_.actionOpenDirctory.TryGetValue(query.RawQuery, out acDir))
            {
                var rl = new List<Result>();
                var r = new Result();
                r.Title = acDir.Key;
                r.SubTitle = acDir.Dirctory;
                r.IcoPath = "folder_open";
                r.Score = 100;
                r.Action = e =>
                {
                    Process.Start("explorer.exe", acDir.Dirctory);
                    context_.Api.HideAndClear();
                    return true;
                };
                rl.Add(r);
                return rl;
            }

            ActionOpenExe acExe;
            if (ad_.actionOpenExe.TryGetValue(query.RawQuery, out acExe))
            {
                var rl = new List<Result>();
                var r = new Result();
                r.Title = acExe.Key;
                r.SubTitle = acExe.Exe;
                r.IcoPath = "[Res]:app";
                r.Score = 100;
                r.Action = e =>
                {
                    context_.Api.HideApp();
                    Process.Start(acExe.Exe, acExe.Param);
                    return true;
                };
                rl.Add(r);
                return rl;
            }

            return null;
        }

        string LocalFullPath(string file)
        {
            string cur = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            return Path.Combine(cur, file);
        }

        void LoadActionData()
        {
            ad_ = null;
            try
            {
                StreamReader sr = new StreamReader(LocalFullPath(configFile), Encoding.Default);
                var json = sr.ReadToEnd();
                sr.Close();
                ad_ = JsonConvert.DeserializeObject<ActionData>(json);
            }
            catch (Exception)
            {

            }
            if (ad_ == null)
            {
                ad_ = GenerateDefaultActionData();
                var j = JsonConvert.SerializeObject(ad_, Formatting.Indented);
                StreamWriter sw = new StreamWriter(LocalFullPath(configFile), false, Encoding.Default);
                sw.Write(j);
                sw.Close();
            }
        }

        ActionData GenerateDefaultActionData()
        {
            var ad = new ActionData()
            {
                actionOpenDirctory = new Dictionary<string, ActionOpenDirctory>()
                {
                    {
                        "win",
                        new ActionOpenDirctory()
                        {
                            Key = "win",
                            Dirctory = @"c:\windows",
                        }
                    }
                },
                actionOpenExe = new Dictionary<string, ActionOpenExe>()
                {
                    {
                        "cc",
                        new ActionOpenExe()
                        {
                            Key = "cc",
                            Exe = "calc",
                        }
                    }
                },
            };
            return ad;
        }
    }
}
