using CozyAnywhere.PluginBase;
using CozyAnywhere.Protocol;

namespace CozyAnywhere.Plugin.WinCapture
{
    public partial class CapturePlugin : IPlugin, IPluginCommandArgsDispatch
    {
        public static readonly string InnerPluginName = "CapturePlugin";

        public string PluginName { get { return InnerPluginName; } }

        public CapturePlugin()
        {
            RegisterMethod();
        }

        public PluginCommandMethodReturnValue Shell(string commandContent)
        {
            var context = PluginCommandMethod.Create(commandContent);
            var methodName = context.MethodName;
            var methodArgs = context.MethodArgs;
            if (MethodDictionary.ContainsKey(methodName))
            {
                var factory = MethodDictionary[methodName];
                var args = factory.Create(methodArgs);
                var rtvalue = Dispatch(args);

                var result = new PluginCommandMethodReturnValue()
                {
                    PluginName = InnerPluginName,
                    MethodName = methodName,
                    MethodReturnValue = rtvalue,
                };
                return result;
            }
            return null;
        }
    }
}