using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hastaTakipSistemi
{
    public partial class frmİstatistic : Form
    {
        public frmİstatistic()
        {
            InitializeComponent();
        }
        frmSqlBaglanti bgl = new frmSqlBaglanti();

        private void frmİstatistic_Load(object sender, EventArgs e)
        {
            
            toplamHasta();
            yasOrtalama();
            erkekSayi();
            kadinSayi();
            toplamExSayisi();
        }
        private void toplamHasta()
        {
            SqlCommand toplam = new SqlCommand("select COUNT(*) from tbl_HastaBilgi",bgl.baglan());
           SqlDataReader dr = toplam.ExecuteReader();
            while (dr.Read())
            {
                lblToplamHasta.Text = dr[0].ToString();
            }
           


        }
        private void yasOrtalama()
        {
            SqlCommand ortalama = new SqlCommand("select AVG(hYas)from tbl_HastaBilgi", bgl.baglan());
            SqlDataReader dr = ortalama.ExecuteReader();
            while (dr.Read())
            {
                lblYasOrtalama.Text = dr[0].ToString();
            }



        }

        private void erkekSayi()
        {
            SqlCommand erkeksayi = new SqlCommand("select COUNT(*)from tbl_HastaBilgi where hCinsiyet = 'Erkek'", bgl.baglan());
            SqlDataReader dr = erkeksayi.ExecuteReader();
            while (dr.Read())
            {
                lblErkekSayisi.Text = dr[0].ToString();
            }



        
    }

        private void kadinSayi()
        {
            SqlCommand kadinSayi = new SqlCommand("select COUNT(*)from tbl_HastaBilgi where hCinsiyet = 'Kadın'", bgl.baglan());
            SqlDataReader dr = kadinSayi.ExecuteReader();
            while (dr.Read())
            {
                lblKadinSayisi.Text = dr[0].ToString();
            }




        }

        private void toplamExSayisi()
        {
            SqlCommand toplamExSayisi = new SqlCommand("select COUNT(*) from tbl_HastaBilgi where hExMi = 1", bgl.baglan());
            SqlDataReader dr = toplamExSayisi.ExecuteReader();
            while (dr.Read())
            {
                lblExsayi.Text = dr[0].ToString();
            }




        }
    }
}
