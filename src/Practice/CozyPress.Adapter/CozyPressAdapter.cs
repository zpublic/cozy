using CozyPress.Implementation;
using CozyPress.Interface;

namespace CozyPress.Adapter
{
    public class CozyPressAdapter
    {
        public static IBlogEngine CreateBlogEngine()
        {
            return new BlogEngineImpl();
        }
    }
}
