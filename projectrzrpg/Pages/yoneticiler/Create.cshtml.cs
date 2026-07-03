using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace projectrzrpg.Pages.yoneticiler
{
    public class CreateModel : PageModel
    {

        public yoneticiler yoneticibilgi = new yoneticiler();
        public string errorMessage = "";
        public string successMessage = "";


        public void OnGet()
        {
        }

        public void OnPost()
        {
            yoneticibilgi.AdSoyad = Request.Form["AdSoyad"];
            yoneticibilgi.Email = Request.Form["Email"];
            yoneticibilgi.Telefon = Request.Form["Telefon"];
            yoneticibilgi.Adres = Request.Form["Adres"];
            yoneticibilgi.Giris = Request.Form["Giris"];

            if (yoneticibilgi.AdSoyad.Length == 0 || yoneticibilgi.Email.Length == 0 ||
                yoneticibilgi.Telefon.Length == 0 || yoneticibilgi.Adres.Length == 0 ||
                yoneticibilgi.Giris.Length == 0)
            {

                errorMessage = "tüm alanlar zorunludur";
                return;

            }
            try
            {

                string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=crmM;" +
                   "Integrated Security=true;TrustServerCertificate=true;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    string Sql = "insert into yoneticiler(Adsoyad,Email,Telefon,Adres,Giris)" +
                        "values(@Adsoyad,@Email,@Telefon,@Adres,@Giris)";
                    using (SqlCommand command = new SqlCommand(Sql, connection))
                    {
                        command.Parameters.AddWithValue("@Adsoyad", yoneticibilgi.AdSoyad);
                        command.Parameters.AddWithValue("@Email", yoneticibilgi.Email);
                        command.Parameters.AddWithValue("@Telefon", yoneticibilgi.Telefon);
                        command.Parameters.AddWithValue("@Adres", yoneticibilgi.Adres);
                        command.Parameters.AddWithValue("@Giris", yoneticibilgi.Giris);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            successMessage = "Kayýut baþarýlý";
            Response.Redirect("/yoneticiler/Index");
        }
    }
}