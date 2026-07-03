using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace projectrzrpg.Pages.yoneticiler
{
    public class DeleteModel : PageModel
    {
        public yoneticiler yoneticibilgi = new yoneticiler();
        public void OnGet()
        {

            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=crmM;" +
                    "Integrated Security=true;TrustServerCertificate=true;";

            string ID = Request.Query["ID"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                string sql = "delete from yoneticiler where ID=@ID";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    command.Parameters.AddWithValue("@ID", ID);
                    command.ExecuteNonQuery();
                }
            }



            Response.Redirect("/yoneticiler/Index");
        }
    }
}