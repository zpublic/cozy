using System.IO;
using Nancy;

namespace CozyLauncher.Tool.InfoCollectServer
{
    public static class ModuleHelper
    {
        public static string ReadBodyData(this NancyModule module)
        {
            var body = module.Request.Body;
            body.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(body))
            {
                var result = reader.ReadToEnd();
                return result;
            }
        }
    }
}
