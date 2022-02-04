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
    public class ViewFacultyAdminDal
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

        public DataTable ViewFaculty()
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UserView", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Role", "Faculty");
                SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
                sqlda.Fill(dt);
                con.Close();
                return dt;
            }
            catch(SqlException sqlex)
            {
                ExceptionLogging.SendErrorToText(sqlex);
                return dt;
            }



        }

        public string CreateNewFaculty(ViewFacultyAdmin createflt)
        {
            string dt = string.Empty;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Registeruser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", createflt.Name);
                cmd.Parameters.AddWithValue("@PhoneNumber", createflt.PhoneNumber);
                cmd.Parameters.AddWithValue("@Email", createflt.Email);
                cmd.Parameters.AddWithValue("@Password", createflt.Password);
                cmd.Parameters.AddWithValue("@Gender", createflt.Gender);
                cmd.Parameters.AddWithValue("@Role", "Faculty");
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
        public string UpdateFaculty(ViewFacultyAdmin updateflt)
        {
            string dt = string.Empty;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", updateflt.Name);
                cmd.Parameters.AddWithValue("@PhoneNumber", updateflt.PhoneNumber);
                cmd.Parameters.AddWithValue("@Email", updateflt.Email);
                cmd.Parameters.AddWithValue("@Gender", updateflt.Gender);
                cmd.Parameters.AddWithValue("@Id", updateflt.Id);
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
        public string DeleteFaculty(int id)
        {
            ViewFacultyAdmin deleteflt = new ViewFacultyAdmin();
            deleteflt.Id = id;
            string dt = string.Empty;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", deleteflt.Id);
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
        public DataTable FacultyDetails(int id)
        {
            string Error = string.Empty;
            DataTable dt = new DataTable();
            ViewFacultyAdmin fltdetails = new ViewFacultyAdmin();
            fltdetails.Id = id;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UserDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", fltdetails.Id);
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