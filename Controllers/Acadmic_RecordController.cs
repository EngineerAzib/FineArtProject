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
    public class Acadmic_RecordController : Controller
    {
        private FineArtProjectnewEntities db = new FineArtProjectnewEntities();

        // GET: Acadmic_Record
        public ActionResult Index()
        {
            var acadmic_Record = db.Acadmic_Record.Include(a => a.Competition).Include(a => a.student);
            return View(acadmic_Record.ToList());
        }

        // GET: Acadmic_Record/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acadmic_Record acadmic_Record = db.Acadmic_Record.Find(id);
            if (acadmic_Record == null)
            {
                return HttpNotFound();
            }
            return View(acadmic_Record);
        }

        // GET: Acadmic_Record/Create
        public ActionResult Create()
        {
            ViewBag.competition_id = new SelectList(db.Competition, "Id", "CompetitionName");
            ViewBag.student_id = new SelectList(db.student, "Id", "Address");
            return View();
        }

        // POST: Acadmic_Record/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,student_id,competition_id,SubmisionDate,Marks,Remarks")] Acadmic_Record acadmic_Record)
        {
            if (ModelState.IsValid)
            {
                db.Acadmic_Record.Add(acadmic_Record);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.competition_id = new SelectList(db.Competition, "Id", "CompetitionName", acadmic_Record.competition_id);
            ViewBag.student_id = new SelectList(db.student, "Id", "Address", acadmic_Record.student_id);
            return View(acadmic_Record);
        }

        // GET: Acadmic_Record/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acadmic_Record acadmic_Record = db.Acadmic_Record.Find(id);
            if (acadmic_Record == null)
            {
                return HttpNotFound();
            }
            ViewBag.competition_id = new SelectList(db.Competition, "Id", "CompetitionName", acadmic_Record.competition_id);
            ViewBag.student_id = new SelectList(db.student, "Id", "Address", acadmic_Record.student_id);
            return View(acadmic_Record);
        }

        // POST: Acadmic_Record/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,student_id,competition_id,SubmisionDate,Marks,Remarks")] Acadmic_Record acadmic_Record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(acadmic_Record).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.competition_id = new SelectList(db.Competition, "Id", "CompetitionName", acadmic_Record.competition_id);
            ViewBag.student_id = new SelectList(db.student, "Id", "Address", acadmic_Record.student_id);
            return View(acadmic_Record);
        }

        // GET: Acadmic_Record/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acadmic_Record acadmic_Record = db.Acadmic_Record.Find(id);
            if (acadmic_Record == null)
            {
                return HttpNotFound();
            }
            return View(acadmic_Record);
        }

        // POST: Acadmic_Record/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Acadmic_Record acadmic_Record = db.Acadmic_Record.Find(id);
            db.Acadmic_Record.Remove(acadmic_Record);
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
