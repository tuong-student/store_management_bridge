using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Windows.Forms;

using Store_management_tier_03_Bridge.BSLayer;
using Store_management_tier_03_Bridge.Bridge;

namespace Store_management_tier_03_Bridge
{
    public partial class ThanhPho : Form
    {
        BSThanhPho dbThanhPho = null;
        AbstractManage manage = null;
        bool Them = false;
        InfoHolder holder = null;

        public ThanhPho()
        {
            InitializeComponent();
        }

        private void ThanhPho_Load(object sender, EventArgs e)
        {
            holder = new InfoHolder();
            dbThanhPho = new BSThanhPho();
            manage = new AbstractManage(dbThanhPho);

            //Load table data into DataGridView
            manage.LoadData(dgvTHANHPHO);

            // Xóa trống các đối tượng trong Panel 
            this.txtThanhPho.ResetText();
            this.txtTenThanhPho.ResetText();
            // Không cho thao tác trên các nút Lưu / Hủy 
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.panel.Enabled = false;
            // Cho thao tác trên các nút Thêm / Sửa / Xóa /Thoát 
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            this.btnTroVe.Enabled = true;
            //Default is the first line
            dgvTHANHPHO_CellClick(null, null);
        }



        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    //Insert zone
                    
                    holder.ThanhPho = txtThanhPho.Text;
                    holder.TenThanhPho = txtTenThanhPho.Text;

                    manage.Insert(holder);
                    MessageBox.Show("Insert Success");
                    ThanhPho_Load(sender, e);
                }
                catch(SqlException err)
                {
                    MessageBox.Show("Error: " + err.Message);
                }
            }
            else
            {
                try
                {
                    //Update zone
                    string strThanhPho = txtThanhPho.Text;
                    InfoHolder holder = new InfoHolder();
                    holder.ThanhPho = strThanhPho;
                    holder.TenThanhPho = txtTenThanhPho.Text;

                    manage.Update(holder);
                    ThanhPho_Load(sender, e);
                }
                catch(SqlException err)
                {
                    MessageBox.Show("Error: " + err.Message);
                }
            }
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            this.Hide();
            KhachHang khachHang = new KhachHang();
            khachHang.ShowDialog();
            this.Close();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            this.Hide();
            NhanVien nhanVien = new NhanVien();
            nhanVien.ShowDialog();
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kich hoạt biến Them 
            Them = true;
            // Xóa trống các đối tượng trong Panel 
            this.txtThanhPho.ResetText();
            this.txtTenThanhPho.ResetText();
            // Cho thao tác trên các nút Lưu / Hủy / Panel 
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.panel.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát 
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnTroVe.Enabled = false;
            // Đưa con trỏ đến TextField txtThanhPho 
            this.txtThanhPho.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kích hoạt biến Sửa 
            Them = false;
            // Cho phép thao tác trên Panel 
            this.panel.Enabled = true;
            dgvTHANHPHO_CellClick(null, null);
            // Cho thao tác trên các nút Lưu / Hủy / Panel 
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.panel.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát 
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnTroVe.Enabled = false;
            // Đưa con trỏ đến TextField txtMaKH 
            this.txtThanhPho.Enabled = false;
            this.txtTenThanhPho.Focus();
        }

        private void dgvTHANHPHO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Thứ tự dòng hiện hành 
            int r = dgvTHANHPHO.CurrentCell.RowIndex;
            // Chuyển thông tin lên panel 
            this.txtThanhPho.Text =
            dgvTHANHPHO.Rows[r].Cells[0].Value.ToString();
            this.txtTenThanhPho.Text =
            dgvTHANHPHO.Rows[r].Cells[1].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            holder.ThanhPho = txtThanhPho.Text;
            manage.Delete(holder);
            ThanhPho_Load(sender, e);
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            this.Hide();
            HoaDon window = new HoaDon();
            window.ShowDialog();
            this.Close();
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            this.Hide();
            SanPham window = new SanPham();
            window.ShowDialog();
            this.Close();
        }

        private void btnChiTietHoaDon_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChiTietHoaDon window = new ChiTietHoaDon();
            window.ShowDialog();
            this.Close();
        }
    }
}
