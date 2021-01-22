using ShopQuanAo.Public;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopQuanAo.GiaoDien.NhanVien
{
    public partial class QLSanPham : Form
    {
        private getData con;
        private int SloaiId, SnccId, SkhoId;
        private int StrangThai;

        public QLSanPham()
        {
            InitializeComponent();
            con = new getData();
        }

        private void QLSanPham_Load(object sender, EventArgs e)
        {
            GetDataDM();
            cbSLoai.SelectedIndex = 0;
            cbLoai.SelectedIndex = 0;
            cbKho.SelectedIndex = 0;
            cbSKho.SelectedIndex = 0;
            cbNCC.SelectedIndex = 0;
            cbSNcc.SelectedIndex = 0;
            cbGioiTinh.SelectedIndex = 0;
            cbSTrangThai.SelectedIndex = 1;
            getData();
        }

        private void getData()
        {


            dataGridView1.DataSource = con.getDataTable("SELECT TOP (1000) sp.id,sp.ten,sp.soluong,sp.size,sp.mau,sp.gioitinh,sp.gianhap,sp.giaban,sp.khuyenmai,sp.active, " +
                "(CONVERT(nvarchar,k.id) + ' -' + k.ten) as 'Kho',(CONVERT(nvarchar,ncc.id) + ' -' + ncc.ten) as 'NCC'," +
                "(CONVERT(nvarchar,loai.id) + ' -' + loai.ten) as 'Loại' FROM SanPham sp LEFT JOIN Kho k ON sp.khoId = k.id " +
                "LEFT JOIN NhaCungCap ncc ON sp.nccId = ncc.id LEFT JOIN LoaiSanPham loai ON sp.loaispId = loai.id where ('"
                + txtSten.Text + "' = '' or sp.ten like N'%" + txtSten.Text + "%') and (" + SkhoId + " = 0 or k.id = " + SkhoId + ") and ("
                + SloaiId + " = 0 or loai.id = " + SloaiId + ") and (" + SnccId + " = 0 or ncc.id = " + SnccId + ") and (" + StrangThai + " = -1 or sp.active = " + StrangThai + ")");
        }

        private void InsertOrUpdateSP(bool insert)
        {
            int id, soluong, gianhap, giaban, giaKM, khoId, nccId, loaiId, luotxem, danhgia;
            string ten, size, mausac, gioitinh, mota, anh;
            bool active;
            DateTime ngaynhap = DateTime.Now;

            id = Int32.Parse(txtMa.Text);
            ten = txtTen.Text;
            soluong = Int32.Parse(txtSoLuong.Text);
            size = txtSize.Text;
            mausac = txtMauSac.Text;
            gioitinh = cbGioiTinh.SelectedItem.ToString();
            gianhap = Int32.Parse(txtGiaNhap.Text);
            giaban = Int32.Parse(txtGiaBan.Text);
            giaKM = Int32.Parse(txtGiaKM.Text);
            mota = txtMoTa.Text;
            khoId = Ham.GetIdFromCombobox(cbKho.SelectedItem.ToString());
            nccId = Ham.GetIdFromCombobox(cbNCC.Text);
            loaiId = Ham.GetIdFromCombobox(cbLoai.Text);
            active = ckHoatDong.Checked;
            anh = Ham.GetStringFromImage(picAnh.Image);
            if (insert)
            {
                con.ExecuteNonQuery("INSERT INTO [dbo].[SanPham]([ten],[soluong],[size],[mau],[gioitinh],[gianhap],[giaban]" +
               ",[khuyenmai],[ngaynhap],[mota],[khoId],[nccId],[loaispId],[active],[luotxem],[danhgia],[anh]) VALUES " +
               "(N'" + ten + "'," + soluong + ",N'" + size + "',N'" + mausac + "',N'" + gioitinh +
               "'," + gianhap + "," + giaban + "," + giaKM + ",'" + ngaynhap +
               "',N'" + mota + "'," + khoId + " ," + nccId + "," + loaiId + ",'" + active +
               "',0,0,'" + anh + "')");
                MessageBox.Show("Thêm sản phẩm thành công!","Thông báo");
            }
            else
            {
                con.ExecuteNonQuery("UPDATE SanPham set ten =N'" + ten + "',soluong= " + soluong + ", size = N'" + size + "'," +
                    "mau = N'" + mausac + "',gioitinh = N'" + gioitinh + "', gianhap = " + gianhap + ",giaban =" + giaban + ",khuyenmai = " +
                    giaKM + ",mota = N'" + mota + "',khoId = " + khoId + "," +
                    "nccId = " + nccId + ",loaispId = " + loaiId + ",active ='" + active + "', anh = '" + anh + "'  where id = " + id);
                MessageBox.Show("Sửa sản phẩm thành công!", "Thông báo");
            }
            getData();
        }
        private void GetDataDM()
        {
            var loai = con.getDataTable("Select id,ten from LoaiSanPham");
            var ncc = con.getDataTable("Select id,ten from NhaCungCap");
            var kho = con.getDataTable("Select id,ten from Kho");
            if (loai.Rows.Count > 0)
            {
                cbSLoai.Items.Add("Loại sản phẩm");
                cbLoai.Items.Add("Loại sản phẩm");

                foreach (DataRow item in loai.Rows)
                {
                    cbSLoai.Items.Add(item[0].ToString() + " -" + item[1].ToString());
                    cbLoai.Items.Add(item[0].ToString() + " -" + item[1].ToString());
                }
            }
            if (ncc.Rows.Count > 0)
            {
                cbSNcc.Items.Add("Nhà cung cấp");
                cbNCC.Items.Add("Nhà cung cấp");

                foreach (DataRow item in ncc.Rows)
                {
                    cbSNcc.Items.Add(item[0].ToString() + " -" + item[1].ToString());
                    cbNCC.Items.Add(item[0].ToString() + " -" + item[1].ToString());
                }
            }
            if (kho.Rows.Count > 0)
            {
                cbSKho.Items.Add("Kho");
                cbKho.Items.Add("Kho");
                foreach (DataRow item in kho.Rows)
                {
                    cbSKho.Items.Add(item[0].ToString() + " -" + item[1].ToString());
                    cbKho.Items.Add(item[0].ToString() + " -" + item[1].ToString());
                }
            }
        }
        private void GetById(int id)
        {
            var data = con.getDataTable("SELECT TOP (1000) sp.id,sp.ten,sp.soluong,sp.size ,sp.mau,sp.gioitinh,sp.gianhap," +
                "sp.giaban,sp.khuyenmai,sp.ngaynhap ,sp.mota,(convert(varchar,sp.khoId) + ' -' + k.ten) as 'kho',(convert(varchar,sp.nccId) + ' -' + ncc.ten) as 'ncc' ," +
                "(convert(varchar,sp.loaispId) + ' -' + loai.ten) as 'loai',sp.active,sp.luotxem,sp.danhgia,sp.anh FROM SanPham sp " +
                "LEFT JOIN Kho k ON sp.khoId = k.id LEFT JOIN NhaCungCap ncc ON sp.nccId = ncc.id LEFT JOIN LoaiSanPham loai ON sp.loaispId = loai.id where sp.id = " + id);
            if (data.Rows.Count > 0)
            {
                txtMa.Text = data.Rows[0][0].ToString();
                txtTen.Text = data.Rows[0][1].ToString();
                txtSoLuong.Text = data.Rows[0][2].ToString();
                txtSize.Text = data.Rows[0][3].ToString();
                txtMauSac.Text = data.Rows[0][4].ToString();
                cbGioiTinh.SelectedItem = data.Rows[0][5].ToString();
                txtGiaNhap.Text = data.Rows[0][6].ToString();
                txtGiaBan.Text = data.Rows[0][7].ToString();
                txtGiaKM.Text = data.Rows[0][8].ToString();
                txtMoTa.Text = data.Rows[0][10].ToString();
                cbKho.SelectedItem = data.Rows[0][11].ToString();
                cbNCC.Text = data.Rows[0][12].ToString();
                cbLoai.Text = data.Rows[0][13].ToString();
                ckHoatDong.Checked = data.Rows[0][14].ToString() == "True" ? true : false;
                picAnh.Image = Ham.GetImageFromString(data.Rows[0][17].ToString());
            }
        }
        private void cbSLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            SloaiId = cbSLoai.SelectedIndex != 0 ? Ham.GetIdFromCombobox(cbSLoai.SelectedItem.ToString()) : 0;
            getData();


        }

        private void cbSNcc_SelectedIndexChanged(object sender, EventArgs e)
        {
            SnccId = cbSNcc.SelectedIndex != 0 ? Ham.GetIdFromCombobox(cbSNcc.SelectedItem.ToString()) : 0;
            getData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int id = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                GetById(id);
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    PictureBox PictureBox1 = new PictureBox();

                    // Create a new Bitmap object from the picture file on disk,
                    // and assign that to the PictureBox.Image property
                    PictureBox1.Image = new Bitmap(dlg.FileName);
                    picAnh.Image = new Bitmap(dlg.FileName);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            InsertOrUpdateSP(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            InsertOrUpdateSP(false);
        }

        private void cbSTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSTrangThai.SelectedIndex == 0)
            {
                StrangThai = -1;
            }
            else
            {
                StrangThai = cbSTrangThai.SelectedIndex == 1 ? 1 : 0;
            }
            getData();
        }

        private void cbSKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            SkhoId = cbSKho.SelectedIndex != 0 ? Ham.GetIdFromCombobox(cbSKho.SelectedItem.ToString()) : 0;
            getData();
        }

        private void txtSten_TextChanged(object sender, EventArgs e)
        {
            getData();
        }
    }
}
