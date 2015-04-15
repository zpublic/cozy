using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CozyBlog.Models.ViewModels;

namespace CozyBlog.Models
{
    public interface IBlogRepository
    {
        void AddBlog(Blogs blog);
        Blogs GetBlogById(int id);
        IQueryable<Blogs> GetBlogs(int categoryId);
        List<CategoryListViewModel> GetActiveCategories();
        void RemoveBlog(Blogs blog);
        void Save();
    }
}