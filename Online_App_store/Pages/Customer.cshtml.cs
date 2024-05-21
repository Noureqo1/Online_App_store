using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_App_store.models;
using System.Data.SqlClient;

namespace Online_App_store.Pages
{
    public class CustomerModel : PageModel
    {
        public List<App> Apps { get; set; }

        public void OnGet()
        {
            Apps = new List<App>();

            string connectionString = "Data Source=laptop-oh72tn5u; Initial Catalog = OnlineAppStore; Integrated Security = True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT AppName, DevName, Type, AppFile FROM Apps";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Apps.Add(new App
                            {
                                AppName = reader.GetString(0),
                                DevName = reader.GetString(1),
                                Type = reader.GetString(2),
                                AppFile = reader.GetString(3)
                            });
                        }
                    }
                }
            }
        }
    }
    public class DownloadModel : PageModel
    {
        public IActionResult OnGet(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return NotFound();
            }

            var filePath = Path.Combine("wwwroot", "apps", fileName);
            var mimeType = "application/octet-stream";
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, mimeType, fileName);
        }
    }
}
