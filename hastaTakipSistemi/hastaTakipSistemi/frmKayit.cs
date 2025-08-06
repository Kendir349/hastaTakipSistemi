using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

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
            }
            else
            {
                MessageBox.Show("Lütfen Tüm Alanları Doldurunuz","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
