using SJCollegeMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SJCollegeMVC.Data_Access_Layer
{
    public class ChangePasswordDal
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

        public string ChangePassword(ChangePasswordModel changepswd)
        {

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ChangePassword", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Password", changepswd.Password);
                cmd.Parameters.AddWithValue("@Id", changepswd.Id);
                cmd.ExecuteNonQuery();
                return "Password Successfully Changed";
            }
            catch(Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return "Some Technical Error occurred,Please try after some time";
            }
            finally
            {
                con.Close();
            }


        }
    }
}