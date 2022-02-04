using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SJCollegeMVC.Data_Access_Layer;
using SJCollegeMVC.Models;
using System.Data;

namespace SJCollegeMVC.Controllers
{
    public class FacultyAdminController : Controller
    {
        ViewFacultyAdminDal dblayer = new ViewFacultyAdminDal();
        // GET: FacultyAdmin
        public ActionResult Index()
        {
            try { 
            DataTable dt = dblayer.ViewFaculty();
            return View(dt);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }
        }


        // GET: FacultyAdmin/Create
        public ActionResult Create()
        {
            List<SelectListItem> obj = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Male", Value = "Male" },
                new SelectListItem { Text = "Female", Value = "Female" },
            };
            //Assigning generic list to ViewBag
            ViewBag.Gender = obj;

            return View(new ViewFacultyAdmin());
        }

        // POST: FacultyAdmin/Create
        [HttpPost]
        public ActionResult Create(ViewFacultyAdmin createflt)
        {
            List<SelectListItem> obj = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Male", Value = "Male" },
                new SelectListItem { Text = "Female", Value = "Female" },
            };
            //Assigning generic list to ViewBag
            ViewBag.Gender = obj;
            try { 
            string Error = string.Empty;

            {
                Password_Encryption ps = new Password_Encryption();
                createflt.Password = ps.Encryption("1234");
                Error = dblayer.CreateNewFaculty(createflt);
                TempData["msge"] = Error;
            }
            return RedirectToAction("Index", "FacultyAdmin");
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }
        }

        // GET: FacultyAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            List<SelectListItem> obj = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Male", Value = "Male" },
                new SelectListItem { Text = "Female", Value = "Female" },
            };
            //Assigning generic list to ViewBag
            ViewBag.Gender = obj;

            DataTable dt = new DataTable();
            ViewFacultyAdmin editflt = new ViewFacultyAdmin();
            try { 
            dt = dblayer.FacultyDetails(id);
            if (dt != null)
            {
                editflt.Name = dt.Rows[0][1].ToString();
                editflt.PhoneNumber = dt.Rows[0][2].ToString();
                editflt.Email = dt.Rows[0][3].ToString();
                editflt.Gender = dt.Rows[0][5].ToString();
                return View(editflt);
            }
            else
            {
                return RedirectToRoute("Index");
            }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }

        }

        // POST: FacultyAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(ViewFacultyAdmin editflt)
        {
            List<SelectListItem> obj = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Male", Value = "Male" },
                new SelectListItem { Text = "Female", Value = "Female" },
            };
            //Assigning generic list to ViewBag
            ViewBag.Gender = obj;

            try
            {
                dblayer.UpdateFaculty(editflt);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }
        }

        // GET: FacultyAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            DataTable dt = new DataTable();
            ViewFacultyAdmin deleteflt = new ViewFacultyAdmin();
            try { 
            dt = dblayer.FacultyDetails(id);
            if (dt != null)
            {
                deleteflt.Name = dt.Rows[0][1].ToString();
                deleteflt.PhoneNumber = dt.Rows[0][2].ToString();
                deleteflt.Email = dt.Rows[0][3].ToString();
                deleteflt.Gender = dt.Rows[0][5].ToString();
                return View(deleteflt);
            }
            else
            {
                return RedirectToRoute("Index");
            }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }
        }

        // POST: FacultyAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(ViewFacultyAdmin deleteflt)
        {
            try
            {
                dblayer.DeleteFaculty(deleteflt.Id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }
        }
    }
}
