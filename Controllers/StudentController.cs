using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SJCollegeMVC.Models;
using SJCollegeMVC.Data_Access_Layer;
using System.Data;
namespace SJCollegeMVC.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BatchStudent()
        {
            ViewBatchUserDal dblayer = new ViewBatchUserDal();
            ViewBatchUserModel viewbatch = new ViewBatchUserModel();
            try { 
            viewbatch.Id = Session["ID"].ToString();
            DataTable dt = dblayer.ViewBatch(viewbatch);
            return View(dt);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }
        }
        public ActionResult ViewFaculty()
        {
            ViewUserDal dblayer = new ViewUserDal();
            ViewUserModel viewbatch = new ViewUserModel();
            try { 
            viewbatch.Batch = Session["Batch"].ToString();
            viewbatch.Role = "Faculty";
            DataTable dt = dblayer.ViewUser(viewbatch);
            return View(dt);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }
        }
        public ActionResult ViewStudent()
        {
            ViewUserDal dblayer = new ViewUserDal();
            ViewUserModel viewbatch = new ViewUserModel();
            try
            {
                viewbatch.Batch = Session["Batch"].ToString();
                viewbatch.Role = "Student";
                DataTable dt = dblayer.ViewUser(viewbatch);
                return View(dt);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel changepswd)
        {
            Password_Encryption pe = new Password_Encryption();
            ChangePasswordDal dblayer = new ChangePasswordDal();
            try
            {
                if (ModelState.IsValid)
                {
                    changepswd.Id = Session["ID"].ToString();
                    changepswd.Password = pe.Encryption(changepswd.Password);
                    TempData["msge"] = dblayer.ChangePassword(changepswd);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
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