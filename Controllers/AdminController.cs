using FineArtProject.Context;
using FineArtProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Net.Configuration;
using System.Web.Security;
using System.Data.Entity;

namespace FineArtProject.Controllers
{
    public class AdminController : Controller
    {
        FineArtProjectnewEntities db = new FineArtProjectnewEntities();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationDbContext context)
        {
            UserManager = userManager;
            SignInManager = signInManager;
          
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
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

        public ActionResult Register()
        {
            
            return View();
        }

        // ... other actions ...

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create ApplicationUser and set properties
                var user = new ApplicationUser
                {
                    FirstName = model.RegisterModel.FirstName,
                    LastName = model.RegisterModel.LastName,
                    UserName = model.RegisterModel.FirstName,
                    Email = model.RegisterModel.Email,

                };

                // Create the user in the ApplicationUser table
                var result = await UserManager.CreateAsync(user, model.RegisterModel.Password);

                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "student");
                    // Retrieve the user ID of the newly created user
                    string userId = user.Id;

                    // Create Student and set properties
                    var student = new student
                    {
                        userid = userId,

                        Address = model.StudentModel.Address,
                        Phone = model.StudentModel.Phone,
                        AdmissionDate = DateTime.Now
                    };

                    // Save the Student record to the Student table
                    db.student.Add(student);
                    db.SaveChanges(); // Save changes to the database

                    // Redirect to a success page or login page
                    return RedirectToAction("RegView","Admin");
                }
                else
                {
                    // Handle errors if user creation failed
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }

            // If the model is not valid or user creation failed, return to the registration form with validation errors
            return View(model);
        }
        public ActionResult RegView()
        {
            var res = db.student.ToList();
            return View(res);
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Retrieve the user and student data based on the provided user ID
            var user = UserManager.FindById(id);
            var student = db.student.FirstOrDefault(s => s.userid == id);

            if (user == null || student == null)
            {
                return HttpNotFound();
            }

            // Create a view model to combine user and student data
            var model = new RegistrationViewModel
            {
                RegisterModel = new RegisterViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    
                    Email = user.Email,
              
                    // Add other properties as needed
                },
               
            StudentModel = new student
                {
                    userid=student.userid,
                    Address = student.Address,
                    Phone = student.Phone,
                    AdmissionDate = student.AdmissionDate,
                    // Add other student properties as needed
                }
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Find the user by ID
                    var user = await UserManager.FindByIdAsync(model.StudentModel.userid);

                    if (user != null)
                    {
                        // Update user properties from the form data
                        user.FirstName = model.RegisterModel.FirstName;
                        user.LastName = model.RegisterModel.LastName;
                        user.Email = model.RegisterModel.Email;
                     
                        // Update the user in the ApplicationUser table
                        var userUpdateResult = await UserManager.UpdateAsync(user);

                        if (userUpdateResult.Succeeded)
                        {
                            // Find the student record based on the user ID
                            var student = db.student.FirstOrDefault(s => s.userid == user.Id);

                            if (student != null)
                            {
                                // Update student properties from the form data
                                student.Address = model.StudentModel.Address;
                                student.Phone = model.StudentModel.Phone;
                                student.AdmissionDate = model.StudentModel.AdmissionDate;

                                // Save changes to the database
                                db.SaveChanges();

                                // Redirect to a success page or another appropriate action
                                return RedirectToAction("RegView", "Admin");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Student not found.");
                            }
                        }
                        else
                        {
                            foreach (var error in userUpdateResult.Errors)
                            {
                                ModelState.AddModelError("", error);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "User not found.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during the update process
                    ModelState.AddModelError("", "An error occurred while updating the user and student data: " + ex.Message);
                }
            }

            // If the model is not valid or the update failed, return to the edit form with validation errors
            return View(model);
        }




        public ActionResult Delete(string id)
        {
            // Retrieve the user information based on the provided userId
            var user = UserManager.FindById(id);
            if (user == null)
            {
                // Handle the case where the user is not found
                return HttpNotFound();
            }

            // Check if there is an associated student record
            var student = db.student.FirstOrDefault(s => s.userid == id);
            if (student != null)
            {
                // Remove or update the student record as needed
                db.student.Remove(student); // This will remove the student record
                db.SaveChanges();
            }

            // Delete the user
            UserManager.Delete(user);

            // Redirect to a success page or user listing page
            return RedirectToAction("RegView", "Admin");
        }
        public ActionResult DeleteTea(string id)
        {
            // Retrieve the user information based on the provided userId
            var user = UserManager.FindById(id);
            if (user == null)
            {
                // Handle the case where the user is not found
                return HttpNotFound();
            }

            // Check if there is an associated student record
            var teacher = db.teacher.FirstOrDefault(s => s.userid == id);
            if (teacher != null)
            {
                // Remove or update the student record as needed
                db.teacher.Remove(teacher); // This will remove the student record
                db.SaveChanges();
            }

            // Delete the user
            UserManager.Delete(user);

            // Redirect to a success page or user listing page
            return RedirectToAction("TeaView", "Admin");
        }
        public ActionResult TeacherRegister()
        {
            
            return View();
        }
        public ActionResult TeaView()
        {
            var res = db.teacher.ToList();
            return View(res);
        }

        // ... other actions ...

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TeacherRegister(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create ApplicationUser and set properties
                var user = new ApplicationUser
                {
                    FirstName = model.RegisterModel.FirstName,
                    LastName = model.RegisterModel.LastName,
                    UserName = model.RegisterModel.FirstName,
                    Email = model.RegisterModel.Email,

                };

                // Create the user in the ApplicationUser table
                var result = await UserManager.CreateAsync(user, model.RegisterModel.Password);

                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "staff");
                    // Retrieve the user ID of the newly created user
                    string userId = user.Id;

                    // Create Student and set properties
                    var teacher = new teacher
                    {
                        userid = userId,
                      
                        DOJ=DateTime.Now,
                        Class=model.teacherModel.Class,
                        Phone = model.teacherModel.Phone,
                        Address = model.teacherModel.Address,

                    };

                    // Save the Student record to the Student table
                    db.teacher.Add(teacher);
                    db.SaveChanges(); // Save changes to the database

                    // Redirect to a success page or login page
                    return RedirectToAction("TeaView", "Admin");
                }
                else
                {
                    // Handle errors if user creation failed
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }

            // If the model is not valid or user creation failed, return to the registration form with validation errors
            return View(model);
        }
    }
}
