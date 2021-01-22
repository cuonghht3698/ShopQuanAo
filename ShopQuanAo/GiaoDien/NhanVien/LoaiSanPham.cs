﻿using ShopQuanAo.Public;
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
    public partial class LoaiSanPham : Form
    {
        private getData con;
         public LoaiSanPham()
        {
            InitializeComponent();
            con = new getData();
        }
        private void LoaiSanPham_Load(object sender, EventArgs e)
        {
            addButtonDataGripview();
            GetAll();
            
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void GetAll()
        {
            
            dataGridView1.DataSource = con.getDataTable("SELECT ROW_NUMBER() OVER(ORDER BY ID) as 'STT' ,id as 'Mã', ten as 'Tên',mota as 'Mô tả' FROM LoaiSanPham");
            dataGridView1.Columns[0].Width = 44;
            dataGridView1.Columns[1].Width = 44;
            dataGridView1.Columns[2].Width = 44;



        }
        private void Delete(int id)
        {
            var confirmResult = MessageBox.Show("Bạn có muốn xóa ??",
                                    "Cảnh báo!!",
                                    MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                con.ExecuteNonQuery("delete loaisanpham where id = " + id);
                GetAll();
            }
           
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                int id = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                Delete(id);
            }
            else if (e.RowIndex != -1)
            {
                txtMa.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtTen.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtMoTa.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            
            
        }


        
        private void addButtonDataGripview()
        {
            
            
            DataGridViewButtonColumn delete = new DataGridViewButtonColumn();
            delete.HeaderText = "Xóa";
            delete.Name = "btnDelete";
            delete.Text = "Xóa";
            delete.Width = 50;
            delete.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(delete);
           
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                con.ExecuteNonQuery("insert into LoaiSanPham (ten,mota) values (N'" + txtTen.Text + "',N'" + txtMoTa.Text + "')");
                GetAll();
                lbThongBao.Text = "Thêm loại sản phẩm thành công!";
                lbThongBao.ForeColor = Color.Green;
            }
            catch (Exception)
            {
                lbThongBao.Text = "Thêm loại sản phẩm không thành công!";
                lbThongBao.ForeColor = Color.Red;

            }


        }
    }
}
