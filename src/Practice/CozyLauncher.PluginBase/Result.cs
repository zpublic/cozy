using System;

namespace CozyLauncher.PluginBase
{
    public class Result
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string IcoPath { get; set; }
        public int Score { get; set; }
        public Func<ActionContext, bool> Action { get; set; }
    }
}
