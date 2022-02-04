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
    public class ViewBatchUserDal
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        public DataTable ViewBatch(ViewBatchUserModel viewbatch)
        {
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand cmd = new SqlCommand("ViewBS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", viewbatch.Id);
            SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
            sqlda.Fill(dt);
            con.Close();
            return dt;
        }
        public string UpdateBatch(ViewBatchUserModel updatebatch)
        {
            string dt = string.Empty;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateMark", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mark", updatebatch.Mark);
                cmd.Parameters.AddWithValue("@Id", updatebatch.Id);
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