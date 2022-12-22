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
using System.Runtime.CompilerServices;

namespace Proje_Hastane
{
    public partial class frmDoktorPaneli : Form
    {
        public frmDoktorPaneli()
        {
            InitializeComponent();
        }

        private DatabaseConnectionSentence connection = new DatabaseConnectionSentence();
        private void frmDoktorPaneli_Load(object sender, EventArgs e)
        {
            //Datagride doktorlaraa ait bilgileri çekiyoruz.
            GetAllDoctors();

            //Branşlar combobox içini dolduruyoruz.
            SqlCommand command = new SqlCommand("select BransAd from tbl_Branslar", connection.Connection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                cmbBrans.Items.Add(reader[0]);
            }
            reader.Close();
            connection.Connection().Close();
        }

        private void GetAllDoctors()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from tbl_Doktorlar", connection.Connection());
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand command =
                new SqlCommand(
                    "insert into tbl_Doktorlar (DoktorAd, DoktorSoyad, DoktorBrans, DoktorTC, DoktorSifre) values (@DoktorAd, @DoktorSoyad, @DoktorBrans, @DoktorTC, @DoktorSifre)",
                    connection.Connection());
            command.Parameters.AddWithValue("@DoktorAd", txtAd.Text);
            command.Parameters.AddWithValue("@DoktorSoyad", txtSoyad.Text);
            command.Parameters.AddWithValue("@DoktorBrans", cmbBrans.Text);
            command.Parameters.AddWithValue("@DoktorTC", mtxTCKimlik.Text);
            command.Parameters.AddWithValue("@DoktorSifre", txtSifre.Text);
            command.ExecuteNonQuery();
            connection.Connection().Close();
            MessageBox.Show("Doktor eklendi...");
            GetAllDoctors();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text=dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            mtxTCKimlik.Text=dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            cmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            SqlCommand command =
                new SqlCommand(
                    "update tbl_Doktorlar set DoktorAd=@DoktorAd, DoktorSoyad=@DoktorSoyad, DoktorBrans=@DoktorBrans, DoktorTc=@DoktorTC, DoktorSifre=@DoktorSifre where DoktorId=@DoktorId",
                    connection.Connection());
            command.Parameters.AddWithValue("@DoktorAd", txtAd.Text);
            command.Parameters.AddWithValue("@DoktorSoyad", txtSoyad.Text);
            command.Parameters.AddWithValue("@DoktorBrans", cmbBrans.Text);
            command.Parameters.AddWithValue("@DoktorTC", mtxTCKimlik.Text);
            command.Parameters.AddWithValue("@DoktorSifre", txtSifre.Text);
            command.Parameters.AddWithValue("@DoktorId", dataGridView1.Rows[secilen].Cells[0].Value.ToString());
            command.ExecuteNonQuery();
            connection.Connection().Close();
            MessageBox.Show("Doktor bilgileri güncellendi...");
            GetAllDoctors();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("delete from tbl_Doktorlar where DoktorTC=@DoktorTC",
                connection.Connection());
            command.Parameters.AddWithValue("@DoktorTC", mtxTCKimlik.Text);
            command.ExecuteNonQuery();
            connection.Connection().Close();
            MessageBox.Show("Kayıt silindi...");
            GetAllDoctors();
        }
    }
}
