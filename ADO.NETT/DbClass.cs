using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NETT
{
    class DbClass
    {
        // SqlConnection cnn = new SqlConnection(@"Data Source=your connection;initial catalog=your catalog;user id=sa;password=123");
        SqlConnection cnn = new SqlConnection(@ "Data Source=your connection;initial catalog=your catalog;integrated security=True");
        public DataTable TabloGetir(string sql, params SqlParameter[] prms)
        {
            // komut oluşturup connectiona bağlıyoruz
            SqlCommand cmd = new SqlCommand(sql, cnn);

            // veri taşıma işlemlerini yapan bir adaptor oluşturuyoruz
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            
            DataTable dt = new DataTable();
            da.Fill(dt);   // sql cümlesini çalıştırıp dt tablosuna doldurur.
            return dt;
        }

        public void SqlCalistir(string sql, params SqlParameter[] prms)
        {
            SqlCommand cmd = new SqlCommand(sql, cnn);
            if (prms != null)
            {
                cmd.Parameters.AddRange(prms);
            }
            if (cnn.State == ConnectionState.Closed) // connection kapalıysa açmak için yaptık
            {
                cnn.Open();
            }
            cmd.ExecuteNonQuery(); // ınsert, update, delete ve diğer komutları çalıştırır.
            cnn.Close();
        }
        public void UpdateTable(string sql,DataTable dt, params SqlParameter[] prms)
        {
            // komut oluşturup connectiona bağlıyoruz
            SqlCommand cmd = new SqlCommand(sql, cnn);

            if (prms != null)
            {
                cmd.Parameters.AddRange(prms);
            }
            // veri taşıma işlemlerini yapan bir adaptor oluşturuyoruz
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // INSERT UPDATE ve DELETE commandlerini SELECT commandine bakarak oluşturur.
            SqlCommandBuilder cmb = new SqlCommandBuilder(da);
            da.InsertCommand = cmb.GetInsertCommand();
            da.DeleteCommand = cmb.GetDeleteCommand();
            da.UpdateCommand = cmb.GetUpdateCommand();
            // Datatable daki değişiklikleri vt ye kaydederiz.
            da.Update(dt);
            
        }
    }
}
