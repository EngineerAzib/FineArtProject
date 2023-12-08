using FineArtProject.Context;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FineArtProject.Controllers
{

    public class studentController : Controller
    {
        FineArtProjectnewEntities db=new FineArtProjectnewEntities();
        // GET: student
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
        public ActionResult upcomingcompetition()
        {
            var res=db.Competition.ToList();

            return View(res);
        }
        public ActionResult showAward() {
            string currentUserId = User.Identity.GetUserId();

            // Get the current student based on the user's ID
            var currentStudent = db.student.FirstOrDefault(s => s.userid == currentUserId);

            var res = db.Award
                 .Where(a => a.Studentid == currentStudent.Id).ToList();
            return View(res);
        }
        public ActionResult showcomments()
        {
            string currentUserId = User.Identity.GetUserId();

            // Get the current student based on the user's ID
            var currentStudent = db.student.FirstOrDefault(s => s.userid == currentUserId);

            var res = db.StaffComment
                 .Where(a => a.ArtworkStudentTable.StudentId == currentStudent.Id).ToList();
            return View(res);
        }
        public ActionResult showUpcommingExibitions()
        {
            var res = db.UpcommingExibition.ToList();
            return View(res);
        }
    }
}