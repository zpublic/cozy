using Nancy;

namespace CozyNote.ServerCore.Module
{
    class UserModule : NancyModule
    {
        public UserModule()
        {
            Get["/user/notebook"] = x =>
            {
                return "a";
            };

            Get["/user/create"] = x =>
            {
                return "a";
            };

            Get["/user/update"] = x =>
            {
                return "a";
            };
        }
    }
}
