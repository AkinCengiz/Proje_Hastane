using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje_Hastane
{
    public partial class frmGirisler : Form
    {
        public frmGirisler()
        {
            InitializeComponent();
        }

        public void btnHastaGiris_Click(object sender, EventArgs e)
        {
            frmHastaGiris hastaGiris = new frmHastaGiris();
            hastaGiris.Show();
            this.Hide();
        }

        public void frmGirisler_Load(object sender, EventArgs e)
        {

        }

        public void btnDoktorGiris_Click(object sender, EventArgs e)
        {
            frmDoktorGiris doktorGiris = new frmDoktorGiris();
            doktorGiris.Show();
            this.Hide();
        }

        public void btnSekreterGiris_Click(object sender, EventArgs e)
        {
            frmSekreterGiris sekreterGiris = new frmSekreterGiris();
            sekreterGiris.Show();
            this.Hide();
        }
    }
}
