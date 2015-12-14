using Nancy;

namespace CozyPress.WebServer.Core
{
    public class HelloModule : NancyModule
    {
        public HelloModule()
        {
            Get["/"] = x =>
            {
                return "hello world!";
            };
        }
    }

    public class CozyPressServer
    {
        public static void Init()
        {
            CozyPressHolder.Instance.WhosYourDaddy();
        }
    }
}
