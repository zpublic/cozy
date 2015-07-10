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
            int pos             = CommandContent.IndexOf(':');
            result.MethodName   = CommandContent.Substring(0, pos);
            result.MethodArgs   = CommandContent.Substring(pos + 1, CommandContent.Length - pos - 1);
            return result;
        }
    }
}