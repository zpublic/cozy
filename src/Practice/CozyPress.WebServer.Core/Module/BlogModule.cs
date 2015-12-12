using Nancy;

namespace CozyPress.WebServer.Core.Module
{
    public partial class BlogModule : NancyModule
    {
        public BlogModule() : base("/blog")
        {
            Post["/add"] = x =>
            {
                var data = this.ReadBodyData();
                return OnBlogAdd(data);
            };
            Get["/add"] = x =>
            {
                var data = this.ReadBodyData();
                return OnBlogAdd(data);
            };

            Post["/delete"] = x =>
            {
                var data = this.ReadBodyData();
                return OnBlogDelete(data);
            };

            Post["/update"] = x =>
            {
                var data = this.ReadBodyData();
                return OnBlogUpdate(data);
            };

            Post["/get"] = x =>
            {
                var data = this.ReadBodyData();
                return OnBlogGet(data);
            };
        }
    }
}
