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
    public partial class frmSekreterGiris : Form
    {
        public frmSekreterGiris()
        {
            InitializeComponent();
        }

        private DatabaseConnectionSentence connection = new DatabaseConnectionSentence();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select * from tbl_Sekreterler where SekreterSifre=@Sifre and SekreterTC=@TC",connection.Connection());
            command.Parameters.AddWithValue("@Sifre", txtSekreterSifre.Text);
            command.Parameters.AddWithValue("@TC", mtxTCKimlik.Text);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                frmSekreterDetay sekreterDetay = new frmSekreterDetay();
                sekreterDetay.TC = mtxTCKimlik.Text;
                sekreterDetay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC Kimlik No ya da Hatalı Şifre");
            }
            connection.Connection().Close();
        }
    }
}
