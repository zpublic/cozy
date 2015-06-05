using CozyMobi.Core.RequestBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyMobi.Core.Network;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using CozyMobi.Core.ResponseParser;
using CozyMobi.Core.Model;

namespace CozyMobi.Core.Request
{
    public class SocialRequest
    {
        private SocialRequestBuilder mBuild = new SocialRequestBuilder();
        private SocialResponseParser mParser = new SocialResponseParser();

        public void Maopao(string content, string device)
        {
            HttpContent http_content = mBuild.Maopao(content, device);
            HttpResponseMessage rsp = HttpPost.Post(RequestBuilderCommon.SocialMaopao, http_content);
            JObject jo = null;
            if (ResponseParserCommon.AreYouOk(rsp, jo))
            {
            }
        }
    }
}
