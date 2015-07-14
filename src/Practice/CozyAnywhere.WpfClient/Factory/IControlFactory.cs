using CozyAnywhere.WpfClient.Model;

namespace CozyAnywhere.WpfClient.Factory
{
    public interface IControlFactory
    {
        string ProductName { get; }
        DefaultControlInfo Create();
    }
}