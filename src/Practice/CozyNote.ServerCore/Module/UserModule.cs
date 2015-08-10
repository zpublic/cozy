using Nancy;

namespace CozyNote.ServerCore.Module
{
    class UserModule : NancyModule
    {
        public UserModule() : base("/user")
        {
            Post["/notebook"] = x =>
            {
                return "a";
            };

            Post["/create"] = x =>
            {
                var body = this.Request.Body;
                long len = body.Length;
                if (len > 0)
                {
                    byte[] byData = new byte[len];
                    body.Read(byData, 0, (int)len);
                }
                return "a";
            };

            Post["/update"] = x =>
            {
                return "a";
            };
        }
    }
}
