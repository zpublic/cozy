using CozyWeixin.Core.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CozyWexin.Mvc.ApiControllers {

    [RoutePrefix("api/weixin")]
    public class WeixinController : ApiController {

        [HttpGet]
        [Route("test")]
        public string TestFunc() {
            return Account.GetInstance().AccessToken;
        }

        [HttpPost]
        [Route("receive-msg")]
        public string ReceiveMsg() {
            return "什么？ 你叫我";
        }

    }
}