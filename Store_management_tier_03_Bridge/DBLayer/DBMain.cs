using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Store_management_tier_03_Bridge.DBLayer
{
    internal static class DBMain
    {
        private static string ConnStr = "Data Source=DESKTOP-FA7STRC\\SQLSERVER2019;Initial Catalog=QuanLyBanHang;Integrated Security=True";
        private static SqlConnection conn = new SqlConnection(ConnStr);
        private static SqlCommand comm = conn.CreateCommand();
        private static SqlDataAdapter da = null;
        //Get data
        public static DataSet ExecuteQueryDataSet(string strSQL, CommandType ct)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            comm.CommandText = strSQL;
            comm.CommandType = ct;
            da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        //Edit data
        public static bool MyExecuteNonQuery(string strSQL, CommandType ct)
        {
            bool f = false;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            comm.CommandText = strSQL;
            comm.CommandType = ct;
            try
            {
                comm.ExecuteNonQuery();
                f = true;
            }
            catch (SqlException ex)
            {
                
            }
            finally
            {
                conn.Close();
            }
            return f;
        }
    }
}
