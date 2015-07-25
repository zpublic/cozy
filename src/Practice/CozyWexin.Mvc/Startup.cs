using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CozyWexin.Mvc.Startup))]
namespace CozyWexin.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
