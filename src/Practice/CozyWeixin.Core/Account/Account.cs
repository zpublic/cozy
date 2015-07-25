using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace CozyWeixin.Core.Account {

    public class Account {

        string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        string appId = "wx84e01be9fa8ff6c7";
        string secret = "9f1291356eb2da494b2123f7747cd4a0";

        public string AccessToken { get; private set; }

        private static Account instance;

        private Account() { }

        public static Account GetInstance() {
            return instance = instance ?? new Account();
        }


        public void Register() {
            SetAccessToken();
        }

        private void SetAccessToken() {
            using (var clinet = new HttpClient()) {
                var reslut = clinet.GetAsync(string.Format(url, appId, secret)).Result;
                var jsonString = reslut.Content.ReadAsStringAsync().Result;
                AccessToken = JObject.Parse(jsonString)["access_token"].ToString();
            }
        }
    }
}
