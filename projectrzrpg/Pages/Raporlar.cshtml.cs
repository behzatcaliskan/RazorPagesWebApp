using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace projectrzrpg.Pages
{
    public class RaporlarModel : PageModel
    {
        public int YoneticiSayisi { get; set; }
        public int KategoriSayisi { get; set; }
        public int AktifKategoriSayisi { get; set; }
        public int PasifKategoriSayisi { get; set; }
        public int ToplamTabloSayisi { get; set; } = 2;

        public string SonYonetici { get; set; } = "-";
        public string SonKategori { get; set; } = "-";

        public string HataMesaji { get; set; } = "";

        public void OnGet()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=crmM;" +
                "Integrated Security=true;TrustServerCertificate=true;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlYonetici = "SELECT COUNT(*) FROM yoneticiler";
                    using (SqlCommand command = new SqlCommand(sqlYonetici, connection))
                    {
                        YoneticiSayisi = Convert.ToInt32(command.ExecuteScalar());
                    }

                    string sqlKategori = "SELECT COUNT(*) FROM Kategoriler";
                    using (SqlCommand command = new SqlCommand(sqlKategori, connection))
                    {
                        KategoriSayisi = Convert.ToInt32(command.ExecuteScalar());
                    }

                    string sqlAktifKategori = "SELECT COUNT(*) FROM Kategoriler WHERE Durum = 'Aktif'";
                    using (SqlCommand command = new SqlCommand(sqlAktifKategori, connection))
                    {
                        AktifKategoriSayisi = Convert.ToInt32(command.ExecuteScalar());
                    }

                    string sqlPasifKategori = "SELECT COUNT(*) FROM Kategoriler WHERE Durum = 'Pasif'";
                    using (SqlCommand command = new SqlCommand(sqlPasifKategori, connection))
                    {
                        PasifKategoriSayisi = Convert.ToInt32(command.ExecuteScalar());
                    }

                    string sqlSonYonetici = "SELECT TOP 1 AdSoyad FROM yoneticiler ORDER BY ID DESC";
                    using (SqlCommand command = new SqlCommand(sqlSonYonetici, connection))
                    {
                        object sonuc = command.ExecuteScalar();

                        if (sonuc != null)
                        {
                            SonYonetici = sonuc.ToString();
                        }
                    }

                    string sqlSonKategori = "SELECT TOP 1 KategoriAdi FROM Kategoriler ORDER BY ID DESC";
                    using (SqlCommand command = new SqlCommand(sqlSonKategori, connection))
                    {
                        object sonuc = command.ExecuteScalar();

                        if (sonuc != null)
                        {
                            SonKategori = sonuc.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HataMesaji = ex.Message;
            }
        }
    }
}