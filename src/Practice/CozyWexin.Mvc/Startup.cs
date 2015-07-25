using Microsoft.Owin;
using Owin;
using CozyWeixin.Core.Account;

[assembly: OwinStartupAttribute(typeof(CozyWexin.Mvc.Startup))]
namespace CozyWexin.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Account.GetInstance().Register();
        }
    }
}
