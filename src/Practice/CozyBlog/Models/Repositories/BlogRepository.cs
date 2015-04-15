using CozyBlog.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CozyBlog.Models
{
    public class BlogRepository : IBlogRepository
    {
        CozyBlogEntities entities = null;

        public BlogRepository(CozyBlogEntities entities)
        {
            this.entities = entities;
        }

        void IBlogRepository.AddBlog(Blogs blog)
        {
            entities.Blogs.Add(blog);
        }

        public Blogs GetBlogById(int id)
        {
            return entities.Blogs.SingleOrDefault(b => b.Id == id);
        }

        public IQueryable<Blogs> GetBlogs(int categoryId)
        {
            // no category filter needed, show all blogs
            if (categoryId == 0)
            {
                return from b in entities.Blogs
                       orderby b.DatePosted descending
                       select b;
            }

            // category filter is needed
            return from b in entities.Blogs
                   orderby b.DatePosted descending
                   where b.CategoryId == categoryId
                   select b;
        }

        public List<CategoryListViewModel> GetActiveCategories()
        {
            return (from b in entities.Blogs
                    group b by b.Category into g
                    select new CategoryListViewModel
                    {
                        Category = g.Key,
                        BlogCount = g.Count()
                    }).ToList();
        }

        public void RemoveBlog(Blogs blog)
        {
            entities.Blogs.Remove(blog);
        }

        public void Save()
        {
            entities.SaveChanges();
        }
    }
}