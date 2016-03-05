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
        void ShowPanel(string command);
        void Update();
        void PushResults(List<Result> results);
        void RunCommand(string command);
    }
}
