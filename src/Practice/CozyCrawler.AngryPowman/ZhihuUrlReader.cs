using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyCrawler.Interface;
using HtmlAgilityPack;
using System.Net;
using System.Drawing;
using System.Web;
using System.Net.Http;
using CozyCrawler.Component;
using CozyCrawler.Base;

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
            TryLogin();
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

            return new FormUrlEncodedContent(v);
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

            Cap = Console.ReadLine();
            return true;
        }

        private void Login()
        {
            var fromUrl = @"http://www.zhihu.com/login/email";  
            var cont = GetLoginContent();
            var rsp = HttpPost.Post(fromUrl, cont).Content.ReadAsStringAsync().Result;
        }
    }
}
