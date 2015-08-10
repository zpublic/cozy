using Nancy;

namespace CozyNote.ServerCore.Module
{
    public partial class NoteModule : NancyModule
    {
        public NoteModule() : base("/note")
        {
            Post["/create"] = x =>
            {
                return "a";
            };

            Post["/get"] = x =>
            {
                return "a";
            };

            Post["/update"] = x =>
            {
                return "a";
            };

            Post["/move"] = x =>
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
