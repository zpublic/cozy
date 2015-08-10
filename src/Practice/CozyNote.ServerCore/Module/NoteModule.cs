using Nancy;

namespace CozyNote.ServerCore.Module
{
    public class NoteModule : NancyModule
    {
        public NoteModule()
        {
            Get["/note/create"] = x =>
            {
                return "a";
            };

            Get["/note/get"] = x =>
            {
                return "a";
            };

            Get["/note/update"] = x =>
            {
                return "a";
            };

            Get["/note/move"] = x =>
            {
                return "a";
            };

            Get["/note/delete"] = x =>
            {
                return "a";
            };
        }
    }
}
