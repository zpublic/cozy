using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CozyBlog.Models.ViewModels
{
    public class CreateBlogViewModel
    {
        public CreateBlogViewModel(Blogs blog, List<Category> categories)
        {
            Blog = blog;
            Categories = new SelectList(categories.ToList(), "ID", "CategoryName");
        }

        public Blogs Blog { get; private set; }
        public SelectList Categories { get; private set; }
    }
}