using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace projectrzrpg.Pages.yoneticiler
{
    public class EditModel : PageModel
    {

        public yoneticiler yoneticibilgi = new yoneticiler();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {


            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=crmM;" +
                "Integrated Security=true;TrustServerCertificate=true;";

            string ID = Request.Query["ID"];
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    string sql = "select *from yoneticiler where ID=@ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            yoneticibilgi.ID = "" + reader.GetInt32(0);
                            yoneticibilgi.AdSoyad = reader.GetString(1);
                            yoneticibilgi.Email = reader.GetString(2);
                            yoneticibilgi.Telefon = reader.GetString(3);
                            yoneticibilgi.Adres = reader.GetString(4);
                            yoneticibilgi.Giris = reader.GetString(5);

                        }
                    }
                }
            }
            catch { }
        }





        public void OnPost()
        {

            yoneticibilgi.ID = Request.Form["ID"];
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
                    string Sql = "update yoneticiler set Adsoyad=@Adsoyad,Email=@Email,Telefon=@Telefon," +
                        "Adres=@Adres,Giris=@Giris where ID=@ID";
                    using (SqlCommand command = new SqlCommand(Sql, connection))
                    {
                        command.Parameters.AddWithValue("@Adsoyad", yoneticibilgi.AdSoyad);
                        command.Parameters.AddWithValue("@Email", yoneticibilgi.Email);
                        command.Parameters.AddWithValue("@Telefon", yoneticibilgi.Telefon);
                        command.Parameters.AddWithValue("@Adres", yoneticibilgi.Adres);
                        command.Parameters.AddWithValue("@Giris", yoneticibilgi.Giris);
                        command.Parameters.AddWithValue("@ID", yoneticibilgi.ID);
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
