using System;

namespace CozyAnywhere.ClientCore.EventArg
{
    public class CurrentFilePathRefreshEventArgs : EventArgs
    {
        public string Path { get; set; }

        public CurrentFilePathRefreshEventArgs(string path)
        {
            Path = path;
        }
    }
}
