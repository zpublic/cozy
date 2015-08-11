using Nancy;

namespace CozyNote.WebSite.Module
{
    public class UserModule : NancyModule
    {
        public UserModule() : base("/user")
        {
            Get["/{name}/notebook"] = x =>
            {
                return string.Concat("Hello ", x.name);
            };
        }
    }
}
