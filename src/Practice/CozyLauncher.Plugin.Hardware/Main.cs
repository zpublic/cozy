using CozyLauncher.PluginBase;
using System;
using System.Collections.Generic;
using System.Management;
using System.Windows.Forms;

namespace CozyLauncher.Plugin.Hardware
{
    public class Main : BasePlugin
    {
        private PluginInitContext context_;

        public override PluginInfo Init(PluginInitContext context)
        {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "h";
            return info;
        }

        public override List<Result> Query(Query query)
        {
            if (query.RawQuery == "h mem")
            {
                var rl = new List<Result>();
                var r = new Result();
                r.Title = GetTotalPhysicalMemory();
                r.SubTitle = "Copy this number to the clipboard";
                r.IcoPath = "[Res]:sys";
                r.Score = 100;
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
                rl.Add(r);
                return rl;
            }
            return null;
        }

        string GetTotalPhysicalMemory()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st = mo["TotalPhysicalMemory"].ToString();
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }
    }
}
