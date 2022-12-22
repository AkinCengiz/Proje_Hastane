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
using System.Security.Cryptography;

namespace Proje_Hastane
{
    public partial class frmSekreterDetay : Form
    {
        public frmSekreterDetay()
        {
            InitializeComponent();
        }

        public string TC;
        private DatabaseConnectionSentence connection = new DatabaseConnectionSentence();
        private void txtSifre_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TC;
            //Sekreter Bilgilerini getiriyoruz.
            SqlCommand sekreterCommand = new SqlCommand("select SekreterAd, SekreterSoyad from tbl_Sekreterler where SekreterTC=@TC", connection.Connection());
            sekreterCommand.Parameters.AddWithValue("@TC", TC);
            SqlDataReader reader = sekreterCommand.ExecuteReader();
            while (reader.Read())
            {
                lblAdSoyad.Text = reader[0] + " " + reader[1];
            }
            reader.Close();
            connection.Connection().Close();

            //Datagrid üzerinde branşları gösterme
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from tbl_Branslar", connection.Connection());
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            //datagrid üzerinde doktorları gösterme
            DataTable doktorlarDataTable = new DataTable();
            dataAdapter = new SqlDataAdapter("select DoktorId, DoktorAd, DoktorSoyad, DoktorBrans from tbl_Doktorlar",
                connection.Connection());
            dataAdapter.Fill(doktorlarDataTable);
            dataGridView2.DataSource = doktorlarDataTable;

            //Combobox içeriğini branşlar ile doldurmak.
            SqlCommand bransCommand = new SqlCommand("select BransAd from tbl_Branslar", connection.Connection());
            reader = bransCommand.ExecuteReader();

            while (reader.Read())
            {
                cmbBrans.Items.Add(reader[0].ToString());
            }
            reader.Close();
            connection.Connection().Close();

        }

        private void btnBransPaneli_Click(object sender, EventArgs e)
        {
            frmBransPaneli bransPaneli = new frmBransPaneli();
            bransPaneli.Show();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand command =
                new SqlCommand(
                    "insert into tbl_Randevular (RandevuTarih, RandevuSaat,RandevuBrans,RandevuDoktor, RandevuDurum, HastaTC) values (@RandevuTarih, @RandevuSaat,@RandevuBrans,@RandevuDoktor, @RandevuDurum, @HastaTC)",
                    connection.Connection());
            command.Parameters.AddWithValue("@RandevuTarih", mtxtTarih.Text);
            command.Parameters.AddWithValue("@RandevuSaat", mtxtSaat.Text);
            command.Parameters.AddWithValue("@RandevuBrans", cmbBrans.Text);
            command.Parameters.AddWithValue("@RandevuDoktor", cmbDoktor.Text);
            if (cbxDurum.Checked)
            {
                command.Parameters.AddWithValue("@RandevuDurum", true);
            }
            else
            {
                command.Parameters.AddWithValue("@RandevuDurum", false);
            }

            command.Parameters.AddWithValue("@HastaTC", mtxTCKimlik.Text);
            command.ExecuteNonQuery();
            connection.Connection().Close();
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            cmbDoktor.Text = "";
            SqlCommand command =
                new SqlCommand("select DoktorAd, DoktorSoyad from tbl_Doktorlar where DoktorBrans=@Brans",
                    connection.Connection());
            command.Parameters.AddWithValue("@Brans", cmbBrans.Text);
            SqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                cmbDoktor.Items.Add(reader[0] + " " + reader[1]);
            }
            reader.Close();
            connection.Connection().Close();
        }

        private void btnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into tbl_Duyurular (Duyuru) values (@Duyuru)",
                connection.Connection());
            command.Parameters.AddWithValue("@Duyuru", rchDuyuru.Text);
            command.ExecuteNonQuery();
            connection.Connection().Close();
            MessageBox.Show("Duyuru oluşturuldu...");
        }

        private void btnDoktorPaneli_Click(object sender, EventArgs e)
        {
            frmDoktorPaneli doktorPaneli = new frmDoktorPaneli();
            doktorPaneli.Show();
        }

        private void btnRandevuListe_Click(object sender, EventArgs e)
        {
            frmRandevuListesi randevuListesi = new frmRandevuListesi();
            randevuListesi.Show();
            
        }

        private void btnGetir_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select * from tbl_Randevular where HastaTC=@HastaTC",
                connection.Connection());
            command.Parameters.AddWithValue("@HastaTC", mtxTCKimlik.Text);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                txtId.Text = reader[0].ToString();
                mtxtTarih.Text = reader[1].ToString();
                mtxtSaat.Text = reader[2].ToString();
                cmbBrans.Text = reader[3].ToString();
                cmbDoktor.Text = reader[4].ToString();
                if (Convert.ToBoolean(reader[5].ToString()) == true)
                {
                    cbxDurum.Checked=true;
                }
                else
                {
                    cbxDurum.Checked=false;
                }
                mtxTCKimlik.Text=reader[6].ToString();
            }
            reader.Close();
            connection.Connection().Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand command =
                new SqlCommand(
                    "update tbl_Randevular set RandevuTarih=@Tarih, RandevuSaat=@Saat, RandevuBrans=@Brans, RandevuDoktor=@Doktor, RandevuDurum=@Durum where HastaTC=@TC",
                    connection.Connection());
            command.Parameters.AddWithValue("@Tarih", mtxtTarih.Text);
            command.Parameters.AddWithValue("@Saat", mtxtSaat.Text);
            command.Parameters.AddWithValue("@Brans", cmbBrans.Text);
            command.Parameters.AddWithValue("@Doktor", cmbDoktor.Text);
            if (cbxDurum.Checked)
            {
                command.Parameters.AddWithValue("@Durum", true);
            }
            else
            {
                command.Parameters.AddWithValue("@Durum", false);
            }

            command.Parameters.AddWithValue("@TC", mtxTCKimlik.Text);
            command.ExecuteNonQuery();
            connection.Connection().Close();
            MessageBox.Show("Randevu bilgileri güncellendi...");
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            frmDuyurular duyurular = new frmDuyurular();
            duyurular.Show();
        }
    }
}
