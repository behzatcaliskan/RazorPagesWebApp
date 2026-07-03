using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace projectrzrpg.Pages.yoneticiler
{
    public class IndexModel : PageModel
    {

        [BindProperty]//yoneticiler sýnýfýndaki verilere tek tek öznitekil yani attribute
                      //tanýmlamak yere yoneticiler sýnýfdaki kolonlara direk eiþsin istedim aslýnda
        public List<yoneticiler> listele { get; set; } = new List<yoneticiler>();

        public void OnGet()
        {

            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=crmM;" +
                "Integrated Security=true;TrustServerCertificate=true;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    string sql = "select *from yoneticiler";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                yoneticiler yonetici = new yoneticiler
                                {
                                    ID = reader.GetInt32(0).ToString(),
                                    AdSoyad = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                    Email = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                    Telefon = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                    Adres = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                    Giris = reader.IsDBNull(5) ? "" : reader.GetString(5),

                                };
                                listele.Add(yonetici);
                            }
                        }





                    }





                }





            }
            catch
            {


            }



        }
    }
}