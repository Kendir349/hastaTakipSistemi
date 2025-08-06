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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Reflection.Emit;

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
            try
            { 
                if (txtKulAdi.Text != "" && txtSifre.Text != "")
                {
                    SqlCommand giris = new SqlCommand("girisYap", bgl.baglan());
                    giris.CommandType = CommandType.StoredProcedure;
                    giris.Parameters.AddWithValue("kulAdi", txtKulAdi.Text);
                    giris.Parameters.AddWithValue("sifre", txtSifre.Text);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void baglantikontrol()
        {
            if (bgl.baglantikontrol() == false)
            {
                frmveritabaniayar frm = new frmveritabaniayar();
                frm.ShowDialog();
                baglantikontrol();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            baglantikontrol();
            this.MaximizeBox = false;
            ModernTasarim();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmveritabaniayar frmveritabaniayar = new frmveritabaniayar();
            frmveritabaniayar.ShowDialog();
        }
        private void ModernTasarim()
        {
            // Form Arkaplan
            this.BackColor = Color.FromArgb(240, 242, 245);

           

            

            // Label’lar
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
           

            // TextBox’lar
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox txt)
                {
                    txt.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.BackColor = Color.White;
                }
            }

            // Kaydet Butonu
            btnGiris.BackColor = Color.FromArgb(39, 174, 96); // Yeşil
            btnGiris.ForeColor = Color.White;
            btnGiris.FlatStyle = FlatStyle.Flat;
            btnGiris.FlatAppearance.BorderSize = 0;
            btnGiris.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Çıkış Butonu
            btnKayit.BackColor = Color.FromArgb(231, 76, 60); // Kırmızı
            btnKayit.ForeColor = Color.White;
            btnKayit.FlatStyle = FlatStyle.Flat;
            btnKayit.FlatAppearance.BorderSize = 0;
            btnKayit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Veri Tabanı Ayar Butonu
            button1.BackColor = Color.FromArgb(75, 0, 130);
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

      

    }
}
