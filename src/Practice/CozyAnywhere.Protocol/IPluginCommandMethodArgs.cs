namespace CozyAnywhere.Protocol
{
    public abstract class PluginCommandMethodArgs
    {
        public virtual string Execute(IPluginCommandArgsDispatch dispatch)
        {
            return dispatch.Shell(this);
        }
    }
}