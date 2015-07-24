using Newtonsoft.Json;

namespace CozyAnywhere.Protocol
{
    public class PluginCommand
    {
        public string PluginName { get; set; }

        public string PluginCommandContent { get; set; }

        public static PluginCommand CreateWithParse(string command)
        {
            var result = JsonConvert.DeserializeObject<PluginCommand>(command);
            return result;
        }
    }
}