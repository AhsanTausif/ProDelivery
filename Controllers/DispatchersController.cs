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
    public class DispatchersController : Controller
    {
        private Pro_DeliveryEntities db = new Pro_DeliveryEntities();

        // GET: Dispatchers
        public ActionResult Index()
        {
            var dispatchers = db.Dispatchers.Include(d => d.Admin).Include(d => d.Receiver).Include(d => d.Sender);
            return View(dispatchers.ToList());
        }

        // GET: Dispatchers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispatcher dispatcher = db.Dispatchers.Find(id);
            if (dispatcher == null)
            {
                return HttpNotFound();
            }
            return View(dispatcher);
        }

        // GET: Dispatchers/Create
        public ActionResult Create()
        {
            ViewBag.Admin_Id = new SelectList(db.Admins, "Admin_Id", "Admin_Email");
            ViewBag.Rec_Id = new SelectList(db.Receivers, "Rec_Id", "Rec_Email");
            ViewBag.Sender_Id = new SelectList(db.Senders, "Sender_Id", "Sender_Email");
            return View();
        }

        // POST: Dispatchers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Dispatcher_Id,Rec_Name,Sender_Name,Branch_Name,Description,Rec_Id,Sender_Id,Admin_Id")] Dispatcher dispatcher)
        {
            if (ModelState.IsValid)
            {
                db.Dispatchers.Add(dispatcher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Admin_Id = new SelectList(db.Admins, "Admin_Id", "Admin_Email", dispatcher.Admin_Id);
            ViewBag.Rec_Id = new SelectList(db.Receivers, "Rec_Id", "Rec_Email", dispatcher.Rec_Id);
            ViewBag.Sender_Id = new SelectList(db.Senders, "Sender_Id", "Sender_Email", dispatcher.Sender_Id);
            return View(dispatcher);
        }

        // GET: Dispatchers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispatcher dispatcher = db.Dispatchers.Find(id);
            if (dispatcher == null)
            {
                return HttpNotFound();
            }
            ViewBag.Admin_Id = new SelectList(db.Admins, "Admin_Id", "Admin_Email", dispatcher.Admin_Id);
            ViewBag.Rec_Id = new SelectList(db.Receivers, "Rec_Id", "Rec_Email", dispatcher.Rec_Id);
            ViewBag.Sender_Id = new SelectList(db.Senders, "Sender_Id", "Sender_Email", dispatcher.Sender_Id);
            return View(dispatcher);
        }

        // POST: Dispatchers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Dispatcher_Id,Rec_Name,Sender_Name,Branch_Name,Description,Rec_Id,Sender_Id,Admin_Id")] Dispatcher dispatcher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dispatcher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Admin_Id = new SelectList(db.Admins, "Admin_Id", "Admin_Email", dispatcher.Admin_Id);
            ViewBag.Rec_Id = new SelectList(db.Receivers, "Rec_Id", "Rec_Email", dispatcher.Rec_Id);
            ViewBag.Sender_Id = new SelectList(db.Senders, "Sender_Id", "Sender_Email", dispatcher.Sender_Id);
            return View(dispatcher);
        }

        // GET: Dispatchers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispatcher dispatcher = db.Dispatchers.Find(id);
            if (dispatcher == null)
            {
                return HttpNotFound();
            }
            return View(dispatcher);
        }

        // POST: Dispatchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dispatcher dispatcher = db.Dispatchers.Find(id);
            db.Dispatchers.Remove(dispatcher);
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
