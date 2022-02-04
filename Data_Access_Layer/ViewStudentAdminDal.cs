using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using SJCollegeMVC.Models;
namespace SJCollegeMVC.Data_Access_Layer
{
    public class ViewStudentAdminDal
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        

        public DataTable ViewStudent()
        {
            DataTable dt = new DataTable();
            try { 
            con.Open();
            SqlCommand cmd = new SqlCommand("UserView", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Role", "Student");
            SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
            sqlda.Fill(dt);
                return dt;
            }
            catch(SqlException sqlex)
            {
                ExceptionLogging.SendErrorToText(sqlex);
                return dt;
            }
            finally { 
            con.Close();
            
            }


        }

        public string CreateNewStudent(ViewStudentAdmin newstd)
        {
            string dt = string.Empty;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Registeruser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", newstd.Name);
                cmd.Parameters.AddWithValue("@PhoneNumber", newstd.PhoneNumber);
                cmd.Parameters.AddWithValue("@Email", newstd.Email);
                cmd.Parameters.AddWithValue("@Password", newstd.Password);
                cmd.Parameters.AddWithValue("@Gender", newstd.Gender);
                cmd.Parameters.AddWithValue("@Role", "Student");
                cmd.Parameters.AddWithValue("@Approval", "Active");
                cmd.ExecuteNonQuery();
                return "New Data Added";
            }
            catch (SqlException sqlex)
            {
                ExceptionLogging.SendErrorToText(sqlex);
                return "Some Technical Error occurred,Please try after some time";
            }


            finally
            {
                con.Close();

            }

        }
        public string UpdateStudent(ViewStudentAdmin updatestd)
        {
            string dt = string.Empty;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", updatestd.Name);
                cmd.Parameters.AddWithValue("@PhoneNumber", updatestd.PhoneNumber);
                cmd.Parameters.AddWithValue("@Email", updatestd.Email);
                cmd.Parameters.AddWithValue("@Gender", updatestd.Gender);
                cmd.Parameters.AddWithValue("@Id", updatestd.Id);
                cmd.ExecuteNonQuery();
                return "Selected Data Updated";
            }
            catch (SqlException sqlex)
            {
                ExceptionLogging.SendErrorToText(sqlex);
                return "Some Technical Error occurred,Please try after some time";
            }


            finally
            {
                con.Close();

            }

        }
        public string DeleteStudent(int id)
        {
            ViewStudentAdmin deletestd = new ViewStudentAdmin();
            string dt = string.Empty;
            deletestd.Id = id;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", deletestd.Id);
                cmd.ExecuteNonQuery();
                return "Selected Data Deleted";
            }
            catch (SqlException sqlex)
            {
                ExceptionLogging.SendErrorToText(sqlex);
                return "Some Technical Error occurred,Please try after some time";
            }


            finally
            {
                con.Close();

            }

        }
        public DataTable StudentDetails(int id)
        {
            string Error = string.Empty;
            DataTable dt = new DataTable();
            ViewStudentAdmin stddetails = new ViewStudentAdmin();
            stddetails.Id = id;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UserDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", stddetails.Id);
                cmd.ExecuteNonQuery();
                SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
                sqlda.Fill(dt);
               
                return dt;
            }
            catch (SqlException sqlex)
            {
                ExceptionLogging.SendErrorToText(sqlex);
                return dt;
            }


            finally
            {
                con.Close();

            }

        }
    }
}