using CozyAnywhere.PluginBase;
using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinFile
{
    public partial class FilePlugin : BasePlugin
    {
        private static string InnerPluginName = "FilePlugin";
        public override string PluginName { get { return InnerPluginName; } }

        public override object Shell(string commandContent)
        {
            var context     = PluginCommandMethod.Create(commandContent);
            var methodName  = context.MethodName;
            var methodArgs  = context.MethodArgs;
            if (MethodDictionary.ContainsKey(methodName))
            {
                var packet  = MethodDictionary[methodName];
                var func    = packet.Function;
                var args    = packet.ArgsFactory.Create(methodArgs);
                return func(args);
            }
            return PluginCommand.NullReturnValue;
        }

        public FilePlugin()
        {
            RegisterMethod();
        }
    }
}