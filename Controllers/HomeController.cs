using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SJCollegeMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            ViewBag.Message = "About Us";

            return View();
        }

        public ActionResult Course()
        {
            ViewBag.Message = "Course";

            return View();
        }
        public ActionResult Admission()
        {
            return View();
        }
        public ActionResult Infrastructure()
        {
            return View();
        }
    }
}