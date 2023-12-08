using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FineArtProject.Context;

namespace FineArtProject.Controllers
{
    public class UpcommingExibitionsController : Controller
    {
        private FineArtProjectnewEntities db = new FineArtProjectnewEntities();

        // GET: UpcommingExibitions
        public ActionResult Index()
        {
            return View(db.UpcommingExibition.ToList());
        }

        // GET: UpcommingExibitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UpcommingExibition upcommingExibition = db.UpcommingExibition.Find(id);
            if (upcommingExibition == null)
            {
                return HttpNotFound();
            }
            return View(upcommingExibition);
        }

        // GET: UpcommingExibitions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UpcommingExibitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExhibitionName,Location,Date,Description")] UpcommingExibition upcommingExibition)
        {
            if (ModelState.IsValid)
            {
                db.UpcommingExibition.Add(upcommingExibition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(upcommingExibition);
        }

        // GET: UpcommingExibitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UpcommingExibition upcommingExibition = db.UpcommingExibition.Find(id);
            if (upcommingExibition == null)
            {
                return HttpNotFound();
            }
            return View(upcommingExibition);
        }

        // POST: UpcommingExibitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExhibitionName,Location,Date,Description")] UpcommingExibition upcommingExibition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(upcommingExibition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(upcommingExibition);
        }

        // GET: UpcommingExibitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UpcommingExibition upcommingExibition = db.UpcommingExibition.Find(id);
            if (upcommingExibition == null)
            {
                return HttpNotFound();
            }
            return View(upcommingExibition);
        }

        // POST: UpcommingExibitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UpcommingExibition upcommingExibition = db.UpcommingExibition.Find(id);
            db.UpcommingExibition.Remove(upcommingExibition);
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
