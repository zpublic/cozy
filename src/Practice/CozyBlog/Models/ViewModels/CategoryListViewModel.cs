using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CozyBlog.Models.ViewModels
{
    public class CategoryListViewModel
    {
        public Category Category { get; set; }
        public int BlogCount { get; set; }
    }
}