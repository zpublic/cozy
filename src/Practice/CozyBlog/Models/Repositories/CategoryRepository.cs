using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CozyBlog.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        CozyBlogEntities entities = null;

        public CategoryRepository(CozyBlogEntities entities)
        {
            this.entities = entities;
        }

        public List<Category> GetCategories()
        {
            return entities.Category.ToList();
        }

        public void AddCategory(Category category)
        {
            entities.Category.Add(category);
        }

        public Category GetCategoryById(int id)
        {
            Category category = entities.Category.SingleOrDefault(c => c.Id == id);

            return category;
        }
               
        public void Save()
        {
            entities.SaveChanges();
        }
    }
}