using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProDelivery.Models;

namespace ProDelivery.Controllers
{
    public class AdminsController : Controller
    {
        private Pro_DeliveryEntities db = new Pro_DeliveryEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Check()
        {
            return View();
        }



        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TempUser3 tempUser3)
        {
            if (ModelState.IsValid)
            {
                var user = db.Admins.Where(u => u.Admin_Email.Equals(tempUser3.Admin_Email)
                && u.Admin_Password.Equals(tempUser3.Admin_Password)).FirstOrDefault();

                if (user != null)
                {
                    Session["admin_email"] = user.Admin_Email;
                    HttpCookie cookie = new HttpCookie("user", "user_email");
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookie);
                    return RedirectToAction("AdminProfile");
                }
                else
                {
                    ViewBag.Error = "Admin with such Email or Password does not exist!";
                }
            }

            return View();
        }

        public ActionResult AdminProfile()
        {
            HttpCookie cookie = HttpContext.Request.Cookies.Get("user");
            string email = Convert.ToString(Session["admin_email"]);
            var user = db.Admins.Where(u => u.Admin_Email.Equals(email)).FirstOrDefault();

            return View(user);
        }

        public ActionResult SignOut()
        {
            if (Request.Cookies["user"] != null)
            {
                var c = new HttpCookie("user")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(c);
            }
            Session.Abandon();
            return RedirectToAction("Login", "Admins");
        }

    }
}
