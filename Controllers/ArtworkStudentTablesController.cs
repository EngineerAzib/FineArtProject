using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FineArtProject.Context;
using Microsoft.AspNet.Identity;

namespace FineArtProject.Controllers
{
    public class ArtworkStudentTablesController : Controller
    {
        private FineArtProjectnewEntities db = new FineArtProjectnewEntities();

        // GET: ArtworkStudentTables
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();

            // Get the current student based on the user's ID
            var currentStudent = db.student.FirstOrDefault(s => s.userid == currentUserId);
            
            var artworkStudentTable = db.ArtworkStudentTable.Include(a => a.Competition).Include(a => a.student)
                 .Where(a => a.StudentId == currentStudent.Id);
            return View(artworkStudentTable.ToList());
        }

        // GET: ArtworkStudentTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtworkStudentTable artworkStudentTable = db.ArtworkStudentTable.Find(id);
            if (artworkStudentTable == null)
            {
                return HttpNotFound();
            }
            return View(artworkStudentTable);
        }

        // GET: ArtworkStudentTables/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Quotation,Image")] ArtworkStudentTable artworkStudentTable, int id, HttpPostedFileBase ImageFiles)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                int currentCompetitionId = id;

                // Retrieve the student associated with the logged-in user.
                var currentStudent = db.student.FirstOrDefault(s => s.userid == currentUserId);

                if (currentStudent == null)
                {
                    // Handle the case where the student record for the logged-in user doesn't exist.
                    // You can return an error message or redirect to a page to create the student record.
                    // For example:
                    return RedirectToAction("CreateStudent");
                }

                // Check if an award with the same student ID and competition ID already exists
                bool awardExists = db.ArtworkStudentTable.Any(a => a.StudentId == currentStudent.Id && a.CompititionId == currentCompetitionId);

                if (awardExists)
                {
                    // An award with the same student ID and competition ID already exists
                    // You can handle this situation accordingly, such as returning an error view or message.
                    // For example:
                    ModelState.AddModelError(string.Empty, "You have already submitted an artwork for this competition.");
                    return View(artworkStudentTable);
                }

                if (ImageFiles != null && ImageFiles.ContentLength > 0)
                {
                    // Generate a unique file name using the original file name, timestamp, and extension
                    string fileName = Path.GetFileNameWithoutExtension(ImageFiles.FileName);
                    string extension = Path.GetExtension(ImageFiles.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                    // Combine the file name with the server's path to the image folder
                    string imagePath = Path.Combine(Server.MapPath("~/All_Image/"), fileName);

                    // Save the uploaded image to the specified path
                    ImageFiles.SaveAs(imagePath);

                    // Update the model's Image property with the relative image path
                    artworkStudentTable.Image = "~/All_Image/" + fileName;
                }

                // Now, save the model to the database
                artworkStudentTable.StudentId = currentStudent.Id;
                artworkStudentTable.CompititionId = currentCompetitionId;
                artworkStudentTable.UploadDate = DateTime.Now;

                db.ArtworkStudentTable.Add(artworkStudentTable);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(artworkStudentTable);
        }




        // GET: Artwork/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Retrieve the existing ArtworkStudentTable record by ID
            ArtworkStudentTable existingArtwork = db.ArtworkStudentTable.Find(id);

            if (existingArtwork == null)
            {
                return HttpNotFound();
            }

            // Pass the model to the Create view, which can be used for both create and edit
            return View("Edit", existingArtwork);
        }

        // POST: Artwork/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Quotation,Image")] ArtworkStudentTable updatedArtwork, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing ArtworkStudentTable record by ID
                ArtworkStudentTable existingArtwork = db.ArtworkStudentTable.Find(updatedArtwork.Id);

                if (existingArtwork == null)
                {
                    return HttpNotFound();
                }

                // Check if a new image was uploaded, and update the image path accordingly
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    // Generate a unique file name for the uploaded image
                    string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string extension = Path.GetExtension(ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                    // Combine the file name with the server's path to the image folder
                    string imagePath = Path.Combine(Server.MapPath("~/All_Image/"), fileName);

                    // Save the uploaded image to the specified path
                    ImageFile.SaveAs(imagePath);

                    // Update the model's Image property with the relative image path
                    existingArtwork.Image = "~/All_Image/" + fileName;
                }

                // Update other fields as needed
                existingArtwork.Title = updatedArtwork.Title;
                existingArtwork.Description = updatedArtwork.Description;
                existingArtwork.Quotation = updatedArtwork.Quotation;

                // Now, save the updated model to the database
                db.Entry(existingArtwork).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // If the model state is invalid, return to the Create view with the model
            return View("Edit", updatedArtwork);
        }




        // GET: ArtworkStudentTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtworkStudentTable artworkStudentTable = db.ArtworkStudentTable.Find(id);
            if (artworkStudentTable == null)
            {
                return HttpNotFound();
            }
            return View(artworkStudentTable);
        }

        // POST: ArtworkStudentTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtworkStudentTable artworkStudentTable = db.ArtworkStudentTable.Find(id);
            db.ArtworkStudentTable.Remove(artworkStudentTable);
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
