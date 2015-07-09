namespace CozyAnywhere.Protocol
{
    public class PluginCommandMethod
    {
        public string MethodName { get; set; }

        public string MethodArgs { get; set; }

        public static PluginCommandMethod Create(string CommandContent)
        {
            var result = new PluginCommandMethod();

            // TODO deserialization Method

            return result;
        }
    }
}