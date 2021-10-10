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
    public class BranchesController : Controller
    {
        private Pro_DeliveryEntities db = new Pro_DeliveryEntities();

        // GET: Branches
        public ActionResult Index()
        {
            var branches = db.Branches.Include(b => b.Admin).Include(b => b.Receiver).Include(b => b.Sender);
            return View(branches.ToList());
        }

        // GET: Branches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // GET: Branches/Create
        public ActionResult Create()
        {
            ViewBag.Admin_Id = new SelectList(db.Admins, "Admin_Id", "Admin_Email");
            ViewBag.Rec_Id = new SelectList(db.Receivers, "Rec_Id", "Rec_Email");
            ViewBag.Sender_Id = new SelectList(db.Senders, "Sender_Id", "Sender_Email");
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Branch_Id,Branch_Name,Branch_Address,Branch_Phone,Pincode,Request_Id,Rec_Id,Sender_Id,Admin_Id")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Branches.Add(branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Admin_Id = new SelectList(db.Admins, "Admin_Id", "Admin_Email", branch.Admin_Id);
            ViewBag.Rec_Id = new SelectList(db.Receivers, "Rec_Id", "Rec_Email", branch.Rec_Id);
            ViewBag.Sender_Id = new SelectList(db.Senders, "Sender_Id", "Sender_Email", branch.Sender_Id);
            return View(branch);
        }

        // GET: Branches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_Id = new SelectList(db.Admins, "Admin_Id", "Admin_Email", branch.Admin_Id);
            ViewBag.Rec_Id = new SelectList(db.Receivers, "Rec_Id", "Rec_Email", branch.Rec_Id);
            ViewBag.Sender_Id = new SelectList(db.Senders, "Sender_Id", "Sender_Email", branch.Sender_Id);
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Branch_Id,Branch_Name,Branch_Address,Branch_Phone,Pincode,Request_Id,Rec_Id,Sender_Id,Admin_Id")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_Id = new SelectList(db.Admins, "Admin_Id", "Admin_Email", branch.Admin_Id);
            ViewBag.Rec_Id = new SelectList(db.Receivers, "Rec_Id", "Rec_Email", branch.Rec_Id);
            ViewBag.Sender_Id = new SelectList(db.Senders, "Sender_Id", "Sender_Email", branch.Sender_Id);
            return View(branch);
        }

        // GET: Branches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Branch branch = db.Branches.Find(id);
            db.Branches.Remove(branch);
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
