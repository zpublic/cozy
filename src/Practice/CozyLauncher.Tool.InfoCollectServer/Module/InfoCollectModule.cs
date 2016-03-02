using Nancy;

namespace CozyLauncher.Tool.InfoCollectServer.Module
{
    public partial class InfoCollectModule : NancyModule
    {
        public InfoCollectModule() : base("/infoc")
        {
            Post["/active"] = x =>
            {
                var data = this.ReadBodyData();
                return OnInfocActive(data);
            };
        }
    }
}
