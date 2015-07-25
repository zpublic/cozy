using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;


namespace CozyWeixin.WebPage.ApiControllers
{
    [Route("api/[controller]")]
    public class WeixinController : Controller 
    {
        const string appId = "wx84e01be9fa8ff6c7";
        const string appSecret = "9f1291356eb2da494b2123f7747cd4a0";

        [HttpGet]
        [Route("test1")]
        public string GetToken()
        {
            return "Hello";
        }

    }
}
