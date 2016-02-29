using CozyLauncher.PluginBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CozyLauncher.Plugin.Sys
{
    public class Main : BasePlugin
    {
        static readonly IntPtr HWND_BROADCAST = new IntPtr(0xffff);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr PostMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        private const uint WM_SYSCOMMAND = 0x0112;
        private const uint SC_MONITORPOWER = 0xF170;

        private PluginInitContext context_;

        public override PluginInfo Init(PluginInitContext context)
        {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "*";
            return info;
        }

        public override List<Result> Query(Query query)
        {
            if (query.RawQuery == "host" || query.RawQuery == "hosts")
            {
                var rl = new List<Result>();
                var r = new Result();
                r.Title = "Hosts";
                r.SubTitle = "open hosts file";
                r.IcoPath = "[Res]:sys";
                r.Score = 90;
                r.Action = e =>
                {
                    Process.Start("notepad", Environment.GetFolderPath(Environment.SpecialFolder.System) + "/drivers/etc/hosts");
                    context_.Api.HideAndClear();
                    return true;
                };
                rl.Add(r);
                return rl;
            }
            else if (query.RawQuery == "cs")
            {
                var rl = new List<Result>();
                var r = new Result();
                r.Title = "关闭屏幕";
                r.SubTitle = "";
                r.IcoPath = "sys";
                r.Score = 90;
                r.Action = e =>
                {
                    context_.Api.HideAndClear();
                    PostMessage(HWND_BROADCAST, WM_SYSCOMMAND, (IntPtr)SC_MONITORPOWER, new IntPtr(2));
                    return true;
                };
                rl.Add(r);
                return rl;
            }
            return null;
        }
    }
}
