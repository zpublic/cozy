using Nancy;
using Newtonsoft.Json;

namespace CozyNote.ServerCore.Module
{
    public partial class NotebookModule : NancyModule
    {
        public NotebookModule() : base("/notebook")
        {
            Post["/all"] = x =>
            {
                var rsp = new
                {
                    title = "abc",
                    url = "http:://www.laorouji.com",
                    text = "aaaaa",
                };
                return JsonConvert.SerializeObject(rsp);
            };

            Post["/get"] = x =>
            {
                return "a";
            };

            Post["/list"] = x =>
            {
                return "a";
            };

            Post["/update"] = x =>
            {
                return "a";
            };

            Post["/create"] = x =>
            {
                return "a";
            };

            Post["/delete"] = x =>
            {
                return "a";
            };
        }
    }
}
