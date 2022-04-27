using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

using Store_management_tier_03_Bridge.Bridge;
using Store_management_tier_03_Bridge.DBLayer;

namespace Store_management_tier_03_Bridge.BSLayer
{
    public class BSThanhPho : BusinessLayer
    {
        string TABLE_NAME = "ThanhPho";

        public DataSet Select()
        {
            string sqlString = "Select * from " + TABLE_NAME;
            return DBMain.ExecuteQueryDataSet(sqlString, CommandType.Text);
        }

        public void Insert(InfoHolder info)
        {
            string sqlString = "Insert Into "+ TABLE_NAME + " Values(" + "'" +
            info.ThanhPho + "',N'" +
            info.TenThanhPho + "')";
            DBMain.MyExecuteNonQuery(sqlString, CommandType.Text);
        }

        public void Update(InfoHolder info)
        {
            string sqlString = "Update "+ TABLE_NAME + " Set TenThanhPho=N'" +
            info.TenThanhPho + "' Where ThanhPho='" + info.ThanhPho + "'";
            DBMain.MyExecuteNonQuery(sqlString, CommandType.Text);
        }

        public void Delete(InfoHolder info)
        {
            string sqlString = "Delete "+ TABLE_NAME + " where ThanhPho='" + info.ThanhPho + "'";
            DBMain.MyExecuteNonQuery(sqlString, CommandType.Text);
        }

        public void LoadData(DataGridView dgv)
        {
            DataTable data = new DataTable();
            data.Clear();
            DataSet ds = this.Select();
            data = ds.Tables[0];
            //Show data on dgv
            dgv.DataSource = data;
            //Modify column width
            dgv.AutoResizeColumns();
        }
    }
}
