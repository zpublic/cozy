using System;
using System.Diagnostics;

namespace CozyNote.WebSite
{
    class Program
    {
        static void Main(string[] args)
        {
            WebSiteHost.Run("http://localhost:23334");
            Process.Start("http://localhost:23334/user/zapline/notebook");
            Console.ReadLine();
        }
    }
}
