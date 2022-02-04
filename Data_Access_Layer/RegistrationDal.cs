using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SJCollegeMVC.Models;
using SJCollegeMVC.RegistrationService;
namespace SJCollegeMVC.Data_Access_Layer
{
    public class RegistrationDal
    {
        RegisterUserClient client = new RegisterUserClient();
        public string Register(RegistrationModel registerUser)
        {
            try
            {

                UserData user = new UserData();
                user.Name = registerUser.Name;
                user.Phnnum = registerUser.PhoneNumber;
                user.Email = registerUser.Email;
                user.Password = registerUser.Password;
                user.Gender = registerUser.Gender.ToString();
                user.User = registerUser.Role.ToString();
                user.Approval = registerUser.Approval;

                string r = client.Register(user);
                return r;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}