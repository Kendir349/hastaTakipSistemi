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
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Drawing.Printing;

namespace hastaTakipSistemi
{
    public partial class frmAnaSayfa : Form
    {

     

        public frmAnaSayfa()
        {
            InitializeComponent();

        }
        frmSqlBaglanti bgl = new frmSqlBaglanti();
        private PrintDocument printDoc = new PrintDocument();

        private void frmAnaSayfa_Load(object sender, EventArgs e)
        {
            txtTarih.Text = DateTime.Now.ToString("dd.MM.yyyy");
            txtCinsiyet.SelectedIndex = 0;
            rbHayir.Checked = true;




            this.txtTelefon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefon_KeyPress);

            this.txtTc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTc_KeyPress);
            Listele();
            durumDoldur();
            bolumDoldur();
            ButonKontrol();
        }

        
        private void Listele()
        {
            SqlCommand liste = new SqlCommand("listele",bgl.baglan());
            SqlDataAdapter da = new SqlDataAdapter(liste);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

             
        }
        private void durumDoldur()
        {
            SqlCommand durum = new SqlCommand("durumDoldur",bgl.baglan());
            SqlDataAdapter da =new SqlDataAdapter(durum);
            DataTable dt = new DataTable();
            da.Fill(dt);
            txtDurum.DataSource= dt;
            txtDurum.DisplayMember = "durumAd";
            txtDurum.ValueMember = "durumID";
        }
        private void bolumDoldur()
        {
            try
            {
                SqlCommand bolum = new SqlCommand("bolumDoldur", bgl.baglan());
                SqlDataAdapter da = new SqlDataAdapter(bolum);
                DataTable dt = new DataTable();
                da.Fill(dt);
                txtBolum.DataSource = dt;
                txtBolum.DisplayMember = "bolumAd";
                txtBolum.ValueMember = "bolumID";
            }
            catch (Exception)
            {
                MessageBox.Show("Bilinmeyen bir Hata\nLütfen tekrar deneyiniz veya uygulamayı yeniden başlatınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                
            }
           
        }
        

        private void btnListele_Click(object sender, EventArgs e)
        {
            try
            {
                ButonKontrol();
                Listele();
            }
            catch (Exception)
            {
                MessageBox.Show("Bilinmeyen bir Hata\nLütfen tekrar deneyiniz veya uygulamayı yeniden başlatınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

               
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView grid = (DataGridView)sender;
                if (e.RowIndex < 0) return;

               


                var row = grid.Rows[e.RowIndex];



                int secilen = dataGridView1.SelectedCells[0].RowIndex;
                txtID.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
                txtAd.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
                txtSoyad.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
                txtTc.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
                txtTelefon.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
                txtYas.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
                txtCinsiyet.Text = dataGridView1.Rows[secilen].Cells[8].Value.ToString();
                txtSikayet.Text = dataGridView1.Rows[secilen].Cells[9].Value.ToString();
                txtTarih.Text = dataGridView1.Rows[secilen].Cells[10].Value.ToString();
                txtDurum.SelectedValue = row.Cells["durumID"].Value.ToString(); 
                txtBolum.SelectedValue = row.Cells["bolumID"].Value.ToString(); 
                lblEx.Text = dataGridView1.Rows[secilen].Cells[11].Value.ToString();

            }
            catch (Exception)
            {
                MessageBox.Show("Bilinmeyen bir Hata\nLütfen tekrar deneyiniz veya uygulamayı yeniden başlatınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

          

        }

        private void rbEvet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbEvet.Checked == true)
                {
                    lblEx.Text = "True";
                }
                else
                {
                    lblEx.Text = "False";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bilinmeyen bir Hata\nLütfen tekrar deneyiniz veya uygulamayı yeniden başlatınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                
            }
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label10_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void lblEx_TextChanged(object sender, EventArgs e)
        {
            if (lblEx.Text == "True")
            {
                rbEvet.Checked = true;
            }
            else
            {
                rbHayir.Checked = true;
            }
        }
        private bool TelefonKontrol(string telefon)
        {
            
            string pattern = @"^(\+90\s?|0)?(5\d{2})[\s\-\(]?\d{3}[\s\-\)]?\d{2}[\s\-]?\d{2}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(telefon.Trim());
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtCinsiyet.SelectedItem == null)
            {
                MessageBox.Show("Lütfen cinsiyet seçiniz.");
                return;
            }


            string girilenNumara = txtTelefon.Text;

            if (TelefonKontrol(girilenNumara))
            {
                if (txtAd.Text != "" && txtBolum.Text != "" && txtCinsiyet.Text != "" && txtDurum.Text != "" && txtSikayet.Text != "" && txtSoyad.Text != "" && txtTc.Text != "" && txtTelefon.Text != "" && txtYas.Text != "")
                {
                    kaydet();
                    Listele();
                    ButonKontrol();
                }
                else
                {
                    MessageBox.Show("Lütfen İlgili Tüm Alanları Doldurunuz", "Kayıt Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Geçersiz telefon numarası!");
            }

           




        }
        private void kaydet()
        {
            SqlCommand kaydet = new SqlCommand("kaydet",bgl.baglan());
            kaydet.CommandType = CommandType.StoredProcedure;
            kaydet.Parameters.AddWithValue("ad", txtAd.Text.ToString());
            kaydet.Parameters.AddWithValue("soyad",txtSoyad.Text.ToString());
            kaydet.Parameters.AddWithValue("tc",txtTc.Text.ToString());
            kaydet.Parameters.AddWithValue("tel",txtTelefon.Text.ToString());
            kaydet.Parameters.AddWithValue("yas",int.Parse(txtYas.Text.ToString()));
            kaydet.Parameters.AddWithValue("cins",txtCinsiyet.Text.ToString());
            kaydet.Parameters.AddWithValue("sikayet",txtSikayet.Text.ToString());
            kaydet.Parameters.AddWithValue("tarih",DateTime.Now);
            kaydet.Parameters.AddWithValue("durum",(txtDurum.SelectedValue));
            kaydet.Parameters.AddWithValue("bolum",txtBolum.SelectedValue);
            if (lblEx.Text=="True")
            {
                kaydet.Parameters.AddWithValue("ex", 1);
            }
            else
            {
                kaydet.Parameters.AddWithValue("ex", 0);

            }
            kaydet.ExecuteNonQuery();
            MessageBox.Show("Kayıt Başarıyla Eklendi", "Kayıt Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);




        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            sil();
            ButonKontrol();
        }
        private void sil()
        {
            try
            {
                DialogResult dr = MessageBox.Show($"{txtID.Text} Numaralı Kayıt Silicecek Onaylıyor Musunuz ?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    SqlCommand sil = new SqlCommand("sil", bgl.baglan());
                    sil.CommandType = CommandType.StoredProcedure;
                    sil.Parameters.AddWithValue("id", int.Parse(txtID.Text));
                    sil.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Başarıyla Silindi", "Kayıt Sime Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listele();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bilinmeyen bir Hata\nLütfen tekrar deneyiniz veya uygulamayı yeniden başlatınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
            



        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show($"{txtID.Text} Numaralı Kayıt Güncellenecek Onaylıyor Musunuz ?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                guncelle();
                Listele();
            }
            
        }

        private void guncelle()
        {
            try
            {
                SqlCommand guncelle = new SqlCommand("guncelle", bgl.baglan());
                guncelle.CommandType = CommandType.StoredProcedure;
                guncelle.Parameters.AddWithValue("id", int.Parse(txtID.Text));
                guncelle.Parameters.AddWithValue("ad", txtAd.Text.ToString());
                guncelle.Parameters.AddWithValue("soyad", txtSoyad.Text.ToString());
                guncelle.Parameters.AddWithValue("tc", txtTc.Text.ToString());
                guncelle.Parameters.AddWithValue("tel", txtTelefon.Text.ToString());
                guncelle.Parameters.AddWithValue("yas", int.Parse(txtYas.Text.ToString()));
                guncelle.Parameters.AddWithValue("cins", txtCinsiyet.Text.ToString());
                guncelle.Parameters.AddWithValue("sikayet", txtSikayet.Text.ToString());
                guncelle.Parameters.AddWithValue("tarih", DateTime.Now);
                guncelle.Parameters.AddWithValue("durum", (txtDurum.SelectedValue));
                guncelle.Parameters.AddWithValue("bolum", txtBolum.SelectedValue);
                if (lblEx.Text == "True")
                {
                    guncelle.Parameters.AddWithValue("ex", 1);
                }
                else
                {
                    guncelle.Parameters.AddWithValue("ex", 0);

                }
                guncelle.ExecuteNonQuery();
                MessageBox.Show("Güncelleme İşlemi Başarılı", "Güncelleme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

                MessageBox.Show("Güncelleme işlemi yapmak için bir id seçiniz", "İşlem hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
        private void temzile()
        {
            txtAd.Text = "";
            txtBolum.Text = "";
            txtCinsiyet.Text = "";
            txtDurum.Text = "";
            txtID.Text = "";
            txtSikayet.Text = "";
            txtSoyad.Text = "";
            txtTarih.Text = "";
            txtTc.Text = "";
            txtTelefon.Text = "";
            txtYas.Text = "";
            rbHayir.Checked = true;
            lblEx.Text = "False";
        }

        private void btnFormuTemizle_Click(object sender, EventArgs e)
        {
            temzile();
            ButonKontrol();
            Listele();
        }

        private void btnİstatistic_Click(object sender, EventArgs e)
        {
            frmİstatistic fr = new frmİstatistic();
            fr.Show();
        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
                MessageBox.Show("Sadece rakam girebilirsiniz.", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            
            TextBox txt = (TextBox)sender;
            if (!char.IsControl(e.KeyChar) && txt.Text.Length >= 11)
            {
                e.Handled = true;
                MessageBox.Show("TC Kimlik No 11 rakamdan oluşmalıdır.", "Karakter Sınırı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtTelefon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Sadece rakam girebilirsiniz.", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

           
            TextBox txt = (TextBox)sender;
            if (!char.IsControl(e.KeyChar) && txt.Text.Length >= 11)
            {
                e.Handled = true;
                MessageBox.Show("Telefon numarası en fazla 11 haneli olabilir.", "Karakter Sınırı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCinsiyet_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 11 && e.Value != null)
                {
                    bool hastaExMi = Convert.ToBoolean(e.Value);

                    if (hastaExMi)
                    {
                        // Satırı kırmızı yap
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red; // Kırmızı tonlu arka plan
                        e.Value = "Hasta Ölü";
                    }
                    else
                    {
                        // Diğer durumlarda normal arka plan
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                        e.Value = "Hasta Yaşıyor";
                    }
                }
            }
            catch (Exception)
            { 
            }
        }

        private void txtTarih_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAd_TextChanged(object sender, EventArgs e)
        {
            if (txtAd.Text.Length > 0)
            {
                int selectionStart = txtAd.SelectionStart;
                string text = txtAd.Text;
                txtAd.Text = char.ToUpper(text[0]) + text.Substring(1);
                txtAd.SelectionStart = selectionStart;
            }
        }

        private void txtAd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Harf dışı karakteri engelle
                MessageBox.Show("Lütfen sadece harf girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Harf dışı karakteri engelle
                MessageBox.Show("Lütfen sadece harf girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSikayet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Harf dışı karakteri engelle
                MessageBox.Show("Lütfen sadece harf girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtYas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; 
                MessageBox.Show("Lütfen sadece sayı girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPdfKaydet_Click(object sender, EventArgs e)
        {
            ButonKontrol();
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF Dosyası|*.pdf";
            save.FileName = "HastaListesi.pdf";

            if (save.ShowDialog() == DialogResult.OK)
            {
                // Font ayarla 
                string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 10); // boyutu 10 yaptık tablo için

                //PDF oluştur
                Document doc = new Document(PageSize.A4.Rotate());
                PdfWriter.GetInstance(doc, new FileStream(save.FileName, FileMode.Create));
                doc.Open();

                PdfPTable table = new PdfPTable(dataGridView1.Columns.Count);
                table.WidthPercentage = 100;

                //Sütun başlıklarını ekle
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, font));
                    cell.BackgroundColor = BaseColor.LightGray;
                    table.AddCell(cell);
                }

                // Satır verilerini ekle
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    foreach (DataGridViewCell dgvCell in row.Cells)
                    {
                        string cellText = dgvCell.Value?.ToString() ?? "";
                        PdfPCell cell = new PdfPCell(new Phrase(cellText, font));
                        table.AddCell(cell);
                    }
                }

                doc.Add(table);
                doc.Close();

                MessageBox.Show("PDF başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
       

        private void txtSoyad_TextChanged(object sender, EventArgs e)
        {
            if (txtSoyad.Text.Length > 0)
            {
                int selectionStart = txtSoyad.SelectionStart;
                string text = txtSoyad.Text;
                txtSoyad.Text = char.ToUpper(text[0]) + text.Substring(1);
                txtSoyad.SelectionStart = selectionStart;
            }
        }

        private void txtSikayet_TextChanged(object sender, EventArgs e)
        {
            if (txtSikayet.Text.Length > 0)
            {
                int selectionStart = txtSikayet.SelectionStart;
                string text = txtSikayet.Text;
                txtSikayet.Text = char.ToUpper(text[0]) + text.Substring(1);
                txtSikayet.SelectionStart = selectionStart;
            }
        }

        private void txtCinsiyet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmAnaSayfa_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void ButonKontrol()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                // DataGridView boşsa butonları pasif yap
                btnSil.Enabled = false;
                btnGuncelle.Enabled = false;

                // diğer pasif olmasını istediğin butonlar
            }
            else
            {
                // DataGridView doluysa butonları aktif yap
                btnSil.Enabled = true;
                btnGuncelle.Enabled = true;

                // diğer aktif olmasını istediğin butonlar

            }
        }
    }
}
