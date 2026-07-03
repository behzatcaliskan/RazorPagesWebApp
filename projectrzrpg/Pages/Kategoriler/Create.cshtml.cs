using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace projectrzrpg.Pages.Kategoriler
{
    public class CreateModel : PageModel
    {
        public string hataMesaji = "";
        public string basariMesaji = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            string KategoriAdi = Request.Form["KategoriAdi"];
            string Aciklama = Request.Form["Aciklama"];
            string Durum = Request.Form["Durum"];

            if (KategoriAdi.Length == 0 || Aciklama.Length == 0 || Durum.Length == 0)
            {
                hataMesaji = "Tüm alanlarý doldurunuz.";
                return;
            }

            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=crmM;" +
                "Integrated Security=true;TrustServerCertificate=true;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Kategoriler (KategoriAdi, Aciklama, Durum) VALUES (@KategoriAdi, @Aciklama, @Durum)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@KategoriAdi", KategoriAdi);
                    command.Parameters.AddWithValue("@Aciklama", Aciklama);
                    command.Parameters.AddWithValue("@Durum", Durum);

                    command.ExecuteNonQuery();
                }
            }

            Response.Redirect("/Kategoriler/Index");
        }
    }
}