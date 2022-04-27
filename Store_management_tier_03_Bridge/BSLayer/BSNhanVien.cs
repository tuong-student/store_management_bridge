using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Store_management_tier_03_Bridge.Bridge;
using Store_management_tier_03_Bridge.DBLayer;

namespace Store_management_tier_03_Bridge.BSLayer
{
    public class BSNhanVien : BusinessLayer
    {
        string TABLE_NAME = "NhanVien";

        public DataSet Select()
        {
            string sqlString = "Select * from " + TABLE_NAME + "";
            return DBMain.ExecuteQueryDataSet(sqlString, CommandType.Text);
        }

        public void Insert(InfoHolder info)
        {
            string sqlString = "Insert Into " + TABLE_NAME + " Values('" +
                info.MaNV +"', N'" +
                info.Ho +"', N'" +
                info.Ten +"', '" +
                info.Nu +"', '" +
                info.NgayNV +"', N'" +
                info.DiaChi +"', '" +
                info.DienThoai +"','" +
                info.Hinh +"')";
            DBMain.MyExecuteNonQuery(sqlString, CommandType.Text);
        }

        public void Update(InfoHolder info)
        {
            string sqlString = "Update " + TABLE_NAME + " Set MaNV='" +
                info.MaNV +"', Ho=N'" +
                info.Ho +"', Ten=N'" +
                info.Ten +"', Nu='" +
                info.Nu +"', NgayNV='" +
                info.NgayNV +"',DiaChi=N'" +
                info.DiaChi +"', DienThoai='" +
                info.DienThoai +"' " +
                "where MaNV='"+ info.MaNV +"'";
            DBMain.MyExecuteNonQuery(sqlString, CommandType.Text);
        }

        public void Delete(InfoHolder info)
        {
            string sqlString = "Delete " + TABLE_NAME + " where MaNV='" + info.MaNV + "'";
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
