using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CozyBlog.Models
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        void AddCategory(Category category);
        Category GetCategoryById(int id);
        void Save();
    }
}