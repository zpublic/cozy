using CozyPress.Interface;
using System;
namespace CozyPress.Implementation
{
    public class BlogEngineImpl : IBlogEngine
    {
        IOperateBlog OperateBlog;

        public IOperateBlog Blog()
        {
            return OperateBlog;
        }

        public void Init()
        {
            OperateBlog = new OperateBlogImpl();
        }

        public void UnInit()
        {
        }
    }
}
