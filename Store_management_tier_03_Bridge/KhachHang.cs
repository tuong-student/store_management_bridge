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

using Store_management_tier_03_Bridge.BSLayer;
using Store_management_tier_03_Bridge.Bridge;

namespace Store_management_tier_03_Bridge
{
    public partial class KhachHang : Form
    {
        BSKhachHang khachHang = null;
        AbstractManage manage = null;
        InfoHolder holder = null;
        bool Them = false;

        public KhachHang()
        {
            InitializeComponent();
        }

        private void KhachHang_Load(object sender, EventArgs e)
        {
            khachHang = new BSKhachHang();
            manage = new AbstractManage(khachHang);
            manage.LoadData(dgvKHACHHANG);
            holder = new InfoHolder();

            // Xóa trống các đối tượng trong Panel 
            this.txtMaKhachHang.ResetText();
            this.txtTenCongTy.ResetText();
            this.txtThanhPho.ResetText();
            this.txtDienThoai.ResetText();
            this.txtDiaChi.ResetText();
            // Không cho thao tác trên các nút Lưu / Hủy 
            this.btnLuu.Enabled = false;
            this.btnHuyBo.Enabled = false;
            this.panel.Enabled = false;
            // Cho thao tác trên các nút Thêm / Sửa / Xóa /Thoát 
            this.btnThem.Enabled = true;
            this.btnSua.Enabled = true;
            this.btnXoa.Enabled = true;
            this.btnHome.Enabled = true;
            //Default is the first line
            dgvKHACHHANG_CellClick(null, null);
        }



        private void btnThanhPho_Click(object sender, EventArgs e)
        {
            this.Hide();
            ThanhPho thanhpho = new ThanhPho();
            thanhpho.ShowDialog();
            this.Close();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            this.Hide();
            NhanVien nhanVien = new NhanVien();
            nhanVien.ShowDialog();
            this.Close();
        }

        private void dgvKHACHHANG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Thứ tự dòng hiện hành 
            int r = dgvKHACHHANG.CurrentCell.RowIndex;
            // Chuyển thông tin lên panel 
            this.txtMaKhachHang.Text =
                dgvKHACHHANG.Rows[r].Cells[0].Value.ToString();
            this.txtTenCongTy.Text =
                dgvKHACHHANG.Rows[r].Cells[1].Value.ToString();
            this.txtDiaChi.Text =
                dgvKHACHHANG.Rows[r].Cells[2].Value.ToString();
            this.txtThanhPho.Text =
                dgvKHACHHANG.Rows[r].Cells[3].Value.ToString();
            this.txtDienThoai.Text =
                dgvKHACHHANG.Rows[r].Cells[4].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kích hoạt biến Sửa 
            Them = false;
            // Cho phép thao tác trên Panel 
            this.panel.Enabled = true;
            dgvKHACHHANG_CellClick(null, null);
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
            this.txtMaKhachHang.Enabled = false;
            this.txtTenCongTy.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kich hoạt biến Them 
            Them = true;
            // Xóa trống các đối tượng trong Panel 
            this.txtMaKhachHang.ResetText();
            this.txtTenCongTy.ResetText();
            this.txtDiaChi.ResetText();
            this.txtThanhPho.ResetText();
            this.txtDienThoai.ResetText();
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
            this.txtThanhPho.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    //Insert zone
                    
                    holder.ThanhPho = txtThanhPho.Text;
                    holder.TenCongTy = txtTenCongTy.Text;
                    holder.MaKH = txtMaKhachHang.Text;
                    holder.DiaChi = txtDiaChi.Text;
                    holder.DienThoai = txtDienThoai.Text;

                    manage.Insert(holder);
                    MessageBox.Show("Insert Success");
                    KhachHang_Load(sender, e);
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
                    string MaKH = txtMaKhachHang.Text;
                    holder.MaKH = MaKH;
                    holder.ThanhPho = txtThanhPho.Text;
                    holder.TenCongTy = txtTenCongTy.Text;
                    holder.DiaChi = txtDiaChi.Text;
                    holder.DienThoai = txtDienThoai.Text;

                    manage.Update(holder);
                    KhachHang_Load(sender, e);
                }
                catch(SqlException err)
                {
                    MessageBox.Show("Error: " + err.Message);
                }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            manage.LoadData(dgvKHACHHANG);
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            // Xóa trống các đối tượng trong Panel 
            txtMaKhachHang.ResetText();
            txtTenCongTy.ResetText();
            txtDiaChi.ResetText();
            txtThanhPho.ResetText();
            txtDienThoai.ResetText();
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
            holder.MaKH = txtMaKhachHang.Text;
            manage.Delete(holder);
            KhachHang_Load(sender, e);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            btnThanhPho_Click(sender, e);
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            this.Hide();
            SanPham window = new SanPham();
            window.ShowDialog();
            this.Close();
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            this.Hide();
            HoaDon window = new HoaDon();
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
