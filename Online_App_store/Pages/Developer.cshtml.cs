using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Online_App_store.models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace Online_App_store.Pages
{
    public class DeveloperModel : PageModel
    {
        [BindProperty]
        public App App { get; set; }

        public List<App> AppList { get; set; } = new List<App>();

        private readonly string connectionString = "Data Source=laptop-oh72tn5u; Initial Catalog = OnlineAppStore; Integrated Security = True";

        public void OnGet()
        {
            LoadApps();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadApps();
                return Page();
            }

            if (App.UploadedFile != null)
            {
                var filePath = Path.Combine("wwwroot/uploads", App.UploadedFile.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await App.UploadedFile.CopyToAsync(fileStream);
                }
                App.AppFile = "/uploads/" + App.UploadedFile.FileName;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Apps (AppName, DevName, Type, AppFile) VALUES (@AppName, @DevName, @Type, @AppFile)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@AppName", App.AppName);
                    command.Parameters.AddWithValue("@DevName", App.DevName);
                    command.Parameters.AddWithValue("@Type", App.Type);
                    command.Parameters.AddWithValue("@AppFile", App.AppFile);
                    command.ExecuteNonQuery();
                }
            }

            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Apps WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }

            return RedirectToPage();
        }

        private void LoadApps()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT Id, AppName, DevName, Type, AppFile FROM Apps";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        AppList.Clear();
                        while (reader.Read())
                        {
                            AppList.Add(new App
                            {
                                Id = reader.GetInt32(0),
                                AppName = reader.GetString(1),
                                DevName = reader.GetString(2),
                                Type = reader.GetString(3),
                                AppFile = reader.GetString(4)
                            });
                        }
                    }
                }
            }
        }
    }
}
