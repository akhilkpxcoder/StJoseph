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
    public class FeesBatchAdminDal
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        public DataTable ViewFees()
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ViewFees", con);
                cmd.CommandType = CommandType.StoredProcedure;
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
        public string UpdateFees(FeesBatchAdminModel updatefees)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateFees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Batch", updatefees.UserBatch);
                cmd.Parameters.AddWithValue("@Fees", updatefees.Fees);
                SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
                sqlda.Fill(dt);
                return "Fees Updated";
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
    }
}