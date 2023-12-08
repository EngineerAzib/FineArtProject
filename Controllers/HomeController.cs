using FineArtProject.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FineArtProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("administrator"))
            {
                return RedirectToAction("Index", "Admin");       


            }
            else if (User.IsInRole("staff"))
            {
                return RedirectToAction("Index", "Staff");
            }
            else if (User.IsInRole("manager"))
            {
                return RedirectToAction("Index", "Managar");
            }
            else if (User.IsInRole("student"))
            {
                return RedirectToAction("Index", "student");
            }
            else
             
            return RedirectToAction("Login", "Account");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       


    }
}