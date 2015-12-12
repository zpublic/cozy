using CozyPress.Adapter;
using CozyPress.Interface;

namespace CozyPress.WebServer.Core
{
    public class CozyPressHolder
    {
        public static readonly CozyPressHolder Instance = new CozyPressHolder();

        public void WhosYourDaddy()
        {
            BlogEngine = CozyPressAdapter.CreateBlogEngine();
            BlogEngine.Init();
            OperateBlog = BlogEngine.Blog();
        }

        public IBlogEngine BlogEngine;
        public IOperateBlog OperateBlog;
    }
}
