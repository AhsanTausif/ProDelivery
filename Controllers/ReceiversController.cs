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
    public class ReceiversController : Controller
    {
        private Pro_DeliveryEntities db = new Pro_DeliveryEntities();

        public ActionResult Index()
        {
            var consignments = db.Consignments.Include(c => c.Branch).Include(c => c.Receiver);
            return View(consignments.ToList());
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consignment consignment = db.Consignments.Find(id);
            if (consignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Branch_Id = new SelectList(db.Branches, "Branch_Id", "Branch_Name", consignment.Branch_Id);
            ViewBag.Rec_Id = new SelectList(db.Receivers, "Rec_Id", "Rec_Email", consignment.Rec_Id);
            return View(consignment);
        }

        // POST: Consignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Shipper_Name,Shipper_Phone,Total_Weight,Category,No_Of_Items,Description,Branch_Name,Branch_Address,Rec_Name,Rec_Address,Branch_Id,Rec_Id,Req_Status")] Consignment consignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Branch_Id = new SelectList(db.Branches, "Branch_Id", "Branch_Name", consignment.Branch_Id);
            ViewBag.Rec_Id = new SelectList(db.Receivers, "Rec_Id", "Rec_Email", consignment.Rec_Id);
            return View(consignment);
        }


        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Receiver receiver)
        {
            if (ModelState.IsValid)
            {
                if (db.Receivers.Where(u => u.Rec_Email == receiver.Rec_Email).Any())
                {
                    ViewBag.EmailTaken = "Email has been already taken by another User!";
                    return View();
                }
                else if (db.Receivers.Where(u => u.Rec_Name == receiver.Rec_Name).Any())
                {
                    ViewBag.NameTaken = "UserName has been already taken by another User!";
                    return View();
                }
                else if (db.Receivers.Where(u => u.Rec_Password == receiver.Rec_Password).Any())
                {
                    ViewBag.PasswordTaken = "Password has been already taken by another User!";
                    return View();
                }
                else if (db.Receivers.Where(u => u.Rec_Phone == receiver.Rec_Phone).Any())
                {
                    ViewBag.ContactTaken = "Email has been already taken by another User!";
                    return View();
                }
                else
                {
                    db.Receivers.Add(receiver);
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
        public ActionResult Login(TempUser tempUser)
        {
            if (ModelState.IsValid)
            {
                var user = db.Receivers.Where(u => u.Rec_Email.Equals(tempUser.Rec_Email)
                && u.Rec_Password.Equals(tempUser.Rec_Password)).FirstOrDefault();

                if (user != null)
                {   
                    Session["rec_user_email"] = user.Rec_Email;
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

        public ActionResult ReceiverProfile(string email)
        {   
            var user = db.Receivers.Where(u => u.Rec_Email.Equals(email)).FirstOrDefault();

            return View(user);
        }

        public ActionResult UserProfile()
        {
            HttpCookie cookie = HttpContext.Request.Cookies.Get("user");
            string email = Convert.ToString(Session["rec_user_email"]);
            var user = db.Receivers.Where(u => u.Rec_Email.Equals(email)).FirstOrDefault();

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
            return RedirectToAction("Login","Receivers");
        }
    }
}
