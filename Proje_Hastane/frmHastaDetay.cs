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

namespace Proje_Hastane
{
    public partial class frmHastaDetay : Form
    {
        public frmHastaDetay()
        {
            InitializeComponent();
        }

        public string TC;
        private DatabaseConnectionSentence connection = new DatabaseConnectionSentence();
        private void frmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TC;

            //Hasta Ad Soyad bilgiilerini getirme
            SqlCommand command = new SqlCommand("select HastaAd, HastaSoyad from tbl_Hastalar where HastaTC=@HastaTC",
                connection.Connection());
            command.Parameters.AddWithValue("@HastaTC", TC);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lblAdSoyad.Text = reader[0] + " " + reader[1];
            }
            reader.Close();
            connection.Connection().Close();

            //Randevu geçmişini getirme
            GetRandevu();

            //Branşları çekiyoruz.
            SqlCommand commandBranch = new SqlCommand("Select BransAd from tbl_Branslar", connection.Connection());
            reader = commandBranch.ExecuteReader();
            while (reader.Read())
            {
                cmbBrans.Items.Add(reader[0]);
            }
            reader.Close();
            connection.Connection().Close();
            cmbBrans.Text = "Branş Seçiniz...";
        }

        private void GetRandevu()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from tbl_Randevular where HastaTC='" + TC + "'",
                connection.Connection());
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            //Branşa göre doktor çekiyoruz..
            SqlCommand commandDoctor =
                new SqlCommand("select DoktorAd, DoktorSoyad from tbl_Doktorlar where DoktorBrans=@DoktorBrans",
                    connection.Connection());
            commandDoctor.Parameters.AddWithValue("@DoktorBrans", cmbBrans.Text);
            SqlDataReader reader = commandDoctor.ExecuteReader();

            while (reader.Read())
            {
                cmbDoktor.Items.Add(reader[0] + " " + reader[1]);
            }
            reader.Close();
            connection.Connection().Close();
            //cmbDoktor.SelectedIndex = 0;
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEmptyRandevu();
        }

        private void GetEmptyRandevu()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter =
                new SqlDataAdapter(
                    "select * from tbl_Randevular where RandevuBrans='" + cmbBrans.Text + "' and RandevuDoktor='" +
                    cmbDoktor.Text + "' and RandevuDurum=0", connection.Connection());
            dataAdapter.Fill(dataTable);
            dataGridView2.DataSource = dataTable;
        }

        private void lnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmBilgiDuzenle bilgiDuzenle = new frmBilgiDuzenle();
            bilgiDuzenle.TC = lblTC.Text;
            bilgiDuzenle.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secillen = dataGridView2.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView2.Rows[secillen].Cells[0].Value.ToString();
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            
            SqlCommand command = new SqlCommand(
                "update tbl_Randevular set RandevuDurum=@Durum, HastaTC=@TC, Sikayet=@Sikayet where RandevuId=@Id",
                connection.Connection());
            command.Parameters.AddWithValue("@Durum", true);
            command.Parameters.AddWithValue("@TC", lblTC.Text);
            command.Parameters.AddWithValue("@Sikayet", rtxtSikayet.Text);
            command.Parameters.AddWithValue("@Id", txtId.Text);
            command.ExecuteNonQuery();
            connection.Connection().Close();
            MessageBox.Show("Randevu alındı...");
            GetEmptyRandevu();
            GetRandevu();
        }
    }
}
