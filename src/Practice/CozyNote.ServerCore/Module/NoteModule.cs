using Nancy;

namespace CozyNote.ServerCore.Module
{
    public partial class NoteModule : NancyModule
    {
        public NoteModule() : base("/note")
        {
            Post["/create"] = x =>
            {
                var data = this.ReadBodyData();
                return OnNoteCreate(data);
            };

            Post["/get"] = x =>
            {
                var data = this.ReadBodyData();
                return OnNoteGet(data);
            };

            Post["/update"] = x =>
            {
                var data = this.ReadBodyData();
                return OnNoteUpdate(data);
            };

            Post["/move"] = x =>
            {
                var data = this.ReadBodyData();
                return OnNoteMove(data); ;
            };

            Post["/delete"] = x =>
            {
                var data = this.ReadBodyData();
                return OnNoteDelete(data);
            };
        }
    }
}
