using FineArtProject.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FineArtProject.Controllers
{
    public class ManagarController : Controller
    {
        FineArtProjectnewEntities db=new FineArtProjectnewEntities();
        // GET: Managar
        public ActionResult Index()
        {
            using (var db = new FineArtProjectnewEntities()) // Replace YourDbContext with your actual database context type
            {
                int studentCount = db.student.Count(); // Replace Students with your actual DbSet name.
                int teacherCount = db.teacher.Count(); // Replace Awards with your actual DbSet name.
                int competitionCount = db.Competition.Count(); // Replace Competitions with your actual DbSet name.
                int awardCount = db.Award.Count();
                int exhibitionCount = db.UpcommingExibition.Count();
                int Artwork = db.ArtworkStudentTable.Count();
                ViewBag.StudentCount = studentCount;
                ViewBag.AwardCount = awardCount;
                ViewBag.CompetitionCount = competitionCount;
                ViewBag.TeacherCount = teacherCount;
                ViewBag.ExhibitionCount = exhibitionCount;
                ViewBag.ArtWorkCount = Artwork;
            }
            return View();
        }
        public ActionResult StudentDetail()
        {
            var res = db.student.ToList();

            return View(res);
        }
        public ActionResult TeacherDetail()
        {
            var res = db.teacher.ToList();

            return View(res);
        }
        public ActionResult Competition()
        {
            var res = db.Competition.ToList();

            return View(res);
        }
        public ActionResult ArtWork()
        {
            var res = db.ArtworkStudentTable.ToList();

            return View(res);
        }
        public ActionResult comments()
        {
            var res = db.StaffComment.ToList();

            return View(res);
        }
        public ActionResult Exhibition()
        {
            var res = db.UpcommingExibition.ToList();

            return View(res);
        }

    }
}