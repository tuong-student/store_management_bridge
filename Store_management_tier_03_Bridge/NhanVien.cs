using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using Store_management_tier_03_Bridge.Bridge;
using Store_management_tier_03_Bridge.BSLayer;

namespace Store_management_tier_03_Bridge
{
    public partial class NhanVien : Form
    {
        BSNhanVien nhanVien = null;
        AbstractManage manage = null;
        bool Them = false;
        InfoHolder holder = null;

        public NhanVien()
        {
            InitializeComponent();
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            holder = new InfoHolder();
            nhanVien = new BSNhanVien();
            manage = new AbstractManage(nhanVien);
            manage.LoadData(dgvNHANVIEN);

            // Xóa trống các đối tượng trong Panel 
            this.txtMaNV.ResetText();
            this.txtHo.ResetText();
            this.txtTen.ResetText();
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
            dgvNHANVIEN_CellClick(null, null);
        }

        private void btnThanhPho_Click(object sender, EventArgs e)
        {
            this.Hide();
            ThanhPho thanhpho = new ThanhPho();
            thanhpho.ShowDialog();
            this.Close();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            this.Hide();
            KhachHang khachHang = new KhachHang();
            khachHang.ShowDialog();
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kich hoạt biến Them 
            Them = true;
            // Xóa trống các đối tượng trong Panel 
            this.txtMaNV.ResetText();
            this.txtHo.ResetText();
            this.txtDiaChi.ResetText();
            this.txtTen.ResetText();
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
            this.txtHo.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kích hoạt biến Sửa 
            Them = false;
            // Cho phép thao tác trên Panel 
            this.panel.Enabled = true;
            dgvNHANVIEN_CellClick(null, null);
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
            this.txtMaNV.Enabled = false;
            this.txtHo.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (Them)
            {
                try
                {
                    //Insert zone
                    holder.MaNV = txtMaNV.Text;
                    holder.Ho = txtHo.Text;
                    holder.Ten = txtTen.Text;
                    //holder.NgayNV = dtpNgayNV.Text;
                    holder.DiaChi = txtDiaChi.Text;
                    holder.DienThoai = txtDienThoai.Text;

                    manage.Insert(holder);
                    MessageBox.Show("Insert Success");
                    NhanVien_Load(sender, e);
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
                    string MaNV = txtMaNV.Text;
                    holder.MaNV = MaNV;
                    holder.Ho = txtHo.Text;
                    holder.Ten = txtTen.Text;
                    //holder.NgayNV = dtpNgayNV.Text;
                    holder.DiaChi = txtDiaChi.Text;
                    holder.DienThoai = txtDienThoai.Text;

                    manage.Update(holder);
                    NhanVien_Load(sender, e);
                }
                catch
                {

                }
            }
        }

        private void dgvNHANVIEN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Thứ tự dòng hiện hành 
            int r = dgvNHANVIEN.CurrentCell.RowIndex;
            // Chuyển thông tin lên panel 
            this.txtMaNV.Text =
                dgvNHANVIEN.Rows[r].Cells[0].Value.ToString();
            this.txtHo.Text =
                dgvNHANVIEN.Rows[r].Cells[1].Value.ToString();
            this.txtTen.Text =
                dgvNHANVIEN.Rows[r].Cells[2].Value.ToString();
            if (dgvNHANVIEN.Rows[r].Cells[3].Value.ToString().Equals("True"))
            {
                rdtNu.Checked = true;
            }
            else
            {
                rdtNam.Checked = true;
            }
            this.dtpNgayNV.Text =
                dgvNHANVIEN.Rows[r].Cells[4].Value.ToString();
            this.txtDiaChi.Text =
                dgvNHANVIEN.Rows[r].Cells[5].Value.ToString();
            this.txtDienThoai.Text =
                dgvNHANVIEN.Rows[r].Cells[7].Value.ToString();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            btnThanhPho_Click(sender, e);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            holder.MaNV = txtMaNV.Text;
            manage.Delete(holder);
            NhanVien_Load(sender, e);
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
