using CozyAnywhere.PluginBase;
using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinFile
{
    public partial class FilePlugin : BasePlugin, IPluginCommandArgsDispatch
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
                var factory = MethodDictionary[methodName];
                var args    = factory.Create(methodArgs);
                return Dispatch(args);
            }
            return null;
        }

        public FilePlugin()
        {
            RegisterMethod();
        }
    }
}