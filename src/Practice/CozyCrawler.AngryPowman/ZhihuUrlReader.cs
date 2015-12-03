using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Drawing;
using System.Net.Http;
using CozyCrawler.Base;
using Newtonsoft.Json;
using System.Net;

namespace CozyCrawler.AngryPowman
{
    public class ZhihuUrlReader
    {
        public string Name { get; set; }

        public string Pass { get; set; }

        private string XSRF { get; set; }

        private string Cap { get; set; }

        public ZhihuUrlReader(string name, string pass)
        {
            Name = name;
            Pass = pass;

            if (!IsLogin())
            {
                TryLogin();
            }
            else
            {
                Console.WriteLine("已登录");
            }
        }

        private bool TryLogin()
        {
            if (!GetXSRF()) return false;
            if (!GetCaptcha()) return false;
            Login();

            return true; 
        }

        public HttpContent GetLoginContent()
        {
            var v = new Dictionary<string, string>()
            {
                {"_xsrf", XSRF},
                {"password", Pass},
                {"remember_me", "true"},
                {"email", Name},
                {"captcha", Cap},
            };

            var conn = new FormUrlEncodedContent(v);

            return conn;
        }

        private bool GetXSRF()
        {
            HtmlDocument doc = new HtmlDocument();
            try
            {
                doc.LoadHtml(HttpGet.Get(@"http://www.zhihu.com").Content.ReadAsStringAsync().Result);
            }
            catch (Exception)
            {
                // todo 
            }

            var xsrfNode = doc.DocumentNode.SelectSingleNode(@"//input[@value and @name and @name='_xsrf']");
            if (xsrfNode == null)
            {
                return false;
            }

            XSRF = xsrfNode.Attributes["value"].Value;
            return !string.IsNullOrEmpty(XSRF);
        }

        private Random rd = new Random();
        private bool GetCaptcha()
        {
            var CapId   = rd.Next().ToString();
            var capUrl  = @"http://www.zhihu.com/captcha.gif?r=" + CapId;
            Image img   = Image.FromStream(HttpGet.Get(capUrl).Content.ReadAsStreamAsync().Result);
            img.Save(CapId + ".gif");

            Console.WriteLine("输入验证码");
            Cap = Console.ReadLine();
            return true;
        }

        private bool IsLogin()
        {
            var url = @"http://www.zhihu.com/settings/profile";

            var rsp = HttpGet.Get(url, false);
            if(rsp.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        private void Login()
        {
            var fromUrl = @"http://www.zhihu.com/login/email";  
            var cont    = GetLoginContent();

            var headers = new Dictionary<string, string>
            {
                {"Host", "www.zhihu.com"},
                {"Origin", "http://www.zhihu.com"},
                {"Pragma", "no-cache"},
                {"Referer", "http://www.zhihu.com/"},
                {"X-Requested-With", "XMLHttpRequest"},
            };
            var rsp = HttpPost.Post(fromUrl, cont, headers).Content.ReadAsStringAsync().Result;

            var resObj = JsonConvert.DeserializeObject<LoginResult>(rsp);
            Console.WriteLine(resObj.msg);
        }
    }
}
