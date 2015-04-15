using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CozyBlog.Models;
using System.Collections;

namespace CozyBlog.Models.ViewModels
{
    public class PaginatedViewBlogList : List<ViewBlogViewModel>, IEnumerable
    {
        private IUnitOfWork unitOfWork;

        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedViewBlogList(IQueryable<Blogs> source, int pageIndex, int pageSize)
        {
            this.unitOfWork = new UnitOfWork();

            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = source.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

            List<Blogs> blogs = source.Skip(PageIndex * PageSize).Take(PageSize).ToList();

            if (blogs != null)
            {
                foreach (Blogs b in blogs)
                {
                    ViewBlogViewModel model = new ViewBlogViewModel
                    (
                        b,
                        unitOfWork.CommentsRepo.GetCommentsByBlogId(b.Id),
                        unitOfWork.CategoryRepo.GetCategoryById(b.CategoryId).CategoryName
                    );

                    this.Add(model);
                }
            }
        }

        public bool HasPreviousPage
        {
            get
            {
                return PageIndex > 0;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return PageIndex + 1 < TotalPages;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (ViewBlogViewModel item in this)
            {
                yield return item;
            }
        }
    }
}