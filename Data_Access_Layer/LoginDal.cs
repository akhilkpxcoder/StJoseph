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
    public class LoginDal
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

        public string LoginUser(LoginModel loginModel)
        {
            string dt = string.Empty;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("LoginUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", loginModel.Email);
                cmd.Parameters.AddWithValue("@password", loginModel.Password);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    loginModel.Batch = dr["Batch"].ToString();
                    loginModel.Approval = dr["Approval"].ToString();
                    loginModel.Name = dr["Name"].ToString();
                    loginModel.Role = dr["Role"].ToString();
                    loginModel.Id = dr["ID"].ToString();
                    dt = "1";
                    return dt;
                }
                else
                {

                    loginModel.Role = null;
                    dt = "0";
                    return dt;
                }
            }
            catch (Exception ex)
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