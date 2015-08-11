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
                var data = this.ReadBodyData();
                return OnNotebookAll(data);
            };

            Post["/get"] = x =>
            {
                var data = this.ReadBodyData();
                return OnNotebookGet(data);
            };

            Post["/list"] = x =>
            {
                var data = this.ReadBodyData();
                return OnNotebookList(data);
            };

            Post["/update"] = x =>
            {
                var data = this.ReadBodyData();
                return OnNotebookUpdate(data);
            };

            Post["/create"] = x =>
            {
                var data = this.ReadBodyData();
                return OnNotebookCreate(data);
            };

            Post["/delete"] = x =>
            {
                var data = this.ReadBodyData();
                return OnNotebookDelete(data);
            };
        }
    }
}
