using System.Collections.Generic;

namespace CozyLauncher.PluginBase
{
    public interface IPublicApi
    {
        void CloseApp();
        void HideApp();
        void ShowApp();
        void PushResults(List<Result> results);
    }
}
