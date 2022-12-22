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
    public partial class frmDoktorGiris : Form
    {
        public frmDoktorGiris()
        {
            InitializeComponent();
        }

        private DatabaseConnectionSentence connection = new DatabaseConnectionSentence();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select * from tbl_Doktorlar where DoktorTC=@TC and DoktorSifre=@Sifre",
                connection.Connection());
            command.Parameters.AddWithValue("@TC", mtxTCKimlik.Text);
            command.Parameters.AddWithValue("@Sifre", txtDoktorSifre.Text);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                frmDoktorDetay doktorDetay = new frmDoktorDetay();
                doktorDetay.TC = mtxTCKimlik.Text;
                doktorDetay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı T.C. Kimlik No ya da Şifre!!!");
            }
            reader.Close();
            connection.Connection().Close();
        }
    }
}
