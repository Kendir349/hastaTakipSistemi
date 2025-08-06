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

namespace hastaTakipSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       frmSqlBaglanti bgl = new frmSqlBaglanti();
              
        private void btnKayit_Click(object sender, EventArgs e)
        {
            frmKayit fr = new frmKayit();
            fr.Show();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            if (txtKulAdi.Text !="" && txtSifre.Text!="")
            {
                SqlCommand giris = new SqlCommand("girisYap",bgl.baglan());
                giris.CommandType = CommandType.StoredProcedure;
                giris.Parameters.AddWithValue("kulAdi",txtKulAdi.Text);
                giris.Parameters.AddWithValue("sifre",txtSifre.Text);
                SqlDataReader dr = giris.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Giriş İşlemi Başarılı", "Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmAnaSayfa fr = new frmAnaSayfa();
                    this.Hide();
                    fr.Show();
                }
                else
                {
                    MessageBox.Show("Giriş İşlemi Başarısız", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen Tüm Alanları Doldurunuz", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
