using Newtonsoft.Json;

namespace CozyAnywhere.Protocol
{
    public class PluginCommandMethod
    {
        public string MethodName { get; set; }

        public string MethodArgs { get; set; }

        public static PluginCommandMethod Create(string CommandContent)
        {
            var result = JsonConvert.DeserializeObject<PluginCommandMethod>(CommandContent);
            return result;
        }
    }
}