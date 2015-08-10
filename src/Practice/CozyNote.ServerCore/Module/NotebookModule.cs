using Nancy;
using Newtonsoft.Json;
using System.IO;

namespace CozyNote.ServerCore.Module
{
    public partial class NotebookModule : NancyModule
    {
        public NotebookModule() : base("/notebook")
        {
            Post["/all"] = x =>
            {
                var body = this.Request.Body;
                body.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(body))
                {
                    var result = reader.ReadToEnd();
                    return OnNotebookAll(result);
                }
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
