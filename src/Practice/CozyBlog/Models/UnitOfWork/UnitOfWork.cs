using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CozyBlog.Models
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        CozyBlogEntities entities = null;
        ICategoryRepository categoryRepo = null;
        IBlogRepository blogRepo = null;
        ICommentsRepository commentsRepo = null;

        public UnitOfWork()
        {
            entities = new CozyBlogEntities();
            categoryRepo = new CategoryRepository(entities);
            blogRepo = new BlogRepository(entities);
            commentsRepo = new CommentsRepository(entities);
        }

        public ICategoryRepository CategoryRepo
        {
            get
            {
                return categoryRepo;
            }
        }

        public IBlogRepository BlogRepo
        {
            get
            {
                return blogRepo;
            }
        }

        public ICommentsRepository CommentsRepo
        {
            get
            {
                return commentsRepo;
            }
        }


        public void Dispose()
        {
            // If this function is being called the user wants to release the
            // resources. lets call the Dispose which will do this for us.
            Dispose(true);

            // Now since we have done the cleanup already there is nothing left
            // for the Finalizer to do. So lets tell the GC not to call it later.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                //someone want the deterministic release of all resources
                //Let us release all the managed resources
                entities = null;
            }
        }

        ~UnitOfWork()
        {
            // The object went out of scope and finalized is called
            // Lets call dispose in to release unmanaged resources 
            // the managed resources will anyways be released when GC 
            // runs the next time.
            Dispose(false);
        }
    }
}