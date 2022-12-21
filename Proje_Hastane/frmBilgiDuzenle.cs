using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje_Hastane
{
    public partial class frmBilgiDuzenle : Form
    {
        public frmBilgiDuzenle()
        {
            InitializeComponent();
        }

        public string TC;
        private DatabaseConnectionSentence connection = new DatabaseConnectionSentence();
        private void frmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mtxTCKimlik.Text = TC;
            //formları dolduruyoruz
            SqlCommand command = new SqlCommand("select * from tbl_Hastalar where HastaTC=@HastaTC",connection.Connection());
            command.Parameters.AddWithValue("@HastaTC", TC);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                txtAd.Text = reader[1].ToString();
                txtSoyad.Text = reader[2].ToString();
                mtxTelefon.Text = reader[4].ToString();
                txtSifre.Text = reader[5].ToString();
                cmbCinsiyet.Text = reader[6].ToString();
            }
            reader.Close();
            connection.Connection().Close();
        }

        private void btnKayitDuzenle_Click(object sender, EventArgs e)
        {
            SqlCommand command =
                new SqlCommand(
                    "update tbl_Hastalar set HastaAd=@HastaAd, HastaSoyad=@HastaSoyad, HastaTelefon=@HastaTelefon,HastaSifre=@HastaSifre, HastaCinsiyet=@HastaCinsiyet where HastaTc=@HastaTC",
                    connection.Connection());
            command.Parameters.AddWithValue("@HastaAd", txtAd.Text);
            command.Parameters.AddWithValue("@HastaSoyad", txtSoyad.Text);
            command.Parameters.AddWithValue("@HastaTelefon", mtxTelefon.Text);
            command.Parameters.AddWithValue("@HastaSifre", txtSifre.Text);
            command.Parameters.AddWithValue("@HastaCinsiyet", cmbCinsiyet.Text);
            command.Parameters.AddWithValue("@HastaTC", mtxTCKimlik.Text);
            command.ExecuteNonQuery();
            connection.Connection().Close();
            MessageBox.Show("Bilgileriiniz güncellendi...","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
    }
}
