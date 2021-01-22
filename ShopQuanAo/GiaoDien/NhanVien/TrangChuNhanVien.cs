using ShopQuanAo.GiaoDien.User;
using ShopQuanAo.Public;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopQuanAo.GiaoDien.NhanVien
{
    public partial class TrangChuNhanVien : Form
    {
        private Color fromArgb1 = Color.FromArgb(10, 92, 150);
        private Color fromArgb2 = Color.FromArgb(5, 48, 103);

        public TrangChuNhanVien()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void TrangChuNhanVien_Load(object sender, EventArgs e)
        {
            lbTen.Text = LuuThongTin.ten;
            HideDrop();
            panelDrop1.Visible = true;
            openChildForm(new QLSanPham());
            Active();
            btnQLSP.BackColor = fromArgb2;
        }
        private void HideDrop()
        {
            panelDrop1.Visible = false;
            panelDrop2.Visible = false;
            panelDrop3.Visible = false;
            panelDrop4.Visible = false;
        }
        private void Active()
        {
            btnQLSP.BackColor = fromArgb1;
            btnLoaiSP.BackColor = fromArgb1;
            btnNhapHang.BackColor = fromArgb1;
        }
        private Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelParent.Controls.Add(childForm);
            panelParent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
          
        }

       

        private void iconButton20_Click(object sender, EventArgs e)
        {
            
        }

        private void iconButton5_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            openChildForm(new QLSanPham());
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DangNhap dn = new DangNhap();
            this.Visible = false;
            dn.ShowDialog();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            HideDrop();
            panelDrop1.Visible = true;
        }

        private void iconButton11_Click(object sender, EventArgs e)
        {
            HideDrop();
            panelDrop2.Visible = true;
        }

        private void iconButton15_Click(object sender, EventArgs e)
        {
            HideDrop();
            panelDrop3.Visible = true;
        }

        private void iconButton5_Click_1(object sender, EventArgs e)
        {
            openChildForm(new QLSanPham());
            Active();
            btnQLSP.BackColor = fromArgb2;
        }

        private void btnLoaiSP_Click(object sender, EventArgs e)
        {
            openChildForm(new LoaiSanPham());
            Active();
            btnLoaiSP.BackColor = fromArgb2;
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            Active();
            btnNhapHang.BackColor = fromArgb2;
        }
    }
}
