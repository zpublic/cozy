using System;

namespace CozyAnywhere.ClientCore.EventArg
{
    public class ServerConnectEventArgs : EventArgs
    {
        public string Address { get; set; }
        public string Name { get; set; }
        public string Infomation { get; set; }

        public ServerConnectEventArgs(string address, string name, string info)
        {
            Address     = address;
            Name        = name;
            Infomation  = info;
        }
    }
}
