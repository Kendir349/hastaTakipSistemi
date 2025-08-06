using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace hastaTakipSistemi
{
    public partial class frmveritabaniayar : Form
    {
        public frmveritabaniayar()
        {
            InitializeComponent();
            radioButton1.Checked = true; 
            txtKuladi.Enabled = false;    
            txtSifre.Enabled = false;
            ModernTasarim();
        }

        private void ModernTasarim()
        {
            // Form Arkaplan
            this.BackColor = Color.FromArgb(240, 242, 245);

            // Başlık Label
            label1.Text = "Veri Tabanı Kontrol";
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(52, 73, 94);

            // RadioButtonlar
            radioButton1.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            radioButton2.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            // Label’lar
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            label5.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

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
            btnKaydet.BackColor = Color.FromArgb(39, 174, 96); // Yeşil
            btnKaydet.ForeColor = Color.White;
            btnKaydet.FlatStyle = FlatStyle.Flat;
            btnKaydet.FlatAppearance.BorderSize = 0;
            btnKaydet.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Çıkış Butonu
            btnCikis.BackColor = Color.FromArgb(231, 76, 60); // Kırmızı
            btnCikis.ForeColor = Color.White;
            btnCikis.FlatStyle = FlatStyle.Flat;
            btnCikis.FlatAppearance.BorderSize = 0;
            btnCikis.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);


            
        }

        frmSqlBaglanti bgl = new frmSqlBaglanti();
        private static IEnumerable<string> SplitSqlScript(string script)
        {
           
            script = Regex.Replace(script, @"--.*$", "", RegexOptions.Multiline);

            
            script = Regex.Replace(script, @"/\*.*?\*/", "", RegexOptions.Singleline);

          
            script = Regex.Replace(script, @"\s+", " ");

           
            return script.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(x => x.Trim())
                         .Where(x => !string.IsNullOrWhiteSpace(x));
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {

           

            string newConnectionString = "";
            if (radioButton1.Checked) 
                newConnectionString = $"Data Source={txtSqlYolu.Text};Initial Catalog={txtVeriTabanıAdı.Text};Integrated Security=True;";
            if (radioButton2.Checked) 
                newConnectionString = $"Data Source={txtSqlYolu.Text};Initial Catalog={txtVeriTabanıAdı.Text};User ID={txtKuladi.Text};Password={txtSifre.Text};Encrypt=False";

            
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

         
            ConnectionStringsSection connectionStringsSection = config.GetSection("connectionStrings") as ConnectionStringsSection;

            if (connectionStringsSection != null)
            {
               
                if (connectionStringsSection.ConnectionStrings["HastaneBaglantisi"] != null)
                {
                    connectionStringsSection.ConnectionStrings["HastaneBaglantisi"].ConnectionString = newConnectionString;

                    
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("connectionStrings");


                    if (bgl.baglantikontrol())
                    {
                        #region script i yükle

                        if (File.Exists("setup.sql"))
                        {
                            string script = File.ReadAllText("setup.sql");

                            using (SqlConnection conn = new SqlConnection(bgl.baglanticumlesi()))
                            {
                                if (conn.State != ConnectionState.Open) conn.Open();

                                foreach (string commandText in SplitSqlScript(script))
                                {
                                    try
                                    {
                                        using (SqlCommand cmd = new SqlCommand(commandText, conn))
                                        {
                                            cmd.CommandTimeout = 600; // Gerekirse zaman aşımını artır
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Hata: {commandText}");
                                        Console.WriteLine($"Detay: {ex.Message}");
                                        // Burada loglama da yapılabilir
                                    }
                                }
                            }

                        }
                        if (!File.Exists("setup.sql"))
                        {
                            MessageBox.Show("sql dosyası bulunamadı lütfen manuel kurulum yöntemini kullanın");
                        }

                        #endregion


                        MessageBox.Show("Bilgiler Güncellenmiştir Lütfen Programı Tekrar Başlatınız");


                       
                    }



                    Environment.Exit(0);
                }
                else
                {
                    MessageBox.Show("Belirtilen connection string bulunamadı.");
                }
            }
            else
            {
                MessageBox.Show("ConnectionStringsSection alınamadı.");
            }
        }

        private void frmveritabaniayar_Load(object sender, EventArgs e)
        {

        }
      






        void RemoveText(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "Kullanıcı Adı")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }
        void AddText(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Kullanıcı Adı";
                tb.ForeColor = Color.Gray;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                txtKuladi.Enabled = false;
                txtSifre.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                txtKuladi.Enabled = true;
                txtSifre.Enabled = true;
            }
        }

        private void txtKuladi_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSifre_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmveritabaniayar_FormClosing(object sender, FormClosingEventArgs e)
        {
       
        
           
        
    }
    }
}
