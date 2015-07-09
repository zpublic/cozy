namespace CozyAnywhere.PluginBase
{
    public interface IPluginCommand
    {
        uint Id { get; }

        void Execute(BasePlugin plugin);
    }
}