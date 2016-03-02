using CozyLauncher.Shared.InfoCollect.Model;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace CozyLauncher.Plugin.InfoCollect
{
    public class ReportActiveTask : ITask
    {
        public void Execute()
        {
            var r = new InfocActiveInput();
            r.username = GetUserName();
            r.machinename = GetMachineName();
            var macs = GetMacByNetworkInterface();
            if (macs.Count > 0)
            {
                r.mac = macs[0];
            }
        }

        public static string GetUserName()
        {
            try
            {
                return Environment.UserName;
            }
            catch (Exception)
            {
                return "Unkown";
            }
        }

        public static string GetMachineName()
        {
            try
            {
                return Environment.MachineName;
            }
            catch (Exception)
            {
                return "Unkown";
            }
        }

        public static List<string> GetMacByNetworkInterface()
        {
            List<string> macs = new List<string>();
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {
                macs.Add(ni.GetPhysicalAddress().ToString());
            }
            return macs;
        }
    }
}
