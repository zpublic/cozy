using CozyAnywhere.WpfClient.Model;
using CozyAnywhere.WpfClient.UserControls;

namespace CozyAnywhere.WpfClient.Factory
{
    public class ProcessPluginPageFactory : IControlFactory
    {
        public string ProductName { get { return "ProcessPlugin"; } }

        public DefaultControlInfo Create()
        {
            return new DefaultControlInfo()
            {
                Name        = ProductName,
                Controls    = new ProcessPluginPage(),
            };
        }
    }
}