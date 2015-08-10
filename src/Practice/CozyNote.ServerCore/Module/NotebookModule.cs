using Nancy;
using Newtonsoft.Json;

namespace CozyNote.ServerCore.Module
{
    public class NotebookModule : NancyModule
    {
        public NotebookModule()
        {
            Get["/notebook/all"] = x =>
            {
                var rsp = new
                {
                    title = "abc",
                    url = "http:://www.laorouji.com",
                    text = "aaaaa",
                };
                return JsonConvert.SerializeObject(rsp);
            };

            Get["/notebook/get"] = x =>
            {
                return "a";
            };

            Get["/notebook/list"] = x =>
            {
                return "a";
            };

            Get["/notebook/update"] = x =>
            {
                return "a";
            };

            Get["/notebook/create"] = x =>
            {
                return "a";
            };

            Get["/notebook/delete"] = x =>
            {
                return "a";
            };
        }
    }
}
