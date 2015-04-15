using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CozyBlog.Models;

namespace CozyBlog.Controllers
{
    public abstract class AController : Controller
    {
        private IUnitOfWork IUnitOfWork;

        protected IUnitOfWork UnitOfWork_
        {
            get { return IUnitOfWork; }
        }

        public AController(IUnitOfWork uow)
        {
            this.IUnitOfWork = uow;
            //ViewData["Categories"] = UnitOfWork_.BlogRepo.GetActiveCategories();
        }
    }
}
