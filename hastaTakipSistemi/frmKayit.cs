using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Drawing;
using System.Reflection.Emit;

namespace hastaTakipSistemi
{
    public partial class frmKayit : Form
    {
        public frmKayit()
        {
            InitializeComponent();
        }
        frmSqlBaglanti bgl = new frmSqlBaglanti();

        private void btnKayit_Click(object sender, EventArgs e)
        {
            if (txtKulAdi.Text !="" && txtSifre.Text !="")
            {
                SqlCommand kayit = new SqlCommand("kayiOl", bgl.baglan());
                kayit.CommandType = CommandType.StoredProcedure;
                kayit.Parameters.AddWithValue("kulAdi",txtKulAdi.Text);
                kayit.Parameters.AddWithValue("sifre",txtSifre.Text);
                kayit.ExecuteNonQuery();
                MessageBox.Show("Kayıt İşlemi Başarılı","Kayıt Başarılı",MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Lütfen Tüm Alanları Doldurunuz","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void frmKayit_Load(object sender, EventArgs e)
        {
            ModernTasarim();
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
            btnKayit.BackColor = Color.FromArgb(39, 174, 96); // Yeşil
            btnKayit.ForeColor = Color.White;
            btnKayit.FlatStyle = FlatStyle.Flat;
            btnKayit.FlatAppearance.BorderSize = 0;
            btnKayit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            
        }
    }
}
