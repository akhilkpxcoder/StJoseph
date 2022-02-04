using SJCollegeMVC.Data_Access_Layer;
using SJCollegeMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SJCollegeMVC.Controllers
{
    public class FacultyController : Controller
    {
        // GET: Faculty
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BatchFaculty()
        {
            ViewUserDal dblayer = new ViewUserDal();
            ViewUserModel viewstd = new ViewUserModel();
            try { 
            viewstd.Batch = Session["Batch"].ToString();
            viewstd.Role = "Student";
            DataTable dt = dblayer.ViewUser(viewstd);
            return View(dt);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }
        }
        public ActionResult EditBatchFaculty(int id)
        {
            ViewBatchUserDal dblayer = new ViewBatchUserDal();
            DataTable dt = new DataTable();
            ViewBatchUserModel editflt = new ViewBatchUserModel();
            editflt.Id = id.ToString();
            try { 
            dt = dblayer.ViewBatch(editflt);
            if (dt != null)
            {
                editflt.Name = dt.Rows[0][1].ToString();
                editflt.Mark = dt.Rows[0][8].ToString();
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
        [HttpPost]
        public ActionResult EditBatchFaculty(ViewBatchUserModel editflt)
        {
            ViewBatchUserDal dblayer = new ViewBatchUserDal();
            try
            {
                dblayer.UpdateBatch(editflt);

                return RedirectToAction("BatchFaculty","Faculty");
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
            ViewUserModel viewflt = new ViewUserModel();
            try { 
            viewflt.Batch = Session["Batch"].ToString();
            viewflt.Role = "Faculty";
            DataTable dt = dblayer.ViewUser(viewflt);
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
            ViewUserModel viewstd = new ViewUserModel();
            try { 
            viewstd.Batch = Session["Batch"].ToString();
            viewstd.Role = "Student";
            DataTable dt = dblayer.ViewUser(viewstd);
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