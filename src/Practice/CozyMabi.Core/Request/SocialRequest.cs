using CozyMabi.Core.RequestBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyMabi.Core.Network;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using CozyMabi.Core.ResponseParser;
using CozyMabi.Core.Model;

namespace CozyMabi.Core.Request
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
            if (ResponseParserCommon.AreYouOk(rsp, ref jo))
            {
            }
        }

        public void SendMessage(string name, string content)
        {
            HttpContent http_content = mBuild.SendMessage(name, content);
            HttpResponseMessage rsp = HttpPost.Post(RequestBuilderCommon.SocialMessageSend, http_content);
            JObject jo = null;
            if (ResponseParserCommon.AreYouOk(rsp, ref jo))
            {
            }
        }

        public async void GetFriends(int pageId, int pageSize)
        {
            HttpContent http_content = mBuild.GetFriends(pageId, pageSize);
            HttpResponseMessage rsp = HttpGet.Get(
                RequestBuilderCommon.SocialFriends + await http_content.ReadAsStringAsync());
            JObject jo = null;
            if (ResponseParserCommon.AreYouOk(rsp, ref jo))
            {
            }
        }

        public async void GetFollowers(int pageId, int pageSize)
        {
            HttpContent http_content = mBuild.GetFollowers(pageId, pageSize);
            HttpResponseMessage rsp = HttpGet.Get(
                RequestBuilderCommon.SocialFollowers + await http_content.ReadAsStringAsync());
            JObject jo = null;
            if (ResponseParserCommon.AreYouOk(rsp, ref jo))
            {
            }
        }

        public bool Follow(string user)
        {
            HttpContent http_content = mBuild.Follow(user);
            HttpResponseMessage rsp = HttpPost.Post(RequestBuilderCommon.SocialFollow, http_content);
            JObject jo = null;
            if (ResponseParserCommon.AreYouOk(rsp, ref jo))
            {
                return true;
            }
            return false;
        }

        public bool UnFollow(string user)
        {
            HttpContent http_content = mBuild.UnFollow(user);
            HttpResponseMessage rsp = HttpPost.Post(RequestBuilderCommon.SocialUnFollow, http_content);
            JObject jo = null;
            if (ResponseParserCommon.AreYouOk(rsp, ref jo))
            {
                return true;
            }
            return false;
        }
    }
}
