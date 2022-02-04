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
    public class ViewBatchAdminDal
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        public DataTable ViewBatch()
        {
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand("ViewBatch", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
            sqlda.Fill(dt);
            con.Close();
            return dt;
        }

        public string UpdateBatch(ViewBatchAdminModel bad)
        {
            string dt = string.Empty;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateBatch", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Batch",Convert.ToString(bad.UserBatch));
                cmd.Parameters.AddWithValue("@Id", bad.Id);
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