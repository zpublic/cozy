using CozyAnywhere.WpfClient.Model;
using CozyAnywhere.WpfClient.UserControls;

namespace CozyAnywhere.WpfClient.Factory
{
    public class FilePluginPageFactory : IControlFactory
    {
        public string ProductName { get { return "FilePlugin"; } }

        public DefaultControlInfo Create()
        {
            return new DefaultControlInfo()
            {
                Name        = ProductName,
                Controls    = new FilePluginPage(),
            };
        }
    }
}