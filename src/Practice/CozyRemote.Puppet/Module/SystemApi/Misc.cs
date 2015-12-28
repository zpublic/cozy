using Nancy;
using System;
using System.Diagnostics;

namespace CozyRemote.Puppet.Module.SystemApi
{
    public class Misc : NancyModule
    {
        public Misc() : base("/system/misc")
        {
            Get["/run/{exe}"] = x =>
            {
                string fileName = (string)x.exe;
                var p = Process.Start(fileName);
                if (p == null)
                {
                    return "failed";
                }
                else
                {
                    p.Close();
                    return "success";
                }
            };
        }
    }
}
