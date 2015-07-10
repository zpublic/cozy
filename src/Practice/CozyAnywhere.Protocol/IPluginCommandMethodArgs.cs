namespace CozyAnywhere.Protocol
{
    public abstract class PluginCommandMethodArgs
    {
        public virtual object Execute(IPluginCommandArgsDispatch dispatch)
        {
            return dispatch.Shell(this);
        }
    }
}