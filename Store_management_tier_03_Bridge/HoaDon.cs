using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

using Store_management_tier_03_Bridge.Bridge;
using Store_management_tier_03_Bridge.BSLayer;

namespace Store_management_tier_03_Bridge
{
    public partial class HoaDon : Form
    {
        BSHoaDon dbHoaDon = null;
        AbstractManage manage = null;
        bool Them = false;
        InfoHolder holder = null;

        public HoaDon()
        {
            InitializeComponent();
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            dbHoaDon =new BSHoaDon();
            manage = new AbstractManage(dbHoaDon);
            holder = new InfoHolder();

            //Load table data into DataGridView
            manage.LoadData(dgv);

            // Xóa trống các đối tượng trong Panel 
            this.txtMaHD.ResetText();
            this.txtMaKhachHang.ResetText();
            this.txtMaNV.ResetText();
            // Không cho thao tác trên các nút Lưu / Hủy 
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.panel.Enabled = false;
            // Cho thao tác trên các nút Thêm / Sửa / Xóa /Thoát 
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            //Default is the first line
            dgv_CellClick(null, null);
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Thứ tự dòng hiện hành 
            int r = dgv.CurrentCell.RowIndex;
            // Chuyển thông tin lên panel 
            this.txtMaHD.Text =
                dgv.Rows[r].Cells[0].Value.ToString();
            this.txtMaNV.Text =
                dgv.Rows[r].Cells[1].Value.ToString();
            this.txtMaKhachHang.Text =
                dgv.Rows[r].Cells[2].Value.ToString();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            manage.LoadData(dgv);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kich hoạt biến Them 
            Them = true;
            // Xóa trống các đối tượng trong Panel 
            this.txtMaHD.ResetText();
            this.txtMaKhachHang.ResetText();
            this.txtMaNV.ResetText();
            // Cho thao tác trên các nút Lưu / Hủy / Panel 
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.panel.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát 
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnHome.Enabled = false;
            // Đưa con trỏ đến TextField txtThanhPho 
            this.txtMaKhachHang.Focus();
            this.txtMaHD.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kích hoạt biến Sửa 
            Them = false;
            // Cho phép thao tác trên Panel 
            this.panel.Enabled = true;
            dgv_CellClick(null, null);
            // Cho thao tác trên các nút Lưu / Hủy / Panel 
            this.btnLuu.Enabled = true;
            this.btnHuyBo.Enabled = true;
            this.panel.Enabled = true;
            // Không cho thao tác trên các nút Thêm / Xóa / Thoát 
            this.btnThem.Enabled = false;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnHome.Enabled = false;
            // Đưa con trỏ đến TextField txtMaKH 
            this.txtMaHD.Enabled = false;
            this.txtMaNV.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    //Insert zone
                    holder.MaHD = txtMaHD.Text;
                    holder.MaNV = txtMaNV.Text;
                    holder.MaKH = txtMaKhachHang.Text;

                    manage.Insert(holder);
                    MessageBox.Show("Insert Success");
                    HoaDon_Load(sender, e);
                }
                catch (SqlException err)
                {
                    MessageBox.Show("Error: " + err.Message);
                }
            }
            else
            {
                try
                {
                    //Update zone
                    string MaHD = txtMaHD.Text;
                    holder.MaHD = MaHD;
                    holder.MaNV = txtMaNV.Text;
                    holder.MaKH = txtMaKhachHang.Text;

                    manage.Update(holder);
                    HoaDon_Load(sender, e);
                }
                catch (SqlException err)
                {
                    MessageBox.Show("Error: " + err.Message);
                }
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            // Xóa trống các đối tượng trong Panel 
            this.txtMaHD.ResetText();
            this.txtMaKhachHang.ResetText();
            this.txtMaNV.ResetText();
            // Cho thao tác trên các nút Thêm / Sửa / Xóa / Thoát 
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            // Không cho thao tác trên các nút Lưu / Hủy / Panel 
            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;
            panel.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            holder.MaHD = txtMaHD.Text;
            manage.Delete(holder);
            HoaDon_Load(sender, e);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            this.Hide();
            KhachHang window = new KhachHang();
            window.ShowDialog();
            this.Close();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            btnThanhPho_Click(sender, e);
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            this.Hide();
            NhanVien window = new NhanVien();
            window.ShowDialog();
            this.Close();
        }

        private void btnThanhPho_Click(object sender, EventArgs e)
        {
            this.Hide();
            ThanhPho thanhpho = new ThanhPho();
            thanhpho.ShowDialog();
            this.Close();
        }

        private void btnChiTietHoaDon_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChiTietHoaDon window = new ChiTietHoaDon();
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
    }
}
