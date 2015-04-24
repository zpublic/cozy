using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CozyNegativeEnergy.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Index/
        public ActionResult Index()
        {
            ViewBag.fnl = "不要总说自己一无所有好吗？你还有病呢呀。你还有穷丑矮搓呀。";
            return View();
        }

    }
}
