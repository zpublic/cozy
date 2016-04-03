using CozyLauncher.PluginBase;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CozyLauncher.Core.Plugin
{
    public class CppPluginLoader : IPluginLoader
    {
        private CppPluginLoader() { }
        public readonly static CppPluginLoader Instance = new CppPluginLoader();

        List<IPlugin> plugins_ = new List<IPlugin>();

        public List<IPlugin> GetPlugins(PluginInitContext context)
        {
            for (int a = 0; a < CppPluginInterop.GetPluginCount(); a++)
            {
                var p = new CppPluginWrapper(this, a);
                p.Init(context);
                plugins_.Add(p);
            }
            return plugins_;
        }

        public PluginInfo Init(int id, PluginInitContext context)
        {
            PluginInfo pi = new PluginInfo();
            CppPluginInterop.Init(id);
            return pi;
        }

        public List<Result> Query(int id, Query query)
        {
            try
            {
                var data = CppPluginInterop.Query(id, Marshal.StringToCoTaskMemAuto(query.RawQuery));
                var r = Marshal.PtrToStringAuto(data);
                if (r != null && r != "")
                {
                    return new List<Result>()
                    {
                        new Result()
                        {
                            Title = r,
                        }
                    };
                }
            }
            catch (Exception) { }
            return null;
        }

        public void ShowPanel(int id, string command)
        {
            CppPluginInterop.ShowPanel(id, Marshal.StringToCoTaskMemAuto(command));
        }

        public void RunCommand(int id, string command)
        {
            CppPluginInterop.RunCommand(id, Marshal.StringToCoTaskMemAuto(command));
        }
    }
}
