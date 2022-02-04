using SJCollegeMVC.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SJCollegeMVC.Models;
using System.Data;
namespace SJCollegeMVC.Views.Administrator
{
    public class StudentAdminController : Controller
    {
        ViewStudentAdminDal dblayer = new ViewStudentAdminDal();
        [HttpGet]
        // GET: StudentAdmin
        public ActionResult Index()
        {
            try { 
            DataTable dt = dblayer.ViewStudent();
            return View(dt);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }
        }

        // GET: StudentAdmin/Create
        public ActionResult Create()
        {
            List<SelectListItem> obj = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Male", Value = "Male" },
                new SelectListItem { Text = "Female", Value = "Female" },
            };
            //Assigning generic list to ViewBag
            ViewBag.Gender = obj;

            return View(new ViewStudentAdmin());
        }

        // POST: StudentAdmin/Create
        [HttpPost]
        public ActionResult Create(ViewStudentAdmin createstd)
        {
            List<SelectListItem> obj = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Male", Value = "Male" },
                new SelectListItem { Text = "Female", Value = "Female" },
            };
            //Assigning generic list to ViewBag
            ViewBag.Gender = obj;

            string Error = string.Empty;
            try
            {
                Password_Encryption ps = new Password_Encryption();
                createstd.Password = ps.Encryption("1234");
                Error = dblayer.CreateNewStudent(createstd);
                TempData["msge"] = Error;
            return RedirectToAction("Index","StudentAdmin");
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }

        }

        // GET: StudentAdmin/Edit/5
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
            ViewStudentAdmin editstd = new ViewStudentAdmin();
            try { 
            dt=dblayer.StudentDetails(id);
            if(dt!=null)
            {
                editstd.Name = dt.Rows[0][1].ToString();
                editstd.PhoneNumber = dt.Rows[0][2].ToString();
                editstd.Email = dt.Rows[0][3].ToString();
                editstd.Gender = dt.Rows[0][5].ToString();
                return View(editstd);
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

        // POST: StudentAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(ViewStudentAdmin editstd)
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
                dblayer.UpdateStudent(editstd);

                return RedirectToAction("Index");
            }
             catch(Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }
}

        // GET: StudentAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            DataTable dt = new DataTable();
            ViewStudentAdmin deletestd = new ViewStudentAdmin();
            try { 
            dt = dblayer.StudentDetails(id);
            if (dt != null)
            {
                deletestd.Name = dt.Rows[0][1].ToString();
                deletestd.PhoneNumber = dt.Rows[0][2].ToString();
                deletestd.Email = dt.Rows[0][3].ToString();
                deletestd.Gender = dt.Rows[0][5].ToString();
                return View(deletestd);
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

        // POST: StudentAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(ViewStudentAdmin deletestd)
        {
            try
            {
                dblayer.DeleteStudent(deletestd.Id);

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
