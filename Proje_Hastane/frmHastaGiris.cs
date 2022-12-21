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
    public partial class frmHastaGiris : Form
    {
        public frmHastaGiris()
        {
            InitializeComponent();
        }

        private DatabaseConnectionSentence connection = new DatabaseConnectionSentence();
        private void lnlblUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmHastaKayit hastaKayit = new frmHastaKayit();
            hastaKayit.Show();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select * from tbl_Hastalar where HastaTC=@HastaTC and HastaSifre=@HastaSifre",
                    connection.Connection());
            command.Parameters.AddWithValue("@HastaTC", mtxTCKimlik.Text);
            command.Parameters.AddWithValue("@HastaSifre", txtHastaSifre.Text);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                frmHastaDetay hastaDetay = new frmHastaDetay();
                hastaDetay.TC = mtxTCKimlik.Text;
                hastaDetay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı T.C. Kimlik Numarası ya da şifre girdiniz....");
            }
            connection.Connection().Close();
        }
    }
}
