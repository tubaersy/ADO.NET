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
namespace ADO.NETT
{
    public partial class Form1 : Form
    {
        DbClass db = new DbClass();
        // string cnnstr = @"Data Source=localhost\SQLEXPRESS;initial catalog=ISKUR;integrated security=True";
        DataTable dt = new DataTable();

        int satir = -1;
        public Form1()
        {
            InitializeComponent(); // form üzerindeki componentleri oluşturan yer. F12 ye basınca
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

            GridDoldur();
        }

        

        private void DataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            satir = e.RowIndex;
            if (satir>-1)
            {
                txtKATEGORI_REFNO.Text = dataGridView1.Rows[satir].Cells["KATEGORI_REFNO"].Value.ToString();
                txtKATEGORI_ADI.Text = dataGridView1.Rows[satir].Cells["KATEGORI_ADI"].Value.ToString();
            }
            db.UpdateTable("SELECT * FROM KATEGORI", dt);
        }

        private void Button1_Click(object sender, EventArgs e)
        {// yeni
            txtKATEGORI_REFNO.Text = "";
            txtKATEGORI_ADI.Text = "";
        }

        private void Button3_Click(object sender, EventArgs e)
        {//vazgeç
            if (satir > -1)
            {
                txtKATEGORI_REFNO.Text = dataGridView1.Rows[satir].Cells["KATEGORI_REFNO"].Value.ToString();
                txtKATEGORI_ADI.Text = dataGridView1.Rows[satir].Cells["KATEGORI_ADI"].Value.ToString();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {// kaydet

            if (txtKATEGORI_ADI.Text.Trim()=="")
            {
                errorProvider1.SetError(txtKATEGORI_ADI, "Kategori adı giriniz");   // ! işareti çıkarıyor
                return;
            }
            else
            {
                errorProvider1.SetError(txtKATEGORI_ADI, "");
            }

            SqlParameter prm1 = new SqlParameter("@p1", txtKATEGORI_ADI.Text);
            SqlParameter prm2 = new SqlParameter("@p2", txtKATEGORI_REFNO.Text);
            string sql = "";
            if (txtKATEGORI_REFNO.Text == "")
            {
                sql = "INSERT INTO KATEGORI(KATEGORI_ADI) VALUES(@p1)";  // SQL injection engelliyor.

            }
            else
            {
                sql = "UPDATE KATEGORI SET KATEGORI_ADI = @p1 WHERE KATEGORI_REFNO=@p2";
            }

            db.SqlCalistir(sql, prm1, prm2);
            GridDoldur();
        }
        void GridDoldur()
        {

            dt = db.TabloGetir("SELECT * FROM KATEGORI");
            dataGridView1.DataSource = dt;  // veri ızgarasının kaynağı dt yapıldı
        }

        private void Button4_Click(object sender, EventArgs e)
        { // sil
            
            string sql = "";
            if (txtKATEGORI_REFNO.Text != "")
            {
                sql = "DELETE FROM KATEGORI WHERE KATEGORI_REFNO=@p2";
                SqlParameter prm2 = new SqlParameter("@p2", txtKATEGORI_REFNO.Text);
                db.SqlCalistir(sql, prm2);
                GridDoldur();
            }
        }

        private void DataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            db.UpdateTable("SELECT * FROM KATEGORI", dt);
        }
    }
}
