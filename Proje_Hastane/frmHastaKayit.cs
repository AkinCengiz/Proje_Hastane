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
    public partial class frmHastaKayit : Form
    {
        public frmHastaKayit()
        {
            InitializeComponent();
        }

        private DatabaseConnectionSentence sentence = new DatabaseConnectionSentence();
        private void btnKayit_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into tbl_Hastalar (HastaAd, HastaSoyad, HastaTC,HastaTelefon, HastaSifre, HastaCinsiyet) values (@HastaAd, @HastaSoyad, @HastaTC, @HastaTelefon, @HastaSifre, @HastaCinsiyet)", sentence.Connection());
            command.Parameters.AddWithValue("@HastaAd", txtAd.Text);
            command.Parameters.AddWithValue("@HastaSoyad", txtSoyad.Text);
            command.Parameters.AddWithValue("@HastaTC", mtxTCKimlik.Text);
            command.Parameters.AddWithValue("@HastaTelefon", mtxTelefon.Text);
            command.Parameters.AddWithValue("@HastaSifre", txtSifre.Text);
            command.Parameters.AddWithValue("@HastaCinsiyet", cmbCinsiyet.Text);
            command.ExecuteNonQuery();
            sentence.Connection().Close();
            MessageBox.Show("Kaydınız başarı ile oluşturuldu.\nŞifreniz : " + txtSifre.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
