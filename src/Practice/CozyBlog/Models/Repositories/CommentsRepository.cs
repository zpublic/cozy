using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CozyBlog.Models
{
    public class CommentsRepository : ICommentsRepository
    {
        CozyBlogEntities entities = null;

        public CommentsRepository(CozyBlogEntities entities)
        {
            this.entities = entities;
        }

        public List<Comments> GetCommentsByBlogId(int id)
        {
            return entities.Comments.Where(c => c.BlogId == id).ToList();
        }

        public void Save()
        {
            entities.SaveChanges();
        }
    }
}