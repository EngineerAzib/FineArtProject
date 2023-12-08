using FineArtProject.Context;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FineArtProject.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        FineArtProjectnewEntities db=new FineArtProjectnewEntities();
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
        public ActionResult show()
        {
            var res = db.ArtworkStudentTable.ToList();
            return View(res);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int student_id, int competition, Award model)
        {
            // Check if an award with the same student_id and competition already exists
            bool awardExists = db.Award.Any(a => a.Studentid == student_id && a.CompetitionId == competition);

            if (awardExists)
            {
                // An award with the same student_id and competition already exists
                // You can handle this situation accordingly, such as returning an error view or message.
                ModelState.AddModelError(string.Empty, "An award for this student and competition already exists.");
                return RedirectToAction("Index");
            }

            Award award = new Award
            {
                Studentid = student_id,
                CompetitionId = competition,
                AwadDate = DateTime.Now
            };

            // Add the award to the database
            db.Award.Add(award);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult comment()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult comment(int id, StaffComment model)
        {
            string currentUserId = User.Identity.GetUserId();
            

            // Retrieve the student associated with the logged-in user.
            var currentStudent = db.teacher.FirstOrDefault(s => s.userid == currentUserId);

            // Check if a corresponding record exists in ArtworkStudentTable for the given competition
          

          

            // Create a StaffComment object and set its properties
            StaffComment obj = new StaffComment()
            {
                staff_id = currentStudent.Id,
                art_work_Id = id,
                Mark = model.Mark,
                comments = model.comments,
            };

            // Add the StaffComment object to the database
            db.StaffComment.Add(obj);
            db.SaveChanges();

            return RedirectToAction("show");
        }
         public ActionResult ShowAward()
        {
            var res = db.Award.ToList();
            return View(res);
        }



    }






}
