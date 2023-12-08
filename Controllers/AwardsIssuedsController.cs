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
    public class AwardsIssuedsController : Controller
    {
        private FineArtProjectnewEntities db = new FineArtProjectnewEntities();

        // GET: AwardsIssueds
        public ActionResult Index()
        {
            var awardsIssued = db.AwardsIssued.Include(a => a.Competition).Include(a => a.student);
            return View(awardsIssued.ToList());
        }

        // GET: AwardsIssueds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AwardsIssued awardsIssued = db.AwardsIssued.Find(id);
            if (awardsIssued == null)
            {
                return HttpNotFound();
            }
            return View(awardsIssued);
        }

        // GET: AwardsIssueds/Create
        public ActionResult Create()
        {
            ViewBag.competition_Id = new SelectList(db.Competition, "Id", "CompetitionName");
            ViewBag.student_Id = new SelectList(db.student, "Id", "Address");
            return View();
        }

        // POST: AwardsIssueds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,student_Id,competition_Id,AwardDate,Remarks")] AwardsIssued awardsIssued)
        {
            if (ModelState.IsValid)
            {
                db.AwardsIssued.Add(awardsIssued);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.competition_Id = new SelectList(db.Competition, "Id", "CompetitionName", awardsIssued.competition_Id);
            ViewBag.student_Id = new SelectList(db.student, "Id", "Address", awardsIssued.student_Id);
            return View(awardsIssued);
        }

        // GET: AwardsIssueds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AwardsIssued awardsIssued = db.AwardsIssued.Find(id);
            if (awardsIssued == null)
            {
                return HttpNotFound();
            }
            ViewBag.competition_Id = new SelectList(db.Competition, "Id", "CompetitionName", awardsIssued.competition_Id);
            ViewBag.student_Id = new SelectList(db.student, "Id", "Address", awardsIssued.student_Id);
            return View(awardsIssued);
        }

        // POST: AwardsIssueds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,student_Id,competition_Id,AwardDate,Remarks")] AwardsIssued awardsIssued)
        {
            if (ModelState.IsValid)
            {
                db.Entry(awardsIssued).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.competition_Id = new SelectList(db.Competition, "Id", "CompetitionName", awardsIssued.competition_Id);
            ViewBag.student_Id = new SelectList(db.student, "Id", "Address", awardsIssued.student_Id);
            return View(awardsIssued);
        }

        // GET: AwardsIssueds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AwardsIssued awardsIssued = db.AwardsIssued.Find(id);
            if (awardsIssued == null)
            {
                return HttpNotFound();
            }
            return View(awardsIssued);
        }

        // POST: AwardsIssueds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AwardsIssued awardsIssued = db.AwardsIssued.Find(id);
            db.AwardsIssued.Remove(awardsIssued);
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
