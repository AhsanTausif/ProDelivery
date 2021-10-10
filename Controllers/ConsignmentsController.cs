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
    public class ConsignmentsController : Controller
    {
        private Pro_DeliveryEntities db = new Pro_DeliveryEntities();

        // GET: Consignments
        public ActionResult Index()
        {
            var consignments = db.Consignments.Include(c => c.Branch).Include(c => c.Receiver);
            return View(consignments.ToList());
        }

        // GET: Consignments/Details/5
        public ActionResult Details(string id)
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
            return View(consignment);
        }

        // GET: Consignments/Create
        public ActionResult Create()
        {
            ViewBag.Branch_Id = new SelectList(db.Branches, "Branch_Id", "Branch_Name");
            ViewBag.Rec_Id = new SelectList(db.Receivers, "Rec_Id", "Rec_Email");
            return View();
        }

        // POST: Consignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Shipper_Name,Shipper_Phone,Total_Weight,Category,No_Of_Items,Description,Branch_Name,Branch_Address,Rec_Name,Rec_Address,Branch_Id,Rec_Id,Req_Status")] Consignment consignment)
        {
            if (ModelState.IsValid)
            {
                db.Consignments.Add(consignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Branch_Id = new SelectList(db.Branches, "Branch_Id", "Branch_Name", consignment.Branch_Id);
            ViewBag.Rec_Id = new SelectList(db.Receivers, "Rec_Id", "Rec_Email", consignment.Rec_Id);
            return View(consignment);
        }

        // GET: Consignments/Edit/5
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

        // GET: Consignments/Delete/5
        public ActionResult Delete(string id)
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
            return View(consignment);
        }

        // POST: Consignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Consignment consignment = db.Consignments.Find(id);
            db.Consignments.Remove(consignment);
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
