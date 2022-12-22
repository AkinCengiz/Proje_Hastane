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
    public partial class frmDoktorBilgiDuzenle : Form
    {
        public frmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }

        public string TC;
        private DatabaseConnectionSentence connection = new DatabaseConnectionSentence();
        private void frmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mtxTCKimlik.Text = TC;

            //combobox branşlar ile dolduruluyor
            SqlCommand bransCommand = new SqlCommand("select BransAd from tbl_Branslar", connection.Connection());
            SqlDataReader reader = bransCommand.ExecuteReader();
            while (reader.Read())
            {
                cmbBrans.Items.Add(reader[0]);
            }
            reader.Close();
            connection.Connection().Close();

            //Control elemanlarına veri çekiyoruz...
            SqlCommand command =
                new SqlCommand("select * from tbl_Doktorlar where DoktorTC=@TC", connection.Connection());
            command.Parameters.AddWithValue("@TC", mtxTCKimlik.Text);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                txtAd.Text = reader[1].ToString();
                txtSoyad.Text = reader[2].ToString();
                cmbBrans.Text = reader[3].ToString();
                txtSifre.Text = reader[5].ToString();
            }
            reader.Close();
            connection.Connection().Close();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand command =
                new SqlCommand(
                    "update tbl_Doktorlar set DoktorAd=@Ad, DoktorSoyad=@Soyad, DoktorBrans=@Brans, DoktorSifre=@Sifre where DoktorTC=@TC",
                    connection.Connection());
            command.Parameters.AddWithValue("@Ad", txtAd.Text);
            command.Parameters.AddWithValue("@Soyad", txtSoyad.Text);
            command.Parameters.AddWithValue("@Brans", cmbBrans.Text);
            command.Parameters.AddWithValue("@Sifre", txtSifre.Text);
            command.Parameters.AddWithValue("@TC", mtxTCKimlik.Text);
            command.ExecuteNonQuery();
            connection.Connection().Close();
            MessageBox.Show("Bilgiler güncellendi...");
        }
    }
}
