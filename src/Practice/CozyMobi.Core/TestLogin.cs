using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CozyMobi.Core
{
    public class TestLogin
    {
        public String test()
        {
            HttpClient hc = new HttpClient();
            HttpResponseMessage rsp = hc.GetAsync("https://coding.net/api/social/tweet/public_tweets").Result;
            if (rsp.IsSuccessStatusCode)
            {
                string rspText = rsp.Content.ReadAsStringAsync().Result;
                JsonReader jsonReader = new JsonTextReader(new StringReader(rspText));
                return jsonReader.ToString();
            }
            return null;
        }
    }
}
