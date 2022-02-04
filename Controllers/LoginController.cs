using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SJCollegeMVC.Data_Access_Layer;
using SJCollegeMVC.Models;

namespace SJCollegeMVC.Controllers
{
    public class LoginController : Controller
    {
        LoginDal dblayer = new LoginDal();
       [HttpGet]
        public ActionResult Index()
        {
            LoginModel loginModel = new LoginModel();
            return View("Index", loginModel);
        }
       [HttpPost]
        public ActionResult Index(LoginModel loginModel)
        {
            Password_Encryption pe = new Password_Encryption();
            string Msge = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    loginModel.Password = pe.Encryption(loginModel.Password);
                    Msge = dblayer.LoginUser(loginModel);
                    if (Msge == "1")
                    {
                        Session["Name"] = loginModel.Name;
                        Session["Batch"] = loginModel.Batch;
                        Session["ID"] = loginModel.Id;
                        if (loginModel.Role == "Student")
                        {
                            if (loginModel.Approval == "Active")
                            {
                                return RedirectToAction("Index", "Student");
                            }
                            else
                            {
                                TempData["msge"] = "Approval Pending";
                                return View();
                            }

                        }
                        else if (loginModel.Role == "Faculty")
                        {
                            if (loginModel.Approval == "Active")
                            {
                                return RedirectToAction("Index", "Faculty");
                            }
                            else
                            {
                                TempData["msge"] = "Approval Pending";
                                return View();
                            }

                        }
                        else if (loginModel.Role == "Administrator")
                        {
                            return RedirectToAction("Index", "Administrator");
                        }
                        else
                        {
                            TempData["msge"] = "Incorrect Email or Password";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["msge"] = "Incorrect Email or Password";
                        return View();
                    }
                }
                else
                {
                    TempData["msge"] = "Please Fill All Feilds";
                    return View("Index");
                }
            }
            catch(Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View();
            }

}
    }
}