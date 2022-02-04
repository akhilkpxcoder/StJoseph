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
    public class UserApprovalDal
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        public DataTable ApprovalView()
        {
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand("UserApproval", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
            sqlda.Fill(dt);
            con.Close();
            return dt;
        }
        public string UpdateApproval(UserApprovalModel uad)
        {
            string dt = string.Empty;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateUserApproval", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Approval", uad.Approval);
                cmd.Parameters.AddWithValue("@ID", uad.Id);
                cmd.ExecuteNonQuery();
                return "Selected Data Updated";
            }
            catch (SqlException sqlex)
            {
                return sqlex.ToString();
            }


            finally
            {
                con.Close();

            }

        }
    }
}