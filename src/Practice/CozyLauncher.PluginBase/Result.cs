using System;

namespace CozyLauncher.PluginBase
{
    public class Result
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string IcoPath { get; set; }

        // 优先级说明
        // 值为1-100
        // 1-49为低优先级 随便玩
        // 50-79 为普通主要功能的常规优先级（例如文件搜索结果、应用模糊匹配结果）
        // 80-89 为较高优先级，显示比较推荐的内容（例如完全匹配到的应用、高频率使用的应用和命令）
        // 90-100 为极高优先级，供特定命令插件使用（例如计算器、ip、about)
        public int Score { get; set; }
        public Func<ActionContext, bool> Action { get; set; }
    }
}
