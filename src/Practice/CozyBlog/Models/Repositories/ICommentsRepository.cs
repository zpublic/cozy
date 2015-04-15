using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CozyBlog.Models
{
    public interface ICommentsRepository
    {
        List<Comments> GetCommentsByBlogId(int id);
        void Save();
    }
}