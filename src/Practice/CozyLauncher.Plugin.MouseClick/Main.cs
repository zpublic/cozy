using CozyLauncher.PluginBase;
using System.Collections.Generic;
using System.Threading;

namespace CozyLauncher.Plugin.MouseClick
{
    public class Main : BasePlugin
    {
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        const int MOUSEEVENTF_MOVE = 0x0001;
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const int MOUSEEVENTF_RIGHTUP = 0x0010;
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        private PluginInitContext context_;

        public override PluginInfo Init(PluginInitContext context)
        {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "mouseclick";
            return info;
        }

        public override List<Result> Query(Query query)
        {
            if (query.RawQuery.StartsWith("mouseclick "))
            {
                var s = query.RawQuery.Substring(2);
                var rl = new List<Result>();
                var r = new Result();
                r.Title = "Mouse Click";
                r.SubTitle = "鼠标自动点击";
                r.IcoPath = "[Res]:sys";
                r.Score = 60;
                r.Action = e =>
                {
                    context_.Api.HideApp();
                    Thread.Sleep(3000);
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    return true;
                };
                rl.Add(r);
                return rl;
            }
            return null;
        }
    }
}
