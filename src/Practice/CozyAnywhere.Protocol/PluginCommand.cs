namespace CozyAnywhere.Protocol
{
    public class PluginCommand
    {
        private static readonly object _NullReturnValue = new object();

        public static object NullReturnValue
        {
            get
            {
                return _NullReturnValue;
            }
        }

        public string PluginName { get; set; }

        public string PluginCommandContent { get; set; }

        public object PluginReturnValue { get; set; }

        public static PluginCommand CreateWithParse(string command)
        {
            var result = new PluginCommand();

            // TODO deserialization command

            return result;
        }
    }
}