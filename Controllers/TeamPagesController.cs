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
    public class TeamPagesController : Controller
    {
        private Pro_DeliveryEntities db = new Pro_DeliveryEntities();

        // GET: TeamPages
        public ActionResult Index()
        {   
            HttpCookie cookie = HttpContext.Request.Cookies.Get("user");
            return View(db.TeamPages.ToList());
        }

        // GET: TeamPages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamPage teamPage = db.TeamPages.Find(id);
            if (teamPage == null)
            {
                return HttpNotFound();
            }
            return View(teamPage);
        }

        // GET: TeamPages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeamPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Image,Name,Profession,Roll,Section,Group,NickName,fb,gmail")] TeamPage teamPage)
        {
            if (ModelState.IsValid)
            {
                db.TeamPages.Add(teamPage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teamPage);
        }

        // GET: TeamPages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamPage teamPage = db.TeamPages.Find(id);
            if (teamPage == null)
            {
                return HttpNotFound();
            }
            return View(teamPage);
        }

        // POST: TeamPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Image,Name,Profession,Roll,Section,Group,NickName,fb,gmail")] TeamPage teamPage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamPage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teamPage);
        }

        // GET: TeamPages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamPage teamPage = db.TeamPages.Find(id);
            if (teamPage == null)
            {
                return HttpNotFound();
            }
            return View(teamPage);
        }

        // POST: TeamPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamPage teamPage = db.TeamPages.Find(id);
            db.TeamPages.Remove(teamPage);
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
