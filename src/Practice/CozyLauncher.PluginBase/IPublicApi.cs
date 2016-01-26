using System.Collections.Generic;

namespace CozyLauncher.PluginBase
{
    public interface IPublicApi
    {
        void CloseApp();
        void HideApp();
        void Clear();
        void HideAndClear();
        void ShowApp();
        void Config();
        void About();
        void PushResults(List<Result> results);
    }
}
