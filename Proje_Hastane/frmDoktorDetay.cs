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
    public partial class frmDoktorDetay : Form
    {
        public frmDoktorDetay()
        {
            InitializeComponent();
        }

        public string TC;
        private DatabaseConnectionSentence connection = new DatabaseConnectionSentence();
        private void frmDoktorDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TC;
            //Doktorun adını soyadını getiriyoruz.
            SqlCommand command = new SqlCommand("select DoktorAd, DoktorSoyad from tbl_Doktorlar where DoktorTC=@TC",
                connection.Connection());
            command.Parameters.AddWithValue("@TC", TC);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lblAdSoyad.Text = reader[0] + " " + reader[1];
            }
            reader.Close();
            connection.Connection().Close();

            //Randevu Listesini getiriyoruz
            GetAllRandevu();
        }

        private void GetAllRandevu()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from tbl_Randevular where RandevuDoktor='" + lblAdSoyad.Text+"'",
                connection.Connection());
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void btnBilgiDuzenle_Click(object sender, EventArgs e)
        {
            frmDoktorBilgiDuzenle doktorBilgiDuzenle = new frmDoktorBilgiDuzenle();
            doktorBilgiDuzenle.TC = lblTC.Text;
            doktorBilgiDuzenle.Show();
        }

        private void btnDuyuru_Click(object sender, EventArgs e)
        {
            frmDuyurular duyurular = new frmDuyurular();
            duyurular.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            rchSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }
    }
}
