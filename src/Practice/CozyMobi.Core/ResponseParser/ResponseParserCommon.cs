using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CozyMobi.Core.ResponseParser
{
    class ResponseParserCommon
    {
        public static bool AreYouOk(HttpResponseMessage rsp, ref JObject jo)
        {
            if (rsp.IsSuccessStatusCode)
            {
                string rspText = rsp.Content.ReadAsStringAsync().Result;
                jo = JObject.Parse(rspText);
                if (jo["code"].ToString() == "0")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
