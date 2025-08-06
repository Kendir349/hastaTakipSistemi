using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Security.AccessControl; // Bu satırı ekliyoruz!

namespace hastaTakipSistemi
{
    internal class frmSqlBaglanti
    {
        // Eski "adres" değişkenini kaldırıyoruz, çünkü bağlantı dizesini App.config'den alacağız.
        // string adres = @"Data Source=DESKTOP-CJ5PO7H;Initial Catalog=db_HastaneYonetim;Integrated Security=True;Encrypt=False"; 

        public Boolean baglantikontrol()
        {
            Boolean cevap = false;
            try
            { 
                string baglantiDizesi = ConfigurationManager.ConnectionStrings["HastaneBaglantisi"].ConnectionString;
                 SqlConnection baglanti = new SqlConnection(baglantiDizesi);
                if (baglanti.State == System.Data.ConnectionState.Closed)
                    baglanti.Open();

                cevap = true;
            }
            catch (System.Exception )
            {
                
            }

            return cevap;
        }


        public string baglanticumlesi()
        {
            string baglantiDizesi = ConfigurationManager.ConnectionStrings["HastaneBaglantisi"].ConnectionString;
            return baglantiDizesi;
        }


        public SqlConnection baglan()
        {
            try
            {
                // App.config dosyasındaki "HastaneBaglantisi" adlı bağlantı dizesini alıyoruz.
                // Bu, kodunuzu daha esnek ve taşınabilir hale getirir.
                string baglantiDizesi = ConfigurationManager.ConnectionStrings["HastaneBaglantisi"].ConnectionString;

                SqlConnection baglanti = new SqlConnection(baglantiDizesi);

                // Bağlantı zaten açıksa tekrar açmaya çalışmamak iyi bir pratiktir.
                if (baglanti.State == System.Data.ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                return baglanti;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("veritabanı baglantısı yapılamadı : " + ex.Message.ToString());
               
            }
        }

        // Ek olarak: Açık bağlantıları kapatmak için bir metod eklemek çok önemlidir.
        // Veritabanı işlemleriniz bittiğinde bu metodu çağırın.
        public void baglantiKapat(SqlConnection baglanti)
        {
            if (baglanti != null && baglanti.State == System.Data.ConnectionState.Open)
            {
                baglanti.Close();
            }
        }
    }
}