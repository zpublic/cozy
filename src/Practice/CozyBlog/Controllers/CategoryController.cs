using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CozyBlog.Models;

namespace CozyBlog.Controllers
{
    [Authorize(Roles = "Authors")]
    public class CategoryController : AController
    {
        // use our DbContext unit of work in case the page runs
        public CategoryController()
            : this(new UnitOfWork())
        {

        }

        // We will directly call this from the test projects
        public CategoryController(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        //
        // GET: /Category/

        [AllowAnonymous]
        public ActionResult Index()
        {
            List<Category> categories = UnitOfWork_.CategoryRepo.GetCategories();

            return View(categories);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UnitOfWork_.CategoryRepo.AddCategory(category);
                    UnitOfWork_.CategoryRepo.Save();
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id)
        {
            Category category = UnitOfWork_.CategoryRepo.GetCategoryById(id);
            return View(category);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection formCollection)
        {
            try
            {
                Category category = UnitOfWork_.CategoryRepo.GetCategoryById(id);

                if (category != null && TryUpdateModel(category))
                {
                    UnitOfWork_.CategoryRepo.Save();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", new { id = id });
            }
        }
    }
}
