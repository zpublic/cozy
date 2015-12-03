using System;
using Nancy;

namespace CozyOfficialAccounts.Server.Modules {

    public class WeixinModule : NancyModule {

        public WeixinModule() {

            Get["/weixin"] = param => {
                string echostr = Request.Query["echostr"];
                Console.WriteLine(echostr);
                return echostr;
            };
        }
    }
}
