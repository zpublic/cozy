using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CozyBlog.Models
{
    public interface IUnitOfWork
    {
       ICategoryRepository CategoryRepo {get; }
       IBlogRepository BlogRepo { get; }
       ICommentsRepository CommentsRepo { get; }
    }
}