using ProDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProDelivery.Controllers
{
    public class HomeController : Controller
    {
        private Pro_DeliveryEntities db = new Pro_DeliveryEntities();
        public ActionResult Index()
        {
            //HttpCookie cookie = HttpContext.Request.Cookies.Get("user");
            return View();
        }

        public ActionResult About()
        {
            HttpCookie cookie = HttpContext.Request.Cookies.Get("user");
            return View(db.Aboutus.ToList());
        }
        public ActionResult Team()
        {
            return View();
        }

        public ActionResult coverage()
        {
            var branches = db.Branches;
            return View(branches.ToList());
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}