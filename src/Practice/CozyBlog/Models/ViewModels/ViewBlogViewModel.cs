using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CozyBlog.Models.ViewModels
{
    public class ViewBlogViewModel
    {
        public ViewBlogViewModel(Blogs blog, List<Comments> comments, string categoryName)
        {
            this.Blog = blog;
            Comments = comments;
            CategoryName = categoryName;
        }

        public Blogs Blog { get; private set; }
        
        public List<Comments> Comments { get; private set; }
        
        public string CategoryName {get; private set;}
        
        public int CommentCount 
        {
            get
            {
                return Comments.Count;
            }
        }
    }
}