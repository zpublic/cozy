using CozyAnywhere.WpfClient.Model;
using CozyAnywhere.WpfClient.UserControls;

namespace CozyAnywhere.WpfClient.Factory
{
    public class CapturePluginPageFactory : IControlFactory
    {
        public string ProductName { get { return "CapturePlugin"; } }

        public DefaultControlInfo Create()
        {
            return new DefaultControlInfo()
            {
                Name        = ProductName,
                Controls    = new CapturePluginPage(),
            };
        }
    }
}
