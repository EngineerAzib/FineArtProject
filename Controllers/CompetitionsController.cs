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
    public class CompetitionsController : Controller
    {
        private FineArtProjectnewEntities db = new FineArtProjectnewEntities();

        // GET: Competitions
        public ActionResult Index()
        {
            return View(db.Competition.ToList());
        }

        // GET: Competitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competition competition = db.Competition.Find(id);
            if (competition == null)
            {
                return HttpNotFound();
            }
            return View(competition);
        }

        // GET: Competitions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Competitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CompetitionName,StartDate,EndDate,Conditions,AwardDetails")] Competition competition)
        {
            if (ModelState.IsValid)
            {
                db.Competition.Add(competition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(competition);
        }

        // GET: Competitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competition competition = db.Competition.Find(id);
            if (competition == null)
            {
                return HttpNotFound();
            }
            return View(competition);
        }

        // POST: Competitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CompetitionName,StartDate,EndDate,Conditions,AwardDetails")] Competition competition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(competition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(competition);
        }

        // GET: Competitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competition competition = db.Competition.Find(id);
            if (competition == null)
            {
                return HttpNotFound();
            }
            return View(competition);
        }

        // POST: Competitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Competition competition = db.Competition.Find(id);
            db.Competition.Remove(competition);
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
