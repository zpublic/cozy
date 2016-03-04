using CozyLauncher.PluginBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CozyLauncher.Plugin.PasswordGenerator
{
    public class Main : BasePlugin
    {
        private PluginInitContext context_;

        public override PluginInfo Init(PluginInitContext context)
        {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "pass";
            return info;
        }

        public override List<Result> Query(Query query)
        {
            if (query.RawQuery == "pass")
            {
                return genResult(16);
            }
            else if (query.RawQuery.StartsWith("pass "))
            {
                var querySrting = query.RawQuery.Substring(5);
                int num = 0;
                if (int.TryParse(querySrting, out num))
                {
                    return genResult(num);
                }
            }
            return null;
        }

        private string randomChars1 = "0123456789abcdefghijklmnopqrstuvwxyz";
        private string randomChars2 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
        private string randomChars3 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz!@#$%^&*";

        private Result genPass(int len, int mod)
        {
            if (len == 0)
                len = 16;
            string pass = "";
            Random random = new Random();
            for (int i = 0; i < len; ++i)
            {
                switch (mod)
                {
                    case 1:
                        pass += randomChars1[random.Next(randomChars1.Length)];
                        break;
                    case 2:
                        pass += randomChars2[random.Next(randomChars2.Length)];
                        break;
                    default:
                        pass += randomChars3[random.Next(randomChars3.Length)];
                        break;
                }
            }
            var r = new Result();
            r.Title = pass;
            r.SubTitle = "Copy this password to the clipboard";
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

        private List<Result> genResult(int len)
        {
            var rl = new List<Result>();
            var more = new Result();
            more.Title = "重新生成";
            more.SubTitle = "可以使用（pass n）来生成长度为n的密码";
            more.IcoPath = "[Res]:txt";
            more.Score = 100;
            more.Action = e =>
            {
                context_.Api.PushResults(genResult(len));
                return true;
            };
            rl.Add(more);
            rl.Add(genPass(len, 1));
            rl.Add(genPass(len, 2));
            rl.Add(genPass(len, 3));
            return rl;
        }
    }
}
