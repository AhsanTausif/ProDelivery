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
    public class SendersController : Controller
    {
        private Pro_DeliveryEntities db = new Pro_DeliveryEntities();

        [HttpGet]
        public ActionResult SignUp()
        {    
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Sender sender)
        {
            if (ModelState.IsValid) {
                 if (db.Senders.Where(u => u.Sender_Email == sender.Sender_Email).Any())
                  {
                    ViewBag.EmailTaken = "Email has been already taken by another User!";
                    return View();
                  }
                  else if (db.Senders.Where(u => u.Sender_Name == sender.Sender_Name).Any())
                  {
                    ViewBag.NameTaken = "UserName has been already taken by another User!";
                    return View();
                  }
                  else if (db.Senders.Where(u => u.Sender_Password == sender.Sender_Password).Any())
                  {
                    ViewBag.PasswordTaken = "Password has been already taken by another User!";
                    return View();
                  }
                  else if (db.Senders.Where(u => u.Sender_Phone == sender.Sender_Phone).Any())
                  {
                    ViewBag.ContactTaken = "Email has been already taken by another User!";
                    return View();
                  }
                 else {
                    db.Senders.Add(sender);
                    db.SaveChanges();

                    return RedirectToAction("Login");
                   }
            }
            return View();
        } 

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TempUser2 tempUser2)
        {
            if (ModelState.IsValid)
            {
                var user = db.Senders.Where(u => u.Sender_Email.Equals(tempUser2.Sender_Email)
                && u.Sender_Password.Equals(tempUser2.Sender_Password)).FirstOrDefault();

                if (user != null)
                {
                    Session["sec_user_email"] = user.Sender_Address;
                    HttpCookie cookie = new HttpCookie("user", "user_email");
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookie);
                    return RedirectToAction("UserProfile");
                }
                else 
                {
                    ViewBag.Error = "User with such Email or Password does not exist!";
                }
            }

            return View();
        }

        public ActionResult UserProfile()
        {
            HttpCookie cookie = HttpContext.Request.Cookies.Get("user");
            string email = Convert.ToString(Session["sec_user_email"]);
            var user = db.Senders.Where(u => u.Sender_Email.Equals(email)).FirstOrDefault();

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
            return RedirectToAction("Login", "Senders");
        }
        public ActionResult p2p()
        {
            return View();
        }

        public ActionResult air()
        {
            return View();
        }

        public ActionResult corporate()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Services()
        {
            var receiver = db.Receivers;
            var branch = db.Branches;
            ViewBag.Branch_Id = new SelectList(branch, "Branch_Id", "Branch_Id");
            ViewBag.Branch_Name = new SelectList(branch, "Branch_Name", "Branch_Name");
            ViewBag.Branch_Address = new SelectList(branch, "Branch_Address", "Branch_Address");
            ViewBag.Rec_Id = new SelectList(receiver, "Rec_Id", "Rec_Id");
            ViewBag.Rec_Name = new SelectList(receiver, "Rec_Name", "Rec_Name");
            ViewBag.Rec_Address = new SelectList(receiver, "Rec_Address", "Rec_Address");
            return View();
        }

        [HttpPost]
        public ActionResult Services(Consignment consignment)
        {
            if (ModelState.IsValid)
            {
                db.Consignments.Add(consignment);
                db.SaveChanges();

                return Content("Package Delivered");
            }
            return View();
        }

        public ActionResult Track()
        {
            var consignments = db.Consignments.Include(c => c.Branch).Include(c => c.Receiver);
            return View(consignments.ToList());
        }

        [HttpPost]
        public ActionResult Track(string Senderphone)
        {
            ViewBag.number = Senderphone;
            var consignments = db.Consignments.Include(c => c.Branch).Include(c => c.Receiver);
            return View(consignments.ToList());
        }
    }
}
