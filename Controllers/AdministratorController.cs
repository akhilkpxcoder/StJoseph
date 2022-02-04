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
    public class AdministratorController : Controller
    {
      
        // GET: Administrator
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BatchAdmin()
        {
            ViewBatchAdminDal dblayer = new ViewBatchAdminDal();
            try
            {
                DataTable dt = dblayer.ViewBatch();
                return View(dt);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }
        }
         public ActionResult EditBatch(int id)
        {
            ViewBatchUserDal dblayer = new ViewBatchUserDal();
            DataTable dt = new DataTable();
            ViewBatchUserModel editbth = new ViewBatchUserModel();
            ViewBatchAdminModel databth = new ViewBatchAdminModel();
            editbth.Id = id.ToString();
            List<SelectListItem> ObjList = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Computer Science", Value = "Computer Science" },
                new SelectListItem { Text = "Mechanical", Value = "Mechanical" },
                new SelectListItem { Text = "Civil", Value = "Civil" },
                new SelectListItem { Text = "Electrical", Value = "Electrical" },

            };
            //Assigning generic list to ViewBag
            ViewBag.Locations = ObjList;
            try
            {
                dt = dblayer.ViewBatch(editbth);
                if (dt != null)
                {

                    databth.Id = dt.Rows[0][0].ToString();
                    databth.Name = dt.Rows[0][1].ToString();
                    databth.Role = dt.Rows[0][6].ToString();

                    return View(databth);
                }
                else
                {
                    return RedirectToRoute("Index");
                }
            }catch(Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }
        }
        [HttpPost]
        public ActionResult EditBatch(ViewBatchAdminModel editbth)
        {
            ViewBatchAdminDal dblayer = new ViewBatchAdminDal();
            try
            {
                dblayer.UpdateBatch(editbth);

                return RedirectToAction("BatchAdmin");
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }
        }
        public ActionResult FeesAdmin()
        {
            List<SelectListItem> ObjList = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Computer Science", Value = "Computer Science" },
                new SelectListItem { Text = "Mechanical", Value = "Mechanical" },
                new SelectListItem { Text = "Civil", Value = "Civil" },
                new SelectListItem { Text = "Electrical", Value = "Electrical" },

            };
            //Assigning generic list to ViewBag
            ViewBag.Locations = ObjList;

            return View();
        }
        [HttpPost]
        public ActionResult FeesAdmin(FeesBatchAdminModel updatefees)
        {
            FeesBatchAdminDal dblayer = new FeesBatchAdminDal();
            try { 
            TempData["msge"]= dblayer.UpdateFees(updatefees);
            return RedirectToAction("ViewFees");
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }
        }

        public ActionResult ViewFees()
        {
            FeesBatchAdminDal dblayer = new FeesBatchAdminDal();
            try { 
            DataTable dt = dblayer.ViewFees();
            return View(dt);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }
        }
        public ActionResult FacultyAdmin()
        {
            return this.RedirectToAction("Index", "FacultyAdmin");
        }

         public ActionResult StudentAdmin()
        {  
            return this.RedirectToAction("Index","StudentAdmin");
        }

        public ActionResult UserApproval()
        {
            UserApprovalDal dblayer = new UserApprovalDal();
            UserApprovalModel userApproval = new UserApprovalModel();
            try
            {
            DataTable dt = new DataTable();
            dt = dblayer.ApprovalView();
            return View(dt);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }
        }
        public ActionResult EditApproval(int id)
        {
            ViewBatchUserDal dblayer = new ViewBatchUserDal();
            DataTable dt = new DataTable();
            ViewBatchUserModel editbth = new ViewBatchUserModel();
            editbth.Id = id.ToString();
            UserApprovalModel userApproval = new UserApprovalModel();
            List<SelectListItem> ObjList = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Active", Value = "Active" },
                new SelectListItem { Text = "Pending", Value = "Pending" },

            };
            //Assigning generic list to ViewBag
            ViewBag.Locations = ObjList;
            try { 
            dt = dblayer.ViewBatch(editbth);
            if (dt != null)
            {

                userApproval.Id = dt.Rows[0][0].ToString();
                userApproval.Name = dt.Rows[0][1].ToString();
                userApproval.Role = dt.Rows[0][6].ToString();

                return View(userApproval);
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
        public ActionResult EditApproval(UserApprovalModel userApproval)
        {
            UserApprovalDal dblayer = new UserApprovalDal();
            try { 
            TempData["msge"] = dblayer.UpdateApproval(userApproval);
            return RedirectToAction("UserApproval");
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
                    if (TempData["msge"] != null)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["msge"] = "Please try after some time";
                        return RedirectToAction("ChangePassword");
                    }
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