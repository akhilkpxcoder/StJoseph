using SJCollegeMVC.Data_Access_Layer;
using SJCollegeMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SJCollegeMVC.Controllers
{
    public class RegistrationController : Controller
    {
        RegistrationDal dblayer = new RegistrationDal();
        // GET: Registration
        public ActionResult Index()
        {
            List<SelectListItem> obj = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Male", Value = "Male" },
                new SelectListItem { Text = "Female", Value = "Female" },
            };
            //Assigning generic list to ViewBag
            ViewBag.Gender = obj;

            List<SelectListItem> obj2 = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Student", Value = "Student" },
                new SelectListItem { Text = "Faculty", Value = "Faculty" },
            };
            //Assigning generic list to ViewBag
            ViewBag.Role = obj2;


            RegistrationModel registrationModel = new RegistrationModel();
            return View("Index", registrationModel);
        }
        [HttpPost]
        public ActionResult Index(RegistrationModel registrationModel)
        {
            List<SelectListItem> obj = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Male", Value = "Male" },
                new SelectListItem { Text = "Female", Value = "Female" },
            };
            //Assigning generic list to ViewBag
            ViewBag.Gender = obj;

            List<SelectListItem> obj2 = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Student", Value = "Student" },
                new SelectListItem { Text = "Faculty", Value = "Faculty" },
            };
            //Assigning generic list to ViewBag
            ViewBag.Role = obj2;
            Password_Encryption pe = new Password_Encryption();
            try
            {
                if (ModelState.IsValid)
                {
                    registrationModel.Password = pe.Encryption(registrationModel.Password);
                    registrationModel.Approval = "Pending";
                    TempData["Msge"] = dblayer.Register(registrationModel);
                    if (TempData["Msge"] != null)
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        TempData["Msge"] = "Error Occured";
                        return View("Index");
                    }
                }
                else
                {
                    TempData["Msge"] = "Fill all the Fields";
                    return View("Index");
                }

            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }
        }

    }
}