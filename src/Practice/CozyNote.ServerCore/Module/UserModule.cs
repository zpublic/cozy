using Nancy;

namespace CozyNote.ServerCore.Module
{
    public partial class UserModule : NancyModule
    {
        public UserModule() : base("/user")
        {
            Post["/notebook"] = x =>
            {
                var data = this.ReadBodyData();
                return OnUserNotebook(data);
            };

            Post["/create"] = x =>
            {
                var data = this.ReadBodyData();
                return OnUserCreate(data);
            };

            Post["/update"] = x =>
            {
                var data = this.ReadBodyData();
                return OnUserUpdate(data);
            };
        }
    }
}
