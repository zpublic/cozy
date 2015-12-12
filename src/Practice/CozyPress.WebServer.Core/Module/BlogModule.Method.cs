using CozyPress.Model;
using Newtonsoft.Json;

namespace CozyPress.WebServer.Core.Module
{
    public partial class BlogModule
    {
        public string OnBlogAdd(string data)
        {
            Blog bb = new Blog();
            bb.Title = "hehe";
            bb.Content = "aaaa";
            CozyPressHolder.Instance.OperateBlog.Add(bb);
            return JsonConvert.SerializeObject(bb); ;
        }

        public string OnBlogDelete(string data)
        {
            return "hehe";
        }

        public string OnBlogUpdate(string data)
        {
            return "hehe";
        }

        public string OnBlogGet(string data)
        {
            return "hehe";
        }
    }
}
