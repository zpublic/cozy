using CozyBlog.Models;
using CozyBlog.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CozyBlog.Controllers
{
    public class BlogController : AController
    {
        // use our DbContext unit of work in case the page runs
        public BlogController()
            : this(new UnitOfWork())
        {

        }

        // We will directly call this from the test projects
        public BlogController(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        //
        // GET: /Blog/

        // Show a list of blog for this user only, this will be a dashboard screen for the user
        public ActionResult Index(int id = 0, int catagoryId = 0)
        {
            if (catagoryId == 0)
            {
                ViewData["Message"] = "Showing a list of all blogs";
            }
            else
            {
                ViewData["Message"] = string.Format("Blogs under Category: {0}", UnitOfWork_.CategoryRepo.GetCategoryById(catagoryId).CategoryName);
            }

            IQueryable<Blogs> blogs = UnitOfWork_.BlogRepo.GetBlogs(catagoryId);

            PaginatedViewBlogList paginatedList = new PaginatedViewBlogList(blogs, id, 5);

            return View(paginatedList);
        }

        public ActionResult List(int id = 0, int catagoryId = 0)
        {
            if (catagoryId == 0)
            {
                ViewData["Message"] = "Showing a list of all blogs";
            }
            else
            {
                ViewData["Message"] = string.Format("Blogs under Category: {0}", UnitOfWork_.CategoryRepo.GetCategoryById(catagoryId).CategoryName);
            }

            List<Blogs> blogs = UnitOfWork_.BlogRepo.GetBlogs(catagoryId).ToList();

            return View(blogs);
        }

        //
        // GET: /Blog/Details/5

        public ActionResult Details(int id)
        {
            Blogs blog = UnitOfWork_.BlogRepo.GetBlogById(id);
            ViewBlogViewModel viewModel = null;

            if (blog != null)
            {
                List<Comments> comments = UnitOfWork_.CommentsRepo.GetCommentsByBlogId(id);
                string categoryName = UnitOfWork_.CategoryRepo.GetCategoryById(blog.CategoryId).CategoryName;

                viewModel = new ViewBlogViewModel(blog, comments, categoryName);
            }


            return View(viewModel);
        }

        //
        // GET: /Blog/Create

        [Authorize(Roles = "Authors")]
        public ActionResult Create()
        {
            Blogs blog = new Blogs();

            CreateBlogViewModel viewModel = new CreateBlogViewModel
            (
                blog,
                UnitOfWork_.CategoryRepo.GetCategories()
            );

            return View(viewModel);
        }

        //
        // POST: /Blog/Create

        [HttpPost, Authorize(Roles = "Authors")]
        public ActionResult Create(Blogs blog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    blog.PostedBy = User.Identity.Name;
                    blog.DatePosted = DateTime.Now;

                    UnitOfWork_.BlogRepo.AddBlog(blog);
                    UnitOfWork_.BlogRepo.Save();

                    return RedirectToAction("Index");
                }
                else
                {
                    CreateBlogViewModel viewModel = new CreateBlogViewModel
                    (
                        blog,
                        UnitOfWork_.CategoryRepo.GetCategories()
                    );

                    return View(viewModel);
                }


            }
            catch
            {
                CreateBlogViewModel viewModel = new CreateBlogViewModel
                (
                    blog,
                    UnitOfWork_.CategoryRepo.GetCategories()
                );

                return View(viewModel);
            }
        }

        //
        // GET: /Blog/Edit/5
        [Authorize(Roles = "Authors")]
        public ActionResult Edit(int id)
        {
            Blogs blog = UnitOfWork_.BlogRepo.GetBlogById(id);

            CreateBlogViewModel viewModel = new CreateBlogViewModel
            (
                blog,
                UnitOfWork_.CategoryRepo.GetCategories()
            );

            return View(viewModel);
        }

        //
        // POST: /Blog/Edit/5

        [HttpPost]
        [Authorize(Roles = "Authors")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Blogs blog = UnitOfWork_.BlogRepo.GetBlogById(id);

            try
            {
                if (blog != null && TryUpdateModel(blog, "Blog"))
                {
                    blog.PostedBy = User.Identity.Name;
                    blog.DatePosted = DateTime.Now;

                    UnitOfWork_.BlogRepo.Save();

                    return RedirectToAction("Index");
                }

                else
                {
                    CreateBlogViewModel viewModel = new CreateBlogViewModel
                    (
                        blog,
                        UnitOfWork_.CategoryRepo.GetCategories()
                    );

                    return View(viewModel);
                }
            }
            catch
            {
                CreateBlogViewModel viewModel = new CreateBlogViewModel
                (
                    blog,
                    UnitOfWork_.CategoryRepo.GetCategories()
                );

                return View(viewModel);
            }
        }

        //
        // GET: /Blog/Delete/5
        [Authorize(Roles = "Authors")]
        public ActionResult Delete(int id)
        {
            Blogs blog = UnitOfWork_.BlogRepo.GetBlogById(id);

            return View(blog);
        }

        //
        // POST: /Blog/Delete/5

        [HttpPost]
        [Authorize(Roles = "Authors")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Blogs blog = UnitOfWork_.BlogRepo.GetBlogById(id);

                UnitOfWork_.BlogRepo.RemoveBlog(blog);
                UnitOfWork_.BlogRepo.Save();
                return View("Deleted");
            }
            catch
            {
                return RedirectToAction("Delete");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddComment(int id, Comments comment)
        {
            try
            {
                Blogs blog = UnitOfWork_.BlogRepo.GetBlogById(id);
                if (blog != null)
                {
                    if (ModelState.IsValid)
                    {
                        comment.Name = User.Identity.Name;
                        comment.DatePosted = DateTime.Now;

                        blog.Comments.Add(comment);
                        UnitOfWork_.BlogRepo.Save();
                    }
                }
            }
            catch
            {
                return View("Details", new { id = id });
            }
            return RedirectToAction("Details", new { id = id });
        }
    }
}
