using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace projectrzrpg.Pages.Kategoriler
{
    public class IndexModel : PageModel
    {
        public List<kategoriler> listele { get; set; } = new List<kategoriler>();

        public void OnGet()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=crmM;" +
                "Integrated Security=true;TrustServerCertificate=true;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Kategoriler";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            kategoriler kategori = new kategoriler
                            {
                                ID = reader.GetInt32(0).ToString(),
                                KategoriAdi = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                Aciklama = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Durum = reader.IsDBNull(3) ? "" : reader.GetString(3)
                            };

                            listele.Add(kategori);
                        }
                    }
                }
            }
        }
    }
}