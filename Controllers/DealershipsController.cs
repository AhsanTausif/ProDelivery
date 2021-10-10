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
    public class DealershipsController : Controller
    {
        private Pro_DeliveryEntities db = new Pro_DeliveryEntities();

        // GET: Dealerships
        public ActionResult Index()
        {
            var dealerships = db.Dealerships.Include(d => d.Admin).Include(d => d.Sender);
            return View(dealerships.ToList());
        }

        // GET: Dealerships/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealership dealership = db.Dealerships.Find(id);
            if (dealership == null)
            {
                return HttpNotFound();
            }
            return View(dealership);
        }

        // GET: Dealerships/Create
        public ActionResult Create()
        {
            ViewBag.Admin_Id = new SelectList(db.Admins, "Admin_Id", "Admin_Email");
            ViewBag.Sender_Id = new SelectList(db.Senders, "Sender_Id", "Sender_Email");
            return View();
        }

        // POST: Dealerships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Dealership_Id,Sender_Name,Branch_Address,Date_Of_Apply,Money_Deposited,Sender_Id,Admin_Id")] Dealership dealership)
        {
            if (ModelState.IsValid)
            {
                db.Dealerships.Add(dealership);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Admin_Id = new SelectList(db.Admins, "Admin_Id", "Admin_Email", dealership.Admin_Id);
            ViewBag.Sender_Id = new SelectList(db.Senders, "Sender_Id", "Sender_Email", dealership.Sender_Id);
            return View(dealership);
        }

        // GET: Dealerships/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealership dealership = db.Dealerships.Find(id);
            if (dealership == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_Id = new SelectList(db.Admins, "Admin_Id", "Admin_Email", dealership.Admin_Id);
            ViewBag.Sender_Id = new SelectList(db.Senders, "Sender_Id", "Sender_Email", dealership.Sender_Id);
            return View(dealership);
        }

        // POST: Dealerships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Dealership_Id,Sender_Name,Branch_Address,Date_Of_Apply,Money_Deposited,Sender_Id,Admin_Id")] Dealership dealership)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dealership).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_Id = new SelectList(db.Admins, "Admin_Id", "Admin_Email", dealership.Admin_Id);
            ViewBag.Sender_Id = new SelectList(db.Senders, "Sender_Id", "Sender_Email", dealership.Sender_Id);
            return View(dealership);
        }

        // GET: Dealerships/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealership dealership = db.Dealerships.Find(id);
            if (dealership == null)
            {
                return HttpNotFound();
            }
            return View(dealership);
        }

        // POST: Dealerships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dealership dealership = db.Dealerships.Find(id);
            db.Dealerships.Remove(dealership);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
