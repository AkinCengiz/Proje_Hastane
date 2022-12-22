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
    public partial class frmBransPaneli : Form
    {
        public frmBransPaneli()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private DatabaseConnectionSentence connection = new DatabaseConnectionSentence();
        private void frmBransPaneli_Load(object sender, EventArgs e)
        {
            GetAllBrachs();
        }

        private void GetAllBrachs()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from tbl_Branslar", connection.Connection());
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtBrans.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into tbl_Branslar (BransAd) values (@BransAd)",
                connection.Connection());
            command.Parameters.AddWithValue("@BransAd", txtBrans.Text);
            command.ExecuteNonQuery();
            connection.Connection().Close();
            MessageBox.Show("Branş başarılı bir şekilde eklendi...");
            GetAllBrachs();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("update tbl_Branslar set BransAd=@BransAd where BransId=@BransId",
                connection.Connection());
            command.Parameters.AddWithValue("@BransAd", txtBrans.Text);
            command.Parameters.AddWithValue("@BransId", txtId.Text);
            command.ExecuteNonQuery();
            connection.Connection().Close();
            MessageBox.Show("Branş başarılı bir şekilde güncellendi...");
            GetAllBrachs();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand command =
                new SqlCommand("Delete from tbl_Branslar where BransId=@BransId", connection.Connection());
            command.Parameters.AddWithValue("@BransId", txtId);
            command.ExecuteNonQuery();
            connection.Connection().Close();
            MessageBox.Show("Branş başarılı bir şekilde silindi...");
        }
    }
}
