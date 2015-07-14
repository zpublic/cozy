using CozyAnywhere.PluginBase;
using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinProcess
{
    public partial class ProcessPlugin : IPlugin, IPluginCommandArgsDispatch
    {
        public readonly static string InnerPluginName = "ProcessPlugin";

        public string PluginName { get { return InnerPluginName; } }

        public ProcessPlugin()
        {
            RegisterMethod();
        }

        public PluginCommandMethodReturnValue Shell(string commandContent)
        {
            var context     = PluginCommandMethod.Create(commandContent);
            var methodName  = context.MethodName;
            var methodArgs  = context.MethodArgs;
            if (MethodDictionary.ContainsKey(methodName))
            {
                var factory     = MethodDictionary[methodName];
                var args        = factory.Create(methodArgs);
                var rtvalue     = Dispatch(args);

                var result = new PluginCommandMethodReturnValue()
                {
                    PluginName          = InnerPluginName,
                    MethodName          = methodName,
                    MethodReturnValue   = rtvalue,
                };
                return result;
            }
            return null;
        }
    }
}